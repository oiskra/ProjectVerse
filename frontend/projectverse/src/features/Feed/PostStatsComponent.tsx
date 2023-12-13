import React from 'react'
import ThumbUpIcon from '@mui/icons-material/ThumbUp';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { useDispatch } from 'react-redux';
import { FEED_UnLikePost, FEED_LikePost } from './feedThunks';

const PostStatsComponent:React.FC<{postID:string,isLiked:boolean,likeCount:number,viewCount:number}> = ({postID,isLiked,likeCount,viewCount}) => {

  const dispatch = useDispatch();

  const likeColor = isLiked ? "text-accent" : "text-white";

  const handleLikePress = () =>{

    if(isLiked){
      dispatch(FEED_UnLikePost(postID))
    }
    else{
      dispatch(FEED_LikePost(postID))
    }

  }


  return (
    <>  

      <div className='flex items-center gap-2 text-2xl'>
        <ThumbUpIcon onClick={handleLikePress} className={`${likeColor}`} />
        {likeCount}
      </div>

      <div className='flex items-center gap-2 text-2xl'>
        <VisibilityIcon className="text-white" />
        {viewCount}
      </div>
      
    </>    
  )
}

export default PostStatsComponent