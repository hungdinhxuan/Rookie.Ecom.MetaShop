import axios from "axios";
import qs from "qs";
import userManager from "src/utils/userManager";

const axiosClient = axios.create({
  baseURL: process.env.REACT_APP_API,
  headers: {
    "content-type": "application/json",
  },
  paramsSerializer: (params) => {
    return qs.stringify(params, { skipNulls: true });
  },
});

axiosClient.interceptors.request.use(async (config) => {
  // Handle token here ...
  const user = localStorage.getItem("user");

  if (user) {
    config.headers.Authorization = `Bearer ${JSON.parse(user).access_token}`;
  }
  return config;
});

axiosClient.interceptors.response.use(
  (response) => {
    if (response && response.data) {
      return response.data;
    }
    return response;
  },
  async (error) => {
    // Handle errors
    if (error.response.status === 400) {
      alert(error.response.data.message);
    }

    if (error.response.status === 403) {
      window.location.href = "/403";
    }

    if (error.response.status === 401) {
      const user = await userManager.getUser();
      localStorage.removeItem("user");
      userManager.signoutRedirect({ id_token_hint: user.id_token });
      userManager.removeUser();
    }

    throw error;
  }
);

export default axiosClient;
