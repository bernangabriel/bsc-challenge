import * as Constants from "../../utils/Constants";

// utils
import Storage from "../../utils/Storage";

const urlToByPassAuth = ["api/account/signin"];

export const requestInterceptor = {
  onFulfilled: async (config: any) => {
    const token = await Storage.get(Constants.storageKeys.ACCESS_TOKEN_KEY);
    const allowToByPassToken = urlToByPassAuth.includes(config.url || "");
    const headersForAuth = {
      headers: {
        ...config.headers,
        Authorization: `Bearer ${token}`,
      },
    };
    return {
      ...config,
      ...(token && !allowToByPassToken ? headersForAuth : {}),
    };
  },
  onRejected: (error: any) => {
    return Promise.reject(error);
  },
};

export const responseInterceptor = {
  onFulfilled: (response: any) => {
    return response;
  },
  onRejected: async (error: any) => {
    if (error.response && error.response.status === 401) {
      localStorage.clear();
      window.location.href = "/login";
    }
    return Promise.reject(error);
  },
};

export default {
  requestInterceptor,
  responseInterceptor,
};
