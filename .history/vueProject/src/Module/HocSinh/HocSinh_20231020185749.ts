import { defineStore } from "pinia";
import axios from "axios";
import Cookies from "js-cookie";
export const GetHocSinh = defineStore("api", {
  state: () => ({
    dataHocSinh: null as null | any,
    loading: false,
    error: null as unknown | null,
  }),
  actions: {
    async fetchData(userId, pageIndex, pageSize, token) {
      // Thêm tham số token vào hàm fetchData
      try {
        this.loading = true;

        // Gửi yêu cầu GET với các tham số và token
        const response = await axios.get(
          `https://localhost:7060/api/SinhVien/getFilter?userId=${
            userId || ""
          }&pageIndex=${pageIndex || 1}&pageSize=${pageSize || 6}`,
          {
            headers: {
              Authorization: `Bearer ${token}`, // Gửi token trong tiêu đề yêu cầu
            },
          }
        );

        if (response.status === 200) {
          this.dataHocSinh = response.data;
          return this.dataHocSinh;
        }
      } catch (error: any) {
        this.error = error.message;
      } finally {
        this.loading = false;
      }
    },

    async deleteData(id, token) {
      // Thêm tham số token vào hàm deleteData
      try {
        this.loading = true;
        const response = await axios.delete(
          `https://localhost:7060/api/SinhVien/Delete/id?id=${id}`,
          {
            headers: {
              Authorization: `Bearer ${token}`, // Gửi token trong tiêu đề yêu cầu
            },
          }
        );

        if (response.status === 200) {
          return true;
        }
      } catch (error: any) {
        this.error = error.message;
        return false;
      } finally {
        this.loading = false;
      }
    },

    async suaData(credentials, token) {
      // Thêm tham số token vào hàm suaData
      try {
        const response = await axios.put(
          "https://localhost:7060/api/SinhVien/UpdateUser",
          credentials,
          {
            headers: {
              Authorization: `Bearer ${token}`, // Gửi token trong tiêu đề yêu cầu
            },
          }
        );
        this.userData = response.data;
      } catch (error) {
        this.error = error;
      } finally {
      }
    },
  },
});
