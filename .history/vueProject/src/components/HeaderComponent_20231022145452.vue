<template>
  <div class="">
    <header class="bg-blue-500 p-4 w-full z-50 flex justify-between px-12">
      <div class="">
        <div class="text-white text-lg font-bold">Logo hoặc Tiêu đề</div>
      </div>
      <div class="flex gap-3">
        <div v-if="decodedToken">
          <img
            :src="`${decodedToken.avata}`"
            alt=""
            class="rounded-full h-10 w-10 object-cover"
          />
        </div>
        <button @click="logout" class="font-bold text-white">Đăng Xuất</button>
      </div>
    </header>
  </div>
</template>

<script>
export default {
  name: "Header",
  data() {
    return {
      jwtToken: null, // Khởi tạo giá trị ban đầu của jwt token là null
      decodedToken: null, // Khởi tạo giá trị ban đầu của giải mã jwt token là null
    };
  },
  mounted() {
    this.jwtToken = this.getCookieValue("jwtToken"); // Gọi hàm để lấy giá trị jwt token từ cookie khi component được mounted
    this.decodedToken = this.decodeJwt(this.jwtToken); // Giải mã jwt token khi đã lấy được
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
    decodeJwt(jwtToken) {
      const base64Url = jwtToken.split(".")[1];
      const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      const decodedData = decodeURIComponent(
        atob(base64)
          .split("")
          .map(function (c) {
            return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join("")
      );
      return JSON.parse(decodedData);
    },
    logout() {
      document.cookie =
        "jwtToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    },
  },
};
</script>
