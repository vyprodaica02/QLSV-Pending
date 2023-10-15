import { defineStore } from "pinia";
import axios from "axios";

export const useApiStore = defineStore("api", {
  state: () => ({
    userData: null as null | any,
    error: null as unknown | null,
  }),
  actions: {
    async fetchData(credentials) {
      try {
        const response = await axios.post(
          "https://localhost:7060/api/Login/DangNhap",
          credentials
        );
        this.userData = response.data;
      } catch (error) {
        this.error = error;
      } finally {
      }
    },
  },
});
