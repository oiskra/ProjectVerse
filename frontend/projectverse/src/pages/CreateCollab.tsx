import { Field, Formik } from 'formik';
import React, { useState } from 'react'
import CollaborationSchema from '../data/ValidationSchemas/CollaborationSchema';
import { TextFieldS } from '../customElements/styledTextField';
import { ButtonS } from '../customElements/ButtonS';
import { Box, Chip, FormControl, FormGroup, FormHelperText, InputLabel, MenuItem, OutlinedInput, Select } from '@mui/material';
import technologiesList from '../data/tempTechnologiesList';
import { useAddCollabMutation } from '../features/Collaborations/colabApiSlice';
import { useDispatch } from 'react-redux';
import Collaboration from '../data/Collaboration';
import {addCollab } from '../features/Collaborations/collabSlice';
import { useNavigate } from 'react-router-dom';

export const CreateCollab = () => {

  const [fetchAddColab] = useAddCollabMutation()
  const dispatch = useDispatch();
  const navigate = useNavigate();

  return (
    <Formik
      validateOnChange={false}
      validateOnBlur={false}
      initialValues={{ name: "", description: "", difficulty: 1, technologies: [],collaborationPositions:[{name:"",description:""}] }}
      validationSchema={CollaborationSchema}
      onSubmit={async (data, { setSubmitting }) => {
        setSubmitting(true);
        
        try {
          
          fetchAddColab(data).unwrap()
          .then((response: Collaboration)=>{
            dispatch(addCollab(response));
            navigate(`/collab_dashboard/${response.id}`);
          })         
          
        }
        catch (err) {
          console.error(err)
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

            <Field           
              value={values.collaborationPositions.name}
              onChange={handleChange}
              onBlur={handleBlur}
              name="Position name"
              label="Position name"
              variant="outlined"
              error={errors.technologies ? true : false}
              helperText={errors.technologies}
              as={TextFieldS}
            />

            <Field          
              value={values.collaborationPositions.description}
              onChange={handleChange}
              onBlur={handleBlur}
              name="Position Description"
              label="Position description"
              variant="outlined"
              error={errors.technologies ? true : false}
              helperText={errors.technologies}
              as={TextFieldS}
            />

            





            <div className='flex flex-col'>
              <ButtonS variant="contained" type="submit">Create colaboration</ButtonS>
            </div>
          </form>

        </>
      )}


    </Formik>
  )
}
