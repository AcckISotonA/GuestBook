import Vue from 'vue';

import Router from 'vue-router';
Vue.use(Router);


export const router = new Router({
    mode: 'history',
    routes: [
        {
            path: "*",
            redirect: { path: "/messages/1&userName&false" }
        },
        {
            name: "messages",
            path: "/messages/:pageNumber&:sortColumn&:descendingOrder",
            component: () => import('./components/Messages.vue'),
            props: true
        },
        {
            name: "create-message",
            path: "/create-message/:pageNumber&:sortColumn&:descendingOrder",
            component: () => import('./components/CreateMessage.vue'),
            props: true
        }
    ]
});