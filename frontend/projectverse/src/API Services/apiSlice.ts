import {BaseQueryApi, createApi,fetchBaseQuery} from '@reduxjs/toolkit/query/react'
import { logOut, setCredentials } from '../features/Auth/authSlice'

const baseQuery = fetchBaseQuery({
  baseUrl: 'http://localhost:3000/api',
  credentials:'include',
  prepareHeaders: (headers:Headers,{getState}) =>{

    const token = getState().auth.token;   

    headers.set('Content-Type',"application/json");
    headers.set('origin',"http://localhost:5173");    

    if(token){        
      headers.set('Authorization',`Bearer ${token}`)      
    }
    return headers
  }
})

const baseQueryWithReauth = async (args:any,api:BaseQueryApi,extraOptions:any) =>{
  let result = await baseQuery(args,api,extraOptions)

  if(result?.error?.originalStatus === 403){
    const refreshResult = await baseQuery('/refresh',api,extraOptions);
    console.log(refreshResult);
    if(refreshResult?.data){
      const user = api.getState().auth.user
      api.dispatch(setCredentials({...refreshResult.data,user}))
      

      result = await baseQuery(args,api,extraOptions)
    }
    else{
      api.dispatch(logOut({}))
    }
  }
  return result
}


export const apiSlice = createApi({
  baseQuery:baseQuery,
  endpoints:builder =>({})
}) 