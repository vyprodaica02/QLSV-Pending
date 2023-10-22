<template>
  <div v-if="isOpen" class="fixed inset-0 flex items-center justify-center">
    <div
      class="overlay fixed inset-0 bg-gray-900 bg-opacity-50"
      @click="closeForm"
    ></div>
    <form
      @submit.prevent="submitForm"
      class="bg-white p-6 rounded-lg shadow-md grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 z-50"
    >
      <div class="mb-4">
        <label for="HoSinhVien" class="block text-gray-700">Họ Sinh Viên</label>
        <input
          v-model="selectedItem.hoSinhVien"
          type="text"
          id="HoSinhVien"
          name="HoSinhVien"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeHoSinhVien" class="text-red-500">
          Họ Sinh Viên không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="TenSinhVien" class="block text-gray-700"
          >Tên Sinh Viên</label
        >
        <input
          v-model="selectedItem.tenSinhVien"
          type="text"
          id="TenSinhVien"
          name="TenSinhVien"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeTenSinhVien" class="text-red-500">
          Tên Sinh Viên không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="NgaySinh" class="block text-gray-700">Ngày Sinh</label>
        <input
          v-model="selectedItem.ngaySinh"
          type="date"
          id="NgaySinh"
          name="NgaySinh"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeNgaySinh" class="text-red-500">
          Ngày Sinh không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="NoiSinh" class="block text-gray-700">Nơi Sinh</label>
        <input
          v-model="selectedItem.noiSinh"
          type="text"
          id="NoiSinh"
          name="NoiSinh"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeNoiSinh" class="text-red-500">
          Nơi Sinh không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="DiaChi" class="block text-gray-700">Địa Chỉ</label>
        <input
          v-model="selectedItem.diaChi"
          type="text"
          id="DiaChi"
          name="DiaChi"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeDiaChi" class="text-red-500">
          Địa chỉ không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="DienThoai" class="block text-gray-700">Điện Thoại</label>
        <input
          v-model="selectedItem.dienThoai"
          type="text"
          id="DienThoai"
          name="DienThoai"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeDienThoai" class="text-red-500">
          Điện thoại không được để trống.
        </div>
        <div
          v-else-if="
            selectedItem.dienThoai !== null &&
            isNaN(Number(selectedItem.dienThoai))
          "
          class="text-red-500"
        >
          Điện thoại phải là số.
        </div>
        <div
          v-else-if="
            selectedItem.dienThoai !== null &&
            (selectedItem.dienThoai.toString().length < 10 ||
              selectedItem.dienThoai.toString().length > 10)
          "
          class="text-red-500"
        >
          Điện thoại phải có đúng 10 số.
        </div>
      </div>
      <div class="mb-4">
        <label for="HocBong" class="block text-gray-700">Học Bổng</label>
        <input
          v-model="selectedItem.hocBong"
          type="text"
          id="HocBong"
          name="HocBong"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeHocBong" class="text-red-500">
          Học bổng không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="GioiTinh" class="block text-gray-700">Giới Tính</label>
        <input
          v-model="selectedItem.gioiTinh"
          type="text"
          id="GioiTinh"
          name="GioiTinh"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeGioiTinh" class="text-red-500">
          Giới tính không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="Email" class="block text-gray-700">Email</label>
        <input
          v-model="selectedItem.email"
          type="email"
          id="Email"
          name="Email"
          class="bg-gray-200 rounded-md p-2 w-full"
          autocomplete="nope"
        />
        <div v-if="isValidtaeEmail" class="text-red-500">
          Email không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="Password" class="block text-gray-700">Mật Khẩu</label>
        <input
          v-model="selectedItem.password"
          type="password"
          id="Password"
          name="Password"
          class="bg-gray-200 rounded-md p-2 w-full"
          autocomplete="nope"
        />
        <div v-if="isValidtaePassword" class="text-red-500">
          Password không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="avata" class="block text-gray-700">Ảnh Đại Diện</label>
        <input
          type="file"
          id="avata"
          name="avata"
          class="bg-gray-200 rounded-md p-2 w-full"
          @change="handleFileChange"
        />
        <div v-if="isValidtaefile" class="text-red-500">
          avata không được để trống.
        </div>
      </div>
      <div class="mb-4">
        <label for="MaLop" class="block text-gray-700">Mã Lớp</label>
        <input
          v-model="selectedItem.maLop"
          type="number"
          id="MaLop"
          name="MaLop"
          class="bg-gray-200 rounded-md p-2 w-full"
        />
        <div v-if="isValidtaeMaLop" class="text-red-500">
          Mã Lớp không được để trống.
        </div>
      </div>
      <div class="col-span-full text-center">
        <button
          type="submit"
          class="bg-blue-500 text-white rounded-md p-2 w-1/3"
        >
          Đăng ký
        </button>
      </div>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { GetHocSinh } from "../../Module/HocSinh/HocSinh";

export default defineComponent({
  props: {
    isOpen: {
      type: Boolean,
    },
    selectedItem: Object,
  },
  data() {
    return {
      file: null,
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
    formatDate = (dateString) => {
      const date = new Date(dateString);
      return date.toLocaleDateString();
    },

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
      if (this.selectedItem.hoSinhVien !== null) {
        formData.append("HoSinhVien", this.selectedItem.hoSinhVien);
      }
      if (this.selectedItem.tenSinhVien !== null) {
        formData.append("TenSinhVien", this.selectedItem.tenSinhVien);
      }
      if (this.selectedItem.ngaySinh !== null) {
        formData.append("NgaySinh", this.selectedItem.ngaySinh);
      }
      if (this.selectedItem.noiSinh !== null) {
        formData.append("NoiSinh", this.selectedItem.noiSinh);
      }
      if (this.selectedItem.diaChi !== null) {
        formData.append("DiaChi", this.selectedItem.diaChi);
      }
      if (
        this.selectedItem.dienThoai !== null &&
        !isNaN(Number(this.selectedItem.dienThoai))
      ) {
        if (this.selectedItem.dienThoai.length === 10) {
          formData.append("DienThoai", this.selectedItem.dienThoai.toString());
          this.isValidtaeDienThoai = false;
        } else {
          this.isValidtaeDienThoai = true;
          return;
        }
      }
      if (this.selectedItem.hocBong !== null) {
        formData.append("HocBong", this.selectedItem.hocBong);
      }
      if (this.selectedItem.gioiTinh !== null) {
        formData.append("GioiTinh", this.selectedItem.gioiTinh);
      }
      if (this.selectedItem.email !== null) {
        formData.append("Email", this.selectedItem.email);
      }
      if (this.selectedItem.password !== null) {
        formData.append("Password", this.selectedItem.password);
      }
      if (this.selectedItem.maLop !== null) {
        formData.append("MaLop", this.selectedItem.maLop);
      }

      if (this.file !== null && this.file !== undefined) {
        formData.append("file", this.file);
      }

      try {
        await GetHocSinh().suaData(formData);
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
    closeForm() {
      this.$emit("clickOverLay");
    },
  },
});
</script>
