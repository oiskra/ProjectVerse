import { ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import { useNavigate } from "react-router-dom";

const SideBarItem:React.FC<{name:string,expanded:boolean,href:string|null,Icon?:React.FC}> = ({name,Icon,expanded,href}) =>{

  const navigate = useNavigate();

  const handleRedirect = () =>{
    console.log("redirect triggered");
    console.log(`${href}`);
    if(href !== null ){
      console.log("redirect successfull")
      navigate(href)
    }
      
  }

  return( 
    <ListItemButton onClick={handleRedirect}>
      <ListItemIcon sx={{display:"flex",alignItems:"center",gap:1}}>
            {Icon &&
            <Icon sx={{color:"white"}}/>
            }
              
            {expanded && 
            <ListItemText sx={{color:"white"}} primary={name} />
            }
            
      </ListItemIcon>
    </ListItemButton>
  )

}

export default SideBarItem;