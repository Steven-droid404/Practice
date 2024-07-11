﻿import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

export default defineConfig({
    plugins: [vue()],
    server: {
        port: 5173,
        proxy: {
            '/api': {
                target: 'https://localhost:44300',
                changeOrigin: true,
                secure: false
            }
        }
    }
})