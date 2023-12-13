import { Formik, Field } from 'formik';
import { useDispatch } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import { ButtonS } from '../customElements/ButtonS';
import { TextFieldS } from '../customElements/styledTextField';
import ProjectSchema from '../data/ValidationSchemas/ProjectSchema';
import { FormControl, InputLabel, Select, OutlinedInput, Box, Chip, MenuItem, FormHelperText, Checkbox, FormGroup, FormControlLabel } from '@mui/material';
import technologiesList from '../data/tempTechnologiesList';
import { useAddCollabMutation } from '../features/Collaborations/colabApiSlice';
import { useAddProjectMutation } from '../features/Portfolio/portfolioApiSlice';
import { PORTFOLIO_AddProject } from '../features/Portfolio/portfolioThunks';

const AddProjectPage = () => {

  const dispatch = useDispatch();
  const navigate = useNavigate();
  const params = useParams();

  const [addProject,isLoading] = useAddProjectMutation();

  return (
    <Formik
      initialValues={
        { 
          name: "",
          description: "",
          projectUrl:"",
          usedTechnologies:[],
          isPrivate:false,
          isPublished:true,
        }
      }
      validationSchema={ProjectSchema}
      onSubmit={async (project, { setSubmitting }) => {

        setSubmitting(true);

        try {         
          dispatch(PORTFOLIO_AddProject(project))
          .then(()=>{navigate("/portfolio")})       

          console.log(project)
        }

        catch (err) {
          console.log(err)
        }

        setSubmitting(false);
      }

      }
    >
      {({ values, handleChange, handleBlur, handleSubmit, errors }) => (
        
          
 
            <form className="bg-background w-full max-w-3xl flex flex-col gap-5 p-10 neo rounded-xl z-20" onSubmit={handleSubmit}>

              <h1 className="text-accent text-5xl font-bold my-5">Add new Project</h1>

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
                name="description"
                label="Description"
                variant="outlined"
                error={errors.description ? true : false}
                helperText={errors.description}
                as={TextFieldS}
              />

              <Field
                value={values.projectUrl}
                onChange={handleChange}
                name="projectUrl"
                label="Project URL"
                variant="outlined"
                error={errors.projectUrl ? true : false}
                helperText={errors.projectUrl}
                as={TextFieldS}
              />

              <FormControl>
                <InputLabel id="demo-multiple-chip-label">Used technologies in order of importance</InputLabel>
                <Select 
                  multiple
                  id="usedTechnologies"
                  name="usedTechnologies"                
                  value={values.usedTechnologies}
                  error={errors.usedTechnologies ? true : false}            
                  onChange={handleChange}
                  input={<OutlinedInput id="select-multiple-chip" label="usedTechnologies" />}
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
                {errors.usedTechnologies}
                </FormHelperText>

              </FormControl>


              <FormGroup>

                <FormControlLabel 
                  label = "Private"
                  control={
                    <Field
                    checked={values.isPrivate}
                    onChange={handleChange}
                    name="isPrivate"
                    label="Private project"
                    variant="outlined"                    
                    as={Checkbox}
                  />
                }
                />

                <FormControlLabel 
                  label = "Publish to feed after adding"
                  control={
                    <Field
                    checked={values.isPublished}
                    onChange={handleChange}
                    name="isPublished"
                    label="Publish to feed after adding"
                    variant="outlined"
                    as={Checkbox}
                    />
                  }
                />
                

                
              </FormGroup>
              <div className='flex flex-col'>
                <ButtonS variant="contained" type="submit">Create position</ButtonS>
              </div>

            </form>
          
      )}


    </Formik>
  )
}

export default AddProjectPage