import { Formik, Field } from 'formik';
import { ButtonS } from '../../../CustomElements/ButtonS';
import { TextFieldS } from '../../../CustomElements/styledTextField';

import CollaborationPositionSchema from '../../../data/ValidationSchemas/CollaborationPositionSchema';

export const ColabPosForm = () => {
  return (
    <Formik
      initialValues={{ name: "", description: "" }}
      validationSchema={CollaborationPositionSchema}
      onSubmit={async (data, { setSubmitting }) => {

        setSubmitting(true);

        try {

        }

        catch (err) {
          console.log(err)
        }

        setSubmitting(false);
      }

      }
    >
      {({ values, handleChange, handleBlur, handleSubmit, errors }) => (
        
          

            <form className="w-full max-w-3xl flex flex-col gap-5 p-10 neo rounded-xl z-20" onSubmit={handleSubmit}>

              <h1 className="text-accent text-5xl font-bold my-5">Colaboration position</h1>

              <Field
                name="name"
                value={values.name}
                label="Name"
                variant="outlined"
                error={errors.name ? true : false}
                helperText={errors.name}
                as={TextFieldS}
              />

              <Field
                value={values.description}
                onChange={handleChange}
                onBlur={handleBlur}
                name="description"
                label="Description"
                variant="outlined"
                error={errors.description ? true : false}
                helperText={errors.description}
                as={TextFieldS}
              />

              <div className='flex flex-col'>
                <ButtonS variant="contained" type="submit">Create position</ButtonS>
              </div>

            </form>
          
      )}


    </Formik>
  )
}
