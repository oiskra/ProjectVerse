import React, { useEffect } from 'react'
import {useDispatch, useSelector } from 'react-redux'
import { fetchCollabs, getCollabs } from '../features/Collaborations/colabSlice';
import { useGetAllCollabsMutation } from '../features/Collaborations/colabApiSlice';

const CollaborationPage = () => {

  const dispatch = useDispatch();
  const collabs = useSelector(getCollabs)
  const [collabMutation] = useGetAllCollabsMutation();
  
  useEffect(()=>{
    //@ts-ignore
    dispatch(fetchCollabs(collabMutation));
    console.log(collabs);
  },[dispatch])

  if(collabs == undefined)
  return
  <></>


  return (
    <>
    {collabs.map(() => <div>CollaborationPage</div>)}
    </>
  )
}

export default CollaborationPage