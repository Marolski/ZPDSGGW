@ts-ignore
<template>
  <div class="container">
    <div class="large-12 medium-12 small-12 cell">
      <div class="fatherDiv">
        <blockquote class="blockquote text-center padding">
          <p class="mb-0">Wybierz rodzaj dokumentu</p>
          <a-select default-value="wybierz" style="width: 250px" @change="handleChange" id="validate" class="required">
            <a-select-option v-for="item in documentKindList" :key="item" :value="item">
              {{item}}
            </a-select-option>
          </a-select>
        </blockquote>
        <div class="file-input">
          <input type="file" id="file" class="file" ref="file" @change="submitFile">
          <label for="file">Wybierz plik</label>
        </div><div style="clear:both;"></div>
      </div>
      <mdb-list-group>        
        <mdb-list-group-item class="removeStyle" :action="true" v-for="item in fileListDict" @click.native="downloadFile(item.value, $event)"  tag="a" :key="item.value.Id">{{item.kind}} {{'___'}} {{item.key}} 
          <a-popconfirm title="Czy na pewno chcesz usunąć plik?" ok-text="Tak" cancel-text="Nie" @confirm="confirm(item.value)" style="z-index: 60;">
            <mdb-btn style="z-index: 1100;" color="danger" @mouseover="hover = true">Usuń</mdb-btn></a-popconfirm></mdb-list-group-item>
      </mdb-list-group>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import DocumentService from '../services/DocumentService'
import PathHelper from '../services/helpers/PathHelper'
import { mdbListGroup, mdbListGroupItem, mdbBtn, mdbBadge, mdbContainer  } from 'mdbvue';
import IFile from "../types/File";
import {documentKind, DocumentKind} from "../enums/Enum";
import { message } from 'ant-design-vue'

const documentService = new DocumentService;
const pathHelper = new PathHelper;
@Component({
    name: "Documents",
    components:{mdbListGroup, mdbListGroupItem, mdbBtn, mdbBadge, mdbContainer }
})
export default class Documents extends Vue {
    fileList: Array<object> =[]
    file = '';
    fileListDict: object[] =[];
    clickedItemId = '';
    selectedKindOfDocs = '';
    documentKindList: Array<string>;
    hover = false;
    created(){
      this.documentKindList = Object.keys(documentKind).map(x=>{return documentKind[x]})
      this.getFiles();
    }

    handleChange(e){
      const number = Object.keys(documentKind).filter(x=>{return e == documentKind[x]})[0];
      this.selectedKindOfDocs = number;
    }
    async confirm(e) {
      try {
        message.success('Plik został usunięty');
        await documentService.deleteFile(this.clickedItemId).catch((error)=> console.log(error))
        this.getFiles()
      } catch (error) {
        message.error("Wystąpił błąd, skontaktuj się z administratorem");
      }
    }

    async getFiles(){
      try {
        const documentsList = await documentService.getDocumentList(localStorage.getItem('id'));
        const documentsListData = documentsList.data;
        this.fileListDict = pathHelper.getPathList(documentsListData);
        console.log(this.fileListDict)
      } catch (error) {
        message.error("Wystąpił błąd, skontaktuj się z administratorem");
      }
    }
    
    async submitFile(event) {
      try {
        if(this.selectedKindOfDocs==''){
          const elementToValidate = document.getElementById('validate');
          elementToValidate.style.borderColor = 'red';
          elementToValidate.style.borderStyle = 'solid';
          elementToValidate.style.borderWidth = '1px';
          elementToValidate.style.borderRadius = '4px'
          message.info('Wybierz rodzaj dokumentu')
          return
        }
        this.file = event.target.files[0];
        if(this.file==''){
          message.info("Plik nie został wybrany");
          return;
        }
        const formData = new FormData();
        formData.append('file',this.file)
        await documentService.uploadDocument(this.selectedKindOfDocs,false,localStorage.getItem('id'),formData);
        this.getFiles();
      } catch (error) {
        message.error("Wystąpił błąd, skontaktuj się z administratorem");
      }
    }

    async downloadFile(value: IFile, e){
      try {
        this.clickedItemId = value.Id;
        if(e.target.type=='button') return false;
        else{
          await documentService.getDocumentByUserId(value.Id)
          .then((response) => {
                const fileLink = document.createElement('a');

                fileLink.href = response.config.url;
                fileLink.setAttribute('download', value.FileName);
                document.body.appendChild(fileLink);
                console.log(fileLink)
                fileLink.click();
          });
        }
      } catch (error) {
        message.error("Wystąpił błąd, skontaktuj się z administratorem");
      }
    }
}
</script>

<style scoped>
.file {
  opacity: 0;
  width: 0.1px;
  height: 0.1px;
  position: absolute;
}
.file-input label {
  float: left;
  margin-left: 50px;
  margin-bottom: 15px;
  display: block;
  position: relative;
  width: 200px;
  height: 50px;
  border-radius: 25px;
  background: linear-gradient(40deg, #3381F6, #7873f5);
  box-shadow: 0 4px 7px rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  justify-content: center;
  color: #fff;
  font-weight: bold;
  cursor: pointer;
  transition: transform .2s ease-out;
  justify-content: center;
}
.padding{
  float: left;
  width: 70%;
}
.fatherDiv{
  margin-top: 50px;
}
</style>