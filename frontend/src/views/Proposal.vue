<template>
  <div class="component">
    <form novalidate class="md-layout" @submit.prevent="saveUser">
      <md-card class="md-layout-item md-size-50 md-small-size-100" style="margin:auto;">
        <md-card-header>
          <div class="md-title">Wniosek</div>
        </md-card-header>

        <md-card-content>
          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">
              <md-field>
                <label for="first-name">Imię i nazwisko studenta</label>
                <md-input name="first-name" id="first-name" autocomplete="given-name" v-model="form.studentName" disabled />
              </md-field>
            </div>
          </div>

          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">          
              <md-field>
                <label for="last-name">Imię i nazwisko promotora</label>
                <md-input name="last-name" id="last-name" autocomplete="family-name" v-model="form.promoterName" disabled />
              </md-field>
            </div>
          </div>

          <md-field>
            <label for="email">Temat pracy</label>
            <md-input type="email" name="email" id="email" autocomplete="email" v-model="form.topic" disabled />
          </md-field>
        </md-card-content>

        <md-progress-bar md-mode="indeterminate" v-if="sending" />

        <md-card-actions>
          <md-button type="submit" class="md-primary md-raised" :disabled="sending">Wyślij</md-button>
        </md-card-actions>
      </md-card>
      <md-snackbar :md-active.sync="userSaved">{{message}}</md-snackbar>
    </form>
  </div>
</template>

<script lang="ts">
  import { validationMixin } from 'vuelidate'
  import {
    required,
    minLength,
    maxLength
  } from 'vuelidate/lib/validators'
import Vue from 'vue';
import InvitationService from '../services/InvitationService';
import ProposalService from '../services/ProposalService';
import UserService from '../services/UserService';
import IInvitation from '../types/Invitation';
import { Component } from "vue-property-decorator";
import UserHelper from '../services/helpers/UserHelper';
import IProposal from '../types/Proposal';


  const proposalService = new ProposalService();
  const userService = new UserService();
  const invitationServive = new InvitationService();
  const userHelper =  new UserHelper();
  @Component({
        name: 'Proposal',
        components: { },
  })
  export default class Proposal extends Vue{
    mixins: [validationMixin];
    userId: string = localStorage.getItem('id');
    invitationModel: IInvitation;
    proposal: IProposal;
    message = '';
    form = {
      studentName: null,
      promoterName: null,
      topic: null,
    };
    userSaved = false;
    sending = false;
    created(){
      this.getInvitationData()
    }
       async saveUser () {
        try {
          const invitationAccepted = await invitationServive.getInvitation(this.userId);
          const proposalExist = await proposalService.getProposal(this.userId);
          console.log(proposalExist);
          if(invitationAccepted.data.Accepted == false){
            this.message = "Nie masz nawiązanej współpracy z promotorem";
            this.sending = true;
          }
          else if (proposalExist.data!=""){
            this.message = "wniosek został już wysłany";
            this.sending = true;
          }
          else{
            // Instead of this timeout, here you can call your API
            this.proposal = {
              StudentId: this.invitationModel.StudentId,
              PromoterId: this.invitationModel.PromoterId,
              Topic: this.invitationModel.Topic,
              Status: 2
            }
            const newProposal = proposalService.postProposal(this.proposal);
            this.message = "Wniosek został wysłany";
            this.sending = true
          }
          window.setTimeout(() => {
              this.userSaved = true
              this.sending = false
            }, 1000);
            
        } catch (error) {
          this.message = "Wystąpił problem skontaktuj się z administratorem";
          this.sending = true;
        }
      }
      async getInvitationData(){
        try {
          const invitation = await invitationServive.getInvitation(this.userId);
          const invitationData= invitation.data;
          this.invitationModel = invitationData;
          console.log(invitationData)
          if(invitationData==""){
            this.form.studentName = '';
            this.form.promoterName = '';
            this.form.topic = '';
          }else{
            const student = await userService.getUser(this.invitationModel.StudentId);
            const promoter = await userService.getUser(this.invitationModel.PromoterId);
            this.form.studentName = await userHelper.getUserName(student.data);
            this.form.promoterName = await userHelper.getUserName(promoter.data);
            this.form.topic = this.invitationModel.Topic;
          }
        } catch (error) {
          this.message = "Wystąpił problem skontaktuj się z administratorem";
          this.sending = true;
        }
        
      }
    }
</script>

<style lang="scss" scoped>
  .md-progress-bar {
    position: absolute;
    top: 0;
    right: 0;
    left: 0;
  }
</style>