import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
  name:'auth',
  initialState:{user:null,token:null},
  reducers:{
    setCredentials:(state,action) =>{;
      // const {user,token} = action.payload
      const {accessToken,refreshToken} = action.payload;

      //decode 
      let base64Url = accessToken.split(".")[1];
      let data = base64Url.replace("-", "+").replace("_", "/");    
  
      state.user = JSON.parse(window.atob(data));
      state.token = accessToken;      

      localStorage.setItem("credentials",JSON.stringify({accessToken,refreshToken}));

    },
    logOut :(state) => {
      state.user = null;
      state.token = null;
    }    
  }
})


export const {setCredentials,logOut} = authSlice.actions

export default authSlice.reducer

export const selectCurrentUser = (state:any) => state.auth.user
export const selectCurrentToken = (state:any) => state.auth.token