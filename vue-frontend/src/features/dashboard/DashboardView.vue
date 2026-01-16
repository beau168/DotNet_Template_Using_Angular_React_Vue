<script setup lang="ts">
import { useRouter } from 'vue-router';
import { useAuthStore } from '../../core/auth/authStore';
import { LogOut, User, LayoutDashboard, Settings, Bell, Search } from 'lucide-vue-next';

const authStore = useAuthStore();
const router = useRouter();

const handleLogout = async () => {
  await authStore.logout();
  router.push('/login');
};

const stats = [
  { label: 'Total Users', value: '2,543', trend: '+12.5%', icon: User },
  { label: 'Active Sessions', value: '432', trend: '+5.2%', icon: LayoutDashboard },
  { label: 'API Health', value: '100%', trend: 'Stable', icon: Settings },
];
</script>

<template>
  <div class="dashboard-container">
    <nav class="nav">
      <div style="display: flex; align-items: center; gap: 2rem">
        <h2 style="color: var(--primary)">Platform</h2>
        <div style="display: flex; gap: 1.5rem; color: var(--text-muted); font-size: 0.875rem">
          <span style="color: var(--text); cursor: pointer">Overview</span>
          <span style="cursor: pointer">Analytics</span>
          <span style="cursor: pointer">Reports</span>
        </div>
      </div>
      
      <div style="display: flex; align-items: center; gap: 1.5rem">
        <Search :size="20" class="text-muted" style="cursor: pointer" />
        <Bell :size="20" class="text-muted" style="cursor: pointer" />
        <div class="user-badge" v-if="authStore.user">
          <div class="avatar">
            {{ authStore.user.email[0].toUpperCase() }}
          </div>
          <span style="font-size: 0.875rem">{{ authStore.user.email }}</span>
          <button 
            @click="handleLogout" 
            style="background: transparent; border: none; color: var(--text-muted); cursor: pointer; display: flex"
            title="Logout"
          >
            <LogOut :size="18" />
          </button>
        </div>
      </div>
    </nav>

    <main>
      <div style="margin-bottom: 2rem">
        <h1 style="font-size: 2rem; font-weight: 700; margin-bottom: 0.5rem">Dashboard Overview</h1>
        <p style="color: var(--text-muted)">Welcome back, {{ authStore.user?.email }}! Here's what's happening today.</p>
      </div>

      <div style="display: grid; grid-template-columns: repeat(auto-fit, minmax(240px, 1fr)); gap: 1.5rem">
        <div v-for="stat in stats" :key="stat.label" class="auth-card" style="padding: 1.5rem; max-width: none">
          <div style="display: flex; justify-content: space-between; margin-bottom: 1rem">
            <span style="color: var(--text-muted); font-size: 0.875rem">{{ stat.label }}</span>
            <component :is="stat.icon" :size="20" color="var(--primary)" />
          </div>
          <div style="font-size: 1.5rem; font-weight: 700">{{ stat.value }}</div>
          <div style="font-size: 0.75rem; color: var(--success); margin-top: 0.5rem">{{ stat.trend }}</div>
        </div>
      </div>
    </main>
  </div>
</template>
