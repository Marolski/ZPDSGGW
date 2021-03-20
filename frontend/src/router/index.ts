import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Profile from '../views/Profile.vue'
import Proposal from '../views/Proposal.vue'
import TopicList from '../views/ThesisTopicList.vue'
import LoginPage from '../views/LoginPage.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'LoginPage',
    component: LoginPage
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/profile',
    name: 'MyProfile',
    component: Profile
  },
  {
    path: '/proposal',
    name: 'Proposal',
    component: Proposal
  },
  {
    path: '/topics',
    name: 'ThesisTopic',
    component: TopicList
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
