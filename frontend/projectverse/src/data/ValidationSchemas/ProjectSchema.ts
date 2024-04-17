import * as Yup from 'yup'

const ProjectSchema = Yup.object({
  name:Yup.string().required(),
  description:Yup.string().required(),
  projectUrl:Yup.string().matches(
    /^((ftp|http|https):\/\/)?(www.)?(?!.*(ftp|http|https|www.))[a-zA-Z0-9_-]+(\.[a-zA-Z]+)+((\/)[\w#]+)*(\/\w+\?[a-zA-Z0-9_]+=\w+(&[a-zA-Z0-9_]+=\w+)*)?$/gm,
    'You need to provide the url in the correct format!'
).required(),
  usedTechnologies:Yup.array().required(),
  isPrivate:Yup.bool().required(),
  isPublished:Yup.bool().required()
});

export default ProjectSchema;

