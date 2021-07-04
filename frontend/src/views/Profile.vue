<template>
  <div class="about">
    <div><Alert v-show="showError"/></div>
    <mdb-card>
        <mdb-card-image src="hat.png" alt="Card image cap" style=""></mdb-card-image>
        <mdb-card-body>
        <mdb-card-title>Mój Profil</mdb-card-title>
        <mdb-card-text>Imie: {{myProfile.Name}}</mdb-card-text>
        <mdb-card-text>Nazwisko:{{myProfile.Surname}}</mdb-card-text>
        <mdb-card-text>Numer indeksu: {{myProfile.StudentNumber}}</mdb-card-text>
        <mdb-card-text class="cardText">Promotor: </mdb-card-text><div class="cardText"><a v-if="invitation.PromoterId!=''">{{promoterName}}</a><br/><mdb-btn color="primary" @click.native="modal = true" v-if="canChangePromoter">Znajdź promotora</mdb-btn></div><div style="clear: both;"></div>
        <mdb-card-text class="cardText">Temat pracy: </mdb-card-text><div class="cardText"><a v-if="invitation.Topic">{{invitation.Topic}}</a><br/><router-link to="/topics"><mdb-btn color="primary" v-if="canChangePromoter">Znajdź temat</mdb-btn></router-link></div>
        </mdb-card-body>
    </mdb-card>
    <div>
        <router-link to="/proposal"><md-button class="md-primary md-raised">Wyślij wniosek</md-button></router-link>
        <md-button @click.native="contact = true" class="md-primary md-raised">Wyślij zaproszenie do promotora</md-button>
        <md-button to="/documents" class="md-primary md-raised">Dokumenty</md-button>
    </div>
    <div>
        <mdb-modal :show="modal " @close="modal = false">
            <mdb-modal-header>
                <mdb-modal-title>Lista promotorów</mdb-modal-title>
            </mdb-modal-header>
            <mdb-modal-body>
                <mdb-list-group>
                <mdb-list-group-item :action="true" v-for="item in promotersList" :key="item.Name" @click.native="updatePromoter(item)" v-html="getName(item)"></mdb-list-group-item>
                </mdb-list-group>
            </mdb-modal-body>
            <mdb-modal-footer>
                <mdb-btn color="secondary" @click.native="modal = false">Close</mdb-btn>
            </mdb-modal-footer>
        </mdb-modal>
    </div>

    <mdb-container>
        <mdb-modal :show="contact" @close="contact = false">
            <mdb-modal-header class="text-center">
            <mdb-modal-title tag="h4" bold class="w-100">Wyślij zapytanie do promotora</mdb-modal-title>
            </mdb-modal-header>
            <mdb-modal-body class="mx-3 grey-text">
            <mdb-input v-model="studentName" disabled label="Student" />
            <mdb-input v-model="promoterName" disabled label="Promotor"/>
            <mdb-input v-model="invitation.Topic" disabled label="Temat"/>
            <mdb-textarea v-model="invitationDesc" label="Wiadomość"/>
            </mdb-modal-body>
            <mdb-modal-footer center>
            <mdb-btn @click.native="createInvitation" color="unique">Wyślij <mdb-icon icon="paper-plane" class="ml-1"/></mdb-btn>
            </mdb-modal-footer>
        </mdb-modal>
    </mdb-container>
    <md-snackbar :md-active.sync="userSaved">{{message}}</md-snackbar>
  </div>
</template>

<script lang='ts'>
import IUser from "../types/User";
import UserService from "../services/UserService";
import ProposalService from "../services/ProposalService";
import Vue from 'vue';
import { Component } from "vue-property-decorator";
import IProposal from '../types/Proposal';
import { mdbModal,mdbCard, mdbCardImage, mdbCardBody, mdbCardTitle, mdbCardText, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter, mdbBtn,mdbListGroup, mdbListGroupItem, mdbBadge,mdbContainer, mdbInput, mdbTextarea, mdbDatatable2, mdbIcon } from 'mdbvue';
import UserHelper from '../services/helpers/UserHelper'
import Alert from  '../components/Alert.vue';
import ProposalHelper from '../services/helpers/ProposalHelper';
import InvitationHelper from "../services/helpers/InvitationHelper";
import IInvitation from '../types/Invitation';
import InvitationService from "../services/InvitationService";
import { message } from 'ant-design-vue'
import { InvitationStatus, degrees } from "../enums/Enum";

    const userService = new UserService();
    const proposalService = new ProposalService();
    const userHelper = new UserHelper();
    const proposalHelper = new ProposalHelper();
    const invitationHelper = new InvitationHelper();
    const invitationservice = new InvitationService();
    @Component({
        name: 'MyProfile',
        components: { mdbModal,
            mdbModalHeader,
            mdbModalTitle,
            mdbModalBody,
            mdbModalFooter,
            mdbBtn,
            mdbListGroup, 
            mdbListGroupItem, 
            mdbBadge,
            Alert,
            mdbDatatable2, 
            mdbInput, 
            mdbContainer,
            mdbIcon,
            mdbTextarea,
            mdbCard,
			mdbCardImage,
			mdbCardBody,
			mdbCardTitle,
			mdbCardText
        },
    })
    export default class MyProfile extends Vue{
        //data
        showError: boolean | any = false;
        promotersList: Array<IUser> = [];
        modal: any = false;
        promoterName = '';
        studentName = '';
        invitationDesc = '';
        fakeGuid = '00000000-0000-0000-0000-000000000000';
        contact = false;
        canChangePromoter = true;
        userSaved = false;
        message = '';
        myProfile: IUser = {
            Id: '',
            Name: '',
            Surname: '',
            StudentNumber: '',
            Degrees: 0,
            Availability: 0,
            Role: '',

        };
        proposal: IProposal = {
            Status: 1,
            Topic: '',
            PromoterId: '',
            StudentId: ''
        }
        invitation: IInvitation ={
          StudentId: '',
          PromoterId: '',
          Topic: '',
          Description: '',
          Accepted: 1
        }
        //computed properties
        get userCount(){
            return this.myProfile;
        }
        //props
        //methods
        //lifecycles hooks
        created(){
            this.getData();
        }
        async getData() {
            try {
                //pobranie danych usera i ustawienie imienia 
                const userdata = await userService.getUser(localStorage.getItem('id'));
                this.myProfile = userdata.data;
                this.studentName = userHelper.getUserName(userdata.data);

                //pobranie listy promotorów
                const promoterList = await userService.getAllUsers('Promoter');             
                this.promotersList = promoterList.data;
                const invitation = await invitationservice.getInvitation(localStorage.getItem('id'));
                if(invitation.data!=""){
                    //tworzenie zaproszenia promotora w zmiennej invitation
                    this.invitation.StudentId = invitation.data.StudentId
                    this.invitation.PromoterId = invitation.data.PromoterId;
                    this.invitation.Topic = invitation.data.Topic;
                    if(invitation.data.Accepted == InvitationStatus.Send || invitation.data.Accepted == InvitationStatus.Accepted)
                        this.canChangePromoter = false;
                    
                    //ustawienie imienia pormotora
                    if(this.invitation.PromoterId!=this.fakeGuid){
                        const promoterUserData = await userService.getUser(this.invitation.PromoterId);
                        this.promoterName = userHelper.getUserName(promoterUserData.data)
                    }    
                }
                else{
                    this.invitation.StudentId = localStorage.getItem('id');
                    this.invitation.PromoterId = this.fakeGuid;
                    await invitationservice.postInvitation(this.invitation);
                }
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z administratorem");
            }
        }
        //aktualizowanie promotora z listy promotorów
        async updatePromoter(promoter: IUser){
            try {
                //pobranie zaproszenia
                const invitation = await invitationservice.getInvitation(localStorage.getItem('id'));
                if(invitation.data.Accepted == InvitationStatus.Send){
                    message.info('Aktualnie nie możesz zmienić promotora');
                    this.modal = false;
                    return;
                }
                if(invitation.data != '')
                //jesli zaproszenie istanieje to tylko zmiana promotora
                    await invitationservice.patchInvitation(localStorage.getItem('id'),[{ "op":"replace", "path":"/PromoterId", "value": promoter.Id}]);
                //podmiana id promotora 
                this.proposal.PromoterId = promoter.Id;

                //pobranie obiektu promotora z tabeli user i ustawienie wyswietlanego imienia
                const promoterObj = await userService.getUser(promoter.Id)
                this.promoterName = userHelper.getUserName(promoterObj.data);
                this.modal = false;
                //rerender komponentu
                this.getData();
            } catch (error) {
                message.error("Wystąpił błąd, skontaktuj się z administratorem");
            }
        }
        async createInvitation(){
          try { 
              const invitation = await invitationservice.getInvitation(localStorage.getItem('id'));
                if(invitation.data.Accepted == InvitationStatus.Accepted){
                    message.info('Współpraca z promotorem została już nawiązana.');
                    this.contact = false;
                    return;
                }
              //jeśli zmienna zaproszenia jest pusta pokaz błąd
              if(this.invitation.PromoterId ==''|| this.invitation.Topic=='' || this.invitation.PromoterId == this.fakeGuid){
                    message.error("Nie można wysłać zaproszenia do współpracy. Brak danych");
                    this.contact = false;
                    return;
                }
                //tworzy zaproszenie albo zwraca false
                const isExist = await invitationHelper.updateInvitationStatus('');
                if(isExist==true){
                    message.info("Przesłałeś już zaproszenie do współpracy z promotorem");
                }
                else{
                    await invitationservice.patchInvitation(localStorage.getItem('id'),[{ "op":"replace", "path":"/Description", "value": this.invitationDesc}])
                    message.success("Wysłałeś zaproszenie do promotora");
                }
                this.contact = false;
            } catch (error) {
                    message.error("Wystąpił błąd, skontaktuj się z administratorem");
            }
        }
        getName(item){
            return userHelper.getUserName(item);
        }
        //watchers
    }

</script>

<style scoped>
.about{
    width: 50%;
    margin: auto;
}
.cardText{
    display: inline-block;
    margin-right: 10px;
    margin-left: 10px;
}
</style>