<template>
  <div class="container w-11/12 mx-auto">
    <input
      type="text"
      v-model="searchText"
      placeholder="Tìm kiếm"
      class="w-full px-3 py-2 rounded-md border border-gray-300 focus:border-blue-500 focus:ring focus:ring-blue-200 mb-9 mt-9 shadow-md"
      @keyup.enter="performSearch"
    />
    <VueTable :headers="headers" :data="data">
      <template #td="{ item }">
        <td class="text-center">
          <img
            :src="item.avata"
            alt="Avata"
            style="width: 50px; height: 50px"
            class="mx-auto my-auto"
          />
        </td>
        <td class="text-center">
          <span>{{ item.tenSinhVien }}</span>
        </td>
        <td class="text-center">
          <span>{{ formatDate(item.ngaySinh) }}</span>
        </td>
        <td class="text-center">
          <span>{{ item.maLop }}</span>
        </td>
        <td class="text-center">
          <span>{{ item.gioiTinh }}</span>
        </td>
        <td class="text-center cursor-pointer">
          <i class="material-icons" @click="deleteItem(item)">delete</i>
          <i class="material-icons" @click="edit(item)">edit</i>
        </td>
      </template>
    </VueTable>
    <!-- pagination -->
    <div class="flex justify-center items-center mt-4">
      <button
        @click="goToPage(currentPage - 1)"
        class="px-4 py-2 mr-2 border rounded-md"
      >
        Trang trước
      </button>
      <span
        v-for="page in totalPages"
        :key="page"
        @click="goToPage(page)"
        :class="[
          'px-2',
          'border',
          'border-gray-500',
          'm-1',
          'rounded-md',
          {
            'font-bold border-purple-500 text-purple-500 cursor-pointer':
              currentPage === page,
          },
          { 'cursor-pointer': currentPage !== page },
        ]"
      >
        {{ page }}
      </span>
      <button
        @click="goToPage(currentPage + 1)"
        class="px-4 py-2 ml-2 border rounded-md"
      >
        Trang tiếp theo
      </button>
    </div>
    <SuaThongTinHocSinh :isOpen="open" @clickOverLay="edit" />
  </div>
</template>

<script setup>
import SuaThongTinHocSinh from "./SuaThongTinHocSinh.vue";
import { VueTable } from "@harv46/vue-table";
import "@harv46/vue-table/dist/style.css";
import { ref, onMounted, computed, watch } from "vue";
import { GetHocSinh } from "../../Module/HocSinh/HocSinh.ts";
import axios from "axios";

const headers = [
  "Avata",
  "Tên Sinh Viên",
  "Ngày Sinh",
  "Mã lớp",
  "Giới Tinh",
  "Thao Tác",
];
const searchText = ref("");
const data = ref([]);
const perPage = ref(10);
const currentPage = ref(1);
const totalPages = ref([]);
const idXoa = ref(0);
const selectedItem = ref(null);
// bỏ 00:00:00
const formatDate = (dateString) => {
  const date = new Date(dateString);
  return date.toLocaleDateString();
};

// pagination
const goToPage = (page) => {
  if (page === "a" && currentPage.value > 1) {
    currentPage.value--;
  } else if (page === "b" && currentPage.value < totalPages.value) {
    currentPage.value++;
  } else if (
    typeof page === "number" &&
    page >= 1 &&
    page <= totalPages.value
  ) {
    currentPage.value = page;
  }
};
// search
const performSearch = () => {
  return searchText.value;
};

//xóa

const deleteItem = (item) => {
  idXoa.value = item.id;
  Xoa(idXoa.value);
};

const Xoa = async (id) => {
  await GetHocSinh().deleteData(id);
};
//sửa
let open = ref(false);
const edit = (item) => {
  open.value = !open.value;
  selectedItem.value = response.data;
};
// data
const fetchData = async () => {
  const fetchedData = await GetHocSinh().fetchData(
    searchText.value,
    currentPage.value,
    6
  );
  data.value = fetchedData.data;
  totalPages.value = fetchedData.totalPages;
};

onMounted(async () => {
  await fetchData();
});

watch(currentPage, async (newVal) => {
  await fetchData();
});

watch(performSearch, async (newVal) => {
  await fetchData();
});
</script>

<style>
th.px-6.py-3 {
  text-align: center;
}
</style>
