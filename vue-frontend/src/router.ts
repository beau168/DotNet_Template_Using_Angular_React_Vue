import { createRouter, createWebHistory } from 'vue-router';
import { useAuthStore } from './core/auth/authStore';
import LoginView from './features/login/LoginView.vue';
import SignupView from './features/signup/SignupView.vue';
import DashboardView from './features/dashboard/DashboardView.vue';

const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: '/login', component: LoginView },
        { path: '/signup', component: SignupView },
        {
            path: '/dashboard',
            component: DashboardView,
            meta: { requiresAuth: true }
        },
        { path: '/', redirect: '/dashboard' }
    ]
});

router.beforeEach(async (to, _from, next) => {
    const authStore = useAuthStore();

    if (authStore.loading) {
        await authStore.checkAuth();
    }

    if (to.meta.requiresAuth && !authStore.user) {
        next('/login');
    } else {
        next();
    }
});

export default router;
