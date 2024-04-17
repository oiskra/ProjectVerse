import React from 'react'
import CollaborationApplicant from '../../../data/CollaborationApplicant';
import { ApplicantRow } from './ApplicantRow';



export const ApplicantsList:React.FC<{applicants : CollaborationApplicant[]}> = ({applicants}) => {

  
 
  return (

      <div className="w-full">
        <div className="">
          <span></span>
        </div>
        {applicants.map((applicant:CollaborationApplicant) =>

          <ApplicantRow applicant={applicant} />
        )}

      </div>
    
  )
}
