import React, { PropsWithChildren, ReactNode } from 'react'

export const ContentLayout:React.FC<{children:any}> = ({children}) => {
  return (
    <div className='w-10/12 m-auto max-w-8xl neo p-5 '>
      {children}
    </div>
  )
}
