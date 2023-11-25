interface User {

  id : number,
  username : string,
  email : string,
  surname : string,
  country : string,
  refreshToken : string,
  refreshTokenExpiryTime : Date
  
}


export default User;
