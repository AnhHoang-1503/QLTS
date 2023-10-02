import { createRouter, createWebHistory } from "vue-router";
import AssetsManagement from "@/views/AssetsManagement/AssetsManagement.vue";
import Overview from "@/views/Overview/Overview.vue";
import Home from "@/views/Home/Home.vue";
import AssetsManagementIncrease from "@/views/AssetsManagementIncrease/AssetsManagementIncrease.vue";
import Login from "@/views/Login/Login.vue";

const routes = [
    {
        path: "/overview",
        name: "Overview",
        component: Overview,
        meta: { requiresAuth: true },
    },
    {
        path: "/assets-management/increase",
        name: "AssetsManagementIncrease",
        component: AssetsManagementIncrease,
        meta: { requiresAuth: true },
    },
    {
        path: "/assets-management",
        name: "AssetsManagement",
        component: AssetsManagement,
        meta: { requiresAuth: true },
    },
    {
        path: "/login",
        name: "Login",
        component: Login,
    },
    {
        path: "/",
        name: "Home",
        component: Home,
        meta: { requiresAuth: true },
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
});

router.beforeEach(async (to, from, next) => {
    // Chưa đăng nhập thì chuyển về màn đăng nhập
    const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);
    // const loggedIn = await loginApi.loggedIn();
    const loggedIn = !!localStorage.getItem("token");
    if (requiresAuth && !loggedIn) {
        next({ name: "Login" });
    } else {
        /// Nếu đã đăng nhập thì không truy cập vào login được nữa
        if (loggedIn && to.name == "Login") {
            next({ name: "AssetsManagement" });
        } else {
            next();
        }
    }
});

export default router;
