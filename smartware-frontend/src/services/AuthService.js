import API from "./api";

const login = async(email, password) => {
    return API.post("/api/Auth/login", { email, password });
};

const registerUser = async(userData) => {
    return API.post("/api/Auth/register", userData);
};

export default { login, registerUser };
