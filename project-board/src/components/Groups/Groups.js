import React, { useState, useEffect } from 'react';
import GroupService from '../../services/GroupService';
import {useParams} from "react-router-dom";
import {Container, Form, Table, Button} from 'react-bootstrap';
import {Link} from 'react-router-dom';
import CategoryService from '../../services/CategoryService';
import { async } from 'q';


function Groups() {
    const[groups, setGroups] =useState([]);
    const [rpp, setRpp] = useState(3);
    const [pageNumber, setPageNumber] = useState(1);
    const [orderBy, setOrderBy] = useState("Name");
    const [sortOrder, setSortOrder] = useState("Desc");
    const [nameSearch, setNameSearch] = useState("");
   /* const[category, setCategory]=useState([]);
    const {categoryId} = useParams();*/
    
    useEffect(() =>{
        findGroups();
        
    }, []);
    
    const findGroups = async() =>{
       return  await GroupService.find(rpp, pageNumber, orderBy, sortOrder).then(response => {
        setGroups(response.data);
      });
    };

    /*const getCategory = () =>{
      return CategoryService.get(categoryId).then(response =>{
        setCategory(response.data);
      });
    }*/
    
    const handleNameSearch = (e) =>{
      setNameSearch(e.target.value);
  }

  const handleRpp = (e) =>{
      setRpp(e.target.value);
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

  return (
    <div>
    <Container>
      <Form>
        <Form.Control type="text" placeholder="Enter Name" value={nameSearch} onChange={handleNameSearch}/> 
      </Form>
      

      <Form.Select onChange={handleOrderBy}>
        <option value="GroupName">Name</option>
        <option value="TimeCreated"> Time Created</option>
      </Form.Select>
      <Form.Text> Order by </Form.Text>

      <Form.Select onChange={handleSortOrder}>
        <option value="Desc">Desc</option>
        <option value="Asc">Asc</option>
      </Form.Select>
      <Form.Text> Sort Order </Form.Text>

    </Container>

    <Button onClick={findGroups}>Find </Button>

    <Table stripped="true" bordered hover variant="dark" size="sm">
        <thead>
            <tr key="GroupTable">
                <th>Name</th>
                <th>Category</th>
            </tr>
        </thead>

        <tbody>
          {groups && groups.map((group) =>
              <tr key={group.GroupId}>
                <td> <Link to={`/Group/${group.GroupId}`}>{group.Name}</Link></td>
                <td>{group.CategoryName}</td>
              </tr>
          )}
        </tbody>
    </Table>
    </div>
  )
}

export default Groups