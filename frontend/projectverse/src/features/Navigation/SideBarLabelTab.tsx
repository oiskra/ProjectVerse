import { Collapse } from "@mui/material";
import { useEffect, useState } from "react";
import SideBarItem from "./SideBarItem";

const SideBarLabelTab:React.FC<{name:string,children:JSX.Element[] | JSX.Element,sideBarExpanded:boolean,headerIcon?:React.FC}> = ({name,children,sideBarExpanded,headerIcon}) =>{

  const [expanded, setExpanded] = useState(false)

  useEffect(() => {
    if(sideBarExpanded == false)
    setExpanded(sideBarExpanded);
  
  }, [sideBarExpanded])
  

  const handleExpand = () =>{
    setExpanded(!expanded);
  }

  return(
    <>

      <div onClick={handleExpand}>
      <SideBarItem name={name} href={null} expanded = {sideBarExpanded} Icon = {headerIcon}/>
      </div>

      <Collapse in={expanded} unmountOnExit>
        <div className='border-l border-white ml-5'>
        {children}
        </div>
              
      </Collapse>

    </>

  )
  
}

export default SideBarLabelTab;