<template>
  <div style="width: 70%; margin:auto; margin-bottom: 10%">
    <h1 style="margin-top: 5%">Wiadomości</h1>
    <a-comment>
      <div slot="content">
        <a-form-item>
          <a-textarea :rows="4" :value="value" @change="handleChange" />
        </a-form-item>
        <div style="float: left">
          <div class="ant-btn ant-btn-primary">
            <input type="file" id="file" class="file" ref="file" v-on:change="submitFile()">
            <label for="file" style="cursor: pointer; padding-top:4px">Załącz plik</label>
          </div>
          <a-form-item>
            <a-button html-type="submit" :loading="submitting" type="primary" @click.native="createMessage">
              Wyślij wiadomość
            </a-button>
          </a-form-item>
        </div>
        <div style="margin:0 0 10% 0%; width:50%">
          <a v-if="fileName!=''" >{{fileName}}</a>
        </div>
      </div>
    </a-comment>
    <div style="margin-bottom: 10%" @click="checkElement">
      <a-list
      v-if="comments.length"
      :data-source="comments"
      :header="`${comments.length} ${comments.length > 1 ? 'wiadomości' : 'wiadomość'}`"
      item-layout="horizontal"
      class="pointer"
      >
        <a-list-item  v-if="collapsed" slot="renderItem" slot-scope="item">
          <a-comment :author="item.author" :avatar="item.avatar" :datetime="item.datetime">
            <template slot="actions">
              <span v-for="action in item.actions" :key="action" @click="downloadFile">{{ action }}</span>
            </template>
            <p slot="content">
              {{ item.content }}
            </p>
        </a-comment>
        </a-list-item>
      </a-list>
    </div>
    <md-snackbar :md-active.sync="userSaved">{{message}}</md-snackbar>
  </div>
</template>
<script lang="ts">
import moment from 'moment';
import { Button } from 'ant-design-vue';
import Vue from 'vue';
import MessageService from '../services/MessageService';
import Component from 'vue-class-component';
import IMessage from '../types/IMessage';
import UserHelper from '../services/helpers/UserHelper';
import UserService from '../services/UserService';
import DateHelper from '../services/helpers/DateHelper'
import PathHelepr from '../services/helpers/PathHelper';
import ProposalService from '../services/ProposalService';
import IInvitation from '../types/Invitation';
import { message } from 'ant-design-vue'
import InvitationService from '../services/InvitationService';
const messageService = new MessageService();
const userService = new UserService();
const userHelper = new UserHelper();
const dateHelper = new DateHelper();
const pathHelper = new PathHelepr();
const proposalService = new ProposalService();
const invitationService = new InvitationService();
@Component({
    name: "Messages",
    components:{Button }
})
export default class Messages extends Vue{
  comments: Array<object> =[]
  submitting = false;
  value = '';
  moment;
  message = "";
  userSaved = false;
  userId = localStorage.getItem('id');
  file= '';
  fileName = '';
  invitation: IInvitation;
  pathDictionary = new Map();
  collapsed = false;
    created(){
      this.getMessages();
    }

    checkElement(e){
      if(e.target.className == 'ant-list-header')
        this.collapsed = !this.collapsed;
    }

    async downloadFile(e){
      try {
        const name  = e.target.innerHTML;
        await messageService.getMessageFile(this.pathDictionary.get(name))
          .then((response) => {
                const fileLink = document.createElement('a');

                fileLink.href = response.config.url;
                fileLink.setAttribute('download', name);
                document.body.appendChild(fileLink);
                fileLink.click();
          });
      } catch (error) {
        message.error("Wystąpił błąd, skontaktuj się z administratorem");
      }
    }
    submitFile(e){
      this.file = this.$refs.file.files[0];
      this.fileName = this.file.name;
    }
    async getMessages(){
      try {
        const invitation = await invitationService.getInvitation(this.userId);
        this.invitation = invitation.data;
        const messages = await messageService.getAllRecivierMessage(this.userId);
        for(const element of messages.data){
            const receiver = await userService.getUser(element.SendFrom);
            const name = pathHelper.getName(element.Path);
            this.pathDictionary.set(name,element.Id);
            const newComment = {
              author: userHelper.getUserName(receiver.data),
              content: element.Description,
              actions: [name],
              datetime: dateHelper.cutDate(element.Date),
            }
            this.comments.push(newComment)
          }
      } catch (error) {
        message.error("Wystąpił błąd, skontaktuj się z administratorem");
      }
    }

    async createMessage() {
      try {
        if (!this.value) {
        return;
        }
        this.submitting = true;
        const author = await userService.getUser(this.userId);
        const authorName = userHelper.getUserName(author.data)
        setTimeout(() => {
          this.submitting = false;
          const newComment = {
            author: authorName,
            content: this.value,
            datetime: moment().fromNow(),
            actions: [this.fileName]
          };
          this.comments.unshift(newComment);
          this.value = '';
                  this.collapsed = true;
        }, 1000);
        const formData = new FormData();
        formData.append('file',this.file)
        await messageService.postMessage(formData, this.invitation.PromoterId, this.userId, this.value);
        this.appendDict();
      } catch (error) {
        message.error("Nie udało się wysłać wiadomości, skontaktuj się z administratorem");
      }
    }
    handleChange(e) {
      this.value = e.target.value;
    }
    async appendDict(){
      try {
        const messages = await messageService.getAllRecivierMessage(this.invitation.PromoterId);
        for(const element of messages.data){
          const name = pathHelper.getName(element.Path);
          this.pathDictionary.set(name,element.Id);
        }
      } catch (error) {
        message.error("Wystąpił błąd, skontaktuj się z administratorem");
      }
    }

}
</script>

<style>
.file {
  opacity: 0;
  width: 0.1px;
  height: 0.1px;
  position: absolute;
}
.file-input label {
  margin-left: 50px;
  margin-bottom: 15px;
  display: block;
  position: relative;
  width: 200px;
  height: 50px;
  border-radius: 25px;
  background-color: #3381F6;
  box-shadow: 0 4px 7px rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  color: #fff;
  font-weight: bold;
  cursor: pointer;
  transition: transform .2s ease-out;
  justify-content: center;
}
.ant-btn-primary {
    color: #fff;
    background-color: #1890ff;
    border-color: #1890ff;
    text-shadow: 0 -1px 0 rgb(0 0 0 / 12%);
    box-shadow: 0 2px 0 rgb(0 0 0 / 5%);
    cursor: pointer;
}
.ant-btn, .ant-btn:active, .ant-btn:focus {
    outline: 0;
}
.ant-btn {
    margin-left: 3%;
    text-align: center;
    position: relative;
    font-weight: 400;
    border: 1px solid transparent;
    box-shadow: 0 2px 0 rgb(0 0 0 / 2%);
    cursor: pointer;
    transition: all 0.3s cubic-bezier(0.645, 0.045, 0.355, 1);
    touch-action: manipulation;
    height: 32px;
    font-size: 14px;
    border-radius: 4px;
    margin-bottom: 5%;
    margin-right: 30%;
}
.pointer{
  cursor: pointer;
}
</style>
