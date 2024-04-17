import * as Yup from 'yup'

const LoginSchema = Yup.object({
  email:Yup.string().required(),
  password:Yup.string().required()
})

export default LoginSchema;