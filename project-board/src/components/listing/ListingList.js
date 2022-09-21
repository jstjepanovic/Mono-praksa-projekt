import React, { useState, useEffect } from 'react'
import ListingService from '../../services/ListingService'
import UserService from '../../services/UserService'
import ListingDelete from './ListingDelete';
import ListingEdit from './ListingEdit';


import { Table, Button, Container, Row, Col, Form, FormLabel } from "react-bootstrap"
import { useParams } from 'react-router-dom';
import { Link } from 'react-router-dom'
import ListingCreate from './ListingCreate'

function ListingList() {
    const [listings, setListings] = useState([]);
    const [price, setPrice] = useState();
    const [condition, setCondition] = useState("");
    const [timeCreated, setTimeCreated] = useState();
    const [pageNumber, setPageNumber] = useState();
    const [recordsPerPage, setRecordsPerPage] = useState();
    const [orderBy, setOrderBy] = useState("TimeCreated");
    const [sortOrder, setSortOrder] = useState("Asc");
    const [isAdding, setIsAdding] = useState(false);

    useEffect(() => {
        findListings();
    }, []);
    

    const findListings = () =>{
        return ListingService.find(price, condition, timeCreated, pageNumber, recordsPerPage, 
            orderBy, sortOrder).then(response =>{
                        setListings(response.data);
                    });
    }

    const handleRecordsPerPage = (e) =>{
        setRecordsPerPage(e.target.value);
    }

    const handlePageNumber = (e) =>{
        setPageNumber(e.target.value);
    }

    const handleOrderBy = (e) =>{
        setOrderBy(e.target.value);
    }
    
    const handleSortOrder = (e) =>{
        setSortOrder(e.target.value);
    }

    const handlePrice = (e) =>{
        setPrice(e.target.value);
    }

    const handleCondition = (e) =>{
        setCondition(e.target.value);
    }

    const handleTimeCreated = (e) =>{
        setTimeCreated(e.target.value);
    }

  return (
    <div>
    <Container>
        <Row>
            <Col>
                <Form>
                    <Form.Control type="number" placeholder="Enter Price" value={price} onChange={handlePrice}/> 
                </Form>
            </Col>
            <Col>
                <Form>
                    <Form.Control type="text" placeholder="Condition" value={condition} onChange={handleCondition}/> 
                </Form>
            </Col>
            <Col>
                <Form>
                    <Form.Control type="number" placeholder="TimeCreated" value={timeCreated} onChange={handleTimeCreated}/> 
                </Form>
            </Col>
            <Col>
                <Form.Select onChange={handleOrderBy}>
                    <option value="Price">Price</option>
                    <option value="TimeCreated">TimeCreated</option>
                </Form.Select>
                <Form.Text> Order by </Form.Text>
            </Col>
            <Col>
                <Form.Select onChange={handleSortOrder}>
                    <option value="Desc">Desc</option>
                    <option value="Asc">Asc</option>
                </Form.Select>
                <Form.Text> Sort Order </Form.Text>
            </Col>
        </Row>
    </Container>
    <Button variant="outline-dark" onClick={findListings}>Find</Button>
    <div>
        {
            isAdding ? ( <> <ListingCreate/> <Button variant="outline-dark" onClick={() => setIsAdding(false)}> Cancel </Button> </>
            ) : (
            <Button variant="outline-dark" onClick={() => setIsAdding(true)}>Create Listing</Button> )
        }
        </div>
    <Table stripped="true" bordered hover variant="dark" size="sm">
        <thead>
            <tr key="LTable">
                <th width="170">Seller</th>
                <th width="170">Game</th>
                <th width="170">Price</th>
                <th width="170">Condition</th>
                <th width="170">TimeCreated</th>
                <th width="100">Order</th>
            </tr>
        </thead>
        <tbody>
        {listings.map((listing) =>
        listing.OrderId !== null ? <></> :
        (<tr key={listing.ListingId}>
            <td><Link class="link-info" to={`/user/${listing.UserId}`} target="_top">{listing.Username}</Link></td>
            <td><Link class="link-info" to={`/boardGame/${listing.BoardGameId}`} target="_top"> {listing.BoardGameName} </Link></td>
            <td>{listing.Price}</td>
            <td>{listing.Condition}</td>
            <td>{listing.TimeCreated}</td>
            <td><Link class="link-info" to={`/create-order/${listing.ListingId}`} target="_top"><Button variant="outline-info">Order</Button></Link></td>
        </tr>)
        )}
        </tbody>
    </Table>
    </div>
  ) 
}

export default ListingList