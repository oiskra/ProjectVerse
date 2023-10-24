import React, { useEffect, useState } from 'react'
import { ColabListing } from '../features/Collaborations/ColabListing'
import Collaboration from '../data/Collaboration'
import { ColabDescCard } from '../features/Collaborations/ColabDescCard'
import {  useGetAllColabsMutation } from '../features/Collaborations/colabApiSlice'

export const CollaborationPage: React.FC<{}> = () => {

  const [colabList, setColabList] = useState([]);
  const [colabs,{isLoading}] = useGetAllColabsMutation();

  useEffect(() => {    
    
      (async () =>{
        setColabList(await colabs({}).unwrap())
      })();

  }, [])
  


  return (
    <>

      <div className=" h-full  items-center mx-20">


        <div className='h-full w-full flex justify-center gap-10 items-center p-5'>
          <div className='w-2/5 h-full flex flex-col justify-between  rounded-xl'>
            <div className="flex justify-around gap-5 pb-4">
              <button className="neo w-2/5 text-white px-6 py-2 rounded-md bg-background hover:text-accent transition-all">Technology</button>
              <button className="neo w-2/5 text-white px-6 py-2 rounded-md bg-background hover:text-accent transition-all">Difficulty</button>

              <div className='flex gap-5 justify-center'>
                <span className='text-white text-center'>Only your techstack</span>
                <input type="checkbox" />
              </div>

            </div>

            <div className="neo w-full bg-background h-full rounded-xl p-3 flex flex-col gap-3">
              {colabList.map((colab:Collaboration)=><ColabListing key={colab.ID} colab={colab} />)}             
            </div>
            
          </div>
          <div className='w-2/5 h-full neo bg-background rounded-md'>
            {/* <ColabDescCard colab={sampleCollaboration} /> */}
          </div>
        </div>

      </div>

    </>
  )
}
