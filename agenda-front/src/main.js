import { createApp } from 'vue'
import App from './App.vue'
import Aura from '@primevue/themes/aura'
import router from './router'

import PrimeVue from 'primevue/config'

import 'primeicons/primeicons.css'
import 'primeflex/primeflex.css'

const app = createApp(App)

app.use(router)

app.use(PrimeVue, {
  theme: {
    preset: Aura,
    options: {
      primaryColor: '#6366F1'
    }
  }
})

app.mount('#app')
