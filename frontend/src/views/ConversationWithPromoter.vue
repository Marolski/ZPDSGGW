<template>
  <div style="width: 70%; margin:auto;">
    <a-comment>
      <div slot="content">
        <a-form-item>
          <a-textarea :rows="4" :value="value" @change="handleChange" />
        </a-form-item>
        <a-form-item>
          <a-button html-type="submit" :loading="submitting" type="primary" @click.native="handleSubmit">
            Wyślij wiadomość
          </a-button>
        </a-form-item>
      </div>
    </a-comment>
    <a-list
      v-if="comments.length"
      :data-source="comments"
      :header="`${comments.length} ${comments.length > 1 ? 'wiadomości' : 'wiadomość'}`"
      item-layout="horizontal"
    >
      <a-list-item slot="renderItem" slot-scope="item">
        <a-comment
          :author="item.author"
          :content="item.content"
          :datetime="item.datetime"
        />
      </a-list-item>
    </a-list>
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
const messageService = new MessageService();
const userService = new UserService();
const userHelper = new UserHelper();
@Component({
    name: "Messages",
    components:{Button }
})
export default class Messages extends Vue{
  comments: Array<object> =[]
  submitting = false;
  value = '';
  moment;
  userId = localStorage.getItem('id');

    created(){
      console.log('hahaha')
      this.getMessages();
    }

    async getMessages(){
      const messages = await messageService.getAllRecivierMessage(this.userId);
      console.log(messages.data)
      for(const element of messages.data){
        const receiver = await userService.getUser(element.SendFrom);
        const newComment = {
          author: userHelper.getUserName(receiver.data),
          avatar: '',
          content: element.Description,
          datetime:element.Date,
        }
        this.comments.push(newComment)
      }
    }

    handleSubmit() {
      if (!this.value) {
        return;
      }
      this.submitting = true;

      setTimeout(() => {
        this.submitting = false;
        const newComment = {
          author: 'Han Solo',
          avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png',
          content: this.value,
          datetime: moment().fromNow(),
        };
        this.comments.push(newComment);
        this.value = '';
      }, 1000);
    }
    handleChange(e) {
      this.value = e.target.value;
    }

}
</script>
