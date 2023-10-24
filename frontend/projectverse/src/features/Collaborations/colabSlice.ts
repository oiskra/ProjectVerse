import { createSlice } from "@reduxjs/toolkit";
import Collaboration from "../../data/Collaboration";

const colabSlice = createSlice({
  name:'colab',
  initialState:{colabs:[]},
  reducers:{
    setColabs:(state,action) =>{
        const {colabs} = action.payload;
        state.colabs = colabs;
    },    
  }
})


// export const {setColabs,getColabs} = colabSlice.actions
export const {setColabs} = colabSlice.actions

export default colabSlice.reducer

// export const selectCurrentToken = (state:any) =>state.auth.token