import { apiSlice } from "../../API Services/apiSlice";

export const feedApiSlice = apiSlice.injectEndpoints({
  
  endpoints: builder =>({

    getPosts:builder.query({
      query:() =>({
        url:`/posts`,
        method:'GET',        
      })
    }),

    getComments:builder.query({
      query:(postID:string) =>({
        url:`/posts/${postID}/comments`,
        method:'GET',        
      })
    }),

    addComment:builder.query({
      query:({postID,body}:{postID:string,body:object}) =>({
        url:`/posts/${postID}/comments`,
        method:'POST',
        body:body        
      })
    }),

    likePost:builder.query({
      query:(postID:string) =>({
        url:`/posts/${postID}/like`,
        method:'POST',     
        body:'' 
      })
    }),

    unLikePost:builder.query({
      query:(postID:string) =>({
        url:`/posts/${postID}/unlike`,
        method:'DELETE',      
      })
    }),

  })
})



export const {
useGetPostsQuery,
useAddCommentQuery
} = feedApiSlice