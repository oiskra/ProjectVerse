
import User from "./User"

interface CollaborationApplicant{  
  
  id:string,
  applicantUserName:string,
  applicantEmail:string,
  applicantUserId:string,
  appliedPositionId:string,
  applicationStatus : ApplicationStatus,
  appliedOn:Date
  
}

enum ApplicationStatus
{
    Rejected,
    Pending,
    Accepted
}

export default CollaborationApplicant;