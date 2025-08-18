import axios from "axios";

const API = axios.create({
    baseURL: "https://localhost:5162",
});

export default API;