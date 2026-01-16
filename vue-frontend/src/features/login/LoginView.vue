<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '../../core/auth/authStore';
import { Github, Mail } from 'lucide-vue-next';

const email = ref('');
const password = ref('');
const error = ref('');
const loading = ref(false);

const authStore = useAuthStore();
const router = useRouter();

const handleSubmit = async () => {
  error.value = '';
  loading.value = true;
  try {
    await authStore.login({ email: email.value, password: password.value });
    router.push('/dashboard');
  } catch (err: any) {
    error.value = err.message || 'Login failed. Please check your credentials.';
  } finally {
    loading.value = false;
  }
};
</script>

<template>
  <div class="auth-container">
    <div class="auth-card">
      <div class="auth-header">
        <h1>Welcome Back</h1>
        <p>Login to your account to continue</p>
      </div>

      <div v-if="error" class="error-message">{{ error }}</div>

      <form @submit.prevent="handleSubmit">
        <div class="form-group">
          <label for="email">Email Address</label>
          <input
            id="email"
            v-model="email"
            type="email"
            placeholder="name@company.com"
            required
          />
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <input
            id="password"
            v-model="password"
            type="password"
            placeholder="••••••••"
            required
          />
        </div>
        <button type="submit" className="btn btn-primary" :disabled="loading">
          {{ loading ? 'Logging in...' : 'Login' }}
        </button>
      </form>

      <div style="margin: 1.5rem 0; display: flex; align-items: center; gap: 1rem; color: var(--text-muted); font-size: 0.875rem">
        <div style="flex: 1; height: 1px; background: var(--border)"></div>
        OR
        <div style="flex: 1; height: 1px; background: var(--border)"></div>
      </div>

      <button class="btn btn-outline">
        <Github :size="20" />
        Continue with Github
      </button>
      <button class="btn btn-outline" style="margin-top: 0.75rem">
        <Mail :size="20" />
        Continue with Google
      </button>

      <div class="auth-footer">
        Don't have an account? <router-link to="/signup">Sign up for free</router-link>
      </div>
    </div>
  </div>
</template>
