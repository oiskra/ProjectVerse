import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { FEED_GetPosts } from '../features/Feed/feedThunks';
import { Loader } from '../components/Loader';
import Post from '../data/Post';
import PostCard from '../features/Feed/PostCard';
import FeedPageHeader from '../features/Feed/FeedPageHeader';

export const FeedPage = () => {

  const dispatch = useDispatch();
  const [isLoading,setLoading] = useState(true);


  const posts = useSelector((state:any) => state.feed.posts);

  useEffect(() => {
    dispatch(FEED_GetPosts())
    .then(()=>{setLoading(false)})
  }, [dispatch])
  

  if(isLoading)
    return <Loader />

  return (
    <>
      <FeedPageHeader />
      <div className="overflow-scroll h-[80%]">

        <div className="flex gap-5 flex-wrap justify-center customTransition">
          {posts.map((post:Post)=><PostCard key={post.id} post={post} />)}
        </div>

      </div>
    </>    
  )
}
