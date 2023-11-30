import React from 'react'
import { ApplicantsList } from './ApplicantsList'
import CollaborationApplicant from '../../../data/CollaborationApplicant'

export const CollaborationApplicants:React.FC<{applicants:CollaborationApplicant[]}> = ({applicants}) => {

  applicants = applicants.filter((x:CollaborationApplicant) => x.applicationStatus !== 2);
  
  return (

    <ApplicantsList applicants={applicants} />
   
  )
}
