import React, { useEffect, useState } from 'react'
import Collaboration from '../../data/Collaboration'
import Technology from '../../data/Technology'
import CollaborationPositions from '../../data/CollaborationPosition'
import img from '../../assets/logo.png'
import ribbon from '../../assets/ribbon.png'
import CollaborationPosition from '../../data/CollaborationPosition'
import { usePostColabPosApplyMutation } from './colabApiSlice'

export const ColabDescCard: React.FC<{ colab: Collaboration }> = ({ colab }) => {

  const [apply] = usePostColabPosApplyMutation();
 
  const [targetDesc, setTargetDesc] = useState({id:"XD",name:"test",description:"XD"} as CollaborationPosition | null);

  const [applySlider,setApplySlider] = useState(false);

  useEffect(() => {
    setTargetDesc(colab.collaborationPositions[0]);
  }, [])

  const applyHandler = (colabPosID:string, ColabID:string) =>{
    console.log(colabPosID,ColabID)
    apply({colabPosID,ColabID});
  }
  

  return (
    <div key={colab.id} className='w-full h-full flex flex-col gap-4 items-center text-white p-3 relative animate-fadeIn'>
      <div className='absolute w-full -top-1 h-1/6 z-20'>
        <img src={ribbon} alt="" />
        {/* TODO RIBBON HERE */}
      </div>

      <div className='relative w-full h-1/6 text-black z-30'>

        <div className="absolute left-5 top-8 text-4xl font-bold">
          {colab.name}
          {/* OPIS APKI MAMY TUTAJ ALE NIE WIEM BO NIE MAMY TEGO W MODELACH FIXME */}
        </div>

        <div className="absolute right-3 top-8 text-2xl flex gap-5 items-center">
          {colab.author.username}
          <img className='w-12 border rounded-full' src={img} />

        </div>

      </div>
      <div className='neo text-xl w-full p-5 pt-20 rounded-xl'>

        <h2>
          About the <span className='text-accent'>Project</span>
        </h2>

        <p className='text-sm opacity-70 py-2 text-justify'>
          {colab.description}
        </p>


      </div>
      <div className='neo text-xl w-full py-5 px-5 rounded-xl'>

        <h2 className=' pb-4'>
          <span className='text-accent'>Technologies</span> we use
        </h2>

        <div className='flex justify-start gap-3'>
          {colab.technologies.map((tech: Technology) => {
            return <div key={tech.id} className='neo p-3 text-sm rounded-xl'>{tech.name}</div>
          })}
        </div>


      </div>
      <div className='neo text-xl w-full p-5 rounded-xl h-2/5 flex flex-wrap gap-10'>

        <div className='w-3/5 text-justify justify-between'>
          <h2 className='w-full'>
            <span className='text-accent'>Who</span> we'll need
          </h2>
          <p style={{ fontSize: "0.7em", lineHeight: "1.4em" }} className='w-full opacity-70 text-justify justify-between animate-fadeIn'>
            {targetDesc!.description}
          </p>
        </div>

        <div className='flex p-1 justify-start gap-2 w-2/6 h-full flex-col transition-all overflow-scroll' style={{ cursor: "pointer" }}>
          {colab.collaborationPositions.map((pos: CollaborationPositions) => {
            return pos.id === targetDesc!.id ?
              <div 
                key={pos.id}              
                onClick={() => { setTargetDesc(pos) }}
                className='neo rounded-xl w-full h-1/8 transition-all overflow-hidden relative flex-col flex gap-5'
                onMouseOver={()=>setApplySlider(true)}  
                onMouseOut={()=>setApplySlider(false)}  

                style={{ fontSize: "0.7em", border: "solid 2px #FFC328" }}> 
                <div className="w-full p-3 h-full relative">{pos.name}</div>
                {applySlider ? <div onClick = {() =>{applyHandler(pos.id!,colab.id)}} className="w-full h-full absolute bg-accent text-background text-2xl font-black animate-slideIn flex items-center justify-center">Apply</div> : ""}
                
              </div>
              :
              <div key={pos.id}
                onClick={() => { setTargetDesc(pos) }}
                className='neo p-3 rounded-xl w-full'
                style={{ fontSize: "0.7em" }}>{pos.name}</div>

          })}
        </div>


      </div>
    </div>

  )
}
