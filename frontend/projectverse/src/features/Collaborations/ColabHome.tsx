import React from 'react'
import { ContentLayout } from '../../layouts/ContentLayout';
import { DashboardTile } from '../../components/DashboardTile';
import { ApplicantsList } from '../../components/ApplicantsList';
import { NavLink } from 'react-router-dom';
import { Select, MenuItem, Box, Tab, Tabs } from '@mui/material';


export const ColabHome:React.FC<{}> = () => {

  let id = 1;

  let data = [
    {
    id:1,
    name:"test",
    score:24
    },
    {
      id:2,
      name:"test2",
      score:24
    }
  ];

  return (
    <>
    
     <section className="flex p-3">
       <div className='p-5 flex w-1/2 flex-wrap gap-5'>
          <DashboardTile header={"Applicants"} count={25} lastWeekCount={7}/>
          <DashboardTile header={"Members"} count={5} lastWeekCount={2}/>
          <DashboardTile header={"Rejected applicants"} count={5} lastWeekCount={2}/>
          <DashboardTile header={"Random data go brrr"} count={999} lastWeekCount={125}/>
       
       
       
       </div>
       <div className="w-1/2 neo p-5 rounded bg-background">
          {/* <ApplicantsList applicants={data}/> */}
          <h2 className="text-white text-2xl p-3">Last aplicants</h2>
          <ApplicantsList />
        </div>
     </section>
  
    </>  
  )
}
