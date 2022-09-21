import React, {useState, useEffect} from 'react'
import GenreService from '../../services/GenreService';
import BoardGameService from '../../services/BoardGameService';
import { Stack, Button, Form } from "react-bootstrap";

function BoardGameEdit(props) {
    const [boardGame, setBoardGame] = useState({});
    const [genres, setGenres] = useState([]);

    useEffect(() => {
        getGenres();
    }, []);

    const handleInputChange = (e) => {
        setBoardGame({ ...boardGame, [e.target.name]: e.target.value });
        console.log(boardGame)
    };

    const editBoardGame = () => {
        BoardGameService.update(props.boardGameId, boardGame);
    }

    const getGenres = () =>{
        return GenreService.getAll().then(response =>{
            setGenres(response.data);
        })
    }

  return (
    <div>
       <Stack>
            <Form>
                <label htmlFor="Name">Name</label>
                <Form.Control id="Name" required={true} type="text" placeholder="Enter Name" name="Name" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <label htmlFor="MinPlayers">Max Players</label>
                <Form.Control id="MinPlayers" required={true} type="number" placeholder="Minimal number of players required" name="NoPlayersMin" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <label htmlFor="MaxPlayers">Min Players</label>
                <Form.Control id="MaxPlayers" required={true} type="number" placeholder="Maximal number of players" name="NoPlayersMax" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <label htmlFor="Age">Age</label>
                <Form.Control id="Age" required={true} type="number" placeholder="Age recommendation" name="Age" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <label htmlFor="Time">Average Playing Time</label>
                <Form.Control id="Time" required={true} type="number" placeholder="Average playing time in minutes" name="AvgPlayingTime" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <label htmlFor="Publisher">Publisher</label>
                <Form.Control id="Publisher" required={true} type="text" placeholder="Enter Publisher" name="Publisher" onChange={handleInputChange}/> 
            </Form>
            <Form.Select onChange={handleInputChange} defaultValue={'DEFAULT'} name="GenreId">
                <option value="DEFAULT" disabled={true}>-- Select genre --</option>
            {
                genres.map((genre) => (<option key={genre.GenreId} value={genre.GenreId}>{genre.Name}</option>))
            }
            </Form.Select>
        <Button variant="outline-dark" onClick={editBoardGame}>Confirm</Button>
        </Stack>
    </div>
  )
}

export default BoardGameEdit