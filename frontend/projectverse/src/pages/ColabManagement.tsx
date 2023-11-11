import { Box, Tabs, Tab } from '@mui/material'
import React from 'react'
import { ColabHome } from '../features/Collaborations/ColabHome';
import { CollaborationApplicants } from '../features/Collaborations/CollaborationApplicants';
import { ContentLayout } from '../layouts/ContentLayout';
import { CollaborationPositions } from '../features/Collaborations/CollaborationPositions';
import { CollaborationMembers } from '../features/Collaborations/CollaborationMembers';

export const ColabManagement = () => {
  const [page, setPage] = React.useState('home');

  let colabName = "ProjectVerse";

  const handlePageChange = (event: React.SyntheticEvent, newValue: string) => {
    setPage(newValue);
  };

  return (  
    <ContentLayout>

      <h2 className="text-4xl text-white"><span className="text-accent">{colabName}</span> {page}</h2>
      <Box sx={{ width: '100%' }}>
        <Tabs
          value={page}
          onChange={handlePageChange}
          textColor="secondary"
          indicatorColor="secondary"
          aria-label="secondary tabs example"
        >
          <Tab value="home" label="Home" />
          <Tab value="collaboration positions" label="Collaboration positions" />
          <Tab value="applicants" label="Applicants" />
          <Tab value="members" label="Members" />
        </Tabs>
      </Box>

      {page === "home" ? <ColabHome /> : ""}
      {page === "collaboration positions" ? <CollaborationPositions /> : ""}
      {page === "applicants" ? <CollaborationApplicants /> : ""}
      {page === "members" ? <CollaborationMembers /> : ""}

    </ContentLayout>
    
  );
}
