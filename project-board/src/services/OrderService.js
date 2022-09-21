import axios from "axios";

const find = (recordsPerPage, pageNumber, orderBy, sortOrder, search) => {
    return axios.get(`https://localhost:44369/find-order?pageNumber=${pageNumber}&recordsPerPage=${recordsPerPage}&orderBy=${orderBy}&sortOrder=${sortOrder}&search=${search}`)
}

const findUserId = (userId) => {
    return axios.get(`https://localhost:44369/find-order?UserId=${userId}`)
}

const get = (orderId) => {
    return axios.get(`https://localhost:44369/get-order?orderId=${orderId}`);
}

const create = (listingId, order) => {
    return axios.post(`https://localhost:44369/create-order/${listingId}`, order);
}

const update = (orderId, order) => {
    return axios.put(`https://localhost:44369/update-order?orderId=${orderId}`, order);
}

const remove = (orderId) => {
    return axios.delete(`https://localhost:44369/delete-order?orderId=${orderId}`);
}

const OrderService = { get, create, update, remove, find, findUserId};

export default OrderService