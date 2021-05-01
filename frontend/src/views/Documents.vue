<template>
  <div class="container">
    <div class="large-12 medium-12 small-12 cell">
      <label>File</label>
      <input type="file" id="file" ref="file" v-on:change="handleFileUpload()"/>
      <button v-on:click="submitFile()">Submit</button>
      <a :href="file.url" v-text="file.label" @click.prevent="downloadItem(file)" />
    </div>
  </div>
</template>
<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import DocumentService from '../services/DocumentService'
const documentService = new DocumentService;
@Component({
    name: "Documents"
})
export default class Documents extends Vue {
    fileList: Array<object> =[]
    userId: string = localStorage.getItem('id');
    file = '';
    handleFileUpload() {
      this.file = this.$refs.file.files[0];
      console.log(this.file)
    }
    
    async submitFile() {
      const formData = new FormData();
      formData.append('file',this.file)
      await documentService.uploadDocument(1,false,this.userId,formData);
      console.log(formData);
    }

    async downloadItem(){
      return false;
    }
}
</script>