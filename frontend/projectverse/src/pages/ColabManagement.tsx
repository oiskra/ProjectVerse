import { Box, Tabs, Tab } from '@mui/material'
import React, { useEffect, useState } from 'react'
import { ColabHome } from '../features/Collaborations/ColabHome';
import { CollaborationApplicants } from '../features/Collaborations/CollaborationApplicants';
import { ContentLayout } from '../layouts/ContentLayout';
import { CollaborationPositionsPage } from '../features/Collaborations/CollaborationPositionsPage';
import { CollaborationMembers } from '../features/Collaborations/CollaborationMembers';

import ChangeCircleIcon from '@mui/icons-material/ChangeCircle';
import { useGetColabMutation } from '../features/Collaborations/colabApiSlice';
import Collaboration, { sampleColaboration } from '../data/Collaboration';
import { Loader } from '../components/Loader';
import { useParams } from 'react-router';

export const ColabManagement = () => {

  const params  = useParams();
  const id = params.id;

  const [page, setPage] = useState('home');

  const [colabData, setColabData] = useState(null as Collaboration || null);
  const [colab, {isLoading}] = useGetColabMutation();
  
  useEffect(()=>{
    colab(id!).unwrap().then((data:Collaboration)=>{      
      setColabData(data);
      console.log(data);
    })
    
  }, [])  

  const handlePageChange = (event : React.SyntheticEvent, newValue: string) => {
    setPage(newValue);
  };

  const handleColabChange = () =>{
    // TODO colab swicher
  }

  if(colabData === null){
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
