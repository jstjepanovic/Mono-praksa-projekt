import React, { useState, useEffect } from 'react'
import OrderService from '../../services/OrderService'
import { Stack, Button, Form } from "react-bootstrap"
import UserService from '../../services/UserService'
import { useNavigate, useParams } from 'react-router-dom';


function OrderCreate(props) {
    const [order, setOrder] = useState({});
    const [isValid, setIsValid] = useState(true);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();
    const { listingId } = useParams();

    const handleInputChange = (e) => {
        setOrder({ ...order, [e.target.name]: e.target.value });
    };
    const handleUsername = (e) =>{
        setUsername(e.target.value);
      }
    
      const handlePassword = (e) =>{
        setPassword(e.target.value);
      }
      const addOrder = async () =>{
        let user = {}
        await UserService.find(username).then(response=>{
          user = response.data[0]
        })
    
        if (user.Password !== password){
          setIsValid(false)
          return;
        }
    
        let ord = { DeliveryAddress : order.DeliveryAddress, UserId : user.UserId}
        OrderService.create(listingId, ord)
        navigate("/marketplace")
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
                <Form.Control required={true} type="text" placeholder="Username" name="Username" value={username || ""} onChange={handleUsername}/> 
            </Form>
            <Form>
                <Form.Control required={true} type="password" placeholder="Password" name="Password" value={password || ""} onChange={handlePassword}/> 
            </Form>
            <Form>
                <Form.Control required={true} type="text" placeholder="DeliveryAddress" name="DeliveryAddress" onChange={handleInputChange}/> 
            </Form>
            <Button variant="outline-dark" onClick={addOrder}>Order</Button>
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

export default OrderCreate