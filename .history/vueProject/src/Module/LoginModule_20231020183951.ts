import { defineStore } from "pinia";
import axios from "axios";

export const useApiStore = defineStore("api", {
  state: () => ({
    userData: null as null | any,
    error: null as unknown | null,
    role: null as null | string, // Lưu trữ vai trò của người dùng
  }),
  actions: {
    async fetchData(credentials) {
      try {
        const response = await axios.post(
          "https://localhost:7060/api/Login/DangNhap",
          credentials
        );
        this.userData = response.data;

        // Lưu trữ thông tin vai trò của người dùng
        this.role = response.data.role;
      } catch (error) {
        this.error = error;
      } finally {
      }
    },
  },
});
