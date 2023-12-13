import { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { PORTFOLIO_GetUserProjects } from '../features/Portfolio/portfolioThunks';
import { Project } from '../data/Profile';
import ProjectCard from '../features/Portfolio/ProjectCard';
import { Loader } from '../components/Loader';
import CodeIcon from '@mui/icons-material/Code';

const PortfolioPage = () => {

  const dispatch = useDispatch();
  const user = useSelector((state:any) => state.auth.user)
  const projects = useSelector((state:any) => state.portfolio.projects);

  const [isLoading ,setIsLoading] = useState(true);

  useEffect(()=>{
    // dispatch(PORTFOLIO_GetUserProjects(user.id));
    dispatch(PORTFOLIO_GetUserProjects(user.id)).then(()=>{setIsLoading(false)});
  },[dispatch]);
  

  if(isLoading)
    return <Loader />


  return (

    <>
      <h1 className='text-accent text-3xl flex items-center gap-5'>        
        <CodeIcon style={{fontSize:"2em",color:"whitesmoke"}}/>
        Portfolio
      </h1>


      <div className="mt-20 p-5 flex flex-wrap gap-5">
        {projects.map((project:Project)=><ProjectCard key={project.id} project={project} />)}
      </div>
    </>


  )
}

export default PortfolioPage