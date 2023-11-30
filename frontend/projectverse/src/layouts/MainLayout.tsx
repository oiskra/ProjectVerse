import { Outlet, useLocation } from 'react-router-dom'
import { SideNav } from '../features/Navigation/SideNav'

export const MainLayout = () => {

  const location = useLocation();

  const displaySideBar = location.pathname == "/" ? false : true;

  return (
    <>
      
        
      <main className='flex h-screen m0 overflow-hidden' id="contentWrapper">

        {displaySideBar && 
        <SideNav />  
        }
        
        <div className='w-fit p-5 flex-grow'>
        <Outlet></Outlet>
        </div>    

        

      </main>
    
    </>
    
  )
}
