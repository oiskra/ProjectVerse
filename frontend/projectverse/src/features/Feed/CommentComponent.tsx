import React from 'react'
import PostComment from '../../data/PostComments'

const CommentComponent:React.FC<{comment:PostComment}> = ({comment}) => {

  return (
    <div className="py-5 glass px-5">

        <h4 className='font-bold text-2xl'>{comment.author.userName}</h4>

        <p className='opacity-70 text-justify'>
          {comment.body}
        </p>

    </div>
  )
}

export default CommentComponent