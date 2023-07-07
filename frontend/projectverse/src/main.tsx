//Dependencies
import React from 'react'
import ReactDOM from 'react-dom/client'
import { RouterProvider} from 'react-router-dom'

//Styles
import './index.css'

//Pages
import { PageLoading } from './pages/PageLoading';
import router from './routes';




ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>  
    <RouterProvider router={router} fallbackElement={ <PageLoading/> }/>
  </React.StrictMode>,
)
