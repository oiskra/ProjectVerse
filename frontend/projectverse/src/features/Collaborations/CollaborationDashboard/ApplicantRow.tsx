import React, { useState } from 'react'
import AccountCircleRoundedIcon from '@mui/icons-material/AccountCircleRounded';
import CollaborationApplicant from '../../../data/CollaborationApplicant';

import DoNotDisturbIcon from '@mui/icons-material/DoNotDisturb';
import WarningIcon from '@mui/icons-material/Warning';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';

import { ButtonS } from '../../../CustomElements/ButtonS';
import { Button, Popover, Typography } from '@mui/material';
import { usePatchApplicationStatusMutation } from '../colabApiSlice';

export const ApplicantRow:React.FC<{applicant:CollaborationApplicant}> = ({applicant}) => {

  const [anchorEl, setAnchorEl] = useState<HTMLButtonElement | null>(null);
  const [status,setStatus] = useState(applicant.applicationStatus)

  const [changeStatus,{isLoading}] = usePatchApplicationStatusMutation();

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleStatusChange = (applicantID:string,status:number) =>{

    changeStatus({applicantID,status}).then(()=>{
      setStatus(status);
    });
    
  }

  const open = Boolean(anchorEl);
  const id = open ? 'simple-popover' : undefined;

  return (    
    <div className="w-full flex justify-between text-white">

            

    <div className="min-w-2xl flex">

      <AccountCircleRoundedIcon sx={{fontSize:60}}/>

      <div className='flex flex-col justify-around p-1'>
        <div className='text-lg text-accent'>{applicant.applicantUserName}</div>
        <div className='opacity-70'>{applicant.applicantEmail}</div>
      </div>

    </div>

    <div className="w-15 gap-5 flex items-center">
      <div>{new Date(applicant.appliedOn).toLocaleString()}</div>
      {status === 0 ? <div><DoNotDisturbIcon sx={{color:"red"}} /> Rejected</div> : <></>}
      {status === 1 ? <div><WarningIcon sx={{color:"yellow"}} /> Pending</div> : <></>}
      {status=== 2 ? <div><CheckCircleIcon sx={{color:"green"}} /> Accepted</div> : <></>}
    

    <ButtonS aria-describedby={applicant.applicantUserId} variant="contained" onClick={handleClick}>
      Change status
    </ButtonS>

    <Popover
      id={applicant.applicantUserId}
      open={open}
      anchorEl={anchorEl}
      onClose={handleClose}
      anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'left',
      }}
    >
      <Button onClick={()=>{handleStatusChange(applicant.id,2)}} sx={{color:"black"}} variant="text">Accept</Button>
      <Button onClick={()=>{handleStatusChange(applicant.id,1)}} sx={{color:"black"}} variant="text">Pending</Button>
      <Button onClick={()=>{handleStatusChange(applicant.id,0)}} sx={{color:"black"}} variant="text">Rejected</Button>      
      
      
    </Popover>
    </div>

 </div> 
  )
}
