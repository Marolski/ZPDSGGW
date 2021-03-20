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
            Promotor: <a v-if="proposal.PromoterId">{{proposal.PromoterId}}</a><md-button @click.native="modal = true" class="md-primary md-raised">Znajdź promotora</md-button>
        </div>
        <div>
            Temat pracy: <a v-if="proposal.Topic && proposal.PromoterId">{{proposal.Topic}}</a><router-link v-else to="/topics"><md-button class="md-primary md-raised">Przeglądaj propozycje</md-button></router-link>
        </div>
    </div>
    <div>
        <mdb-modal :show="modal" @close="modal = false">
            <mdb-modal-header>
                <mdb-modal-title>Lista promotorów</mdb-modal-title>
            </mdb-modal-header>
            <mdb-modal-body>
                <mdb-list-group>
                   <mdb-list-group-item :action="true" v-for="item in users" :key="item.Name">{{item.Degrees}} {{item.Name}} {{item.Surname}}</mdb-list-group-item>
                </mdb-list-group>
            </mdb-modal-body>
            <mdb-modal-footer>
                <mdb-btn color="secondary" @click.native="modal = false">Close</mdb-btn>
                <mdb-btn color="primary">Save changes</mdb-btn>
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

    const userService = new UserService();
    const proposalService = new ProposalService();
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
            mdbBadge
        },
    })
    export default class MyProfile extends Vue{
        //data
        users: Array<IUser> = [];
        modal: any = false;
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
            const id = localStorage.getItem('id');
            const userdata = await userService.getUser(id);
            const promoterList = await userService.getAllUsers('Promoter');
            const proposalData = await proposalService.getProposal();
            this.myProfile = userdata.data;
            this.proposal = proposalData.data;
            this.users = promoterList.data;
            console.log(promoterList.data);
            this.proposal.PromoterId = '';
        }
        //watchers
    }

</script>