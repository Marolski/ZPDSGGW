<script lang='ts'>
    import IUser from "../types/User";
    import UserService from "../services/UserService";
    import ProposalService from "../services/ProposalService";
    import Vue from 'vue';
    import { Component } from "vue-property-decorator";
    import IProposal from '../types/Proposal';

    const userService = new UserService();
    const proposalService = new ProposalService();
    @Component({
        name: 'MyProfile',
        components:{}
    })
    export default class MyProfile extends Vue{
        //data
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
            this.myProfile = userdata.data;
            this.proposal = proposalData.data;
        }
        //watchers
    }

</script>

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
            Promotor: <md-button class="md-primary md-raised">Znajdź promotora</md-button>
        </div>
        <div>
            Temat pracy: {{proposal.Topic}} /<md-button class="md-primary md-raised">Dodaj temat pracy</md-button>
        </div>
    </div>
  </div>
</template>
