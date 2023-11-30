import { configureStore} from '@reduxjs/toolkit'
import { apiSlice } from '../API Services/apiSlice';
import authReducer from '../features/Auth/authSlice'
import collabSlice from '../features/Collaborations/collabSlice';
import collabReducer from '../features/Collaborations/collabSlice'

const store = configureStore({

  reducer:{
    [apiSlice.reducerPath]: apiSlice.reducer,
    auth:authReducer,
    [collabSlice.reducerPath]: collabSlice.reducer,
    collab:collabReducer
  },
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware().concat(apiSlice.middleware),
  devTools:true
})

export default store;