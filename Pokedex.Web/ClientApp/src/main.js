import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import {store} from './store'
import './index.css'


// or with options
//const loadimage = require('./assets/loading.gif')


// or with options



createApp(App)
    .use(store)
    .use(router)
    .mount('#app')
