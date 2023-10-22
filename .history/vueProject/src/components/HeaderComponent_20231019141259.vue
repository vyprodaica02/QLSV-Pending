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
      myCookieValue: null, // Khởi tạo giá trị ban đầu của cookie là null
    };
  },
  mounted() {
    this.myCookieValue = this.getCookieValue("jwtToken"); // Gọi hàm để lấy giá trị cookie khi component được mounted
  },
  methods: {
    getCookieValue(cookieName) {
      const name = cookieName + "=";
      const decodedCookie = decodeURIComponent(document.cookie);
      const cookieArray = decodedCookie.split(";");
      for (let i = 0; i < cookieArray.length; i++) {
        let cookie = cookieArray[i];
        while (cookie.charAt(0) === " ") {
          cookie = cookie.substring(1);
        }
        if (cookie.indexOf(name) === 0) {
          return cookie.substring(name.length, cookie.length);
        }
      }
      return ""; // Trả về chuỗi rỗng nếu không tìm thấy cookie
    },
  },
};
</script>
