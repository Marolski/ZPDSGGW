<template>
  <div class="tableStyle">
    <div style="padding-top: 50px;"></div>
    <mdb-datatable-2 v-model="data" selectable  @selected="handleClick(selected = $event)"/>
    <div>
        <a-modal
        title="Title"
        :visible="visible"
        :confirm-loading="confirmLoading"
        @ok="handleOk"
        @cancel="handleCancel"
        cancelText="Odrzuć"
        okText="Zaakceptuj"
        :width="700"
        >
        <p>Imie i Nazwisko: {{ name }} <br>Temat pracy: {{topic}} <br> {{description}}</p>
        </a-modal>
    </div>
    <md-snackbar :md-active.sync="userSaved">{{message}}</md-snackbar>
  </div>
</template>

<script lang='ts'>
import Vue from "vue";
import Component from "vue-class-component";
import UserService from '../services/UserService';
import InvitationService from "../services/InvitationService";
import IInvitation from '../types/Invitation';
import { mdbDatatable2} from 'mdbvue';
import UserHelper from "../services/helpers/UserHelper";
import {InvitationStatus, ThesisTopicStatus} from "../enums/Enum";

const userService = new UserService();
const invitationservice = new InvitationService();
const userHelper = new UserHelper();
interface Row{
  name: string;
  topic: string;
  description: string;
  accepted: string;
}
@Component({
        name: 'PromoterInvitation',
        components:{mdbDatatable2 }
    })
    export default class TopicList extends Vue {
        userId: string = localStorage.getItem('id');
        sendInvitations: Array<IInvitation> = [];
        visible = false;
        confirmLoading = false;
        topic: any = '';
        description: any = '';
        checkedStudentId = '';
        name: any = '';
        message = "";
        userSaved = false;
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
            label: 'Temat',
            field: 'topic',
            sort: true
            },
            {
            label: 'Wiadomość',
            field: 'description',
            sort: true
            },
            {
            label: 'Zaakceptowany',
            field: 'accepted',
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
          this.populate();
        }
        
        async populate(){
            try {
                this.sendInvitations = [];
                const studentList = await userService.getAllUsers('Student');
                const Invitations = await invitationservice.getAllInvitations(this.userId).then(response => {
                    response.data.forEach(element => {
                        if(element.Accepted != 1)
                            this.sendInvitations.push(element);
                    });
                })
                const rows = [];
                const columns = this.colums;
                const newDataObject = {
                    columns,
                    rows
                };
                this.sendInvitations.forEach(element => {
                    const row ={
                        name: element.StudentId,
                        topic: element.Topic,
                        description: element.Description,
                        accepted: element.Accepted.toString()
                    };
                    const name = studentList.data.filter(function(user){
                        if(user.Id === element.StudentId){
                            return user;
                        }
                    })
                    row.name = userHelper.getUserName(name[0]);
                    if (element.Accepted == 2)
                    row.accepted = 'Nie';
                    else if(element.Accepted == 3)
                    row.accepted = 'Tak';
                    else if(element.Accepted == 4)
                    row.accepted = 'Odrzucony';
                    rows.push(row)
              });
              this.setRows = newDataObject;
            } catch (error) {
                this.message = "Wystąpił problem, skontaktuj się z Administratorem";
                this.userSaved = true;
            }
        }
        async handleClick(param: Row){
            try {
                const invitation = this.sendInvitations.filter(function(invitation){
                    if(invitation.Topic === param.topic){
                        return invitation;
                    }
                })
                this.checkedStudentId = invitation[0].StudentId;
                this.name = param.name;
                this.topic = param.topic;
                this.description = param.description;
                this.visible = true;
            } catch (error) {
                console.log(error)
            }
        }
        async handleOk(e) {
            try {
                await invitationservice.patchInvitation(this.checkedStudentId,[{ "op":"replace", "path":"/Accepted", "value": InvitationStatus.Accepted}]);
                this.visible = false;
                this.populate();
            } catch (error) {
                this.message = "Wystąpił problem, skontaktuj się z Administratorem";
                this.userSaved = true; 
            }
        }
        async handleCancel(e) {
            try {
                await invitationservice.patchInvitation(this.checkedStudentId,[{ "op":"replace", "path":"/Accepted", "value": InvitationStatus.Rejected}]);
                this.visible = false;
                this.populate();
            } catch (error) {
                this.message = "Wystąpił problem, skontaktuj się z Administratorem";
                this.userSaved = true; 
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
