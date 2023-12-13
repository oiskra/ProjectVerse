import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import Post from "../../data/Post";
import { Project } from "../../data/Profile";
import portfolioSlice from "../Portfolio/portfolioSlice";
import PostComment from "../../data/PostComments";

export const feedSlice = createSlice({
  name:'feed',

  initialState:{
    posts:[] as Post[]
  },

  reducers:{

    setPosts:(state,action:PayloadAction<Post[]>) =>{
      state.posts = action.payload;
    },
 
    addPost:(state,action) =>{     
      state.posts.push(action.payload);
    },

    addComment:(state,action:PayloadAction<{postID:string,comment:PostComment}>) =>{  
      console.log("add Coment reducer triggered")
      const index = state.posts.findIndex((x:Post)=>x.id === action.payload.postID);
      state.posts[index].postComments.unshift(action.payload.comment);
    },

    updatePostComments:(state,action:PayloadAction<{postID:string,comments:PostComment[]}>) =>{
      const index = state.posts.findIndex((x:Post)=>x.id === action.payload.postID);
      state.posts[index].postComments = action.payload.comments;
    },

    likePost:(state,action:PayloadAction<string>) =>{
      const index = state.posts.findIndex((x:Post)=>x.id === action.payload);
      state.posts[index].isLikedByCurrentUser = true;
      state.posts[index].likesCount++;
    },

    unLikePost:(state,action:PayloadAction<string>) =>{
      const index = state.posts.findIndex((x:Post)=>x.id === action.payload);
      state.posts[index].isLikedByCurrentUser = false
      state.posts[index].likesCount--;
    }

  },

  extraReducers: (builder) => {   
    
  },

})



export const {
  setPosts
} = feedSlice.actions

export default feedSlice.reducer

