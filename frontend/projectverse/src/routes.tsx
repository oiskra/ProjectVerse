//Dependencies
import { Route, createBrowserRouter, createRoutesFromElements } from 'react-router-dom'

//Pages
import { MainLayout } from "./layouts/MainLayout"
import { CollaborationPage } from "./pages/CollaborationPage"
import { HomePage } from "./pages/HomePage"
import { FeedPage } from './pages/FeedPage';

const router = createBrowserRouter(
    createRoutesFromElements(
      <Route element={<MainLayout/>}>
        <Route index element={ <HomePage/> }></Route>
        <Route path="colabs" element={ <CollaborationPage/> }></Route>
        <Route path="feed" element={ <FeedPage/> }></Route>
      </Route>
    )
);


export default router
