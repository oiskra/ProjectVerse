import { configureStore} from '@reduxjs/toolkit'
import { apiSlice } from '../API Services/apiSlice';
import authReducer from '../features/Auth/authSlice'
import colabSlice from '../features/Collaborations/colabSlice';
import colabReducer from '../features/Collaborations/colabSlice'

const store = configureStore({

  reducer:{
    [apiSlice.reducerPath]: apiSlice.reducer,
    auth:authReducer,
    [colabSlice.reducerPath]: colabSlice.reducer,
    collab:colabReducer
  },
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware().concat(apiSlice.middleware),
  devTools:true
})

export default store;