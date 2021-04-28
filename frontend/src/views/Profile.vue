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
        <mdb-card-text class="cardText">Promotor: </mdb-card-text><div class="cardText"><a v-if="proposal.PromoterId!=''">{{promoterName}}</a><br/><mdb-btn color="primary" @click.native="modal = true" >Znajdź promotora</mdb-btn></div>
        <mdb-card-text >Temat pracy: <a v-if="proposal.Topic && proposal.PromoterId">{{proposal.Topic}}</a><br/><router-link to="/topics"><mdb-btn color="primary">Przeglądaj propozycje</mdb-btn></router-link></mdb-card-text>
        </mdb-card-body>
    </mdb-card>
    <div>
        <router-link to="/proposal"><md-button class="md-primary md-raised">Wyślij wniosek</md-button></router-link>
        <md-button @click.native="contact = true" class="md-primary md-raised">Wyślij zaproszenie do promotora</md-button>
        <md-button class="md-primary md-raised">Dokumenty</md-button>
    </div>
    <div>
        <mdb-modal :show="modal" @close="modal = false">
            <mdb-modal-header>
                <mdb-modal-title>Lista promotorów</mdb-modal-title>
            </mdb-modal-header>
            <mdb-modal-body>
                <mdb-list-group>
                <mdb-list-group-item :action="true" v-for="item in promotersList" :key="item.Name" @click.native="updatePromoter(item)">{{item.Degrees}} {{item.Name}} {{item.Surname}}</mdb-list-group-item>
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
            <mdb-input v-model="proposal.Topic" disabled label="Temat"/>
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

    const userService = new UserService();
    const proposalService = new ProposalService();
    const userHelper = new UserHelper();
    const proposalHelper = new ProposalHelper();
    const invitationHelper = new InvitationHelper()
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
        userId: string = localStorage.getItem('id');
        showError: boolean | any = false;
        promotersList: Array<IUser> = [];
        modal: any = false;
        promoterName = '';
        studentName = '';
        invitationDesc = '';
        contact = false;
        userSaved = false;
        message = '';
        myProfile: IUser = {
            Id: '',
            name: '',
            surname: '',
            studentNumber: '',
            degrees: 0,
            availability: 0,
            role: '',

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
          Accepted: false
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
                const userdata = await userService.getUser(this.userId);
                this.myProfile = userdata.data;
                this.studentName = userHelper.getUserName(userdata.data);

                //pobranie listy promotorów
                const promoterList = await userService.getAllUsers('Promoter');             
                this.promotersList = promoterList.data;
              
                const proposalData = await proposalService.getProposal(this.userId);

                if(proposalData.data!=""){
                    //tworzenie zaproszenia promotora w zmiennej invitation
                    this.proposal = proposalData.data;
                    this.invitation.StudentId = this.proposal.StudentId;
                    this.invitation.PromoterId = this.proposal.PromoterId;
                    this.invitation.Topic = this.proposal.Topic;

                    //ustawienie imienia pormotora
                    const promoterUserData = await userService.getUser(this.proposal.PromoterId);
                    this.promoterName = userHelper.getUserName(promoterUserData.data)
                }
            } catch (error) {
                this.message = "Wystąpił problem, skontaktuj się z Administratorem";
                this.userSaved = true;
            }
        }
        //aktualizowanie promotora z listy promotorów
        async updatePromoter(promoter: IUser){
            try {
                //pobranie wniosku
                const proposalDataUser = await proposalService.getProposal(this.userId);
                if(proposalDataUser.data != '')
                //jesli wniosek istanieje to tylko zmiana promotora
                    await proposalService.patchProposal(this.userId,[{ "op":"replace", "path":"/PromoterId", "value": promoter.Id}]);
                else {
                    //jesli wniosek nie istanieje to utworzenie wniosku z id studenta(usera) i id promotora, przypisanie do zmiennej proposal
                    await proposalHelper.createProposalWithPromotorId(this.userId, promoter.Id);
                    const newProposal = await proposalService.getProposal(this.userId);
                    this.proposal = newProposal.data;
                }
                //podmiana id promotora 
                this.proposal.PromoterId = promoter.Id;

                //atuaizacja zaproszenia
                this.invitation.StudentId = this.proposal.StudentId;
                this.invitation.PromoterId = this.proposal.PromoterId;
                this.invitation.Topic = this.proposal.Topic;

                //pobranie obiektu promotora z tabeli user i ustawienie wyswietlanego imienia
                const promoterObj = await userService.getUser(this.proposal.PromoterId)
                this.promoterName = userHelper.getUserName(promoterObj.data);
                this.modal = false;
                //rerender komponentu
                this.getData();
            } catch (error) {
                this.message = "Wystąpił problem, skontaktuj się z Administratorem";
                this.userSaved = true;
            }
        }
        async createInvitation(){
          try { 
              console.log(this.invitation);
              console.log(this.proposal)
              //jeśli zmienna zaproszenia jest pusta pokaz błąd
              if(this.invitation.PromoterId ==''|| this.invitation.Topic==''){
                    this.message = "Nie można wysłać zaproszenia do współpracy. Brak danych";
                    this.userSaved = true;
                    return;
                }
                console.log(this.invitation)
                const isExist = await invitationHelper.postInvitationIfNotExist('',this.invitation,this.invitationDesc);
                if(isExist==true){
                    this.message = "Przesłałeś już zaproszenie do współpracy z promotorem";
                    this.userSaved = true;
                }
                else{
                    this.message = "Wysłałeś zaproszenie do promotora";
                    this.userSaved = true;
                }
                this.contact = false;
            } catch (error) {
                    this.message = "Wystąpił problem, skontaktuj się z Administratorem";
                    this.userSaved = true;
            }
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