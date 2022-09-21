import React from 'react';
import {useState, useEffect} from 'react';
import {useParams} from 'react-router-dom';
import GroupService from '../../services/GroupService';

function Group() {
    const[group, setGroup] =useState({});
    const {groupId}=useParams();

    const getGroup=() =>{
      return GroupService.get(groupId).then(response=>{
        setGroup(response.data);
      });
    };

    useEffect(() =>{
      getGroup();
    }, []);

  return (
    <h1>{group.Name}</h1>
    
  )
}

export default Group