import React, { useEffect } from 'react'
import {useDispatch, useSelector } from 'react-redux'
import { fetchCollabs, getCollabs } from '../features/Collaborations/collabSlice';
import { useGetAllCollabsMutation } from '../features/Collaborations/colabApiSlice';
import CollabPageHeader from '../features/Collaborations/CollaborationsPage/CollabPageHeader';
import { Divider } from '@mui/material';
import CollaborationList from '../features/Collaborations/CollaborationsPage/CollaborationList';
import CollabCard from '../features/Collaborations/CollaborationsPage/CollabCard';
import Collaboration from '../data/Collaboration';

const CollaborationPage = () => {

  const dispatch = useDispatch();
  const collabs = useSelector(getCollabs)
  const [collabMutation] = useGetAllCollabsMutation();
  
  useEffect(()=>{
    //@ts-ignore
    dispatch(fetchCollabs(collabMutation));
  },[dispatch])

  if(collabs == undefined)
  return
  <></>


  return (
    <>
    <CollabPageHeader />
    <Divider></Divider>

 
    <div className="py-5">
      {collabs.map((collab:Collaboration) =><CollabCard key={collab.id} collab = {collab} />)}
    </div>


    
    </>
  )
}

export default CollaborationPage