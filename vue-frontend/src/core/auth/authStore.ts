import { defineStore } from 'pinia';
import { ref } from 'vue';
import { config } from '../../config';

interface User {
    id: string;
    email: string;
}

export const useAuthStore = defineStore('auth', () => {
    const user = ref<User | null>(null);
    const loading = ref(true);
    const API_URL = `${config.apiBaseUrl}/auth`;

    const checkAuth = async () => {
        try {
            const response = await fetch(`${API_URL}/me`, {
                headers: { 'Accept': 'application/json' },
                credentials: 'include'
            });
            if (response.ok) {
                user.value = await response.json();
            }
        } catch (error) {
            console.error('Check auth failed', error);
        } finally {
            loading.value = false;
        }
    };

    const login = async (credentials: any) => {
        const response = await fetch(`${API_URL}/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(credentials),
            credentials: 'include'
        });
        if (response.ok) {
            user.value = await response.json();
        } else {
            const errorText = await response.text();
            throw new Error(errorText || 'Login failed');
        }
    };

    const signup = async (credentials: any) => {
        const response = await fetch(`${API_URL}/signup`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(credentials),
            credentials: 'include'
        });
        if (response.ok) {
            user.value = await response.json();
        } else {
            const errorText = await response.text();
            throw new Error(errorText || 'Signup failed');
        }
    };

    const logout = async () => {
        await fetch(`${API_URL}/logout`, {
            method: 'POST',
            credentials: 'include'
        });
        user.value = null;
    };

    return {
        user,
        loading,
        checkAuth,
        login,
        signup,
        logout
    };
});
