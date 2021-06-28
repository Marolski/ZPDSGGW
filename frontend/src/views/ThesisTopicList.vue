<template>
  <div class="tableStyle">
      <div><Alert v-show="showError"/></div>
    <div style="padding-top: 50px;"></div>
    <div class="inputDiv">
      <mdb-input v-model="thesisTopicName" />
      <mdb-btn color="primary" style="margin-bottom: 20px; " @click.native="updateTopicStatus">Wybierz</mdb-btn>
    </div>
    <mdb-datatable-2 v-model="data" selectable  @selected="handleClick(selected = $event)"/>
    <mdb-container>
      <mdb-modal :show="contact" @close="contact = false">
        <mdb-modal-header class="text-center">
          <mdb-modal-title tag="h4" bold class="w-100">Wyślij zapytanie do promotora</mdb-modal-title>
        </mdb-modal-header>
        <mdb-modal-body class="mx-3 grey-text">
          <mdb-input v-model="invitationName" disabled label="Student" />
          <mdb-input v-model="invitationPromoterName" disabled label="Promotor"/>
          <mdb-input v-model="thesisTopicName" disabled label="Temat"/>
          <mdb-textarea v-model="invitationDesc" label="Wiadomość"/>
        </mdb-modal-body>
        <mdb-modal-footer center>
          <mdb-btn @click.native="createInvitation" color="unique">Wyślij <mdb-icon icon="paper-plane" class="ml-1"/></mdb-btn>
        </mdb-modal-footer>
      </mdb-modal>
    </mdb-container>
  </div>
</template>

<script lang='ts'>
import Vue from "vue";
import Component from "vue-class-component";
import { message } from 'ant-design-vue'
import ThesisTopicService from '../services/ThesisTopicService';
import ITopic from '../types/ThesisTopic';
import UserService from '../services/UserService';
import Alert from  '../components/Alert.vue';
import ProposalService from "../services/ProposalService";
import InvitationService from "../services/InvitationService";
import router from "../router";
import IInvitation from '../types/Invitation';
import { mdbContainer, mdbInput, mdbTextarea, mdbDatatable2, mdbBtn, mdbIcon, mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter } from 'mdbvue';
import UserHelper from "../services/helpers/UserHelper";
import InvitationHelper from "../services/helpers/InvitationHelper";
import IProposal from "../types/Proposal";
import {InvitationStatus, thesisTopicStatus} from "../enums/Enum";

const topics = new ThesisTopicService();
const userService = new UserService();
const proposalService = new ProposalService();
const invitationservice = new InvitationService();
const userHelper = new UserHelper();
const invitationHelper = new InvitationHelper();
interface Row{
  name: string;
  topic: string;
  available: string;
}
@Component({
        name: 'TopicList',
        components:{
          mdbDatatable2, 
          mdbBtn, 
          mdbInput, 
          Alert,
          mdbContainer,
          mdbModal,
          mdbModalHeader,
          mdbModalBody,
          mdbModalFooter,
          mdbTextarea,
          mdbModalTitle,
          mdbIcon,
          
        }
    })
    export default class TopicList extends Vue {
        inputValue: string;
        topicsArray: Array<ITopic> = [];
        showError: boolean | any = false;
        onlyTopic: boolean;
        checkedTopicId: string;
        thesisTopicName: any = '';
        fakeGuid = '00000000-0000-0000-0000-000000000000';
        myTopic: boolean;
        contact: any = false;
        invitationDesc: any = '';
        invitationName: any = '';
        invitationPromoterName: any = '';
        invitation: IInvitation ={
          StudentId: '',
          PromoterId: '',
          Topic: '',
          Description: '',
          Accepted: 1
        }
        data = {
          rows: [],
          
        }
        proposal: IProposal = {
        Status:1,
        StudentId: '',
        PromoterId: '',
        Topic: ''
        };
        public get setRows(){
          return this.data
        }
        public set setRows(newdata: object){
          this.data = newdata
        }
        created(){
          this.populate();
          this.invitation.StudentId = localStorage.getItem('id');
        }
        
        async populate(){
            try {
              this.data = {rows:[]};
              const allTopics = await topics.getTopics();
              const allPromoters = await userService.getAllUsers('Promoter');
              const promotersData = allPromoters.data;
              this.topicsArray = allTopics.data;
              const rows = [];
              const newDataObject = {
                columns: [{
                  label: 'Imie',
                  field: 'name',
                  sort: true
                },
                {
                  label: 'Temat',
                  field: 'topic',
                  sort: true
                },
                {
                  label: 'Dostępny',
                  field: 'available',
                  sort: true
                }],
                rows
              };
              this.topicsArray.forEach(element => {
                const row ={
                  name: element.PromoterId,
                  topic: element.Topic,
                  available: '',
                };          
                const name = promotersData.filter(function(elem){
                  if(elem.Id === element.PromoterId){
                    return elem
                  }
                })
                row.name = userHelper.getUserName(name[0]);
                row.available = thesisTopicStatus[element.Available]
                rows.push(row)
              });
              this.setRows = newDataObject;
              const user = await userService.getUser(localStorage.getItem('id'));
              this.invitationName = userHelper.getUserName(user.data);
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z administratorem");
            }
        }
        async sendDataToProposal(id: string){
          try {
            const userData = await userService.getUser(id)
            return userData.data;
          } catch (error) {
              message.error("Wystąpił błąd, skontaktuj się z administratorem");
          }
        }
        async handleClick(param: Row){
          try {
            if(param == undefined){
            this.invitationPromoterName = '';
            return
            }
            if(param.available != thesisTopicStatus[1]){
              message.info("Wybrany temat nie jest dostępny");
              return;
            }
            else if(typeof(param)!=typeof("")){
              this.contact = true;
              this.onlyTopic = false;
            }
            this.checkedTopicId = this.topicsArray.find(element => element.Topic == param.topic).Id;
            const promoterId = await topics.getTopicById(this.checkedTopicId);

            this.invitation.PromoterId = promoterId.data.PromoterId;
            this.invitation.StudentId = localStorage.getItem('id');
            this.invitation.Topic = param.topic;
            this.thesisTopicName = param.topic;
            this.invitationPromoterName = param.name;
          } catch (error) {
              message.error("Wystąpił błąd, skontaktuj się z administratorem");
          }
        }
        async updateTopicStatus(){
          try {
            const invitation = await invitationservice.getInvitation(localStorage.getItem('id'));
            if(invitation.data.Accepted == InvitationStatus.Send || invitation.data.Accepted == InvitationStatus.Accepted){
              message.info('Przesłałeś już zaproszenie do współpracy, nie możesz zmienić tematu')
              router.push('Profile');
              return;
            }
            if(this.invitation.PromoterId!='' && this.thesisTopicName!=''){
              await invitationservice.patchInvitation(localStorage.getItem('id'),[{ "op":"replace", "path":"/Topic", "value": this.thesisTopicName}]);
              await invitationservice.patchInvitation(localStorage.getItem('id'),[{ "op":"replace", "path":"/PromoterId", "value": this.invitation.PromoterId}])
              router.push('Profile');
            }
            else if(this.thesisTopicName!=''){
                this.invitation.Topic = this.thesisTopicName;
                await invitationservice.patchInvitation(localStorage.getItem('id'),[{ "op":"replace", "path":"/Topic", "value": this.thesisTopicName}]);
                router.push('Profile');
            }
            else return;
          } catch (error) {
              message.error("Wystąpił błąd, skontaktuj się z administratorem");
          }
        }
        async createInvitation(){
          try {   
            if(this.invitation.PromoterId ==''|| this.invitation.Topic=='' || this.invitation.PromoterId == this.fakeGuid){
                message.error("Nie można wysłać zaproszenia do współpracy. Brak danych");
                return;
            }         
            const isExist = await invitationHelper.updateInvitationStatus(this.checkedTopicId);
            this.contact = false;
            if(isExist==true){
              message.info("Przesłałeś już zaproszenie do współpracy z promotorem");
              return;
            }
            if(this.invitation.PromoterId!='' && this.thesisTopicName!=''){
              await invitationservice.patchInvitation(localStorage.getItem('id'),[{ "op":"replace", "path":"/Topic", "value": this.thesisTopicName}]);
              await invitationservice.patchInvitation(localStorage.getItem('id'),[{ "op":"replace", "path":"/PromoterId", "value": this.invitation.PromoterId}])
              await invitationservice.patchInvitation(localStorage.getItem('id'),[{ "op":"replace", "path":"/Description", "value": this.invitationDesc}])
              message.info("Wysłano zaproszenie do promotora");
              this.populate();
            }
          } catch (error) {
              message.error("Wystąpił błąd, skontaktuj się z administratorem");
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
