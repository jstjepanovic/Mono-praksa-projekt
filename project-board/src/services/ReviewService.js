import axios from "axios";

const find = (boardGameId, rpp, pageNumber, orderBy, sortOrder, rating, weight) =>{
    return axios.get(`https://localhost:44369/find-review?boardGameId=${boardGameId}&rpp=${rpp}&pageNumber=${pageNumber}&orderBy=${orderBy}&sortOrder=${sortOrder}&rating=${rating}&weight=${weight}`)
}

const get = (reviewId) =>{
    return axios.get(`https://localhost:44369/get-review/${reviewId}`)
}

const countUserReviews = (userId)=>{
    return axios.get(`https://localhost:44369/count-review/${userId}`)
}

const create = (review) =>{
    return axios.post(`https://localhost:44369/create-review`, review)
}

const update = (reviewId, review) =>{
    return axios.put(`https://localhost:44369/update-review/${reviewId}`, review)
}

const remove = (reviewId) =>{
    return axios.delete(`https://localhost:44369/delete-review/${reviewId}`)
}

const ReviewService = { find, get, create, update, remove, countUserReviews }

export default ReviewService