import { apiSlice } from "../../API Services/apiSlice";
import Collaboration from "../../data/Collaboration";
import CollaborationPosition from "../../data/CollaborationPosition";


export const colabApiSlice = apiSlice.injectEndpoints({
  
  endpoints: builder =>({

    //Main colab endpoints

    getAllCollabs:builder.mutation({
      query:() =>({
        url:'/collaborations',
        method:'GET',        
      })
    }),

    getUserCollabs:builder.mutation({
      query:(id:string) =>({
        url:`/collaborations?userId=${id}`,
        method:'GET',        
      })
    }),

    getCollab:builder.mutation({
      query:(id:string) =>({
        url:`/collaborations/${id}`,
        method:'GET',        
      })
    }),    
    
    addCollab:builder.mutation({
      query:collab =>({
        url:'/collaborations',
        method:'POST',
        body:collab
      })
    }),

    updateCollab:builder.mutation({
      query:({colab,id}:{colab:Collaboration,id:string}) =>({
        url:`/collaborations/${id}`,
        method:'PUT',
        body:colab
      })
    }),

    deleteCollab:builder.mutation({
      query:(id:string) =>({
        url:`/collaborations/${id}`,
        method:'DELETE'
      })
    }),

    // patchColab:builder.mutation({
    //   query:(id:string,colab:any) =>({
    //     url:`/collaborations/${id}`,
    //     method:'PATCH',
    //     body:colab
    //   })
    // })   

    //Collaboration positions endpoints
    //FIXME Pytanie do brzega

    postCollabPos:builder.mutation({
      query:({collabPos,CollabID}:{collabPos:CollaborationPosition,CollabID:string}) =>({
        url:`/collaborations/${CollabID}/collaboration-positions`,
        method:'PATCH',
        body:collabPos
      })
    }),

    postCollabPosApply:builder.mutation({
      query:({collabPosID,CollabID}:{collabPosID:string,CollabID:string}) =>({
        url:`/collaborations/${CollabID}/collaboration-positions/${collabPosID}/apply`,
        method:'POST',        
       
      })
    }),

    patchApplicationStatus:builder.mutation({
      query:({applicantID,status}:{applicantID:string,status:number}) =>({
        url:`/collaborations/applicants/${applicantID}/change-application-status`,
        method:'PATCH',
        body:{applicationStatus:status}
      })
    }),

  })
})



export const {
  useGetAllCollabsMutation,
  useGetUserCollabsMutation,
  useGetCollabMutation,
  useUpdateCollabMutation,
  useAddCollabMutation,
  usePostCollabPosMutation,
  usePatchApplicationStatusMutation,
  usePostCollabPosApplyMutation
} = colabApiSlice