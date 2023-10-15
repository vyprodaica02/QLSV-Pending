import { defineStore } from "pinia";
import axios from "axios";
export const DangKyAPI = defineStore("api", {
  state: () => ({
    dangKyData: null as null | any,
    loading: false,
    error: null as unknown | null,
  }),
  actions: {
    async fetchData(credentials) {
      try {
        this.loading = true;
        const response = await axios.post(
          "https://localhost:7060/api/SinhVien/CreatUser",
          credentials
        );

        if (response.status === 200) {
          this.dangKyData = response.data;
        } else if (response.status === 400) {
          const responseData = response.data;
          if (responseData === "Email đã tồn tại") {
            throw new Error("Email đã tồn tại");
          } else {
            throw new Error("Lỗi");
          }
        } else {
          throw new Error("Lỗi không xác định");
        }
      } catch (error: any) {
        this.error = error.message;
      } finally {
        this.loading = false;
      }
    },
  },
});
