<template>
  <a-upload
    :file-list="fileList"
    @change="handleChange"
  >
    <a-button> <a-icon type="upload" /> Upload </a-button>
  </a-upload>
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
    
    handleChange(info) {
        const formData = new FormData();
        console.log(info)
        formData.append('fileToUpload', info.file);
        console.log(formData)
        documentService.uploadDocument(1,false,this.userId,formData)
    }
}
</script>