//Dependencies
import { Route, createBrowserRouter, createRoutesFromElements } from 'react-router-dom'

//Pages
import { MainLayout } from "./layouts/MainLayout"
import { CollaborationPage } from "./pages/CollaborationPage"
import { HomePage } from "./pages/HomePage"
import { FeedPage } from './pages/FeedPage';
import { LoginPage } from './pages/LoginPage';
import { CreateColab } from './pages/CreateColab';
import { ColabDashboard } from './features/Collaborations/ColabHome';
import { CollaborationApplicants } from './features/Collaborations/CollaborationApplicants';
import { ColabManagement } from './pages/ColabManagement';

const router = createBrowserRouter(
    createRoutesFromElements(
      <Route element={<MainLayout/>}>
        <Route index element={ <LoginPage/> }></Route>
        <Route path="home" element={ <HomePage/> }></Route>
        <Route path="colabs" element={ <CollaborationPage/> }></Route>
        <Route path="feed" element={ <FeedPage/> }></Route>
        <Route path="new_colab" element={ <CreateColab/> }></Route>
        <Route path="colab_dashboard" element={ <ColabManagement/> }></Route>
        <Route path="colab_applicants" element={ <CollaborationApplicants/> }></Route>
        <Route path="colab_members" element={ <CollaborationApplicants/> }></Route>
      </Route>
    )
);


export default router
