<template>
    <div>
        <a @click.prevent="show">aaaaaaaaaaaaaaaaaaaaa</a>
        <modal name="modal-promoters">
            <md-table v-model="users" md-sort="name" md-sort-order="asc" md-card md-fixed-header>
      <md-table-toolbar>
        <h1 class="md-title">Users</h1>
      </md-table-toolbar>

      <md-table-row slot="md-table-row" slot-scope="{ item }">
        <md-table-cell md-label="ID" md-sort-by="id" md-numeric>{{item.Degrees}}</md-table-cell>
        <md-table-cell md-label="Name" md-sort-by="name">{{ item.Name }}</md-table-cell>
        <md-table-cell md-label="Email" md-sort-by="email">{{ item.Surname }}</md-table-cell>
        <md-table-cell md-label="Gender" md-sort-by="gender">{{ item.Availability }}</md-table-cell>
        <md-table-cell md-label="Job Title" md-sort-by="title">{{ item.title }}</md-table-cell>
      </md-table-row>
    </md-table>
        </modal>
    </div>
</template>
<script lang="ts">
import Vue from 'vue'
import UserService from '../services/UserService'
import IUser from '../types/User';

const userService = new UserService()
export default Vue.extend({
    name: 'TableFixed',
    data: () => ({
      users: [] as IUser[]
      }),
    methods:{
        show(){
            this.$modal.show('modal-promoters')
        },
        hide(){
            this.$modal.hide('modal-promoters')
        }
    },
    async mounted(){
        const dataUsers = await userService.getAllUsers();
        this.users = dataUsers.data;   
    }  
})
</script>
