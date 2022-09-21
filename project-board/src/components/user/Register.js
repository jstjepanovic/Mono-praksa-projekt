import React, { useState } from 'react'
import UserService from '../../services/UserService'
import { Stack, Button, Form } from "react-bootstrap"
import { useNavigate } from 'react-router-dom';

function Register() {
    const [user, setUser] = useState({});

    const navigate = useNavigate();

    const handleInputChange = (e) => {
        setUser({ ...user, [e.target.name]: e.target.value });
    };

    const addUser = () =>{
        UserService.create(user)
        navigate("/");
    }

  return (
    <div>
        <Stack>
            <Form>
                <label htmlFor="Username">Username</label>
                <Form.Control id="Username" required={true} type="text" placeholder="Enter Username" name="Username" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <label htmlFor="Email">Email</label>
                <Form.Control id="Email" required={true} type="text" placeholder="Enter Email" name="Email" onChange={handleInputChange}/> 
            </Form>
            <Form>
                <label htmlFor="Password">Password</label>
                <Form.Control id="Password" required={true} type="password" placeholder="Enter Password" name="Password" onChange={handleInputChange}/> 
            </Form>
            <Button variant="outline-dark" onClick={addUser}>Register</Button>
        </Stack>
    </div>
  )
}

export default Register