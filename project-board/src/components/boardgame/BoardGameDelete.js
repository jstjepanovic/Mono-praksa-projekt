import React, { useState } from 'react'
import BoardGameService from '../../services/BoardGameService'
import { Button } from 'react-bootstrap'
import { useNavigate } from 'react-router-dom';

function BoardGameDelete(props) {
    const [toDelete, setToDelete] = useState(false);

    const navigate = useNavigate();

    const deleteBoardGame = () =>{
        BoardGameService.remove(props.boardGameId);
        navigate("/");
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
                <h3>Are you sure?</h3>
                <Button variant="outline-dark" onClick={deleteBoardGame}>Confirm</Button>
                <Button variant="outline-dark" onClick={() => setToDelete(false)}>Cancel</Button>
            </div>
        ) : (
            <div>
                <Button variant="outline-dark" onClick={del}>Delete</Button>
            </div>
        )
    }
    </>
  )
}

export default BoardGameDelete