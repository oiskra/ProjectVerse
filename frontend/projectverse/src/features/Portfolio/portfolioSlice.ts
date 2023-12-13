import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Project } from "../../data/Profile";
import { PORTFOLIO_GetUserProjects } from "./portfolioThunks";

export const portfolioSlice = createSlice({
  name:'portfolio',

  initialState:{
    projects:[] as Project[],
    currentProject:null as Project | null
  },

  reducers:{

    setProjects:(state,action:PayloadAction<Project[]>) =>{
      state.projects = action.payload;
    },
 
    addProject:(state,action) =>{     
      state.projects.push(action.payload);
    }

  },

  extraReducers: (builder) => {   
    // builder
    // .addCase(PORTFOLIO_GetUserProjects.fulfilled,(state) =>{
    //   console.log("ADD PROJECT EXTRA REDUCER TRIGGERED")
    // })
  },

})



export const {
  addProject
} = portfolioSlice.actions

export default portfolioSlice.reducer


