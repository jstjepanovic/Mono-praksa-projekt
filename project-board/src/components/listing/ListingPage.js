import React, { useState, useEffect } from 'react'
import ListingService from '../../services/ListingService';
import ListingDelete from './ListingDelete';
import { useParams } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import ListingEdit from './ListingEdit';

function ListingPage() {
    const [listing, setListing] = useState({});
    const [isEditing, setIsEditing] = useState(false);

    const { listingId } = useParams();

    useEffect(() => {
        getListing();
    }, []);

    const getListing = () =>{
        return ListingService.get(listingId).then(response =>{
            setListing(response.data);
        });
    }

  return (
    <div>
        <h1>
            {listing.Price}
        </h1>
        <ListingDelete listingId={listingId} />
        {
            isEditing ? ( <> <ListingEdit listingId={listingId} /> <Button variant="outline-dark" onClick={() => setIsEditing(false)}> Abort </Button> </> 
            ) : (
            <Button variant="outline-dark" onClick={() => setIsEditing(true)}>Edit</Button> )
        }
    </div>
  )
}

export default ListingPage