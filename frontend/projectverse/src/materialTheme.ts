import { ThemeOptions } from '@mui/material/styles';
import { createTheme } from '@mui/material/styles';



const themeOptions: ThemeOptions = {
  palette: {
    mode: 'dark',
    primary: {
      main: '#FFC328',
    },
    secondary: {
      main: '#FFC328',
    },
    text: {
      primary: '#F3F3F3',
      disabled: 'rgba(0,0,0,0.5)',
    },
    background: {
      default: '#161616',
      paper: '#000000',
    },
  },
};

export const theme = createTheme(themeOptions)