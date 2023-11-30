import { UseMutation } from "@reduxjs/toolkit/dist/query/react/buildHooks";
import { useState, useEffect } from "react";
import SideBarItem from "./SideBarItem";
import { useSelector } from "react-redux";
import User from "../../data/User";
import Collaboration from "../../data/Collaboration";

const SideBarMutationList:React.FC<{data:Collaboration[],baseHref:string}> = ({data,baseHref}) =>{

  console.log(data);

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