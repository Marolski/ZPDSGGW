<template>
  <div class="tableStyle">
    <div style="padding-top: 50px;"></div>
    <h2>Zaproszenia do współpracy</h2>
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
        closable
        :width="700"
        >
        <p>Imie i Nazwisko: {{ name }} <br>Temat pracy: {{topic}} <br> {{description}}</p>
        </a-modal>
    </div>
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
import { message } from 'ant-design-vue'
import ThesisTopicService from "../services/ThesisTopicService";

const userService = new UserService();
const invitationservice = new InvitationService();
const userHelper = new UserHelper();
const thesisTopics = new ThesisTopicService();
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
        sendInvitations: Array<IInvitation> = [];
        visible = false;
        confirmLoading = false;
        topic: any = '';
        checkedTopicId = '';
        description: any = '';
        checkedStudentId = '';
        name = '';
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
        public set setRows(newdata: {rows}){
          this.data = newdata
        }
        created(){
          this.populate();
        }
        
        async populate(){
            try {
                this.sendInvitations = [];
                const studentList = await userService.getAllUsers('Student');
                const Invitations = await invitationservice.getAllInvitations(localStorage.getItem('id')).then(response => {
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
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }
        async handleClick(param: Row){
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
        }
        async handleOk(e) {
            try {
                if(e.target.value!=''){ 
                    this.visible = false;
                    return;
                }
                this.confirmLoading = true;
                setTimeout(() => {
                  this.confirmLoading = false;
                  message.success('Wniosek o współprace został zaakceptowany')
                  this.visible = false;
                  this.populate();
                }, 1000);
                let topictoUpdate = false;
                await invitationservice.patchInvitation(this.checkedStudentId,[{ "op":"replace", "path":"/Accepted", "value": InvitationStatus.Accepted}]);
                const promoterTopics = await thesisTopics.getPromoterTopics(localStorage.getItem('id'));
                promoterTopics.data.forEach(element => {
                  if(element.Topic == this.topic){
                    topictoUpdate = true;
                    this.checkedTopicId = element.Id;
                  }  
                });
                await thesisTopics.patchTopic(this.checkedTopicId,[{ "op":"replace", "path":"/Available", "value": ThesisTopicStatus.unavailable}])
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z Administratorem");
            }
        }
        async handleCancel(e) {
            try {
                if(e.target.value!=''){ 
                  this.visible = false;
                  return;
                }
                let topictoUpdate = false;
                await invitationservice.patchInvitation(this.checkedStudentId,[{ "op":"replace", "path":"/Accepted", "value": InvitationStatus.Rejected}]);
                const promoterTopics = await thesisTopics.getPromoterTopics(localStorage.getItem('id'));
                promoterTopics.data.forEach(element => {
                  if(element.Topic == this.topic){
                    topictoUpdate = true;
                    this.checkedTopicId = element.Id;
                  }  
                });
                await thesisTopics.patchTopic(this.checkedTopicId,[{ "op":"replace", "path":"/Available", "value": ThesisTopicStatus.available}])
                message.error("Wniosek o współprace został odrzucony.")
                this.visible = false;
                this.populate();
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
