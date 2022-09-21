import axios from "axios";

const getAll = () =>{
    return axios.get(`https://localhost:44369/getall-genre`);
}

const GenreService = { getAll };

export default GenreService