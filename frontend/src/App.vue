<template>
  <div id="app">
    <div class="routing" v-if="this.$route.path !== '/'">
      <md-toolbar class="md-primary">
        <md-button class="md-icon-button" @click="showNavigation = true">
          <md-icon>menu</md-icon>
        </md-button>
        <span class="md-title">System zarządzania pracą dyplomową</span>
        <a-button style="margin: 0;position:absolute; right: 50px" type="default" shape="circle" icon="poweroff" @click="logout" />
      </md-toolbar>

      <md-drawer :md-active.sync="showNavigation" md-swipeable>
        <md-toolbar class="md-transparent" md-elevation="0">
          <span class="md-title">Menu</span>
        </md-toolbar>

        <md-list v-if="userRole == 'Student'">
          <md-list-item>
            <span id="nav" class="md-list-item-text" @click="showNavigation = false"><router-link to="/profile">Profil</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text" @click="showNavigation = false"><router-link to="/proposal">Wniosek</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text" @click="showNavigation = false"><router-link to="/topics">Tematy</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text" @click="showNavigation = false"><router-link to="/conversation">Komunikacja</router-link></span>
          </md-list-item>
        </md-list>

        <md-list v-if="userRole == 'Promoter'">

          <md-list-item>
            <span id="nav" class="md-list-item-text" @click="showNavigation = false"><router-link to="/promoterConversation">Komunikacja</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text" @click="showNavigation = false"><router-link to="/promoterInvitations">Zaproszenia</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text" @click="showNavigation = false"><router-link to="/deaneryPromoter">Dziekanat</router-link></span>
          </md-list-item>

        </md-list>

        <md-list v-if="userRole == 'Deanery'">

          <md-list-item>
            <span id="nav" class="md-list-item-text" @click="showNavigation = false"><router-link to="/deaneryView">Dokumenty</router-link></span>
          </md-list-item>
          
        </md-list>

      </md-drawer>
    </div>
    <router-view/>
  </div>
</template>

<script>
import router from './router';
  export default {
    name: 'Temporary',
    components: { },
    data: () => ({
      showNavigation: false,
      showSidepanel: false,
      userRole: localStorage.getItem('role')
    }),
    methods: {
      logout(){
        localStorage.clear();
        setTimeout(() => {
                router.push('/')
            }, 500);
      }
    }
  }
</script>

<style lang="scss">
#app {
  overflow: auto;
  height: 95%;
  width: 100%;
  height: 95vh;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 10px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
.page-container {
    min-height: 300px;
    overflow: hidden;
    position: relative;
    border: 1px solid rgba(#000, .12);
  }

   // Demo purposes only
  .md-drawer {
    width: 230px;
    max-width: calc(100vw - 125px);
  }

  .md-content {
    padding: 8px;
  }
  .logout{
    position: absolute;
    right: 10px;
  }
</style>
