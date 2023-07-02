export const ColabListing = () => {
  return (
    <div className="neo p-5 rounded-md text-white flex">
      <div className="w-1/2 p5">

        <div className="flex flex-col pb-5 ">
          <h2 className="text-3xl font-black">TODO</h2>
          <p>by <span className="text-accent">Allyn</span></p>
        </div>

        <p className="text-sm flex gap-5"> 
          <span>Added <span className="text-accent">1 day</span> ago</span>

          <span><span className="text-accent">3</span> people involved</span>
        </p>

      </div>
      <div className="w-1/2 flex flex-col gap-3">

        <div className="flex justify-between gap-2 h-1/2">
          <div className="bg-background neo w-1/2 text-center rounded-md">asf</div>
          <div className="bg-background neo w-1/2 text-center rounded-md">asf</div>
        </div>

        <div className="flex justify-around h-1/2 gap-3">
          <div className="bg-background neo text-center rounded-md h-full w-1/4 flex items-center justify-center">Mongo</div>
          <div className="bg-background neo text-center rounded-md h-full w-1/4 flex items-center justify-center">Mongo</div>
          <div className="bg-background neo text-center rounded-md h-full w-1/4 flex items-center justify-center">Mongo</div>
          <div className="bg-background neo text-center rounded-md h-full w-1/4 flex items-center justify-center">Mongo</div>
        </div>

      </div>
     
    </div>
  )
}
