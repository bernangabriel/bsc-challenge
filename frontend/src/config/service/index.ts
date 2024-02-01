import axios from "axios";
import env from "../../config/env";
import {requestInterceptor, responseInterceptor} from "./interceptors";

const client = axios.create({
    baseURL: env.API_URL,
});

client.interceptors.request.use(
    requestInterceptor.onFulfilled,
    requestInterceptor.onRejected,
);
client.interceptors.response.use(
    responseInterceptor.onFulfilled,
    responseInterceptor.onRejected,
);

export default client;
