import React, { useState } from 'react'
import ReviewService from '../../services/ReviewService'
import UserService from '../../services/UserService'
import { Stack, Button, Form } from "react-bootstrap"
import { useNavigate } from 'react-router-dom';

function ReviewCreate(props) {
  const [review, setReview] = useState({});
  const [isValid, setIsValid] = useState(true);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const handleInputChange = (e) => {
    setReview({ ...review, [e.target.name]: e.target.value });
  };

  const handleUsername = (e) =>{
    setUsername(e.target.value);
  }

  const handlePassword = (e) =>{
    setPassword(e.target.value);
  }

  const addReview = async () =>{
    let user = {}
    await UserService.find(username).then(response=>{
      user = response.data[0]
    })

    if (user.Password !== password){
      setIsValid(false)
      return;
    }

    let rev = { UserId : user.UserId, BoardGameId : props.boardGameId,  Rating : review.Rating, Weight : review.Weight, ReviewText : review.ReviewText }
    ReviewService.create(rev)
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
                <label for="RevUsername">Username</label>
                  <Form.Control id="RevUsername" required={true} type="text" placeholder="Enter Username" name="Username" value={username || ""} onChange={handleUsername}/> 
              </Form>
              <Form>
                <label for="RevPassword">Password</label>
                  <Form.Control id="RevPassword" required={true} type="password" placeholder="Enter Password" name="Password" value={password || ""} onChange={handlePassword}/> 
              </Form>
              <Form>
                <label for="RevRating">Rating</label>
                  <Form.Control id="RevRating" required={true} type="number" placeholder="Enter Rating" name="Rating" onChange={handleInputChange}/> 
              </Form>
              <Form>
                <label for="RevWeight">Weight</label>
                  <Form.Control id="RevWeight" required={true} type="number" placeholder="Enter Weight" name="Weight" onChange={handleInputChange}/> 
              </Form>
              <Form>
                <label for="RevText">ReviewText</label>
                  <Form.Control id="RevText" type="text" placeholder="Enter Review Text" name="ReviewText" onChange={handleInputChange}/> 
              </Form>
          </Stack>
          <Button variant="outline-dark" onClick={addReview}>Submit</Button>
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

export default ReviewCreate