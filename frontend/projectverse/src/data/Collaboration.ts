import CollaborationPositions from "./CollaborationPosition";
import Technology, { sampleTech } from "./Technology";
import User from "./User";

interface Collaboration{
  id: string,
  //FIX ME XD
  author:{id:string,username:string},
  name:string,
  description:string,
  difficulty:number,
  technologies:Technology[],
  addedDate :Date,
  peopleInvolved:number,
  collaborationPositions:CollaborationPositions[],
}

export default Collaboration;
