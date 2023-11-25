import Collaboration from "../../data/Collaboration"
import Technology from "../../data/Technology"
import { Difficulty } from "./Difficulty"

export const ColabListing : React.FC<{colab:Collaboration ,switchDetails:Function, selected:boolean }>  = ({colab,switchDetails,selected}) => {

  const switchDetailsHandler = () =>{
    switchDetails(colab.id);
  }

  return (    
    <div className="neo p-5 rounded-md text-white flex" style={{border:"solid 1px"+ selected ? "rgb(255 195 40":"none"}} onClick={switchDetailsHandler}>

      <div className="w-1/2 p5">

        <div className="flex flex-col pb-5 ">

          <h2 className="text-3xl font-bold">{colab.name}</h2>
          <p>by <span className="text-accent">@{colab.author.username}</span></p>

        </div>

        <p className="text-sm flex gap-5" style={{fontSize:"0.8em"}}> 

          <span>Added <span className="text-accent">{new Date(colab.createdAt).toLocaleDateString()}</span></span>

          {/* <span><span className="text-accent">{colab.members.length}</span> people involved</span> */}
          <span><span className="text-accent">5</span> people involved</span>

        </p>

      </div>

      <div className="w-1/2 flex flex-col gap-3">

        <div className="flex justify-between gap-2 h-1/2">

          <div className="bg-background neo w-1/2 text-center rounded-md">
            <Difficulty difficulty={colab.difficulty}/>
          </div>

          <div className="bg-background neo w-1/2 text-center rounded-md flex items-center justify-center">TODO :)</div>

        </div>

        <div className="flex justify-around h-1/2 gap-3">

          {colab.technologies.slice(0,2).map((tech:Technology)=>{            
              return <div key={tech.id} className="bg-background neo text-sm text-center rounded-md h-full w-2/4 flex items-center justify-center">{tech.name}</div>
            }            
          )}

        </div>

      </div>
     
    </div>
  )
}
