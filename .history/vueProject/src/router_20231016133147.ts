import { createRouter, createWebHistory } from "vue-router";
import Home from "./components/TableHocSinh/TableHocSinh.vue";
import About from "./components/About.vue";
import LoginComponent from "./components/login/LoginComponent.vue";
import DangkiComponent from "./components/dangki/DangkiComponent.vue";
import SuaThongTinHocSinh from "./components/TableHocSinh/SuaThongTinHocSinh.vue";
const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/about",
    name: "About",
    component: About,
  },
  {
    path: "/LoginComponent",
    component: LoginComponent,
  },
  {
    path: "/DangkiComponent",
    component: DangkiComponent,
  },
  {
    path: "/SuaThongTinHocSinh",
    component: SuaThongTinHocSinh,
  },
];

export const router = createRouter({
  history: createWebHistory(),
  routes,
});
