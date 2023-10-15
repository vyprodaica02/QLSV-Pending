import { createApp } from "vue";
import App from "./App.vue";
import { router } from "./router";
import { pinia } from "./Store/storePinia";
import "./style.css";

const app = createApp(App);
app.use(router);
app.use(pinia);
app.mount("#app");
