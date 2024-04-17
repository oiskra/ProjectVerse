import { TextField } from "@mui/material";


const sxProp ={

  color:"white",
  label:{color:"white"}

};

export function TextFieldS(props:any){
  return <TextField {...props} className="neo rounded xl" sx={sxProp} />      
}

