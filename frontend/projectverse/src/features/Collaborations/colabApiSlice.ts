import { apiSlice } from "../../API Services/apiSlice";
import Collaboration from "../../data/Collaboration";
import CollaborationPosition from "../../data/CollaborationPosition";
import CollaborationPositions from "../../data/CollaborationPosition";

export const colabApiSlice = apiSlice.injectEndpoints({
  endpoints: builder =>({

    //Main colab endpoints

    getAllColabs:builder.mutation({
      query:() =>({
        url:'/collaborations',
        method:'GET',        
      })
    }),

    getColab:builder.mutation({
      query:(id:string) =>({
        url:`/collaborations/${id}`,
        method:'GET',        
      })
    }),
    
    postColab:builder.mutation({
      query:colab =>({
        url:'/collaboration',
        method:'POST',
        body:colab
      })
    }),

    updateColab:builder.mutation({
      query:({colab,id}:{colab:Collaboration,id:string}) =>({
        url:`/collaboration/${id}`,
        method:'PUT',
        body:colab
      })
    }),

    deleteColab:builder.mutation({
      query:(id:string) =>({
        url:`/collaboration/${id}`,
        method:'DELETE'
      })
    }),

    // patchColab:builder.mutation({
    //   query:(id:string,colab:any) =>({
    //     url:`/collaboration/${id}`,
    //     method:'PATCH',
    //     body:colab
    //   })
    // })   

    //Collaboration positions endpoints
    //FIXME Pytanie do brzega

    postColabPos:builder.mutation({
      query:({colabPos,ColabID}:{colabPos:any,ColabID:string}) =>({
        url:`/collaborations/${ColabID}/collaboration-positions`,
        method:'POST',
        body:colabPos
      })
    }),

    postColabPosApply:builder.mutation({
      query:({colabPosID,ColabID}:{colabPosID:string,ColabID:string}) =>({
        url:`/collaborations/${ColabID}/collaboration-positions/${colabPosID}/apply`,
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
  useGetAllColabsMutation,
  useGetColabMutation,
  useUpdateColabMutation,
  usePostColabMutation,
  usePostColabPosMutation,
  usePatchApplicationStatusMutation,
  usePostColabPosApplyMutation
} = colabApiSlice