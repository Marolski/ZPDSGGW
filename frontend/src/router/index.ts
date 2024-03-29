import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Profile from '../views/Profile.vue'
import Proposal from '../views/Proposal.vue'
import TopicList from '../views/ThesisTopicList.vue'
import LoginPage from '../views/LoginPage.vue'
import Documents from '../views/Documents.vue'
import Conversation from '../views/ConversationWithPromoter.vue'
import PromoterMessages from '../views/PromoterConversation.vue'
import PromoterInvitations from '../views/PromoterInvitations.vue'
import DeaneryPromoterVue from '../views/DeaneryPromoter.vue'
import DeaneryView from '../views/DeaneryView.vue'

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
  },
  {
    path: '/documents',
    name: 'Documents',
    component: Documents
  },
  {
    path: '/conversation',
    name: 'Conversation',
    component: Conversation
  },
  {
    path: '/promoterConversation',
    name: 'PromoterMessages',
    component: PromoterMessages
  },
  {
    path: '/promoterInvitations',
    name: 'PromoterMessages',
    component: PromoterInvitations
  },
  {
    path: '/deaneryPromoter',
    name: 'DeaneryPromoterVue',
    component: DeaneryPromoterVue
  },
  {
    path: '/deaneryView',
    name: 'DeaneryView',
    component: DeaneryView
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
