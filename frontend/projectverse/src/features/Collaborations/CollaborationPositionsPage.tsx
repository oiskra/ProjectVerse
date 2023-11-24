import React, { useState } from 'react'
import CollaborationPositions from '../../data/CollaborationPosition'
import { Card, CardContent, Typography, Box, Container } from '@mui/material'
import AddBoxIcon from '@mui/icons-material/AddBox';

import { ColabPosForm } from './CollaborationDashboard/ColabPosForm';
import CollaborationPosition from '../../data/CollaborationPosition';
import { neoOverride } from '../../CustomElements/overrideStyleGroups';


export const CollaborationPositionsPage:React.FC<{positions:CollaborationPosition[]}> = ({positions}) => {

  const [modalState,setModalState] = useState(false);

  const handleOpen  = () => setModalState(true);
  const handleClose = () => setModalState(false);  

  return (
      <>

      {modalState === true ? 
  
        <div className='absolute left-0 top-0 h-0 w-screen h-screen p-5 h-full grid place-items-center'>
            
          <div onClick={handleClose} className='absolute bg-background left-0 top-0 h-0 w-screen h-screen p-5 h-full grid items-center z-10'></div>

          <ColabPosForm /> 
          
        </div>
     

      : <></>}
      
      
      {positions.map((position:CollaborationPosition) =>   
        <Card key={position.id} sx={neoOverride}>
            <CardContent>
              <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                {position.name}
              </Typography>
              
              <Typography variant="body2">
                {position.description}
              </Typography>

              {/* <Typography variant="body2">
                applicants: 14
              </Typography> */}

            </CardContent>
          
          </Card>
      ) }

        <div className="flex gap-5">

          <Card sx={{ height:100, width:100, display:"grid", placeItems:"center" }}>
            <CardContent onClick={handleOpen}>              
              <AddBoxIcon  sx={{fontSize:50}} />
            </CardContent>        
          </Card>

        </div>
    </>
  )
}

