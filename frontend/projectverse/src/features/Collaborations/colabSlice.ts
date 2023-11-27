import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import Collaboration from "../../data/Collaboration";

export const fetchCollabs = createAsyncThunk('collab/fetchCollabs', async (mutation) => {

  try {

    const response = await mutation().unwrap();
    return response;

  } 
  catch (error) {
    throw error;
  }
});

export const fetchSingle = createAsyncThunk('collab/fetchSingle',async (data) =>{


  try {
    const {colabMutation,id} = data;
    const response = await colabMutation(id).unwrap();
    return response;

  } 
  catch (error) {
    throw error;
  }
})

const colabSlice = createSlice({
  name:'collab',
  initialState:{
    collabs:[] as Collaboration[],
    currentCollab:null as Collaboration | null
  },
  reducers:{

    addCollab:(state,action) =>{
      action.payload;
      state.collabs.push(action.payload);
    },
    
    updateCollab:(state,action) => {
      const [id] = action.payload;
      const targetIndex = state.collabs.findIndex((collab:Collaboration)=> collab.id == id);
      state.collabs[targetIndex] = action.payload;
    },

    deleteCollab:(state,action) =>{
      const [id] = action.payload
      const targetIndex = state.collabs.findIndex((collab:Collaboration)=> collab.id == id);
      state.collabs.splice(targetIndex,1)
    }

  },
  extraReducers: (builder) => {   

    builder.addCase(fetchCollabs.fulfilled, (state, action) => {    
      state.collabs = action.payload;
    });

    builder.addCase(fetchCollabs.rejected, (state, action) => {      
      console.error('Failed to fetch collaborations:', action.error);
    });

    builder.addCase(fetchSingle.fulfilled, (state, action) => {    
      state.currentCollab = action.payload;
    });

    builder.addCase(fetchSingle.rejected, (state, action) => {      
      console.error('Failed to fetch collaboration:', action.error);
    });

  },})



export const {
  addCollab
} = colabSlice.actions

export default colabSlice.reducer

export const getCollabs = (state) => state.collab.collabs
export const getCurrentColab = (state) => state.collab.currentCollab


// export const getCollab = (state,id:string) => state.colab.find((collab:Collaboration) => id == collab.id)
// export const getUserColabs = (state,id:string) => state.colab.filter()

