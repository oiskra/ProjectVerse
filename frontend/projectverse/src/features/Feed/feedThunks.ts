import { feedApiSlice } from "./feedApiSlice";
import {feedSlice} from "./feedSlice"


export function FEED_GetPosts(){  

  return async function getPostsThunk(dispatch:any) {
    const response = await dispatch(feedApiSlice.endpoints.getPosts.initiate({}));
    dispatch(feedSlice.actions.setPosts(response.data));
  }

}

export function FEED_GetComments(postID:string){  

  return async function getCommentsThunk(dispatch:any) {
    const response = await dispatch(feedApiSlice.endpoints.getComments.initiate(postID));
    dispatch(feedSlice.actions.updatePostComments({postID:postID,comments:response.data}));
  }

}

export function FEED_AddComment(postID:string,body:object){  

  return async function addCommentThunk(dispatch:any) {
    const response = await dispatch(feedApiSlice.endpoints.addComment.initiate({postID:postID,body:body}));
    dispatch(feedSlice.actions.addComment({postID:postID,comment:response.data}));
  }

}

export function FEED_LikePost(postID:string){

  return async function likePostThunk(dispatch:any) {
    const response = await dispatch(feedApiSlice.endpoints.likePost.initiate(postID));
    dispatch(feedSlice.actions.likePost(postID));
  }

}

export function FEED_UnLikePost(postID:string){

  return async function unLikePostThunk(dispatch:any) {
    const response = await dispatch(feedApiSlice.endpoints.unLikePost.initiate(postID));
    dispatch(feedSlice.actions.unLikePost(postID));
  }

}

