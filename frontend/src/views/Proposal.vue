<script lang="ts">
import Vue from 'vue';
import ProposalService from '../services/ProposalService';
import {Component} from 'vue-property-decorator'

const proposalService = new ProposalService();
@Component({
    name: 'Proposal',
    components:{}
})
export default class Proposal extends Vue{
    myOptions = ['a', 's','d']
}
</script>
<template>
    <div>
        <md-button class="md-primary md-raised">Dodaj</md-button>
        <div class="md-layout md-gutter">
            <div class="md-layout-item">
                <md-field>
                <label for="promoter">Promotor</label>
                <md-select v-model="promoter" name="promoter" id="promoter">
                    <md-option value="fight-club">Fight Club</md-option>
                    <md-option value="godfather">Godfather</md-option>
                    <md-option value="godfather-ii">Godfather II</md-option>
                    <md-option value="godfather-iii">Godfather III</md-option>
                    <md-option value="godfellas">Godfellas</md-option>
                    <md-option value="pulp-fiction">Pulp Fiction</md-option>
                    <md-option value="scarface">Scarface</md-option>
                    <md-option value="fight-club">Fight Club</md-option>
                    <md-option value="godfather">Godfather</md-option>
                    <md-option value="godfather-ii">Godfather II</md-option>
                    <md-option value="godfather-iii">Godfather III</md-option>
                    <md-option value="godfellas">Godfellas</md-option>
                    <md-option value="pulp-fiction">Pulp Fiction</md-option>
                    <md-option value="scarface">Scarface</md-option>
                </md-select>
                </md-field>
                <div class="md-layout-item">
                <md-field>
                <label for="promoter">Promotor</label>
                <md-select v-model="promoter" name="promoter" id="promoter" options="myOptions">
                    
                </md-select>
                </md-field>
                </div>
                <div>
                    <md-button class="md-primary md-raised" @click="promoter = 'pulp-fiction'">Zapisz</md-button>
                </div>
            </div>
        </div>
        
    </div>
</template>