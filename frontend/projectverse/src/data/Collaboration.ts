import CollaborationApplicant from "./CollaborationApplicant";
import CollaborationPosition from "./CollaborationPosition";
import Technology from "./Technology";

interface Collaboration{
  id: string,
  //FIX ME XD
  author : {id : string , username : string},
  authorId : number,
  name : string,
  description : string,
  difficulty : number,
  technologies : Technology[],  
  peopleInvolved : number,
  collaborationPositions : CollaborationPosition[],
  collaborationApplicants : CollaborationApplicant[],
  createdAt : Date,
}

export default Collaboration;



