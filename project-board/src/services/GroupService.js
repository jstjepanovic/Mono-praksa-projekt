import axios from 'axios';

const find = (rpp, pageNumber, orderBy, sortOrder) => {
    return axios.get(`https://localhost:44369/find-groups?rpp=${rpp}&pageNumber=${pageNumber}&orderBy=${orderBy}&sortOrder=${sortOrder}`);
}

const get = (GroupId) => {
    return axios.get(`https://localhost:44369/get-group/${GroupId}`);
}

const create = (Group) => {
    return axios.post(`https://localhost:44369/create-group`, Group);
}

const update = (GroupId, Group) => {
    return axios.put(`https://localhost:44369/update-group/${GroupId}`,Group);
}

const remove = (GroupId) => {
    return axios.delete(`https://localhost:44369/delete-group/${GroupId}`);
}

const GroupService = {find, get, create, update, remove};
export default GroupService;
