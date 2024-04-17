import * as Yup from 'yup';

const CollaborationSchema = Yup.object({
  name:Yup.string().required(),
  description:Yup.string().required(),
  difficulty:Yup.number().min(0).max(3).required(),
  technologies:Yup.array().min(2),
  positions:Yup.array()
  
});

export default CollaborationSchema;