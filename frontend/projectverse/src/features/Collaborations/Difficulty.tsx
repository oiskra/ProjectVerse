import React from 'react'

export const Difficulty : React.FC<{difficulty:number}> = ({difficulty}) => {

  let balls:JSX.Element[] = [];

  for(let i = 0 ; i < difficulty ; i++){
    balls.push(
      <div style={{"backgroundColor":color[difficulty]}} className='difBall'></div>
    )
  }


  return (
    <div  className='w-full h-full flex gap-3 items-center p-2'>
      <div className='w-2/5 text-1 justify-center flex' style={{fontSize:"0.6em"}}>Difficulty</div>
      <div className='w-3/5 flex justify-center gap-1'>
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
