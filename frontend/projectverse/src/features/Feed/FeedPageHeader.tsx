import React from 'react'
import SearchIcon from '@mui/icons-material/Search';
import FilterAltIcon from '@mui/icons-material/FilterAlt';
import ExploreIcon from '@mui/icons-material/Explore';
import { FormControl, Input, InputAdornment, Button } from '@mui/material';

const FeedPageHeader:React.FC<{}> = () => {
  return (
    <>
      <h1 className='text-accent glass bg-glassMorph p-5  text-3xl flex items-center gap-5'>        
        <ExploreIcon style={{fontSize:"2em",color:"whitesmoke"}}/>
        Community posts
      </h1>

      <div className='w-full glass bg-glassMorph my-2 flex justify-center flex-wrap items-center' >

        <div className="w-full flex py-10 px-40 items-center gap-5">
          <FormControl className='flex-grow' variant="standard">            
            <Input
              id="input-with-icon-adornment"
              placeholder='Search Posts'
              startAdornment={
                <InputAdornment position="start">
                  <SearchIcon />
                </InputAdornment>
              }
            />
          </FormControl>
          <FilterAltIcon className='text-white text-xl' />
          
        </div>

      </div>

    </>
  )
}

export default FeedPageHeader