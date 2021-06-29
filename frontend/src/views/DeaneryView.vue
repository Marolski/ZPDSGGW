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
        <div class="container">
            <div class="large-12 medium-12 small-12 cell">
                <mdb-list-group>        
                    <mdb-list-group-item class="removeStyle" :action="true" v-for="item in fileListDict" @click.native="downloadFile(item.value, $event)"  tag="a" :key="item.value.Id">{{item.kind}} {{' ___  '}}{{item.key}} 
                        <a-popconfirm title="Czy na pewno chcesz zatwierdzić plik?" ok-text="Tak" cancel-text="Nie" @confirm="confirm(item.value)" style="z-index: 60;">
                            <mdb-btn style="z-index: 1100;" color="primary" @mouseover="hover = true">Zatwierdź</mdb-btn>
                        </a-popconfirm>

                        <a-popconfirm title="Czy na pewno chcesz odrzucić plik?" ok-text="Tak" cancel-text="Nie" @confirm="reject(item.value)" style="z-index: 60;">
                            <mdb-btn style="z-index: 1100;" color="danger" @mouseover="hover = true">Odrzuć</mdb-btn>
                        </a-popconfirm>
                        
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
import { mdbDatatable2,mdbListGroup, mdbListGroupItem, mdbBtn, mdbBadge, mdbContainer } from 'mdbvue';
import IUser from "../types/User";
import DocumentService from "../services/DocumentService";
import PathHelper from "../services/helpers/PathHelper";
import IFile from '../types/File'
import { message } from 'ant-design-vue'
import UserHelper from "../services/helpers/UserHelper";

const userService = new UserService();
const documentService = new DocumentService;
const pathHelper = new PathHelper;
const userHelper = new UserHelper;
interface Row{
  name: string;
  studentNumber: string;
}
@Component({
        name: 'UserList',
        components:{mdbDatatable2,mdbListGroup, mdbListGroupItem, mdbBtn, mdbBadge, mdbContainer  }
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
        public set setRows(newdata: object){
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
                this.userList.forEach(element => {
                    if(userHelper.getUserName(element) == param.name && element.StudentNumber == param.studentNumber)
                        this.checkedUserId = element.Id
                });
                this.getFiles(this.checkedUserId)
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }

        async getFiles(userId){
            try {
                const documentsList = await documentService.getDocumentList(localStorage.getItem('id'));
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
                await documentService.patchDocument(e.Id,[{ "op":"replace", "path":"/Accepted", "value": 1}])
                message.success('Plik został zaakceptowany');
                this.getFiles(this.checkedUserId)
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }

        async reject(e) {
            try {
                await documentService.patchDocument(e.Id,[{ "op":"replace", "path":"/Accepted", "value": 0}])
                message.error('Plik został odrzucony');
                this.getFiles(this.checkedUserId)
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
</style>
