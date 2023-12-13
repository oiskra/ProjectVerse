import PostComment from "./PostComments";
import { Project } from "./Profile";

interface Post{
  id:string,
  project:Project,
  viewsCount:number,
  likesCount:number,
  isLikedByCurrentUser:boolean,
  postComments:PostComment[]  
}

export default Post;