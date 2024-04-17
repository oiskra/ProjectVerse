import React from 'react'
import { ContentLayout } from '../../layouts/ContentLayout';
import { DashboardTile } from '../../components/DashboardTile';
import { ApplicantsList } from './CollaborationDashboard/ApplicantsList';
import { NavLink } from 'react-router-dom';
import { Select, MenuItem, Box, Tab, Tabs } from '@mui/material';
import Collaboration from '../../data/Collaboration';


export const ColabHome:React.FC<{data:Collaboration}> = ({data}) => {

  let id = 1;

  return (
    <>
    
     <section className="flex p-3">
       <div className='p-5 flex w-1/2 flex-wrap gap-5'>

          <DashboardTile 
            header={"New applicants"} 
            count={data.collaborationApplicants.filter(x=>x.applicationStatus === 1).length} 
            lastWeekCount={7}
          />

          <DashboardTile 
            header={"Members"}
            count={data.collaborationApplicants.filter(x=>x.applicationStatus === 2).length} 
            lastWeekCount={2}
          />

          <DashboardTile 
            header={"Rejected applicants"}
            count={data.collaborationApplicants.filter(x=>x.applicationStatus === 0).length} 
            lastWeekCount={2}
          />
          
          <DashboardTile header={"Random data go brrr"} count={999} lastWeekCount={125}/>
       
       
       
       </div>
       <div className="w-1/2 neo p-5 rounded bg-background">
          {/* <ApplicantsList applicants={data}/> */}
          <h2 className="text-white text-2xl p-3">Last aplicants</h2>
          
        </div>
     </section>
  
    </>  
  )
}
