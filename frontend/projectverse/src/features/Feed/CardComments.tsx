import { Button, Input } from '@mui/material'
import React, { useEffect, useState } from 'react'
import PostComment from '../../data/PostComments'
import Post from '../../data/Post';
import CommentComponent from './CommentComponent';

const CardComments: React.FC<{ comments: PostComment[], name: string, author: string, handleExpand: Function,expanded:boolean }> = ({ comments, name, author, handleExpand ,expanded }) => {

  const expandHandler = () => {
    handleExpand();
  }

  return (
    <>
      {expanded &&
        <div className="absolute flex flex-col z-40 bg-glassMorph glass text-white w-[95%]  h-[95%] animate-slide">

          <div className="glass w-full h-[70px] p-2 px-5" style={{ zIndex: 40 }}>
            <h2
              className="text-3xl text-accent font-black">{name}</h2>
            <div className='text-white'>by @{author}</div>
          </div>

          <div className="top-0 flex flex-col z-50 flex-grow gap-1 py-2 overflow-y-scroll">

            {
              comments.map((comment: PostComment) =>
                <CommentComponent key={comment.id} comment={comment} />
              )
            }

          </div>


          <Button sx={{ color: "white" }} onClick={expandHandler}>Hide comments</Button>
        </div>

      }

    </>
  )
}

export default CardComments