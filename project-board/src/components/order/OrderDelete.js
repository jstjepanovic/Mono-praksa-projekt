import React, { useState } from 'react'
import OrderService from '../../services/OrderService'
import { Button } from 'react-bootstrap'
import { useNavigate } from 'react-router-dom';

function OrderDelete(props) {
    const [toDelete, setToDelete] = useState(false);

    const navigate = useNavigate();

    const deleteOrder = () =>{
        OrderService.remove(props.OrderId);
        navigate(0);
    }

    const del = () =>{
        setToDelete(true)
    }

  return (
    <>
    {
        toDelete ? (
            <div>
                <Button variant="outline-success" onClick={deleteOrder}>Confirm</Button>
                <Button variant="outline-info" onClick={() => setToDelete(false)}>Cancel</Button>
            </div>
        ) : (
            <div>
                <Button variant="outline-danger" onClick={del}>Delete</Button>
            </div>
        )
    }
    </>
  )
}

export default OrderDelete