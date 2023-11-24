import React from 'react'
import CollaborationApplicant from '../../data/CollaborationApplicant';
import { ApplicantsList } from './CollaborationDashboard/ApplicantsList';

export const CollaborationMembers:React.FC<{members:CollaborationApplicant[]}> = ({members}) => {

    members = members.filter((x:CollaborationApplicant) => x.applicationStatus === 2);
  
  return (

    <ApplicantsList applicants={members} />
   
  )  
}
