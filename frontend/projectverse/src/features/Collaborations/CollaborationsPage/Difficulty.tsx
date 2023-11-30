import React from 'react'

export const Difficulty : React.FC<{difficulty:number}> = ({difficulty}) => {

  let balls:JSX.Element[] = [];

  for(let i = 0 ; i < difficulty ; i++){
    balls.push(
      <div key = {i} style={{"backgroundColor":color[difficulty]}} className='difBall neo'></div>
    )
  }


  return (
    <div  className='w-full flex gap-3 items-between justify-between'>
      <div className='' style={{fontSize:"1em"}}>Difficulty</div>
      <div className='flex justify-center gap-1'>
        {balls}
      </div>
    </div>

  )
}


enum color{
  'green' = 1,
  '#FFC328' = 2,
  '#FC1111' = 3
}
