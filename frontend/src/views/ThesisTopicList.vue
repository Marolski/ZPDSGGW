<template>
  <div class="tableStyle">
  <mdb-datatable-2 v-model="data" selectable  @selected="handleClick($event)"/>
  </div>
</template>

<script lang='ts'>
import Vue from "vue";
import Component from "vue-class-component";
import ThesisTopicService from '../services/ThesisTopicService';
import ITopic from '../types/ThesisTopic';
import { mdbDatatable2 } from 'mdbvue';
import UserService from '../services/UserService';
import { use } from "vue/types/umd";

const topics = new ThesisTopicService();
const userService = new UserService();
@Component({
        name: 'TopicList',
        components:{mdbDatatable2}
    })
    export default class TopicList extends Vue {
        options: Array<ITopic> = [];
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
        }
        async sendDataToProposal(id: string){
          const userData = await userService.getUser(id)
          return userData.data;
        }
        handleClick(param: string){
          console.log(param)
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
</style>
