import React, { useRef, useState } from 'react'
import Post from '../../data/Post'
import img from '../../assets/testimage.png'
import Technology from '../../data/Technology';
import { Button, Input } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import CardComments from './CardComments';
import { FEED_AddComment, FEED_GetComments } from './feedThunks';
import { useDispatch } from 'react-redux';
import PostStatsComponent from './PostStatsComponent';

const PostCard: React.FC<{ post: Post }> = ({ post }) => {

  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [seeComments, setSeeComments] = useState(false);

  const wrapperRef = useRef(null)
  const cardRef = useRef(null)

  let textFieldValue = "";

  const handleTextChange = (e:any) =>{
    textFieldValue = e.target.value;
  }

  let expandedRef = useRef(false);


  const projectRedirectHandler = () => {
    navigate(`/portfolio/project/${post.project.id}`)
  }

  const addCommentHandler = () =>{
    if(textFieldValue.length !== 0){
      dispatch(FEED_AddComment(post.id,{body:textFieldValue}));
    }   
    
  }

  const seeCommentsHandler = () =>{
    setSeeComments(!seeComments);
    dispatch(FEED_GetComments(post.id))
  }


  return (

    <div className={`glass p-5 bg-glassMorph flex-grow rounded-xl flex justify-center transition-all customTransition`}>
      <div className="relative h-full neo p-3 bg-background gap-2 py-5 rounded-xl max-w-[450px] flex flex-col overflow-hidden customTransition">

        <div className="relative flex flex-col justify-between h-full gap-5 overflow-hidden top-0 customTransition" ref={wrapperRef}>
          <div className="flex flex-col flex-grow" ref={cardRef}>

            <div className='relative rounded-xl overflow-hidden'>
              <img src={img} alt="" className="w-full h-full customTransition max-h-screen" style={{ maxHeight: "400px" }}/>
              <div className="absolute bottom-0 bg-blackO w-full h-[70px] p-2 px-5" style={{ zIndex: 40 }}>

                <div className="">
                  <h2 onClick={projectRedirectHandler}
                    className="text-2xl text-accent font-black">{post.project.name}</h2>
                  <div className='text-white'>by @{post.project.author.userName}</div>
                </div>

                <div className="absolute right-0 top-0 h-full text-white flex gap-5 mr-5">
                  <PostStatsComponent postID={post.id} isLiked={post.isLikedByCurrentUser} likeCount={post.likesCount} viewCount={post.viewsCount} />
                </div>

              </div>
            </div>

            <div className="mx-5 mt-5 flex flex-col justify-between gap-5 text-white">

              <div className='max-h-screen customTransition' style={{ maxHeight: "400px" }}>
                <h3 className='text-accent max-h-auto '>Description</h3>
                <div>
                  {post.project.description}
                </div>
              </div>

              <div className="text-white flex gap-2 flex-wrap max-h-screen customTransition" style={{ maxHeight: "400px" }}>
                {post.project.usedTechnologies.slice(0, 3).map((tech: Technology, index: number) =>
                  <span key={tech.id} className={`neo p-2 bg-background flex-grow rounded-md text-center ${index === 0 ? "text-accent" : "text-white"}`}>{tech.name}</span>
                )}
              </div>

            </div>

          </div>

          <div className='px-5'>
            <h3 className="text-accent"> Comments</h3>

            <div className="flex flex-col">
              <Input className='w-full py-2' onChange={handleTextChange}></Input>
              <Button onClick={addCommentHandler}>Add comment</Button>
              {/* <InsertCommentIcon className="text-accent neo rounded-full" /> */}
            </div>

            <Button sx={{color:"whitesmoke"}} onClick={seeCommentsHandler}>See Comments</Button>

          </div>

        
  
          
        </div>


        
        <CardComments comments={post.postComments} author={post.project.author.userName} name={post.project.name} expanded={seeComments} handleExpand={seeCommentsHandler}/>
      </div>
    </div>


  )
}

export default PostCard