import SideBarItem from "./SideBarItem";
import Collaboration from "../../data/Collaboration";

const SideBarMutationList:React.FC<{data:Collaboration[],baseHref:string}> = ({data,baseHref}) =>{

  if(!data){
    return <></>
  }

  return(
    <>
      {data.map((elem:any) =>
        <SideBarItem key={elem.id} href = {`${baseHref}/${elem.id}`} name = {elem.name} expanded={true} />
      )}    
    </>
    
  )

}

export default SideBarMutationList;