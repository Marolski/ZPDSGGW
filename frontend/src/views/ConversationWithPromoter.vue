<template>
  <div>
    <a-list
      v-if="comments.length"
      :data-source="comments"
      :header="`${comments.length} ${comments.length > 1 ? 'replies' : 'reply'}`"
      item-layout="horizontal"
    >
      <a-list-item slot="renderItem" slot-scope="item">
        <a-comment
          :author="item.author"
          :avatar="item.avatar"
          :content="item.content"
          :datetime="item.datetime"
        />
      </a-list-item>
    </a-list>
    <a-comment>
      <div slot="content">
        <a-form-item>
          <a-textarea :rows="4" v-model="value" />
        </a-form-item>
        <a-form-item>
          <a-button html-type="submit" :loading="submitting" type="primary" @click.native="handleSubmit">
            Add Comment
          </a-button>
        </a-form-item>
      </div>
    </a-comment>
  </div>
</template>
<script lang="ts">
import moment from 'moment';
import Vue from 'vue';
export default class Conversation extends Vue {
    comments: Array<object> = [];
    submitting = false;
    value = '';
    moment;

    created(){
        console.log('jaja')
    }

    handleSubmit() {
        if (!this.value)
            return;
        this.submitting = true;
        console.log(this.value)
        setTimeout(() => {
            console.log('chuj ci na')
            this.submitting = false;
            this.comments = [
                {
                    author: 'Han Solo',
                    avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png',
                    content: this.value,
                    datetime: moment().fromNow(),
                },
                ...this.comments
            ];
        this.value = '';
      },100);
    }
}
</script>
