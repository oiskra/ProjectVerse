import { useState } from 'react'
import { Login } from '../features/Auth/Login'
import { ButtonS } from '../CustomElements/ButtonS'
import { Register } from '../features/Auth/Register'

export const LoginPage = () => {

  const [LoginRegisterState,setState] = useState("login");


  return (
    <div className="flex justify-center items-center h-full">

      
        <div className="neo w-2/5 h-3/5 bg-background p-5 rounded flex">

          <div className="w-1/2 h-full" style={{borderRight:"solid 2px yellow"}}>
          {LoginRegisterState === "login" ? 

              <Login />
              : 
              <div className="">
                <h1 className="text-6xl black">Welcome</h1>
                <p className="">Sign up to unlock the full potential of your personal portfolio</p>
                <p className="">Need an account?</p>
                <ButtonS onClick={()=>{setState("login")}}>Sign in</ButtonS>
              </div>
              
            }
            
          </div>

          <div className="w-1/2 h-full">

            {LoginRegisterState === "login" ? 

              <div className="">
                <h1 className="text-6xl black">Welcome</h1>
                <p className="">Sign up to unlock the full potential of your personal portfolio</p>
                <p className="">Need an account?</p>
                <ButtonS onClick={()=>{setState("register")}}>Sign up</ButtonS>
              </div>
              : 
              <Register />
              
            }

            
            
          </div>
        </div>
    </div>
  )
}
