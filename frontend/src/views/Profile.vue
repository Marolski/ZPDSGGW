<template>
  <div class="about">
      <div><Alert v-show="showError"/></div>
    <h1>Moj profil</h1>
    <div>
        <div>
            Imie: {{myProfile.Name}}
        </div>
        <div>
            Nazwisko:{{myProfile.Surname}}
        </div>
        <div>
            Numer indeksu: {{myProfile.StudentNumber}}
        </div>
        <div>
            Promotor: <a v-if="proposal.PromoterId!=''">{{promoterName}}</a><md-button @click.native="modal = true" class="md-primary md-raised">Znajdź promotora</md-button>
        </div>
        <div>
            Temat pracy: <a v-if="proposal.Topic && proposal.PromoterId">{{proposal.Topic}}</a><router-link to="/topics"><md-button class="md-primary md-raised">Przeglądaj propozycje</md-button></router-link>
        </div>
    </div>
    <div>
        <router-link to="/proposal"><md-button class="md-primary md-raised">Wyślij wniosek</md-button></router-link>
        <md-button class="md-primary md-raised">Wyślij zaproszenie do promotora</md-button>
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
  </div>
</template>

<script lang='ts'>
    import IUser from "../types/User";
    import UserService from "../services/UserService";
    import ProposalService from "../services/ProposalService";
    import Vue from 'vue';
    import { Component } from "vue-property-decorator";
    import IProposal from '../types/Proposal';
    import { mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter, mdbBtn,mdbListGroup, mdbListGroupItem, mdbBadge  } from 'mdbvue';
    import UserHelper from '../services/helpers/UserHelper'
    import Alert from  '../components/Alert.vue';
    import ProposalHelper from '../services/helpers/ProposalHelper';
    import { use } from "vue/types/umd";
    const userService = new UserService();
    const proposalService = new ProposalService();
    const userHelper = new UserHelper();
    const proposalHelper = new ProposalHelper();
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
            Alert
        },
    })
    export default class MyProfile extends Vue{
        //data
        userId: string = localStorage.getItem('id');
        showError: boolean | any = false;
        promotersList: Array<IUser> = [];
        modal: any = false;
        promoterName = '';
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
            Id: '',
            Status: 1,
            Topic: '',
            Date: '',
            PromoterId: '',
            StudentId: ''
        }
        //computed properties
        get userCount(){
            return this.myProfile;
        }
        //props
        //methods
        //lifecycles hooks
        created(){
            this.getData()
            this.checkProposalExist()
        }
        async getData() {
            try {
                const userdata = await userService.getUser(this.userId);
                const promoterList = await userService.getAllUsers('Promoter');        
                this.myProfile = userdata.data;
                this.promotersList = promoterList.data;
            } catch (error) {
                this.showError = true;
            }
        }
        async checkProposalExist(){
            try {
                const proposalDataUser = await proposalService.getProposal(this.userId);
                if(proposalDataUser.data == "")
                    proposalHelper.createEmptyProposal(this.userId);
                else {
                    this.proposal = proposalDataUser.data;
                    const promoterFromProposal = await userService.getUser(proposalDataUser.data.PromoterId)
                    this.promoterName = userHelper.getUserName(promoterFromProposal.data);  
                }
            } catch (error) {
                this.showError = true;
            }
        }
        async updatePromoter(user: IUser){
            try {
                await proposalService.patchProposal(this.userId,[{ "op":"replace", "path":"/PromoterId", "value": user.Id}]);
                const proposalDataUser = await proposalService.getProposal(this.userId);
                this.proposal.PromoterId = proposalDataUser.data.PromoterId;
                const promoterFromProposal = await userService.getUser(proposalDataUser.data.PromoterId)
                this.promoterName = userHelper.getUserName(promoterFromProposal.data);
                this.modal = false;
            } catch (error) {
                this.showError = true;
            }
        }
        //watchers
    }

</script>