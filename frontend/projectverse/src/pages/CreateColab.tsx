import { Field, Formik } from 'formik';
import React, { useState } from 'react'
import CollaborationSchema from '../data/ValidationSchemas/CollaborationSchema';
import { TextFieldS } from '../CustomElements/styledTextField';
import { ButtonS } from '../CustomElements/ButtonS';
import { Box, Chip, FormControl, FormGroup, FormHelperText, InputLabel, MenuItem, OutlinedInput, Select } from '@mui/material';
import technologiesList from '../data/tempTechnologiesList';

export const CreateColab = () => {

  return (
    <Formik
      validateOnChange={false}
      validateOnBlur={false}
      initialValues={{ name: "", description: "", difficulty: 1, technologies: [] }}
      validationSchema={CollaborationSchema}
      onSubmit={async (data, { setSubmitting }) => {
        setSubmitting(true);
        console.log("subbmit attemtpt")
        try {
          console.log(data);
          
          // const userData = await login(data).unwrap();
          // dispatch(setCredentials(userData));
          // navigate('/home');             
        }
        catch (err) {
          console.log(err)
        }
        setSubmitting(false);
      }

      }
    >
      {({ values, handleChange, handleBlur, handleSubmit, errors }) => (
        <>

          <form className="w-full flex flex-col gap-5 p-5" onSubmit={handleSubmit}>

            <h1 className="text-accent text-5xl font-bold my-5">Create colab</h1>
            <Field
              name="name"
              value={values.name}
              label="Colaboration name"
              variant="outlined"
              error={errors.name ? true : false}
              helperText={errors.name}
              as={TextFieldS}
            />
            <Field
              value={values.description}
              onChange={handleChange}
              name="description"
              label="Collaboration description"
              variant="outlined"
              error={errors.description ? true : false}
              helperText={errors.description}
              as={TextFieldS}
            />
            <Field
              value={values.difficulty}
              onChange={handleChange}
              onBlur={handleBlur}
              name="difficulty"
              label="Difficulty"
              variant="outlined"
              error={errors.difficulty ? true : false}
              helperText={errors.difficulty}
              as={TextFieldS}
            />

            <Field
              style={{ display: "none" }}
              value={values.technologies}
              onChange={handleChange}
              onBlur={handleBlur}
              name="technologies"
              label="Technologies separated by /"
              variant="outlined"
              error={errors.technologies ? true : false}
              helperText={errors.technologies}
              as={TextFieldS}
            />

            <FormControl>
              <InputLabel id="demo-multiple-chip-label">Chip</InputLabel>
              <Select 
                multiple
                id="technologies"
                name="technologies"                
                value={values.technologies}
                error={errors.technologies ? true : false}            
                onChange={handleChange}
                input={<OutlinedInput id="select-multiple-chip" label="Technologies" />}
                renderValue={(selected) => (
                  <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                    {selected.map((value) => (
                      <Chip variant="outlined"  key={value} label={value} />
                    ))}
                  </Box>
                )}             
              >
                {technologiesList.map((name) => (
                  <MenuItem
                    key={name}
                    value={name}
                  >
                    {name}
                  </MenuItem>
                ))}
              </Select>

              <FormHelperText>
              {errors.technologies}
              </FormHelperText>

            </FormControl>

            

            





            <div className='flex flex-col'>
              <ButtonS variant="contained" type="submit">Create colaboration</ButtonS>
            </div>
          </form>

        </>
      )}


    </Formik>
  )
}
