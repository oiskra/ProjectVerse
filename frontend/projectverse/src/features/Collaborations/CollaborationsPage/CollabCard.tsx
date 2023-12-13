import React from 'react'
import Collaboration from '../../../data/Collaboration'
import { Button, Divider } from '@mui/material';
import Technology from '../../../data/Technology';
import { Difficulty } from './Difficulty';
import WorkIcon from '@mui/icons-material/Work';
import PersonIcon from '@mui/icons-material/Person';
import ConnectWithoutContactIcon from '@mui/icons-material/ConnectWithoutContact';
import { useNavigate } from 'react-router-dom';

const CollabCard: React.FC<{ collab: Collaboration }> = ({ collab }) => {

  const descLength = 100;
  const navigate = useNavigate();

  const cutDescription = collab.description.length > descLength ? `${collab.description.slice(0, descLength)}...` : collab.description;

  const redirectHandler = () =>{
    navigate(`/collab_card/${collab.id}`)
  }


  return (
    <div className=' flex flex-col gap-5 neo p-5 max-w-[500px] rounded-md bg-background'>
      <div className='flex gap-4'>
        <div className="flex flex-col gap-2 w-2/4">

          <div className='text-accent'>{new Date(collab.createdAt).toLocaleDateString()}</div>
          <div className="text-white text-4xl font-black">{collab.name}</div>
          <div className="text-white">by: <span className="text-accent">{collab.author.username}</span></div>
          <div className="text-white text-justify opacity-70">{cutDescription}</div>

        </div>

        <div className='p-1 neo p-4 rounded-md flex flex-col flex-grow justify-around text-white '>
          <div className='flex justify-between items-center'>
            Open positions:
            <div className='flex items-center gap-2'>
            <WorkIcon />
            {collab.collaborationPositions.length}
              
            </div>
            
          </div>
          <div className='flex justify-between items-center'>
            Members:
            
            <div className='flex items-center gap-2'>
            <PersonIcon />
            {collab.peopleInvolved}
            </div>
          </div>     

          <Difficulty difficulty={collab.difficulty} />         
        
          <div className='flex justify-between items-center'>
            Compatible:
            
            <div className='flex items-center gap-2'>
            <ConnectWithoutContactIcon />
            <span className='text-sucess'>yes</span>
            </div>
          </div> 

        </div>

      </div>

      <Divider />

      <div className='flex gap-5 flex-col flex-wrap'>

        <div className='flex gap-5 justify-center'>

          {collab.technologies.slice(0, 2).map((tech: Technology) =>
            <span key={tech.id} className='neo text-center rounded-xl flex-grow text-white p-3'>{tech.name}</span>
          )}

        </div>

        <Button sx={{ flexGrow: 1 }} onClick={redirectHandler}>Learn More</Button>
      </div>

    </div>
  )
}

export default CollabCard