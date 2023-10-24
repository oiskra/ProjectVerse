import CollaborationPositions,{ sampleColabPos1, sampleColabPos2, sampleColabPos3, sampleColabPos4 } from "./CollaborationPosition";
import Technology, { sampleTech } from "./Technology";
import User from "./User";

interface Collaboration{
  ID: number,
  //FIX ME XD
  author:{id:string,username:string},
  name:string,
  description:string,
  difficulty:number,
  technologies:Technology[],
  addedDate :Date,
  members:User[],
  collaborationPositions:CollaborationPositions[],
}

export default Collaboration;
