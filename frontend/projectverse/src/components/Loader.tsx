import React from 'react'

export const Loader = () => {
  return (
    <>
      <div className="w-full h-full flex flex-col justify-center items-center">
        <h2 className='text-white'>We are processing your request</h2>
      <div className="lds-ellipsis"><div></div><div></div><div></div><div></div></div>

      </div>

    </>
  )
}
