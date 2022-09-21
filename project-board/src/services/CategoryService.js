import axios from "axios";

const get = ( categoryId) =>{
    return axios.get(`https://localhost:44369/get-category?categoryId=${categoryId}`);
}

const CategoryService = { get };

export default CategoryService;
