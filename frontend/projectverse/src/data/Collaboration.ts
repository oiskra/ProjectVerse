import CollaborationPositions,{ sampleColabPos1, sampleColabPos2, sampleColabPos3, sampleColabPos4 } from "./CollaborationPosition";
import Technology, { sampleTech } from "./Technology";
import User, { sampleUser } from "./User";

interface Collaboration{
  ID: number,
  Author:User,
  Name:string,
  Description:string,
  Difficulty:number,
  Technologies:Technology[],
  AddedDate :Date,
  Members:User[],
  CollaborationPositions:CollaborationPositions[],
}

export default Collaboration;

export const sampleCollaboration:Collaboration = {
  ID: 0,
  Author: sampleUser,
  Name: "Mentor Me!",
  Description: "At OpenX, we have built a team that is uniquely experienced in designing and operating high-scale ad marketplaces, and we are constantly on the lookout for thoughtful, creative executors who are as fascinated as we are about finding new ways to apply a blend of market design, technical innovation, operational excellence, and empathetic partner service to the frontiers of digital advertising.",
  Difficulty: 1,
  Technologies: [sampleTech,sampleTech,sampleTech,sampleTech],
  AddedDate : new Date("2022-03-25"),
  CollaborationPositions: [sampleColabPos1,sampleColabPos2,sampleColabPos3,sampleColabPos4,],
  Members: [sampleUser]
}