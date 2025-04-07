import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  base: '/',
  plugins: [react()],
  preview: {
    host: '0.0.0.0',
    port: 4173,
    strictPort: true,
  },
  server: {
    port: 4173,
    strictPort: true,
    host: '0.0.0.0',
  },
});
