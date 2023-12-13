import { portfolioApiSlice } from "./portfolioApiSlice";
import {portfolioSlice} from "./portfolioSlice";

export function PORTFOLIO_GetUserProjects(userID:string){  

  return async function getUserProjectsThunk(dispatch:any) {
    const response = await dispatch(portfolioApiSlice.endpoints.getUserProjects.initiate(userID));
    dispatch(portfolioSlice.actions.setProjects(response.data));
  }

}


//Pomysł
// export function PORTFOLIO_GetUserProjects(userID:string){  
//   console.log("outside thunk trigerred")
//   const GetUserThunk = createAsyncThunk(
//     'portfolio/getUserProjects',
//     async () => {
//       console.log("async thunk triggered")
//       const response = await store.dispatch(getUserProjects(userID))
//       // const response = await dispatch(portfolioApiSlice.endpoints.getUserProjects.initiate(userID));
//       return response.data
//     }
//   )

//   return GetUserThunk

// }


//TODO refactor this to match new thunk structure
// export const fetchSingleProject = createAsyncThunk('posts/fetchPosts', async (promise:Promise<Project>) => {
//   throw new Error("not implemented");
//   const response = await promise;
//   return response;
// });

export function PORTFOLIO_AddProject(project:any){

  return async function addProjectThunk(dispatch:any){    
    const response = await dispatch(portfolioApiSlice.endpoints.addProject.initiate(project));

    //FIXME wait for back refactor 
    //back should return newly created project
    // console.log(response);
    // dispatch(portfolioSlice.actions.addProject(project))
    
  }

}