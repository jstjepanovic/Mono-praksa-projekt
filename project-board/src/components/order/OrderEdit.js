import React, {useState} from 'react'
import OrderService from '../../services/OrderService';
import { useParams, useNavigate } from 'react-router-dom';
import { Stack, Button, Form } from "react-bootstrap";


function OrderEdit() {
    const [order, setOrder] = useState({});
    const { orderId } = useParams();
    const navigate = useNavigate();

    const handleInputChange = (e) => {
        setOrder({ ...order, [e.target.name]: e.target.value });
    };

    const editOrder = async() => {
        await OrderService.update(orderId, order);
        navigate("/");
    }

  return (
    <div>

       <Stack>
            <Form>
                <Form.Label>Delivery Address</Form.Label>
                <Form.Control required={true} type="text" placeholder="Enter DeliveryAddress" name="DeliveryAddress" onChange={handleInputChange}/> 
            </Form>
            <Button variant="outline-dark" onClick={editOrder}>Confirm</Button>
        </Stack>
    </div>
  )
}

export default OrderEdit