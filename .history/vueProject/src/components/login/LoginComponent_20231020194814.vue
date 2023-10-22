<template>
  <div
    class="bg-gradient-to-tr from-blue-500 via-purple-500 to-purple-500 w-screen h-screen flex justify-center items-center"
  >
    <div class="bg-white p-8 rounded shadow-md w-full md:w-96">
      <!-- Thay đổi kích thước trên màn hình lớn -->
      <h1 class="text-2xl font-semibold mb-4 text-center">Đăng nhập</h1>
      <form @submit.prevent="login">
        <div class="mb-4">
          <label for="username" class="block text-gray-600 pb-1"
            >Tên đăng nhập:</label
          >
          <div class="relative ml-2">
            <input
              type="text"
              id="username"
              v-model="Email"
              class="w-full border-b-2 rounded py-2 px-3 pl-10 outline-none text-base"
            />
            <span class="absolute left-3 top-2 text-black z-10">
              <i class="material-icons">person</i>
            </span>
          </div>
        </div>
        <div class="mb-4">
          <label for="password" class="block text-gray-600 pb-1"
            >Mật khẩu:</label
          >
          <div class="relative ml-2">
            <input
              type="password"
              id="password"
              v-model="password"
              class="w-full border rounded py-2 px-3 pl-10 outline-none"
            />
            <span class="absolute left-3 top-2 text-black z-10">
              <i class="material-icons">no_encryption</i>
            </span>
          </div>
        </div>
        <!-- Hiển thị thông báo lỗi -->
        <div class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>
        <div class="text-right mb-4">
          <router-link to="/DangkiComponent" class="text-blue-500"
            >Đăng ký tài khoản</router-link
          >
        </div>
        <div class="text-center">
          <button
            type="submit"
            class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          >
            Đăng nhập
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useApiStore } from "../../Module/LoginModule";
import Cookies from "js-cookie";
export default defineComponent({
  data() {
    return {
      Email: "",
      password: "",
      errorMessage: "",
    };
  },
  methods: {
    async login() {
      // Kiểm tra xem Email và password có được cung cấp không
      if (!this.Email || !this.password) {
        this.errorMessage = "Vui lòng nhập Email và password";
        return;
      }

      const credentials = {
        Email: this.Email,
        password: this.password,
      };

      // Gọi action fetchData từ useApiStore và truyền credentials
      await useApiStore().fetchData(credentials);

      // Xử lý kết quả từ useApiStore
      const apiStore = useApiStore();
      if (apiStore.error) {
        console.error("Lỗi đăng nhập:", apiStore.error);
        this.errorMessage =
          "Đăng nhập không thành công. Vui lòng kiểm tra Email và password.";
      } else {
        // Lưu mã token vào cookie
        const token = apiStore.userData;

        Cookies.set("jwtToken", token, { expires: 1 });
        this.errorMessage = "";
        window.location.href = "/";
      }
    },
  },
});
</script>
