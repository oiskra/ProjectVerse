import { Button, FormControl, Input, InputAdornment, InputLabel, TextField } from '@mui/material'
import SearchIcon from '@mui/icons-material/Search';
import FilterAltIcon from '@mui/icons-material/FilterAlt';
import ExploreIcon from '@mui/icons-material/Explore';


const CollabPageHeader = () => {
  return (
    <>
      <h1 className='text-accent text-3xl flex items-center gap-5'>        
        <ExploreIcon style={{fontSize:"2em",color:"whitesmoke"}}/>
        Discover Collaborations
      </h1>

      <div className='w-ful flex justify-center flex-wrap items-center' >

        <div className="w-full flex py-10 px-40 items-center gap-5">
          <FormControl className='flex-grow' variant="standard">            
            <Input
              id="input-with-icon-adornment"
              placeholder='Search Collaborations'
              startAdornment={
                <InputAdornment position="start">
                  <SearchIcon />
                </InputAdornment>
              }
            />
          </FormControl>
          <FilterAltIcon className='text-white text-xl' />
          
        </div>

        <Button>All</Button>
        <Button disabled>For You</Button>
        <Button disabled>Following</Button>
      </div>

    </>


  )
}

export default CollabPageHeader