import React from 'react'
import { ColabListing } from '../features/Collaborations/ColabListing'
import { sampleCollaboration } from '../data/Collaboration'
import { ColabDescCard } from '../features/Collaborations/ColabDescCard'

export const CollaborationPage : React.FC<{}> = () => {


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
              <ColabListing colab={sampleCollaboration}/>
              <ColabListing colab={sampleCollaboration}/>
            </div>
          <div>

          </div>
          </div>
          <div className='w-2/5 h-full neo bg-background rounded-md'>
              <ColabDescCard colab={sampleCollaboration} />
          </div>
        </div>

    </div>

    </>
  )
}
