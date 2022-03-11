import axios from "axios"
import qs from "qs"

const axiosClient = axios.create({
  baseURL: process.env.REACT_APP_API,
  headers: {
    'content-type': 'application/json',
},
  paramsSerializer: (params) => {
    return qs.stringify(params, { skipNulls: true })
  }
})

axiosClient.interceptors.request.use(async (config) => {
  // Handle token here ...
  const user = localStorage.getItem("user");
  
  if (user) {
      config.headers.Authorization = `Bearer ${JSON.parse(user).access_token}`;
  }
  return config;
})

axiosClient.interceptors.response.use((response) => {
  if (response && response.data) {
      return response.data;
  }
  return response;
}, (error) => {
  // Handle errors
  if(error.response.status === 400){
      alert(error.response.data.message);
  }

  if (error.response.status === 403) {
      alert("access forbidden")
  }
  
  throw error;
});

export default axiosClient