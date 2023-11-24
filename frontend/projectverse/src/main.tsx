//Dependencies
import React from 'react'
import ReactDOM from 'react-dom/client'
import { RouterProvider} from 'react-router-dom'

//Styles
import './index.css'

//Pages
import { PageLoading } from './pages/PageLoading';
import router from './routes';
import { GlobalOverride } from './CustomElements/GlobalOverride';
import { Provider, useDispatch } from 'react-redux';
import store from './context/store';
import { setCredentials } from './features/Auth/authSlice';
import { SessionSetter } from './features/Auth/SessionSetter';


console.log("main triggered")

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>  
    <GlobalOverride />
    

    <Provider store={store}>
      <SessionSetter />
      <RouterProvider router={router} fallbackElement={ <PageLoading/> }/>
    </Provider>
    
  </React.StrictMode>,
)
