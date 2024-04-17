import React from 'react';
import { Link, NavLink } from 'react-router-dom';

import logo from '../assets/logo.png'
import { selectCurrentUser } from '../features/Auth/authSlice';
import { useDispatch } from 'react-redux';

export const NavBar = () => {

  return (
    <nav id="navBar" className="text-xl text-white flex items-center justify-around hover:text-accent">
      <div className='h-full flex gap-5 items-center'>
        <img className='h-full rounded-xl' src={logo} alt="" />
        <div>
          <strong className='text-2xl'>ProjectVerse</strong>
          <div className='text-sm text-white/70'>Personal brand made easier</div>
        </div>
      </div>

      <NavLink to="/" >Home</NavLink>
      <NavLink to="/feed" >Feed</NavLink>
      <NavLink to="/colabs" >Colab</NavLink>
      <NavLink to="/new_colab" >temp new colab</NavLink>
      <NavLink to="/colab_dashboard/45cc78e3-9435-4511-b8dc-ac283b78e7e9" >temp dashboard</NavLink>
      
      <input type="text" />

      <div className='h-full flex items-center gap-3'>

        <span>v</span>
        
        <img className='h-full p-5 rounded-full' src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRIH5efPMLE_yEELrFragrSk-6-gnwHI4qXyQ&usqp=CAU" alt="" />

      </div>

    </nav>
  )
}
