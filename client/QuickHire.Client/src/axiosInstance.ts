import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: '/api',
  withCredentials: true,
});

axiosInstance.interceptors.request.use(config => {
  config.headers['Cache-Control'] = 'no-cache';
  config.headers['Pragma'] = 'no-cache';
  config.headers['Expires'] = '0';

  if (config.params) {
    config.params._ = new Date().getTime(); 
  } else {
    config.params = { _: new Date().getTime() };
  }

  return config;
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
