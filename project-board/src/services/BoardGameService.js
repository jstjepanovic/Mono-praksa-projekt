import axios from "axios";

const find = (rpp, pageNumber, orderBy, sortOrder, name, minPlayers, maxPlayers, age, rating, weight, publisher) => {
    return axios.get(`https://localhost:44369/find-boardgame?rpp=${rpp}&pageNumber=${pageNumber}&orderBy=${orderBy}&sortOrder=${sortOrder}&name=${name}&minPlayers=${minPlayers}&maxPlayers=${maxPlayers}&age=${age}&rating=${rating}&weight=${weight}&publisher=${publisher}`)
}

const get = (boardGameId) => {
    return axios.get(`https://localhost:44369/get-boardgame/${boardGameId}`);
}

const getall = () => {
    return axios.get(`https://localhost:44369/find-boardgame`);
}

const create = (boardGame) => {
    return axios.post(`https://localhost:44369/create-boardgame`, boardGame);
}

const update = (boardGameId, boardGame) => {
    return axios.put(`https://localhost:44369/update-boardgame/${boardGameId}`, boardGame);
}

const remove = (boardGameId) => {
    return axios.delete(`https://localhost:44369/delete-boardgame/${boardGameId}`);
}

const count = () =>{
    return axios.get("https://localhost:44369/count-boardgame");
}

const BoardGameService = { get, create, update, remove, find, count, getall };

export default BoardGameService