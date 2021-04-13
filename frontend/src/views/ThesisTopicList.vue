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
        userId: string = localStorage.getItem('id');
        inputValue: string;
        topicsArray: Array<ITopic> = [];
        showError: boolean | any = false;
        onlyTopic: boolean;
        checkedTopicId: string;
        thesisTopicName: any = '';
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
          Accepted: false
        }
        data = {
          rows: [],
          
        }
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
                  if(elem.Id === element.Id){
                    return elem
                  }
                })
                row.name = userHelper.getUserName(name[0]);
                if (element.Available == 1)
                  row.available = 'TAK';
                else if(element.Available == 2)
                  row.available = 'NIE';
                else if(element.Available == 3)
                  row.available = "ZAREZERWOWANE";
                rows.push(row)
              });
              this.setRows = newDataObject;
              const user = await userService.getUser(this.userId);
              this.invitationName = userHelper.getUserName(user.data);
            } catch (error) {
              this.showError = true
            }
        }
        async sendDataToProposal(id: string){
          try {
            const userData = await userService.getUser(id)
            return userData.data;
          } catch (error) {
              this.showError = true;
          }
        }
        handleClick(param: Row){
          if(param == undefined){
            this.invitationPromoterName = '';
            return
          }
          else if(typeof(param)!=typeof("")){
            this.contact = true;
            this.onlyTopic = false;
          }
          this.checkedTopicId = this.topicsArray.find(element => element.Topic == param.topic).Id;
          this.thesisTopicName = param.topic;
          this.invitationPromoterName = param.name;
        }
        async updateTopicStatus(){
          try {
            await proposalService.patchProposal(this.userId,[{ "op":"replace", "path":"/Topic", "value": this.thesisTopicName}])
            router.push('Profile');
          } catch (error) {
            this.showError = true;
          }
        }
        async createInvitation(){
          try {
            invitationHelper.postInvitation(this.checkedTopicId,this.invitation,this.invitationDesc);
            this.contact = false;
          } catch (error) {
            this.showError = true;
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
