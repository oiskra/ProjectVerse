import React from 'react'
import { Login } from '../features/Auth/Login'
import { Loader } from '../components/Loader'

export const LoginPage = () => {
  return (
    <div className="flex justify-center items-center h-full">

      
        <div className="neo w-2/5 h-3/5 bg-background p-5 rounded flex">

          <div className="w-1/2 h-full" style={{borderRight:"solid 2px yellow"}}>
            <Login />
          </div>

          <div className="w-1/2 h-full">
            
          </div>
        </div>
    </div>
  )
}
