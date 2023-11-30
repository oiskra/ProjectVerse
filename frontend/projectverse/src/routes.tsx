//Dependencies
import { Route, createBrowserRouter, createRoutesFromElements } from 'react-router-dom'

//Pages
import { MainLayout } from "./layouts/MainLayout"
import { CollaborationPageOld } from "./pages/CollaborationPageOld"
import { HomePage } from "./pages/HomePage"
import { FeedPage } from './pages/FeedPage';
import { LoginPage } from './pages/LoginPage';
import { CreateCollab } from './pages/CreateCollab';
import { CollaborationApplicants } from './features/Collaborations/CollaborationDashboard/CollaborationApplicants';
import { ColabManagement } from './pages/ColabManagement';
import CollaborationPage from './pages/CollaborationPage';
import CollabDescriptionPage from './pages/CollabDescriptionPage';

const router = createBrowserRouter(
    createRoutesFromElements(
      <Route element={<MainLayout/>}>
        <Route index element={ <LoginPage/> }></Route>
        <Route path="home" element={ <HomePage/> }></Route>
        <Route path="collabs" element={ <CollaborationPage/> }></Route>
        <Route path="collab_card/:id" element={ <CollabDescriptionPage />}></Route>
        <Route path="feed" element={ <FeedPage/> }></Route>
        <Route path="new_collab" element={ <CreateCollab/> }></Route>
        <Route path="collab_dashboard/:id" element={ <ColabManagement/> }></Route>
      </Route>
    )
);


export default router
