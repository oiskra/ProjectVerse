import { Box, Tabs, Tab } from '@mui/material'
import React, { useEffect, useState } from 'react'
import { ColabHome } from '../features/Collaborations/ColabHome';
import { CollaborationApplicants } from '../features/Collaborations/CollaborationApplicants';
import { ContentLayout } from '../layouts/ContentLayout';
import { CollaborationPositionsPage } from '../features/Collaborations/CollaborationPositionsPage';
import { CollaborationMembers } from '../features/Collaborations/CollaborationMembers';

import ChangeCircleIcon from '@mui/icons-material/ChangeCircle';
import { useGetCollabMutation } from '../features/Collaborations/colabApiSlice';

import { Loader } from '../components/Loader';
import { useParams } from 'react-router';
import Collaboration from '../data/Collaboration';
import { useDispatch, useSelector } from 'react-redux';
import { fetchSingle, getCurrentColab} from '../features/Collaborations/colabSlice';

export const ColabManagement = () => {

  const params  = useParams();
  const dispatch = useDispatch();
  const [colabMutation] = useGetCollabMutation();

  const id = params.id;

  const [page, setPage] = useState('home');

  const colabData = useSelector(getCurrentColab);
  
  
  
  useEffect(()=>{
    //@ts-ignore
    dispatch(fetchSingle({colabMutation,id})); 
    console.warn(colabData);
  }, [id])  

  const handlePageChange = (event : React.SyntheticEvent, newValue: string) => {
    setPage(newValue);
  };

  const handleColabChange = () =>{
    // TODO colab swicher
  }

  if(!colabData){
    return (<Loader />)
  }  


  return (  
    <ContentLayout>


      <h2 className="text-4xl text-white">
    
        <span className="text-accent">
          {colabData.name + " "}
        </span>

        {page} 

        <ChangeCircleIcon onClick={handleColabChange} sx={{color:"white"}} />

      </h2>
      
      <Box sx={{ width: '100%' }}>
        <Tabs
          value={page}
          onChange={handlePageChange}
          textColor="primary"
          indicatorColor="primary"
          aria-label="secondary tabs example"
        >
          <Tab value="home" label="Home" />
          <Tab value="collaboration positions" label="Collaboration positions" />
          <Tab value="applicants" label="Applicants" />
          <Tab value="members" label="Members" />

        </Tabs>
      </Box>

      {page === "home" ? <ColabHome data = {colabData} /> : ""}
      {page === "collaboration positions" ? <CollaborationPositionsPage positions ={colabData.collaborationPositions} /> : ""}
      {page === "applicants" ? <CollaborationApplicants applicants = {colabData.collaborationApplicants} /> : ""}
      {page === "members" ? <CollaborationMembers members = {colabData.collaborationApplicants}/> : ""}

    </ContentLayout>
    
  );
}
