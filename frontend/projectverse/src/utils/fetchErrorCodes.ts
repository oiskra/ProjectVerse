export function parseError(err:any):string{
  console.log(typeof err);
  
  if(!err?.response){
    return "No server response"
  }
  if(err.response?.status === 400) {
    return "Missing username or password"
  }
  if(err.response?.status === 401){
     return "Unauthorized"
  }
  return "Login failed"
}