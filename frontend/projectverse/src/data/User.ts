interface User{
  ID : number,
  Username:string,
  Email:string,
  Surname:string,
  Country:string;
}


export default User;

export const sampleUser : User = {
  ID: 0,
  Username: "Allyn",
  Email: "Allyn@gmail.com",
  Surname: "Smith",
  Country: "Poland"
}