<template>
    <div>
        <md-button class="md-primary md-raised" @click.prevent="show">Znajdź promotora</md-button>
        <modal name="modal-promoters" >
            <div class="md-layout md-gutter modalStyle">
                <div class="md-layout-item">
                    <md-field>
                    <label for="movie">Movie</label>
                    <md-select v-model="movie" name="movie" id="movie">
                        <md-option value="fight-club">Fight Club</md-option>
                        <md-option value="godfather">Godfather</md-option>
                        <md-option value="godfather-ii">Godfather II</md-option>
                        <md-option value="godfather-iii">Godfather III</md-option>
                        <md-option value="godfellas">Godfellas</md-option>
                        <md-option value="pulp-fiction">Pulp Fiction</md-option>
                        <md-option value="scarface">Scarface</md-option>
                    </md-select>
                    </md-field>
                </div>
                    <md-field>
                        <label>Type here!</label>
                        <md-input v-model="type"></md-input>
                        <span class="md-helper-text">Helper text</span>
                    </md-field>
            </div>
        </modal>
    </div>
</template>
<script lang="ts">
import Vue from 'vue'
import UserService from '../services/UserService'
import IUser from '../types/User';

const userService = new UserService()
export default Vue.extend({
    name: 'BasicSelect',
    data: () => ({
      users: [] as IUser[],
      selected: {},
      movie: 'godfather',
      country: null,
      font: null,
      type: null,
      }),
    methods:{
        show(){
            this.$modal.show('modal-promoters')
            console.log("pokaz")

        },
        hide(){
            this.$modal.hide('modal-promoters')
            console.log('ukryj')
        },
        onSelect (item) {
        this.selected = item
      }
    },
    async created(){
        const dataUsers = await userService.getAllUsers('Promoter');
        this.users = dataUsers.data;   
    }  
})
</script>
<style scoped>
    .modalStyle{
        width: 80% !important;
    }
</style>
