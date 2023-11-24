import { Formik, Field } from 'formik';
import { useNavigate } from 'react-router-dom';
import { ButtonS } from '../../CustomElements/ButtonS';
import { TextFieldS } from '../../CustomElements/styledTextField';
import { Loader } from '../../components/Loader';
import { useRegisterMutation } from './authApiSlice';
import RegisterSchema from '../../data/ValidationSchemas/RegisterSchema';

export const Register = () => {
  const navigate = useNavigate();
  //Renable after implementing register login
  // const dispatch = useDispatch()  

  const [register,{isLoading}] = useRegisterMutation();
  

  if(isLoading) return <Loader />

  return (

    <Formik
      initialValues={{ email: "",username:"", password: "",confirmPassword:"" }}
      validationSchema={RegisterSchema}
      onSubmit={async (data,{setSubmitting}) => {
          
          setSubmitting(true);
          
          try{
            console.log(data);
            const registerResult = await register(data).unwrap();  
            console.log(registerResult);         
            navigate('/login');   

          }

          catch(err){
            console.log(err)
          }

          setSubmitting(false);        
        }     

      }
    >
      {({ values, handleChange, handleSubmit,errors }) => (
        <>

          <form className="w-full flex flex-col gap-5 p-5" onSubmit={handleSubmit}>

            <h1 className="text-accent text-5xl font-bold my-5">
              Sign Up
            </h1>

            <Field
              name="email"
              value={values.email}             
              label="Email"              
              variant="outlined"   
              error={errors.email ? true : false}                        
              helperText={errors.email}
              as={TextFieldS}
            />

            <Field
              name="username"
              value={values.username}             
              label="Username"              
              variant="outlined"   
              error={errors.username ? true : false}                        
              helperText={errors.username}
              as={TextFieldS}
            />

            <Field            
              value={values.password} 
              onChange={handleChange}              
              name="password" 
              label="Password" 
              variant="outlined"
              error={errors.password ? true : false}            
              helperText={errors.password}
              as={TextFieldS} 
            />

            <Field            
              value={values.confirmPassword} 
              onChange={handleChange}              
              name="confirmPassword" 
              label="Confirm password" 
              variant="outlined"
              error={errors.confirmPassword ? true : false}            
              helperText={errors.confirmPassword}
              as={TextFieldS} 
            />

            <div className='flex flex-col'>
              <ButtonS variant="contained" type="submit">Sign Up</ButtonS>
              {/*TODO FORGOT PASSWORD*/}
              <a href="" className="text-white text-sm flex justify-end p-1">Forgot password?</a>
            </div>

          </form>

          <div className='flex gap-5'>

            <div className='bg-white' style={{ width: "35px", height: "35px" }}></div>
            <div className='bg-white' style={{ width: "35px", height: "35px" }}></div>

            {/* INSERT METODY LOGOWANIA HERE */}

          </div>
        </>
      )}


    </Formik>
  )
}
