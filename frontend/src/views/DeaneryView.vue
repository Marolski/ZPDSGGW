@ts-ignore
<template>
  <div class="tableStyle">
      <div style="padding-top: 50px;">
      <a-radio-group default-value="a" size="large" @change="chngeUserList">
        <a-radio-button value="Promoter">
          Promotorzy
        </a-radio-button>
        <a-radio-button value="Student">
          Studenci
        </a-radio-button>
      </a-radio-group>
    </div>
    <div style="padding-top: 50px;"></div>
    <mdb-datatable-2 v-model="data" selectable  @selected="handleClick(selected = $event)"/>
    <template>
        <div class="container ">
            <div class="large-12 medium-12 small-12 cell">
                <div class="proposal">
                    <a-card v-if="proposalMapped.status!=1" title="Wniosek" style="width: 300px">
                        <h4>{{proposalMapped.promoterName}}</h4>
                        <h5>{{proposalMapped.studentName}}</h5>
                        <p>{{proposalMapped.topic}}</p>
                        <div v-if="proposalMapped.status==2">
                            <a-popconfirm title="Czy na pewno chcesz zatwierdzić wniosek?" ok-text="Tak" cancel-text="Nie" @confirm="confirm(proposalMapped)" style="z-index: 60;">
                                <mdb-btn style="z-index: 1100;" color="primary" @mouseover="hover = true">Zatwierdź</mdb-btn>
                            </a-popconfirm>

                            <a-popconfirm title="Czy na pewno chcesz odrzucić wniosek?" ok-text="Tak" cancel-text="Nie" @confirm="reject(proposalMapped)" style="z-index: 60;">
                                <mdb-btn style="z-index: 1100;" color="danger" @mouseover="hover = true">Odrzuć</mdb-btn>
                            </a-popconfirm>
                        </div>
                        <div v-if="proposalMapped.status==6">
                            <mdb-icon far icon="check-circle" />
                        </div>
                        <div v-if="proposalMapped.status==5">
                            <mdb-icon icon="ban" />
                        </div>
                    </a-card>
                </div>
                <mdb-list-group>        
                    <mdb-list-group-item class="removeStyle" :action="true" v-for="item in fileListDict" @click.native="downloadFile(item.value, $event)"  tag="a" :key="item.value.Id">{{item.kind}} {{' ___  '}}{{item.key}}
                        <div v-if="item.value.Accepted==2">
                            <a-popconfirm title="Czy na pewno chcesz zatwierdzić plik?" ok-text="Tak" cancel-text="Nie" @confirm="confirm(item.value)" style="z-index: 60;">
                                <mdb-btn style="z-index: 1100;" color="primary" @mouseover="hover = true">Zatwierdź</mdb-btn>
                            </a-popconfirm>

                            <a-popconfirm title="Czy na pewno chcesz odrzucić plik?" ok-text="Tak" cancel-text="Nie" @confirm="reject(item.value)" style="z-index: 60;">
                                <mdb-btn style="z-index: 1100;" color="danger" @mouseover="hover = true">Odrzuć</mdb-btn>
                            </a-popconfirm>
                        </div>
                        <div v-if="item.value.Accepted==1">
                            <mdb-icon far icon="check-circle" />
                        </div>
                        <div v-if="item.value.Accepted==0">
                            <mdb-icon icon="ban" />
                        </div>
                        
                    </mdb-list-group-item>
                </mdb-list-group>
            </div>
        </div>
    </template>
  </div>
</template>

<script lang='ts'>
import Vue from "vue";
import Component from "vue-class-component";
import UserService from '../services/UserService';
import { mdbDatatable2,mdbListGroup, mdbListGroupItem, mdbBtn, mdbBadge, mdbContainer, mdbIcon  } from 'mdbvue';
import IUser from "../types/User";
import DocumentService from "../services/DocumentService";
import PathHelper from "../services/helpers/PathHelper";
import IFile from '../types/File'
import { message } from 'ant-design-vue'
import UserHelper from "../services/helpers/UserHelper";
import { FileStatus, ProposalStatus } from "../enums/Enum";
import IProposal from "../types/Proposal";
import ProposalService from "../services/ProposalService";

const userService = new UserService();
const proposalService = new ProposalService();
const documentService = new DocumentService;
const pathHelper = new PathHelper;
const userHelper = new UserHelper;
interface Row{
  name: string;
  studentNumber: string;
}
@Component({
        name: 'UserList',
        components:{mdbDatatable2,mdbListGroup, mdbListGroupItem, mdbBtn, mdbBadge, mdbContainer, mdbIcon   }
    })
    export default class UserList extends Vue {
        visible = false;
        confirmLoading = false;
        topic: any = '';
        description: any = '';
        checkedUserId = '';
        name: any = '';
        fileListDict: object[] =[];
        checkedUser = {};
        file = '';
        userList: Array<IUser> = [];
        proposal: IProposal;
        proposalExist = false;
        proposalMapped = {
            promoterName: '',
            studentName: '',
            topic: '',
            status: 1
        }
        data = {
          rows: [],    
        }
        colums = [
            {
            label: 'Imie i Nazwisko',
            field: 'name',
            sort: true
            },
            {
            label: 'Numer albumu',
            field: 'studentNumber',
            sort: true
            }
        ];

        public get setRows(){
          return this.data
        }
        public set setRows(newdata: {rows}){
          this.data = newdata
        }
        created(){
          this.getUsers("Student");
        }

        chngeUserList(e){
            this.getUsers(e.target.value);
        }
        
        async getUsers(users){
            try {
                this.fileListDict = [];
                const userList = await userService.getAllUsers(users);
                this.userList = userList.data;
                const rows = [];
                const columns = this.colums;
                const newDataObject = {
                    columns,
                    rows
                };
                this.userList.forEach(element => {
                    const row ={
                        name: userHelper.getUserName(element),
                        studentNumber: element.StudentNumber
                    };
                    rows.push(row)
              });
              this.setRows = newDataObject;
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }
        async handleClick(param: Row){
            try {
                this.proposalMapped.status = 1;
                this.userList.forEach(element => {
                    if(userHelper.getUserName(element) == param.name && element.StudentNumber == param.studentNumber)
                        this.checkedUserId = element.Id
                });
                this.getFiles(this.checkedUserId)
                this.getProposal(this.checkedUserId)
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }

        async getFiles(userId){
            try {
                const documentsList = await documentService.getDocumentList(userId);
                const documentsListData = documentsList.data;
                this.fileListDict = pathHelper.getPathList(documentsListData);
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }
        

        async downloadFile(value: IFile, e){
            try {
                this.checkedUser = value.Id;
                if(e.target.type=='button') return false;
                else{
                await documentService.getDocumentByUserId(value.Id)
                .then((response) => {
                        const fileLink = document.createElement('a');

                        fileLink.href = response.config.url;
                        fileLink.setAttribute('download', value.FileName);
                        document.body.appendChild(fileLink);
                        console.log(fileLink)
                        fileLink.click();
                });
                }
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }
        async confirm(e) {
            try {
                if(e.topic!=undefined)
                {
                    await proposalService.patchProposal(this.proposal.StudentId,[{ "op":"replace", "path":"/Status", "value": ProposalStatus.Accepted}]);
                    message.success('Wniosek został zaakceptowany');
                    this.proposalMapped.status = ProposalStatus.Accepted
                }
                if(e.Path!=undefined){
                    await documentService.patchDocument(e.Id,[{ "op":"replace", "path":"/Accepted", "value": FileStatus.accepted}])
                    message.success('Plik został zaakceptowany');
                    this.getFiles(this.checkedUserId)
                } 
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }

        async reject(e) {
            try {
                if(e.topic!=undefined)
                {
                    await proposalService.patchProposal(this.proposal.StudentId,[{ "op":"replace", "path":"/Status", "value": ProposalStatus.Reject}]);
                    message.success('Wniosek został odrzucony');
                    this.proposalMapped.status = ProposalStatus.Reject
                }
                if(e.Path!=undefined){
                    await documentService.patchDocument(e.Id,[{ "op":"replace", "path":"/Accepted", "value": FileStatus.rejected}])
                    message.success('Plik został odrzucony');
                    this.getFiles(this.checkedUserId)
                } 
                
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }

        async getProposal(userId: string){
            try {
                const proposal = await proposalService.getProposal(userId);
                this.proposal = proposal.data;
                if(this.proposal.StudentId!='')
                    this.proposalExist = true;
                const promoter = await userService.getUser(this.proposal.PromoterId);
                const student = await userService.getUser(this.proposal.StudentId);
                this.proposalMapped.promoterName = userHelper.getUserName(promoter.data);
                this.proposalMapped.studentName = userHelper.getUserName(student.data)
                this.proposalMapped.topic = this.proposal.Topic;
                if(this.proposal.Status!=undefined)
                    this.proposalMapped.status = this.proposal.Status;
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }

        
    }
</script>
<style scoped>
.tableStyle{
  text-align: center;
  margin: auto;
}
.mdb-datatable{
  max-width: 80% !important;
}
.md-form{
  margin-left: 10%;
  margin-right: 2%;
  min-width: 80%;
  margin-bottom: 20px;
}
.inputDiv{
  display: flex;
  margin-right: 10%;
  margin-left: 10%;
}
.form-control{
  min-width: 300px;
}
.proposal{
    margin-left: 40%;
    margin-bottom: 5%;
}
</style>
