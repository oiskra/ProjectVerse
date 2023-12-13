interface PostComment{
  id:string,
  author:{
    id:string,
    userName:string,
    
  }
  body:string,
  postedAt:Date
}

export default PostComment