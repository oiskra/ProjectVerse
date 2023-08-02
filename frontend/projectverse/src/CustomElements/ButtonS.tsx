import { Button, TextField, styled } from "@mui/material";
import colorPaletteDark from "../colorPalette";


const sxProp ={
  backgroundColor:"#161616",
  color:colorPaletteDark.accent,
  fontWeight:"bold",
  fontSize:"1.3em",
  padding:"10px"
  
};

// const classes = {
//   "neo"
// }


export function ButtonS(props:any){
  return <Button {...props} className="neo text-accent py-10" sx={sxProp} ></Button>      
}

