import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: '/api',
  withCredentials: true, 
});

axiosInstance.interceptors.response.use(
  response => response,
  async error => {
    const originalRequest = error.config;
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const url = 'https://localhost:7267/auth/refresh-token';
        await axios.post(url, {}, { withCredentials: true });
        return axiosInstance(originalRequest); 
      } catch {
return Promise.reject({ ...error, redirectToLogin: true });
      }
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
