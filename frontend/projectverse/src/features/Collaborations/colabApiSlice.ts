import { apiSlice } from "../../API Services/apiSlice";

export const colabApiSlice = apiSlice.injectEndpoints({
  endpoints: builder =>({

    getAllColabs:builder.mutation({
      query:colabs =>({
        url:'/collaborations',
        method:'GET',        
      })
    }),

    getColab:builder.mutation({
      query:colabs =>({
        url:'/collaborations',
        method:'GET',        
      })
    }),
    
    postColab:builder.mutation({
      query:colabs =>({
        url:'/collaboration',
        method:'POST',
        body:{...colabs}
      })
    })
  })
})



export const {
  useGetAllColabsMutation,
  useGetColabMutation,
  usePostColabMutation
} = colabApiSlice