import React, { useState, useEffect } from 'react'
import OrderService from '../../services/OrderService';
import OrderDelete from './OrderDelete';
import { useParams } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import OrderEdit from './OrderEdit';

function OrderPage() {
    const [order, setOrder] = useState({});
    const [isEditing, setIsEditing] = useState(false);

    const { orderId } = useParams();

    useEffect(() => {
        getOrder();
    }, []);

    const getOrder = () =>{
        return OrderService.get(orderId).then(response =>{
            setOrder(response.data);
        });
    }

  return (
    <div>
        <h1>
            {order.DeliveryAddress}
        </h1>
        <h1>
            {order.TimeCreated}
        </h1>

        <OrderDelete orderId={orderId} />
        {
            isEditing ? ( <> <OrderEdit orderId={orderId} /> <Button variant="outline-dark" onClick={() => setIsEditing(false)}> Abort </Button> </> 
            ) : (
            <Button variant="outline-dark" onClick={() => setIsEditing(true)}>Edit</Button> )
        }
    </div>
  )
}

export default OrderPage