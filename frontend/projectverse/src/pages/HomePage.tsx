import { GlobalStyles } from "@mui/material"
import { Login } from "../features/Auth/Login"
import { selectCurrentToken } from "../features/Auth/authSlice"
import { useDispatch } from "react-redux"
import store from "../context/store"
import FormikJSXGenerator from "../lib/FormikJSXGenerator"
import LoginSchema from "../data/ValidationSchemas/LoginSchema"


const test = {
  onSubmit:() =>{

  }
}

export const HomePage = () => {

  
  return (
    <>   
     {/* <FormikJSXGenerator data={test}  validationSchema ={LoginSchema} /> */}
    </>
  )
}



