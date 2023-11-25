import CollaborationApplicant from "./CollaborationApplicant";
import CollaborationPosition from "./CollaborationPosition";
import Technology from "./Technology";

interface Collaboration{
  id: string,
  //FIX ME XD
  author : {id : string , username : string},
  authorId : number,
  name : string,
  description : string,
  difficulty : number,
  technologies : Technology[],  
  peopleInvolved : number,
  collaborationPositions : CollaborationPosition[],
  collaborationApplicants : CollaborationApplicant[],
  createdAt : Date,
}

export default Collaboration;


export const sampleColaboration = {
  "name": "WebCrafters",
  "description": "WebCrafters is a dynamic web development startup dedicated to building innovative and user-friendly web applications. We are passionate about creating cutting-edge solutions that enhance the online experience for our clients and their customers. Join our team and be a part of shaping the future of the web.",
  "difficulty": 4,
  "technologies": ["Web Development", "Frontend Frameworks", "Backend Development"],
  "collaborationPositions": [
    {
      "name": "Frontend Developer",
      "description": "As a Frontend Developer at WebCrafters, you'll take the lead in crafting visually stunning and responsive user interfaces. You'll work with the latest frontend technologies to create engaging web experiences that captivate users."
    },
    {
      "name": "Backend Developer",
      "description": "Join us as a Backend Developer to architect and maintain the server-side infrastructure of our web applications. You'll build robust APIs, handle data storage, and ensure the smooth functioning of our systems."
    },
    {
      "name": "UI/UX Designer",
      "description": "Our UI/UX Designers are responsible for creating seamless and intuitive user experiences. You'll engage in user research, wireframing, and prototyping to transform ideas into user-friendly web interfaces."
    },
    {
      "name": "Quality Assurance Analyst",
      "description": "As a Quality Assurance Analyst, you'll meticulously test and debug our web applications to guarantee they meet high standards of functionality and performance. Your work ensures a smooth user experience."
    },
    {
      "name": "Project Manager",
      "description": "Project Managers at WebCrafters play a crucial role in planning, organizing, and executing web development projects. You'll ensure projects are completed on time, within budget, and meet client expectations."
    },
    {
      "name": "Content Writer",
      "description": "Join our team as a Content Writer to create compelling, SEO-optimized content for web applications. Your words will engage users and convey our clients' messages effectively."
    },
    {
      "name": "DevOps Engineer",
      "description": "As a DevOps Engineer, you'll focus on optimizing the deployment and automation of our web applications. Your work streamlines the development process and enhances application reliability."
    }
  ]
}


