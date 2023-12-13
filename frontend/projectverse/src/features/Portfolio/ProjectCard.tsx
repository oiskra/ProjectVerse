import React from 'react'
import { Project } from '../../data/Profile'
import img from '../../assets/TechnologiesIcons/react.png'
import Technology from '../../data/Technology'
import { Button } from '@mui/material'
import { useNavigate } from 'react-router-dom'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconMacroParams, brands, icon, solid } from '@fortawesome/fontawesome-svg-core/import.macro'
import { IconName } from '@fortawesome/free-solid-svg-icons'

import { library } from '@fortawesome/fontawesome-svg-core'

import { fab } from '@fortawesome/free-brands-svg-icons'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { technologiesListIcons } from '../../data/tempTechnologiesList'

// This exports the whole icon packs for Brand and Solid.
library.add(fab, fas)




const ProjectCard:React.FC<{project:Project}> = ({project}) => {

  const navigate = useNavigate();

  let techCount = 3;

  const redirectHandler = () =>{
    navigate(`/portfolio/project/${project.id}`)
  }


  const iconTechnology = project.usedTechnologies[0].name;


  const techIconObj = technologiesListIcons.find(x=>x.technologyName == iconTechnology);
  const iconName = techIconObj?.fontawesomeIconName.split(" ")
  const color = techIconObj?.iconColor;

  console.log(`${techIconObj?.technologyName}: text-[${color}]`);



  return (  

      <div className='glass bg-glassMorph p-16 flex'>
        <div className='bg-background rounded-xl neo p-5 max-w-[300px] relative text-white flex flex-col justify-between gap-5'>
          {/* <img className='w-1/2 absolute translate-x-[-50%] left-1/2 origin-center top-[-50px]' src={img} alt="" /> */}
        
          {/* <FontAwesomeIcon icon={icon(name:'react' style:'brand' family:'classic')} /> */}
          {/* <FontAwesomeIcon className={`h-[90px] absolute translate-x-[-50%] left-1/2 origin-center top-[-50px] text-[#68A063]`} icon={[iconName[0],iconName[1]]} /> */}
          <div style={{color:color}}>
            <FontAwesomeIcon className="h-[90px] absolute translate-x-[-50%] left-1/2 origin-center top-[-50px]" icon={[iconName[0],iconName[1]]}/>
          </div>
          <div className="mt-[25px]  flex flex-col gap-5">
            <h2 className="text-2xl my-3 font-bold text-center">{project.name}</h2>
            <div className="flex gap-2 flex-wrap justify-center">
              {project.usedTechnologies.slice(0,techCount).map((tech:Technology)=>{
                return <span key={tech.id} className='w-fit text-center flex-grow text-accent bg-background neo p-2 rounded-xl'>{tech.name}</span>
              })}
            </div>
        
        
          </div>
          <Button className='w-full absolute bottom-0 p-5 bg-background neo rounded-xl' onClick={redirectHandler}>View Project</Button>
        </div>
      </div>

  )
}

export default ProjectCard