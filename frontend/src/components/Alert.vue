<template>
  <section class="grey">
      <div class="container">
        <div class="placement">
          <mdb-toast-notification v-for="message in messages" :key="message.id" :title="message.title" :message="message.message" :icon="message.icon" :iconColor="message.color" :show="show"/>
        </div>
      </div>
  </section>
</template>

<script lang="ts">
  import { mdbToastNotification } from "mdbvue";

  export default {
    name: "NotificationsPage",
    components: {
      mdbToastNotification
    },
    data() {
      return {
        show: true,
        messages: [
          {
            id: 1,
            title: "uwaga",
            message: "Wystąpił błąd skontaktuj się z administratorem",
            icon: 'exclamation',
            color: 'danger'
          },
        ]
      };
    }
  };
</script>
<style>
  .placement {
    position: absolute;
    right: 0;
    margin-right: 20px;
  }
</style>