<template>
  <div class="about">
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
            Promotor: <a v-if="proposal.PromoterId">{{proposal.PromoterId}}</a><md-button v-else class="md-primary md-raised" id="show-modal">Znajdź promotora</md-button>
        </div>
        <div>
            Temat pracy: <a v-if="proposal.Topic">{{proposal.Topic}}</a> <md-button v-else class="md-primary md-raised">Dodaj temat pracy</md-button>
        </div>
        <modal-promoters />
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
    import ModalPromoters from '../components/ModalPromoters.vue';

    const userService = new UserService();
    const proposalService = new ProposalService();
    @Component({
        name: 'MyProfile',
        components:{ModalPromoters}
    })
    export default class MyProfile extends Vue{
        //data
        users: Array<IUser> = [];
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
            Status: '',
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
            this.test()
        }
        async test() {
            const userdata= await userService.getUser();
            const proposalData = await proposalService.getProposal();
            const datatata = await userService.getAllUsers();
            console.log(datatata.data)
            this.myProfile = userdata.data;
            this.proposal = proposalData.data;
            this.proposal.PromoterId = '';
        }
        //watchers
    }

</script>