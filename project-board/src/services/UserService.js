import axios from "axios";
const find = (username) =>{
    return axios.get(`https://localhost:44369/find-user?nameSearch=${username}`);
}
const getAll = (username) =>{
    return axios.get(`https://localhost:44369/find-user`);
}
const get = (userId) =>{
    return axios.get(`https://localhost:44369/get-user/${userId}`);
}
const create = (user) =>{
    return axios.post(`https://localhost:44369/create-user`, user);
}
const update = (userId, user) =>{
    return axios.put(`https://localhost:44369/update-user/${userId}`, user);
}
const remove = (userId) =>{
    return axios.delete(`https://localhost:44369/delete-user/${userId}`);
}
const UserService = { find, get, create, update, remove, getAll }
export default UserService
