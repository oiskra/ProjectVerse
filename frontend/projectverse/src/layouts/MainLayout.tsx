import { Outlet } from 'react-router-dom'
import { SideNav } from '../features/Navigation/SideNav'

export const MainLayout = () => {
  return (
    <>
      
        
      <main className='flex h-screen m0 overflow-hidden' id="contentWrapper">

        <SideNav />  
        <div className='w-fit flex-grow'>
        <Outlet></Outlet>
        </div>    

        

      </main>
    
    </>
    
  )
}
