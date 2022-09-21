import React, { useState, useEffect }  from 'react'
import UserService from '../../services/UserService'
import { Table } from "react-bootstrap"
import { Link } from 'react-router-dom'




function UserList() {
    const [users, setUsers] = useState([]);
    useEffect(() => {
        findUsers();
    }, []);

    const findUsers = () =>{
        return UserService.getAll().then(response =>{
                        setUsers(response.data);
                    });
    }
  return (
    <Table className="user-table" stripped="true" bordered hover variant="dark" size="sm">
        <thead>
            <tr key="UTable">
                <th width="100">Username</th>
                <th width="100">Gmail</th>
            </tr>
        </thead>
        <tbody>
        {users.map((user) =>
        <tr key={user.UserId}>
            <td><Link class="link-info" to={`/user/${user.UserId}`} target="_top">{user.Username}</Link></td>
            <td>{user.Email}</td>
        </tr>
        )}
        </tbody>
    </Table>
  )
}

export default UserList