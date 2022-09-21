import React, { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import UserService from '../../services/UserService';
import ListingService from '../../services/ListingService';
import OrderService from '../../services/OrderService';
import ListingDelete from '../listing/ListingDelete';
import OrderDelete from '../order/OrderDelete';

import { Table, Button} from "react-bootstrap"
import { Link } from 'react-router-dom'

import "../../css/UserProfile.css";

function UserProfile() {
    const [listings, setListings] = useState([]);
    const [orders, setOrders] = useState([]);
    const [user, setUser] = useState({})

    useEffect(() => {
        findListings();
        findOrders();
        getUser();
    }, []);

    
    const { userId } = useParams();

    const getUser = () =>{
        return UserService.get(userId).then(response =>{
            setUser(response.data);
        });
    }   

    const findListings = () =>{
        return ListingService.getSpecific(userId).then(response =>{
                        setListings(response.data);
                    });
    }
    const findOrders = () =>{
        return OrderService.findUserId(userId).then(response =>{
                        setOrders(response.data);
                    });
    }

  return (
    <>
    <div class="row gutters-sm">
    <div class="col-md-4 mb-3">
      <div class="card">
        <div class="card-body">
          <div class="d-flex flex-column align-items-center text-center">
            <img src="https://bootdey.com/img/Content/avatar/avatar7.png" alt="Admin" class="rounded-circle" width="150"></img>
            <div class="mt-3">
</div>
  </div>
      </div>
          </div>
      </div>
      <div class="col-md-8">
      <div class="card mb-3">
        <div class="card-body">
          <div class="row">
            <div class="col-sm-3">
              <h6 class="mb-0">Full Name</h6>
            </div>
            <div class="col-sm-9 text-secondary">
            <h1>
        {user.Username}
        </h1>
            </div>
          </div>
          
          <div class="row">
            <div class="col-sm-3">
              <h6 class="mb-0">Email</h6>
            </div>
            <div class="col-sm-9 text-secondary">
              {user.Email}
            </div>
          </div>
          
          <div class="row">
            <div class="col-sm-3">
              <h6 class="mb-0">Phone</h6>
            </div>
            <div class="col-sm-9 text-secondary">
              (239) 816-9029
            </div>
          </div>
          
          <div class="row">
            <div class="col-sm-3">
              <h6 class="mb-0">Mobile</h6>
            </div>
            <div class="col-sm-9 text-secondary">
              (320) 380-4539
            </div>
          </div>
          
          <div class="row">
            <div class="col-sm-3">
              <h6 class="mb-0">Address</h6>
            </div>
            <div class="col-sm-9 text-secondary">
              Bay Area, San Francisco, CA
            </div>
          </div>
          <hr></hr>
          </div>
          </div>
        </div>
      </div>
      <>
    <b> Listings </b>   
    <Table stripped="true" bordered hover variant="dark" size="sm">
    <thead>
        <tr key="LTable">
            <th width="100">Game</th>
            <th width="100">Price</th>
            <th width="100">Condition</th>
            <th width="100">TimeCreated</th>
            <th width="50">Delete</th>
            <th width="50">Edit</th>
        </tr>
    </thead>
    <tbody>
    {listings.map((listing) =>
    <tr key={listing.ListingId}>
        <td><Link class="link-info" to={`/boardGame/${listing.BoardGameId}`} target="_top"> {listing.BoardGameName} </Link></td>
        <td>{listing.Price}</td>
        <td>{listing.Condition}</td>
        <td>{listing.TimeCreated}</td>
        <td><ListingDelete ListingId={listing.ListingId} /></td>
        <td><Link class="link-info" to={`/listing/${listing.ListingId}`} target="_top"><Button variant="outline-info">Edit</Button></Link></td>
    </tr>
    )}
    </tbody>
</Table>
<b> Orders </b>
<Table width="480"stripped="true" bordered hover variant="dark" size="sm">
    <thead>
        <tr key="LTable">
            <th width="137">Board Game</th>
            <th width="137">DeliveryAddress</th>
            <th width="140">TimeCreated</th>
            <th width="50">Delete</th>
            <th width="50">Edit</th>
        </tr> 
    </thead>
    <tbody>
    {orders.map((order) =>
    <tr key={order.OrderId}>
        <td>{order.BoardGameName}</td>
        <td>{order.DeliveryAddress}</td>
        <td>{order.TimeCreated}</td>
        <td><OrderDelete OrderId={order.OrderId} /></td>
        <td><Link class="link-info" to={`/order/${order.OrderId}`} target="_top"><Button variant="outline-info">Edit</Button></Link></td>
    </tr>
    )}
    </tbody>
</Table>

  </>
  </>
  )
}

export default UserProfile