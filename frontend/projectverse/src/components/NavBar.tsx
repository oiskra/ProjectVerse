import React from 'react';
import { Link, NavLink } from 'react-router-dom';

import logo from '../assets/logo.png'

export const NavBar = () => {
  return (
    <nav id="navBar" className="text-xl text-white flex items-center justify-around hover:text-accent">
      <div className='h-full flex gap-5 items-center'>
        <img className='h-full rounded-xl' src={logo} alt="" />
        <strong className='text-2xl'>ProjectVerse</strong>
      </div>


      <a>
        <NavLink to="/" >Home</NavLink>
      </a>
      <NavLink to="/Feed" >Feed</NavLink>
      <NavLink to="/Colabs" >Colab</NavLink>
      <input type="text" />

      <div className='h-full flex items-center gap-3'>

      <span>v</span>
      <img className='h-full p-5 rounded-full' src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRIH5efPMLE_yEELrFragrSk-6-gnwHI4qXyQ&usqp=CAU" alt="" />

      </div>

    </nav>
  )
}
