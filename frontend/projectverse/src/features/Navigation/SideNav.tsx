import { useEffect, useState } from 'react'
import logo from '../../assets/logo.png'
import { List } from '@mui/material';
import { useSelector } from 'react-redux';
import User from '../../data/User';

import KeyboardDoubleArrowLeftOutlinedIcon from '@mui/icons-material/KeyboardDoubleArrowLeftOutlined';
import KeyboardDoubleArrowRightOutlinedIcon from '@mui/icons-material/KeyboardDoubleArrowRightOutlined';
import LogoutIcon from '@mui/icons-material/Logout';
import HomeIcon from '@mui/icons-material/Home';
import SideBarItem from './SideBarItem';
import SideBarLabelTab from './SideBarLabelTab';
import GroupsIcon from '@mui/icons-material/Groups';
import PersonIcon from '@mui/icons-material/Person';
import ContactPageIcon from '@mui/icons-material/ContactPage';
import ExploreIcon from '@mui/icons-material/Explore';
import NotificationsIcon from '@mui/icons-material/Notifications';
import { useGetUserCollabsMutation } from '../Collaborations/colabApiSlice';
import SideBarMutationList from './SideBarMutationList';
import Collaboration from '../../data/Collaboration';


export const SideNav = () => {
  
  const [expanded,setExpanded] = useState(true);
  const [data,setData] = useState({} as Collaboration)

  const [userCollabMutation] = useGetUserCollabsMutation()

  const handleExpand = () =>{
    setExpanded(!expanded);
  }

  const handleMouseOver = () =>{} //setExpanded(true)
  const handleMouseOut = () =>{} //setExpanded(false)

  const user:User  = useSelector((state:any) => state.auth.user)


  useEffect(() => {
    (async () =>{
      console.log("side nav triggered")
      setData(await userCollabMutation(user.id).unwrap())

    })()
    
  }, [])

  return (

    <aside onMouseOver={handleMouseOver} onMouseOut={handleMouseOut}
    className={`h-screen w-fit bg-background neo transition-all flex flex-col justify-between items-center" ${expanded ? 'w-60' : 'w-[50px]'}`}
    >
      <div className='flex flex-col py-4 gap-4 items-center'>
        <img src={logo} width={expanded ? 100 : 50} className='rounded-xl' alt="" />

        {expanded && 
          <h2 className='text-white text-2xl p-5 font-bold'>
            Project<span className='text-accent'>verse</span></h2>
        }

      </div>
      
      <nav className="h-fit">
        <List>

          <SideBarItem name="Home" href="/home" Icon={HomeIcon} expanded = {expanded}/>
          <SideBarItem name="Explore" href="/feed" Icon={ExploreIcon} expanded = {expanded}/>
          <SideBarItem name="Notifications" href={null} Icon={NotificationsIcon} expanded = {expanded}/>
          

          <SideBarLabelTab name="Collaborations" sideBarExpanded={expanded}  headerIcon={GroupsIcon}>

            <SideBarItem name="Discover" href="/collabs"  expanded = {expanded} />
            <SideBarItem name="Add new" href="/new_collab"  expanded = {expanded} />

            <SideBarLabelTab name="Your Collaborations" sideBarExpanded={expanded}>
              
              <SideBarMutationList data={data} baseHref='/collab_dashboard'/>

            </SideBarLabelTab>
            
          </SideBarLabelTab>

        </List>
      </nav>


      <div>

        <SideBarItem name="Account" href={null} expanded = {expanded} Icon={PersonIcon} />
        <SideBarItem name="Profile" href={null} expanded = {expanded} Icon={ContactPageIcon}/>
        <SideBarItem name="Logout" href={null} expanded = {expanded} Icon={LogoutIcon}/>

        <div className=' bg-black flex gap-6 py-3 p-5  justify-center text-white items-center'>

          <div className='flex gap-1 text-white items-center'>

            <img src={logo} className='rounded-full max-w-[50px]' alt="" />
            {expanded && 

            <div className='flex flex-col justify-center gap-2'>
              <span className="text-accent p-0 m-0">{user.username}</span>
              <span>{user.email}</span>
            </div> 
            
            }
            

          </div>
        
        </div>
      </div>
      
      
    </aside>

  )
}




