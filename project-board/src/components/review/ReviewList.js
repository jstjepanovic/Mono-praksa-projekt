import React, { useState, useEffect } from 'react'
import ReviewService from '../../services/ReviewService'
import { Container, Table, Row, Col, Form, Pagination } from 'react-bootstrap'
import { Link } from 'react-router-dom'

function ReviewList(props) {
  const [reviews, setReviews] = useState([]);
  const [rpp, setRpp] = useState(5);
  const [currentPage, setcurrentPage] = useState(1);
  const [orderBy, setOrderBy] = useState("Rating");
  const [sortOrder, setSortOrder] = useState("Desc");
  const [rating, setRating] = useState();
  const [weight, setWeight] = useState();
  const [totalCount, setTotalCount] = useState();

  useEffect(() => {
    const findReviews = async () =>{
      return await ReviewService.find(props.boardGameId, rpp, currentPage, orderBy, sortOrder, rating, weight).then(response =>{
        setReviews(response.data);
      })  
    }

    const getTotalCount = () =>{
      return ReviewService.countUserReviews(props.boardGameId).then(response=>{
          setTotalCount(response.data)
      })
    }

    findReviews();
    getTotalCount();
  }, [rpp, currentPage, orderBy, sortOrder, rating, weight, props.boardGameId]);

  const handleRpp = (e) =>{
    setRpp(e.target.value);
    setcurrentPage(1);
  }

  const handleOrderBy = (e) =>{
    setOrderBy(e.target.value);
  }

  const handleSortOrder = (e) =>{
    setSortOrder(e.target.value);
  }

  const handleCurrentPage = (e) =>{
    setcurrentPage(parseInt(e.target.text));
  }

  const handleRating = (e) =>{
    if (e.target.value < 1 || e.target.value > 10){
        setRating();
        return;
    }
    setRating(e.target.value);
  }

  const handleWeight = (e) =>{
    if (e.target.value < 1 || e.target.value > 5){
        setWeight();
        return;
    }
    setWeight(e.target.value);
  }

  let items = [];
    for (let number = 1; number <= Math.ceil(totalCount/rpp); number++) {
        items.push(
          <Pagination.Item key={number} active={number === currentPage} onClick={handleCurrentPage}>
            {number}
          </Pagination.Item>,
       );
  }

  return (
    <div>
      <h2>Reviews</h2>
      <Container>
        <Row>
          <Col>
            <Form>
              <label htmlFor="RevWeight">Weight</label>
              <Form.Control type="number" id="RevWeight" placeholder="Enter Minimal Weight" value={weight} onChange={handleWeight}/> 
            </Form>
            <Form.Text> Minimum: 1 Maximum: 5</Form.Text>
          </Col>
          <Col>
            <Form>
              <label htmlFor="RevRating">Rating</label>
              <Form.Control type="number" id="RevRating" placeholder="Enter Minimal Rating" value={rating} onChange={handleRating}/> 
            </Form>
            <Form.Text> Minimum: 1 Maximum: 10</Form.Text>
          </Col>
          <Col>
            <label htmlFor="RevOrderBy">Order by</label>
            <Form.Select id="RevOrderBy" onChange={handleOrderBy}>
              <option value="Rating">Rating</option>
              <option value="Weight">Weight</option>
              <option value="TimeUpdated">TimeUpdated</option>
            </Form.Select>
          </Col>
          <Col>
            <label htmlFor="RevSortOrder">Sort Order</label>
            <Form.Select id="RevSortOrder" onChange={handleSortOrder}>
              <option value="Desc">Desc</option>
              <option value="Asc">Asc</option>
            </Form.Select>
          </Col>
          <Col>
            <Form>
              <label htmlFor="Rpp">Items per page</label>
              <Form.Control id="Rpp" type="number" placeholder="Items per page" value={rpp} onChange={handleRpp}/> 
            </Form>
          </Col>
        </Row>
      </Container>
      <div style={{ display: "flex", justifyContent: "center" }}>
        <Pagination>{items}</Pagination>
      </div>
      <Table stripped="true" bordered hover variant="dark" size="sm">
        <thead>
            <tr key="ReviewTable">
                <th width="340">Username</th>
                <th width="1000">Text</th>
                <th width="170">Rating</th>
                <th width="170">Weight</th>
            </tr>
        </thead>
        <tbody>
            {reviews && reviews.map((review) =>
                <tr key={review.ReviewId}>
                    <td><Link class="link-info" to={`/user/${review.UserId}`} target="_top"> {review.Username} </Link></td>
                    <td>{review.ReviewText}</td>
                    <td>{Math.round((review.Rating + Number.EPSILON) * 100) / 100}</td>
                    <td>{Math.round((review.Weight + Number.EPSILON) * 100) / 100}</td>
                </tr>
            )}
        </tbody>
    </Table>
    </div>
  )
}

export default ReviewList