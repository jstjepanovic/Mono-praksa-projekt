import axios from "axios";

const find = (price, condition, timeCreated, pageNumber, recordsPerPage, orderBy, sortOrder) => {
    return axios.get(`https://localhost:44369/find-listing?price=${price}&condition=${condition}&timeCreated=${timeCreated}&pageNumber=${pageNumber}&recordsPerPage=${recordsPerPage}&orderBy=${orderBy}&sortOrder=${sortOrder}`)
}

const getSpecific = (userId) => {
    return axios.get(`https://localhost:44369/find-listing?userId=${userId}`);
}

const get = (listingId) => {
    return axios.get(`https://localhost:44369/get-listing?listingId=${listingId}`);
}

const create = (listing) => {
    return axios.post(`https://localhost:44369/create-listing`, listing);
}

const update = (listingId, listing) => {
    return axios.put(`https://localhost:44369/update-listing?listingId=${listingId}`, listing);
}

const remove = (listingId) => {
    return axios.delete(`https://localhost:44369/delete-listing?listingId=${listingId}`);
}

const ListingService = { get, create, update, remove, find , getSpecific};

export default ListingService