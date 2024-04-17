import Technology from "./Technology";

export interface Project{
  id:string,
  author:{id:string,userName:string},
  name:string,
  description:string,
  projectUrl:string,
  usedTechnologies:Technology[]
  isPrivate:boolean,
  isPublished:boolean,
  createdAt:Date
}