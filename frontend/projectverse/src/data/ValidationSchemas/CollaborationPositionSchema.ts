import * as Yup from 'yup';

const CollaborationPositionSchema = Yup.object({
  name:Yup.string().required(),
  description:Yup.string().required()
});

export default CollaborationPositionSchema;