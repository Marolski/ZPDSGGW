<template>
  <div>
    <mdb-btn color="primary" @click.native="modal = true">Launch demo modal</mdb-btn>
    <mdb-modal :show="modal" @close="modal = false">
      <mdb-modal-header>
        <mdb-modal-title>Modal title</mdb-modal-title>
      </mdb-modal-header>
      <mdb-modal-body>...</mdb-modal-body>
      <mdb-modal-footer>
        <mdb-btn color="secondary" @click.native="modal = false">Close</mdb-btn>
        <mdb-btn color="primary">Save changes</mdb-btn>
      </mdb-modal-footer>
    </mdb-modal>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import UserService from '../services/UserService'
import IUser from '../types/User';
 import { mdbModal, mdbModalHeader, mdbModalTitle, mdbModalBody, mdbModalFooter, mdbBtn } from 'mdbvue';

const userService = new UserService()
export default Vue.extend({
    name: 'BasicSelect',
    components: {
      mdbModal,
      mdbModalHeader,
      mdbModalTitle,
      mdbModalBody,
      mdbModalFooter,
      mdbBtn
    },
    data() {
      return {
        modal: false
      };
    }
})
</script>
<style scoped>
    .modalStyle{
        width: 80% !important;
    }
</style>
