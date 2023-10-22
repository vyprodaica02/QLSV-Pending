<template>
  <div class="fixed inset-0 flex items-center justify-center">
    <div class="overlay fixed inset-0 bg-gray-900 bg-opacity-50"></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { DangKyAPI } from "../../Module/DangkyModule/DangkyModule";

export default defineComponent({
  data() {
    return {
      HoSinhVien: null as null | string,
      TenSinhVien: null as null | string,
      NgaySinh: null as null | any,
      NoiSinh: null as null | string,
      DiaChi: null as null | string,
      DienThoai: null as null | number,
      HocBong: null as null | string,
      GioiTinh: null as null | string,
      Email: null as null | string,
      Password: null as null | string,
      file: null,
      MaLop: null as null | string,
      isValidtaeHoSinhVien: false,
      isValidtaeTenSinhVien: false,
      isValidtaeNgaySinh: false,
      isValidtaeNoiSinh: false,
      isValidtaeDiaChi: false,
      isValidtaeDienThoai: false,
      isValidtaeHocBong: false,
      isValidtaeGioiTinh: false,
      isValidtaeEmail: false,
      isValidtaePassword: false,
      isValidtaefile: false,
      isValidtaeMaLop: false,
    };
  },
  methods: {
    async submitForm() {
      const fieldsToValidate = [
        "HoSinhVien",
        "TenSinhVien",
        "NgaySinh",
        "NoiSinh",
        "DiaChi",
        "DienThoai",
        "HocBong",
        "GioiTinh",
        "Email",
        "file",
        "MaLop",
        "Password",
      ];
      for (const field of fieldsToValidate) {
        if (this[field] === null) {
          this[`isValidtae${field}`] = true;
          return;
        } else {
          this[`isValidtae${field}`] = false;
        }
      }

      const formData = new FormData();
      if (this.HoSinhVien !== null) {
        formData.append("HoSinhVien", this.HoSinhVien);
      }
      if (this.TenSinhVien !== null) {
        formData.append("TenSinhVien", this.TenSinhVien);
      }
      if (this.NgaySinh !== null) {
        formData.append("NgaySinh", this.NgaySinh);
      }
      if (this.NoiSinh !== null) {
        formData.append("NoiSinh", this.NoiSinh);
      }
      if (this.DiaChi !== null) {
        formData.append("DiaChi", this.DiaChi);
      }
      if (this.DienThoai !== null && !isNaN(Number(this.DienThoai))) {
        if (this.DienThoai.length === 10) {
          formData.append("DienThoai", this.DienThoai.toString());
          this.isValidtaeDienThoai = false;
        } else {
          this.isValidtaeDienThoai = true;
          return;
        }
      }
      if (this.HocBong !== null) {
        formData.append("HocBong", this.HocBong);
      }
      if (this.GioiTinh !== null) {
        formData.append("GioiTinh", this.GioiTinh);
      }
      if (this.Email !== null) {
        formData.append("Email", this.Email);
      }
      if (this.Password !== null) {
        formData.append("Password", this.Password);
      }
      if (this.MaLop !== null) {
        formData.append("MaLop", this.MaLop);
      }

      if (this.file !== null && this.file !== undefined) {
        formData.append("file", this.file);
      }

      try {
        await DangKyAPI().fetchData(formData);
        window.location.href = "/LoginComponent";
      } catch (error) {
        console.error("Lỗi khi gửi biểu mẫu:", error);
      }
    },
    handleFileChange(event) {
      // Xử lý sự kiện thay đổi trường input file
      const selectedFile = event.target.files[0];
      if (selectedFile) {
        this.file = selectedFile;
      } else {
        this.file = null;
      }
    },
  },
});
</script>
