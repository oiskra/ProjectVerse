//Dependencies
import { Route, createBrowserRouter, createRoutesFromElements } from 'react-router-dom'

//Pages
import { MainLayout } from "./layouts/MainLayout"

import { HomePage } from "./pages/HomePage"
import { FeedPage } from './pages/FeedPage';
import { LoginPage } from './pages/LoginPage';

import { CreateCollab } from './pages/CreateCollab';
import { CollaborationApplicants } from './features/Collaborations/CollaborationDashboard/CollaborationApplicants';
import { ColabManagement } from './pages/ColabManagement';
import CollaborationPage from './pages/CollaborationPage';import CollabDescriptionPage from './pages/CollabDescriptionPage';

import PortfolioPage from './pages/PortfolioPage';
import AddProjectPage from './pages/AddProjectPage';
import ProjectPage from './pages/ProjectPage';

const router = createBrowserRouter(
    createRoutesFromElements(
      <Route element={<MainLayout/>}>
        <Route index element={ <LoginPage/> }></Route>
        <Route path="home" element={ <HomePage/> }></Route>


        {/* FIXME REWRITE COLLAB PATHS */}
        <Route path="collabs" element={ <CollaborationPage/> }></Route>
        <Route path="collab_card/:id" element={ <CollabDescriptionPage />}></Route>
        <Route path="new_collab" element={ <CreateCollab/> }></Route>
        <Route path="collab_dashboard/:id" element={ <ColabManagement/> }></Route>

        <Route path="portfolio" element={ <PortfolioPage/> }></Route>
        <Route path="portfolio/addProject" element={ <AddProjectPage/> }></Route>
        <Route path="portfolio/project/:id" element={ <ProjectPage/> }></Route>
        {/* <Route path="portfolio/:id" element={ <PortfolioSinglePage/> }></Route> */}

        <Route path="feed" element={ <FeedPage/> }></Route>
    
        
      </Route>
    )
);


export default router
