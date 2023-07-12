import Collaboration from "../../data/Collaboration"
import Technology from "../../data/Technology"
import { Difficulty } from "./Difficulty"

export const ColabListing : React.FC<{colab:Collaboration}>  = ({colab}) => {
  return (    
    <div className="neo p-5 rounded-md text-white flex">
      <div className="w-1/2 p5">
        <div className="flex flex-col pb-5 ">
          <h2 className="text-3xl font-black">{colab.Name}</h2>
          <p>by <span className="text-accent">@{colab.Author.Username}</span></p>
        </div>

        <p className="text-sm flex gap-5" style={{fontSize:"0.8em"}}> 
          <span>Added <span className="text-accent">{colab.AddedDate.toLocaleDateString()}</span></span>

          <span><span className="text-accent">{colab.Members.length}</span> people involved</span>
        </p>

      </div>
      <div className="w-1/2 flex flex-col gap-3">

        <div className="flex justify-between gap-2 h-1/2">
          <div className="bg-background neo w-1/2 text-center rounded-md">
            <Difficulty difficulty={colab.Difficulty}/>
          </div>
          <div className="bg-background neo w-1/2 text-center rounded-md flex items-center justify-center">TODO :)</div>
        </div>

        <div className="flex justify-around h-1/2 gap-3">
          {colab.Technologies.map((tech:Technology)=>
            <div className="bg-background neo text-center rounded-md h-full w-1/4 flex items-center justify-center">{tech.Name}</div>
          )}
        </div>

      </div>
     
    </div>
  )
}