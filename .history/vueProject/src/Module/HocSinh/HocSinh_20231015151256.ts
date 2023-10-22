import { defineStore } from "pinia";
import axios from "axios";

export const GetHocSinh = defineStore("api", {
  state: () => ({
    dataHocSinh: null as null | any,
    loading: false,
    error: null as unknown | null,
  }),
  actions: {
    async fetchData(userId, pageIndex, pageSize) {
      try {
        this.loading = true;

        // Gửi yêu cầu GET với các tham số
        const response = await axios.get(
          `https://localhost:7060/api/SinhVien/getFilter?userId=${
            userId || ""
          }&pageIndex=${pageIndex || 1}&pageSize=${pageSize || 6}`
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

    async deleteData(id) {
      try {
        this.loading = true;

        // Gửi yêu cầu DELETE với id cụ thể
        const response = await axios.delete(
          `https://localhost:7060/api/SinhVien/Delete/id?id=${id}`
        );

        if (response.status === 200) {
          // Xử lý logic sau khi xóa thành công
          // Ví dụ: làm mới dữ liệu hoặc thực hiện hành động khác
          return true; // hoặc bất kỳ giá trị phù hợp
        }
      } catch (error: any) {
        this.error = error.message;
        return false; // hoặc bất kỳ giá trị phù hợp trong trường hợp lỗi
      } finally {
        this.loading = false;
      }
    },
  },
});
