import React, { useState } from 'react'
import ListingService from '../../services/ListingService'
import { Button } from 'react-bootstrap'
import { useNavigate } from 'react-router-dom';

function ListingDelete(props) {
    const [toDelete, setToDelete] = useState(false);

    const navigate = useNavigate();

    const deleteListing = () =>{
        ListingService.remove(props.ListingId);
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
                <Button variant="outline-success" onClick={deleteListing}>Confirm</Button>
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

export default ListingDelete