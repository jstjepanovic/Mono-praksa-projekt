import React, {useState, useEffect} from 'react'
import ListingService from '../../services/ListingService';
import { useParams, useNavigate } from 'react-router-dom';
import { Stack, Button, Form } from "react-bootstrap";

function ListingEdit() {
    const [listing, setListing] = useState({});
    const { listingId } = useParams();
    const navigate = useNavigate();

    const handleInputChange = (e) => {
        setListing({ ...listing, [e.target.name]: e.target.value });
        console.log(listing)
    };

    const editListing = async() => {
        await ListingService.update(listingId, listing);
        navigate("/marketplace");
    }

  return (
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
            <Button variant="outline-dark" onClick={editListing}>Confirm</Button>
        </Stack>
    </div>
  )
}

export default ListingEdit