import * as Yup from 'yup'

const RegisterSchema = Yup.object({
  email:Yup.string().email("Email field must be a valid email adress").required(),
  username:Yup.string().required(),
  password:Yup.string().required(),
  confirmPassword:Yup.string().required()
})

//TODO match password with backend schema

export default RegisterSchema;