import { Card, CardContent, Typography, CardActions, Button } from '@mui/material'
import React from 'react'
import { neoOverride } from '../customElements/overrideStyleGroups'

export const DashboardTile:React.FC<{header:string,count:number,lastWeekCount:number}> = ({header,count,lastWeekCount}) => {
  return (
    <Card sx={{...neoOverride,maxWidth:"300px",textAlign:"center","flex-grow":1,minWidth:"300px"}} className="neo" >
      <CardContent>
        <Typography sx={{ fontSize: "2em" }} gutterBottom>
          {header}
        </Typography>        
        
        <Typography sx={{fontSize:"3em"}} variant="body2">
          {count}
        </Typography>

        <Typography variant="body2">
          <span className='text-sucess'>+{lastWeekCount}</span> last week
        </Typography>
      </CardContent>   
    </Card>
  )
}
