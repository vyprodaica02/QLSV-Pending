<template>
  <V-app>
    <V-main>
      <main>
        <HeaderComponent></HeaderComponent>
        <div class="grid grid-cols-12">
          <div class="col-span-2">
            <NavigationComponent />
          </div>

          <div class="col-span-10">
            <router-view></router-view>
          </div>
        </div>
      </main>
    </V-main>
  </V-app>
</template>

<script lang="ts">
import HeaderComponent from "./components/HeaderComponent.vue";
import NavigationComponent from "./components/NavigationComponent.vue";
export default {
  name: "App",
  components: {
    HeaderComponent,
    NavigationComponent,
  },
};
</script>
