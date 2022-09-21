import React, { useState, useEffect } from 'react'
import { Stack, Button, Form, FormGroup, FormLabel } from "react-bootstrap"
import ListingService from '../../services/ListingService';
import BoardGameService from '../../services/BoardGameService'
import UserService from '../../services/UserService'
import { useNavigate } from 'react-router-dom';




function ListingCreate() {
    const [listing, setListing] = useState({});
    const [isValid, setIsValid] = useState(true);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [games, setGames] = useState([]);

    const navigate = useNavigate();

    useEffect(() => {
        getGames();
    }, []);

    const getGames = () =>{
        return BoardGameService.getall().then(response =>{
            setGames(response.data);
        })
    }

    const handleInputChange = (e) => {
        setListing({ ...listing, [e.target.name]: e.target.value });
    };

    const handleUsername = (e) =>{
        setUsername(e.target.value);
    }
    
    const handlePassword = (e) =>{
        setPassword(e.target.value);
    }

    const addListing = async () =>{
        let user = {}
        await UserService.find(username).then(response=>{
          user = response.data[0]
        })
    
        if (user.Password !== password){
          setIsValid(false)
          return;
        }
    
        let list = { Price : listing.Price, Condition : listing.Condition, UserId : user.UserId, BoardGameId:listing.BoardGameId}
        ListingService.create(list)
        navigate(0)
    }

    const resetSubmit = () =>{
        setPassword();
        setUsername();
        setIsValid(true);
    }

  return (
    <>
    {isValid ? (
        <div>
        <Stack>
            <Form>
                <Form.Label>Price</Form.Label>
                <Form.Control required={true} type="number" placeholder="Enter Price" name="Price" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <Form.Label>Condition</Form.Label>
                <Form.Control required={true} type="text" placeholder="Specify Condition" name="Condition" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <Form.Label>Username</Form.Label>
                <Form.Control required={true} type="text" placeholder="Enter Username" name="Username" value={username || ""} onChange={handleUsername}/> 
            </Form>
            <Form>
                <Form.Label>Password</Form.Label>
                <Form.Control required={true} type="password" placeholder="Enter Password" name="Password" value={password || ""} onChange={handlePassword}/> 
            </Form>
            <Form.Label>Category</Form.Label>
            <Form.Select onChange={handleInputChange} defaultValue={'DEFAULT'} name="BoardGameId">
                <option value="DEFAULT" disabled={true}>-- Select game --</option>
            {
                games.map((game) => (<option key={game.BoardGameId} value={game.BoardGameId}>{game.Name}</option>))
            }
            </Form.Select>
            <Button variant="outline-dark" onClick={addListing}>Add</Button>
        </Stack>
    </div>
        
    ) : (
        <div>
            <Stack>
                <h3>Invalid Username or password</h3>
                <Button variant="outline-dark" onClick={resetSubmit}>Try again</Button>
            </Stack>
        </div>
    )}
    </>
  )
}

export default ListingCreate