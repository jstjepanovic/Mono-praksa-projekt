import React, { useState, useEffect } from 'react'
import BoardGameService from '../../services/BoardGameService';
import BoardGameDelete from './BoardGameDelete';
import { useParams } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import BoardGameEdit from './BoardGameEdit';
import ReviewList from '../review/ReviewList';
import ReviewCreate from '../review/ReviewCreate';

function BoardGamePage() {
    const [boardGame, setBoardGame] = useState({});
    const [isEditing, setIsEditing] = useState(false);
    const [isAdding, setIsAdding] = useState(false);

    const { boardGameId } = useParams();

    useEffect(() => {
        getBoardGame();
    }, []);

    const getBoardGame = () =>{
        return BoardGameService.get(boardGameId).then(response =>{
            setBoardGame(response.data);
        });
    }

  return (
    <div>
        <h1>
            {boardGame.Name}
        </h1>
        <BoardGameDelete boardGameId={boardGameId} />
        <div>
        {
            isEditing ? ( <> <BoardGameEdit boardGameId={boardGameId} /> <Button variant="outline-dark" onClick={() => setIsEditing(false)}> Abort </Button> </> 
            ) : (
            <Button variant="outline-dark" onClick={() => setIsEditing(true)}>Edit</Button> )
        }
        </div>
        <div>
        {
            isAdding ? ( <> <ReviewCreate boardGameId={boardGameId} /> <Button variant="outline-dark" onClick={() => setIsAdding(false)}> Cancel </Button> </>
            ) : (
            <Button variant="outline-dark" onClick={() => setIsAdding(true)}>AddReview</Button> )
        }
        <ReviewList boardGameId={boardGameId} />
        </div>
    </div>
  )
}

export default BoardGamePage