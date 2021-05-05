<template>
  <div class="container">
    <div class="large-12 medium-12 small-12 cell">
      <label>File</label>
      <input type="file" id="file" ref="file" v-on:change="handleFileUpload()"/>
      <button v-on:click="submitFile()">Zapisz plik</button>
      <mdb-list-group>
        <mdb-list-group-item class="removeStyle" :action="true" v-for="item in fileListDict" @click.native="downloadFile(item.value, $event)"  tag="a" :key="item.value.Id">{{item.key}} <mdb-btn style="z-index: 1100;" color="danger" @mouseover="hover = true">Usuń</mdb-btn></mdb-list-group-item>
      </mdb-list-group>
    </div>
    <md-snackbar :md-active.sync="userSaved">{{message}}</md-snackbar>
  </div>
</template>
<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import DocumentService from '../services/DocumentService'
import PathHelper from '../services/helpers/PathHelper'
import { mdbListGroup, mdbListGroupItem, mdbBtn } from 'mdbvue';
import { message } from 'ant-design-vue';
import IFile from "../types/File";
const documentService = new DocumentService;
const pathHelper = new PathHelper;
@Component({
    name: "Documents",
    components:{mdbListGroup, mdbListGroupItem, mdbBtn}
})
export default class Documents extends Vue {
    fileList: Array<object> =[]
    userId: string = localStorage.getItem('id');
    file = '';
    message = "";
    userSaved = false;
    fileListDict: object[] =[];
    hover = false;
    buttonWidth: 70;
    text: 'Are you sure to delete this task?';
    created(){
      this.getFiles();
    }

    async getFiles(){
      try {
        const documentsList = await documentService.getDocumentList(this.userId);
        const documentsListData = documentsList.data;
        console.log(documentsList)
        this.fileListDict = pathHelper.getPathList(documentsListData);
      } catch (error) {
        this.message = "Wystąpił błąd, skontaktuj się z Administratorem";
        this.userSaved = true;
      }
    }
    handleFileUpload() {
      this.file = this.$refs.file.files[0];
      console.log(this.file)
    }
    
    async submitFile() {
      try {
        if(this.file==''){
          this.message = "Plik nie został wybrany";
          this.userSaved = true;
          return;
        }
        const formData = new FormData();
        formData.append('file',this.file)
        await documentService.uploadDocument(1,false,this.userId,formData);
        this.getFiles();
      } catch (error) {
        this.message = "Wystąpił błąd, skontaktuj się z Administratorem";
        this.userSaved = true;
      }
    }

    async downloadFile(value: IFile, event){
      try {
        console.log(event.target.type)
        if(event.target.type == 'button'){
          await documentService.deleteFile(value.Id).catch((error)=> console.log(error))
          console.log('ile razy')
          this.getFiles()
        }
        else{
          console.log('jajajajajjaja')
          await documentService.getDocumentByUserId(value.Id)
          .then((response) => {
                const fileURL = window.URL.createObjectURL(new Blob([response.data]));
                const fileLink = document.createElement('a');

                fileLink.href = fileURL;
                fileLink.setAttribute('download', value.FileName);
                document.body.appendChild(fileLink);
                console.log(value)
                fileLink.click();
          });
        }
      } catch (error) {
        this.message = "Wystąpił błąd, skontaktuj się z Administratorem";
        this.userSaved = true;
      }
    }
}
</script>

<style scoped>
.removeStyle{
  z-index: 4 !important;
}
</style>