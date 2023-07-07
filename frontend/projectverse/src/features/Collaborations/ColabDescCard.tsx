import React, { useState } from 'react'
import Collaboration from '../../data/Collaboration'
import Technology from '../../data/Technology'
import CollaborationPositions from '../../data/CollaborationPosition'
import img from '../../assets/logo.png'
import ribbon from '../../assets/ribbon.png'

export const ColabDescCard:React.FC<{colab:Collaboration}> = ({colab}) => {

  const [targetDesc,setTargetDesc] = useState(0)
  return (
    <div className='w-full h-full flex flex-col gap-4 items-center text-white p-3 relative'>
      <div className='absolute w-full -top-1 h-1/6 z-20'>
          <img src={ribbon} alt="" />
          {/* TODO RIBBON HERE */}
        </div>

      <div className='relative w-full h-1/6 text-black z-30'>
        
        <div className="absolute left-5 top-8 text-4xl font-bold">
          {colab.Name}
          {/* OPIS APKI MAMY TUTAJ ALE NIE WIEM BO NIE MAMY TEGO W MODELACH FIXME */}
        </div>

        <div className="absolute right-3 top-8 text-2xl flex gap-5 items-center">
          {colab.Author.Username}
          <img className='w-12 border rounded-full' src={img}/>
         
        </div>
      
      </div>
      <div className='neo text-xl w-full p-5 pt-20 rounded-xl'>

        <h2>
          About the <span className='text-accent'>Project</span>
        </h2>
    
        <p className='text-sm opacity-70 py-2 text-justify'>
        {colab.Description}
        </p>
        

      </div>
      <div className='neo text-xl w-full py-5 px-5 rounded-xl'>

        <h2 className=' pb-4'>
            <span className='text-accent'>Technologies</span> we use
        </h2>

        <div className='flex justify-start gap-3'>
          {colab.Technologies.map((tech:Technology)=>{
              return <div className='neo p-3 text-sm rounded-xl'>{tech.Name}</div>
          })}
        </div>

        
      </div>
      <div className='neo text-xl w-full p-5 rounded-xl flex flex-wrap gap-10'>

         
        <div className='w-3/5 text-justify justify-between'>
        <h2 className='w-full'>
            <span className='text-accent'>Who</span> we'll need
        </h2>  
        <p style={{fontSize:"0.7em",lineHeight:"1.4em"}} className='w-full opacity-70 text-justify justify-between'>
          {colab.CollaborationPositions[targetDesc].Description}
        </p>
        </div>     

        

        <div className='flex justify-start gap-2 w-2/6 flex-col transition-all' style={{cursor:"pointer"}}>
          {colab.CollaborationPositions.map((pos:CollaborationPositions)=>{
              return pos.ID === targetDesc ? 
              <div 
              onClick={()=>{setTargetDesc(pos.ID)}} 
              className='neo p-3 rounded-xl w-full' 
              style={{fontSize:"0.7em",border:"solid 2px #FFC328"}}>{pos.Name}
              </div>
              :
              <div 
              onClick={()=>{setTargetDesc(pos.ID)}} 
              className='neo p-3 rounded-xl w-full' 
              style={{fontSize:"0.7em"}}>{pos.Name}</div>
              
          })}
        </div>

        
      </div>
    </div>
    
  )
}
