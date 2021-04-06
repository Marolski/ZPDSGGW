<template>
  <div class="tableStyle">
      <div><Alert v-show="showError"/></div>
    <div style="padding-top: 50px;"></div>
    <div class="inputDiv">
      <mdb-input v-model="thesisTopicName" />
      <mdb-btn color="primary" style="margin-bottom: 20px; " @click.native="updateTopicStatus">Wybierz</mdb-btn>
    </div>
    <mdb-datatable-2 v-model="data" selectable  @selected="handleClick(selected = $event)"/>
  </div>
</template>

<script lang='ts'>
import Vue from "vue";
import Component from "vue-class-component";
import ThesisTopicService from '../services/ThesisTopicService';
import ITopic from '../types/ThesisTopic';
import { mdbDatatable2, mdbBtn  } from 'mdbvue';
import UserService from '../services/UserService';
import { mdbInput } from 'mdbvue';
import Alert from  '../components/Alert.vue';
import ProposalService from "../services/ProposalService";

const topics = new ThesisTopicService();
const userService = new UserService();
const proposalService = new ProposalService();
@Component({
        name: 'TopicList',
        components:{mdbDatatable2, mdbBtn, mdbInput, Alert }
    })
    export default class TopicList extends Vue {
        userId: string = localStorage.getItem('id');
        inputValue: string;
        topicsArray: Array<ITopic> = [];
        showError: boolean | any = false;
        checkedTopicId: string;
        thesisTopicName: any = '';
        data = {
          rows: [],
          columns: [{
          label: 'Imie',
          field: 'name',
          sort: true
        },
        {
          label: 'Temat',
          field: 'topic',
          sort: true
        },
        {
          label: 'Dostępny',
          field: 'available',
          sort: true
        }]
        }
        
        created(){
          this.populate();
        }
        
        async populate(){
            try {
              this.data.rows = [];
              console.log(this.data.rows.length)
              const allTopics = await topics.getTopics();
              const allPromoters = await userService.getAllUsers('Promoter');
              const promotersData = allPromoters.data;
              this.topicsArray = allTopics.data;
              this.topicsArray.forEach(element => {
                const row ={
                  name: element.PromoterId,
                  topic: element.Topic,
                  available: '',
                };
                const name = promotersData.filter(function(elem){
                  if(elem.Id === element.Id){
                    return elem
                  }
                })
                row.name = `${name[0].Degrees} ${name[0].Name} ${name[0].Surname}`;
                if (element.Available == 1)
                  row.available = 'TAK';
                else if(element.Available == 2)
                  row.available = 'NIE';
                else if(element.Available == 3)
                  row.available = "ZAREZERWOWANE";
                this.data.rows.push(row)
                console.log(row)

              });
              console.log(this.data)
              $vm.set()
            } catch (error) {
              this.showError = true
            }
        }
        async sendDataToProposal(id: string){
          try {
            const userData = await userService.getUser(id)
            return userData.data;
          } catch (error) {
              this.showError = true;
          }
        }
        handleClick(param: ITopic){
          if(param == undefined)
            return
          this.checkedTopicId = this.topicsArray.find(element => element.Topic == param.topic).Id;
          this.thesisTopicName = param.topic;
        }
        async updateTopicStatus(){
          try {
            console.log(this.thesisTopicName)
            await topics.patchTopic(this.checkedTopicId,[{ "op":"replace", "path":"/Available", "value": 3}])
            await proposalService.patchProposal(this.userId,[{ "op":"replace", "path":"/Topic", "value": this.thesisTopicName}])
            this.populate();
          } catch (error) {
            this.showError = true;
          }
        }
    }
</script>
<style scoped>
.tableStyle{
  text-align: center;
  margin: auto;
}
.mdb-datatable{
  max-width: 80% !important;
}
.md-form{
  margin-left: 10%;
  margin-right: 2%;
  min-width: 80%;
  margin-bottom: 20px;
}
.inputDiv{
  display: flex;
  margin-right: 10%;
  margin-left: 10%;
}
.form-control{
  min-width: 300px;
}
</style>
