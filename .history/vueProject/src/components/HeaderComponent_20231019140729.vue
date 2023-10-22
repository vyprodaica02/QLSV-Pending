<template>
  <div class="">
    <header class="bg-blue-500 p-4 w-full z-50 flex justify-between px-12">
      <div class="">
        <div class="text-white text-lg font-bold">Logo hoặc Tiêu đề</div>
      </div>
      <div class="flex gap-3">
        <div>avata</div>
        <div>dang xuat</div>
      </div>
    </header>
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
