import React from 'react'
import { useDispatch } from 'react-redux';
import { setCredentials } from './authSlice';

export const SessionSetter = () => { 

  let dispatch = useDispatch();
  let localStorageData = localStorage.getItem("credentials");
  
  if(!localStorageData){     
    return;    
  }

  let credentials = JSON.parse(localStorageData!);
  dispatch(setCredentials(credentials));

  return <></>

}
