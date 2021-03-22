<template>
  <div class="tableStyle">
      <div><Alert v-show="showError"/></div>
    <div style="padding-top: 50px;"></div>
    <div class="inputDiv">
      <mdb-input v-model="value" />
      <mdb-btn color="primary" style="margin-bottom: 20px; ">Wybierz</mdb-btn>
    </div>
    <mdb-datatable-2 v-model="data" selectable  @selected="handleClick($event)"/>
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

const topics = new ThesisTopicService();
const userService = new UserService();
@Component({
        name: 'TopicList',
        components:{mdbDatatable2, mdbBtn, mdbInput, Alert }
    })
    export default class TopicList extends Vue {
        options: Array<ITopic> = [];
        showError: boolean | any = false;
        value: any = '';
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
        rows: Array<any> = [];
        
        mounted(){
          this.populate();
        }
        async populate(){
            try {
              const topicsData = await topics.getTopics();
              const allPromoters = await userService.getAllUsers('Promoter');
              const promotersData = allPromoters.data;
              this.options = topicsData.data;
              this.options.forEach(element => {
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
                if (element.Available == true){
                  row.available = 'TAK';
                  this.data.rows.push(row)
                }
              });
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
        handleClick(param: string){
          this.value = param.topic;
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
