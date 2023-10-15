import { defineStore } from "pinia";
import axios from "axios";

export const useXoaHocSinh = defineStore("api", {
  state: () => ({
    idData: null as null | number,
    error: null as unknown | null,
  }),
  actions: {
    async fetchXoa(id) {
      try {
        await axios.delete(`https://localhost:7060/api/SinhVien/Delete/${id}`);
        console.log("oke");
      } catch (error) {
        this.error = error;
      }
    },
  },
});

export default useXoaHocSinh;
