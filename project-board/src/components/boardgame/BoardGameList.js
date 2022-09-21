import React, { useState, useEffect } from 'react'
import BoardGameService from '../../services/BoardGameService'
import { Table, Container, Row, Col, Form, Pagination } from "react-bootstrap"
import { Link } from 'react-router-dom'

function BoardGameList() {
    const [boardGames, setBoardGames] = useState([]);
    const [rpp, setRpp] = useState(10);
    const [currentPage, setcurrentPage] = useState(1);
    const [orderBy, setOrderBy] = useState("Rating");
    const [sortOrder, setSortOrder] = useState("Desc");
    const [nameSearch, setNameSearch] = useState("");
    const [minPlayers, setMinPlayers] = useState();
    const [maxPlayers, setMaxPlayers] = useState();
    const [age, setAge] = useState();
    const [rating, setRating] = useState();
    const [weight, setWeight] = useState();
    const [publisherSearch, setPublisherSearch] = useState("");
    const [totalCount, setTotalCount] = useState();


    useEffect(() => {
        const fetchBoardGames = async () =>{
            return await BoardGameService.find(rpp, currentPage, orderBy, sortOrder, nameSearch,
                        minPlayers, maxPlayers, age, rating, weight, publisherSearch).then(response =>{
                            setBoardGames(response.data);
                        });
        }
        fetchBoardGames();
        getTotalCount();
    }, [rpp, currentPage, orderBy, sortOrder, nameSearch, minPlayers, maxPlayers, age, rating, weight, publisherSearch]);

    const getTotalCount = () =>{
        return BoardGameService.count().then(response=>{
            setTotalCount(response.data)
        })
    }

    const handleNameSearch = (e) =>{
        setNameSearch(e.target.value);
    }

    const handleRpp = (e) =>{
        setRpp(e.target.value);
        setcurrentPage(1);
    }

    const handleCurrentPage = (e) =>{
        setcurrentPage(parseInt(e.target.text));
    }

    const handleOrderBy = (e) =>{
        setOrderBy(e.target.value);
    }
    
    const handleSortOrder = (e) =>{
        setSortOrder(e.target.value);
    }

    const handleMinPlayers = (e) =>{
        setMinPlayers(e.target.value);
    }

    const handleMaxPlayers = (e) =>{
        setMaxPlayers(e.target.value);
    }

    const handleAge = (e) =>{
        setAge(e.target.value);
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

    const handlePublisherSearch = (e) =>{
        setPublisherSearch(e.target.value);
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
    <Container>
        <Row>
            <Col>
                <Form>
                    <label htmlFor="BGName">Name</label>
                    <Form.Control type="text" id="BGName" placeholder="Enter Name" value={nameSearch} onChange={handleNameSearch}/> 
                </Form>
            </Col>
            <Col>
                <Form>
                    <label htmlFor="BGPublisher">Publisher</label>
                    <Form.Control type="text" id="BGPublisher" placeholder="Enter Publisher" value={publisherSearch} onChange={handlePublisherSearch}/> 
                </Form>
            </Col>
            <Col>
                <Form>
                    <label htmlFor="BGWeight">Weight</label>
                    <Form.Control type="number" id="BGWeight" placeholder="Enter Minimal Weight" value={weight} onChange={handleWeight}/> 
                </Form>
                <Form.Text> Minimum: 1 Maximum: 5</Form.Text>
            </Col>
            <Col>
                <Form>
                    <label htmlFor="BGRating">Rating</label>
                    <Form.Control type="number" id="BGRating" placeholder="Enter Minimal Rating" value={rating} onChange={handleRating}/> 
                </Form>
                <Form.Text> Minimum: 1 Maximum: 10</Form.Text>
            </Col>
        </Row>
        <Row>
            <Col>
                <Form>
                    <label htmlFor="BGmin">Min Players</label>
                    <Form.Control type="number" id="BGmin" placeholder="Enter minimal number of players" value={minPlayers} onChange={handleMinPlayers}/> 
                </Form>
            </Col>
            <Col>
                <Form>
                    <label htmlFor="BGmax">Max Players</label>
                    <Form.Control type="number" id="BGmax" placeholder="Enter maximal number players" value={maxPlayers} onChange={handleMaxPlayers}/> 
                </Form>
            </Col>
            <Col>
                <Form>
                    <label htmlFor="BGAge">Age</label>
                    <Form.Control type="number" id="BGAge" placeholder="Age restriction" value={age} onChange={handleAge}/> 
                </Form>
            </Col>
            <Col>
                <label htmlFor="BGOrderBy">Order by</label>
                <Form.Select id="BGOrderBy" onChange={handleOrderBy}>
                    <option value="Rating">Rating</option>
                    <option value="Weight">Weight</option>
                    <option value="Name">Name</option>
                </Form.Select>
            </Col>
            <Col>
                <label htmlFor="BGSortOrder">Sort Order</label>
                <Form.Select id="BGSortOrder" onChange={handleSortOrder}>
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
            <tr key="BGTable">
                <th width="500">Name</th>
                <th width="170">NoPlayers</th>
                <th width="170">Age</th>
                <th width="170">AvgPlayingTime</th>
                <th width="170">Rating</th>
                <th width="170">Weight</th>
                <th width="400">Publisher</th>
                <th width="400">Genre</th>
            </tr>
        </thead>
        <tbody>
            {boardGames && boardGames.map((boardGame) =>
                <tr key={boardGame.BoardGameId}>
                    <td> <Link class="link-info" to={`/boardGame/${boardGame.BoardGameId}`} target="_top"> {boardGame.Name} </Link> </td>
                    <td>{boardGame.NoPlayersMin}-{boardGame.NoPlayersMax}</td>
                    <td>{boardGame.Age}+</td>
                    <td>{boardGame.AvgPlayingTime} min</td>
                    <td>{Math.round((boardGame.Rating + Number.EPSILON) * 100) / 100}</td>
                    <td>{Math.round((boardGame.Weight + Number.EPSILON) * 100) / 100}</td>
                    <td>{boardGame.Publisher}</td>
                    <td>{boardGame.Genre.Name}</td>
                </tr>
            )}
        </tbody>
    </Table>
    </div>
  ) 
}

export default BoardGameList