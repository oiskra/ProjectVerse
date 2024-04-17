import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import { useGetCollabMutation } from '../features/Collaborations/colabApiSlice';
import { getCurrentColab } from '../features/Collaborations/collabSlice';
import { CollabDescCard } from '../features/Collaborations/CollabDescCard';
import { Loader } from '../components/Loader';
import { fetchSingle } from '../features/Collaborations/collabSlice';

const CollabDescriptionPage = () => {

  const params  = useParams();
  const dispatch = useDispatch();
  const [colabMutation] = useGetCollabMutation();

  const id = params.id;

  const colabData = useSelector(getCurrentColab);   
  
  useEffect(()=>{
    //@ts-ignore
    dispatch(fetchSingle({colabMutation,id}));     
  }, [id])  

  if(!colabData){
    return (<Loader />)
  }

  return (    
    <CollabDescCard collab = {colabData} />
  )
}

export default CollabDescriptionPage