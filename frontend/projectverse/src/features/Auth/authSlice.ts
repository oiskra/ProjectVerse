import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
  name:'auth',
  initialState:{user:null,token:null},
  reducers:{
    setCredentials:(state,action) =>{;
      // const {user,token} = action.payload
      const {token} = action.payload;

      //decode 
      let base64Url = token.split(".")[1];
      let data = base64Url.replace("-", "+").replace("_", "/");    
  
      state.user = JSON.parse(window.atob(data));
      state.token = token;

    },
    logOut :(state,action) =>{
      state.user = null;
      state.token = null;
    }    
  }
})


export const {setCredentials,logOut} = authSlice.actions

export default authSlice.reducer

export const selectCurrentUser = (state:any) => state.auth.user
export const selectCurrentToken = (state:any) => state.auth.token