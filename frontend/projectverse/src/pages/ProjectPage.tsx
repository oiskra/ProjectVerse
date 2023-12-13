import React from 'react'
import { useParams } from 'react-router-dom'
import { useGetProjectQuery } from '../features/Portfolio/portfolioApiSlice';
import { Loader } from '../components/Loader';
import { Project } from '../data/Profile';

const ProjectPage = () => {

  const params = useParams()
  const id = params.id!;

  const {data,error,isLoading} = useGetProjectQuery(id);  

  if(isLoading){
    return <Loader />
  }

  const project = data as Project;

  return (

    <>
    <div>{data.name}</div>
    <iframe src={data.projectUrl} height="700px" width="100%" title="Iframe Example"></iframe>
    </>
    
  )
}

export default ProjectPage