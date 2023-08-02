import * as Yup from 'yup'

const LoginSchema = Yup.object({
  login:Yup.string().required(),
  password:Yup.string().required()
})

export default LoginSchema;