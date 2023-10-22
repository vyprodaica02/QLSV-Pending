<template>
  <div class="">
    <!-- Header -->
    <header class="bg-blue-500 p-4 w-full z-50 flex justify-between px-12">
      <div class="">
        <!-- <button @click="toggleDrawer" class="text-white">
          <i class="material-icons">menu</i>
        </button> -->
        <div class="text-white text-lg font-bold">Logo hoặc Tiêu đề</div>
      </div>
      <div class="flex gap-3">
        <div>avata</div>
        <div>dang xuat</div>
      </div>
    </header>
    <!-- <div
      :class="{
        'translate-x-0': drawerOpen,
        '-translate-x-full': !drawerOpen,
      }"
      class="block fixed h-full w-2/12 bg-white transition-transform duration-300 ease-in-out z-50 shadow-md"
    >
      <nav class="p-4">
        <a href="#" class="block mb-4">Trang Chủ</a>
        <a href="#" class="block mb-4">Sản Phẩm</a>
        <a href="#" class="block mb-4">Dịch Vụ</a>
        <a href="#" class="block mb-4">Liên Hệ</a>
      </nav>
    </div> -->
  </div>
</template>

<script>
export default {
  name: "Header",
  data() {
    return {
      drawerOpen: false,
    };
  },
  methods: {
    toggleDrawer() {
      this.drawerOpen = !this.drawerOpen;
    },
  },
};
</script>
