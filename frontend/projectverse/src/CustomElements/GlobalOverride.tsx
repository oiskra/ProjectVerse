import { GlobalStyles } from '@mui/material'
import React from 'react'
import colorPaletteDark from '../colorPalette'

const globalStyles = {

  button:{
    border:"yellow",
    color:colorPaletteDark.accent,
    backgroundColor:colorPaletteDark.background
  },
  input:{
    color:"yellow"
  }
  // label:{
  //   color:"white",
  // },
  
}


export const GlobalOverride = () => {
  return (
    <GlobalStyles styles={globalStyles} />
  )
}
