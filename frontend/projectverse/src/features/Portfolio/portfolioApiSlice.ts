import { apiSlice } from "../../API Services/apiSlice";

export const portfolioApiSlice = apiSlice.injectEndpoints({
  
  endpoints: builder =>({

    getUserProjects:builder.query({
      query:(userID:string) =>({
        url:`/projects/user/${userID}`,
        method:'GET',        
      })
    }),

    getProject:builder.query({
      query:(projectID:string) =>({
        url:`/projects/${projectID}`,
        method:'GET',        
      })
    }),

    addProject:builder.mutation({
      query:(project) => ({
        url:`/projects`,
        method:'POST',
        body:project
      })
    })

    


  })
})



export const {
useGetUserProjectsQuery,
useGetProjectQuery,
useAddProjectMutation,
} = portfolioApiSlice