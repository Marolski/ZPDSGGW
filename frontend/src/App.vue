<template>
  <div id="app">
    <div class="routing" v-if="this.$route.path !== '/'">
      <md-toolbar class="md-primary">
        <md-button class="md-icon-button" @click="showNavigation = true">
          <md-icon>menu</md-icon>
        </md-button>
        <span class="md-title">My Title</span>

      </md-toolbar>

      <md-drawer :md-active.sync="showNavigation" md-swipeable>
        <md-toolbar class="md-transparent" md-elevation="0">
          <span class="md-title">Menu</span>
        </md-toolbar>

        <md-list>
          <md-list-item>
            <span id="nav" class="md-list-item-text"><router-link to="/">Home</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text"><router-link to="/about">About</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text"><router-link to="/profile">Profile</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text"><router-link to="/proposal">Proposal</router-link></span>
          </md-list-item>

          <md-list-item>
            <span id="nav" class="md-list-item-text"><router-link to="/topics">Topics</router-link></span>
          </md-list-item>
        </md-list>
      </md-drawer>
    </div>
    <router-view/>
  </div>
</template>

<script>
  export default {
    name: 'Temporary',
    data: () => ({
      showNavigation: false,
      showSidepanel: false
    })
  }
</script>

<style lang="scss">
#app {
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
</style>
