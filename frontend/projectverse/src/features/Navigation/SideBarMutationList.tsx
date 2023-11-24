import { UseMutation } from "@reduxjs/toolkit/dist/query/react/buildHooks";
import { useState, useEffect } from "react";
import SideBarItem from "./SideBarItem";

const SideBarMutationList:React.FC<{name:string,mutation:UseMutation<any>,baseHref:string}> = ({name,mutation,baseHref}) =>{

  const [mutationObj] = mutation()

  const [data,setData] = useState([] as Array<{}>);

  useEffect(() => {
    getData();  
  }, [])

  const getData = async () =>{
    const response = await mutationObj({}).unwrap() as Array<{}>
    setData(response);
  }

  return(
    <>
      {data.map((elem:any) =>
        <SideBarItem href = {null} name = {elem.name} expanded={true} />
      )}    
    </>
    
  )

}

export default SideBarMutationList;