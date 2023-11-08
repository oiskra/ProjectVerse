/** @type {import('tailwindcss').Config} */
import {collorPalette} from './src/colorPalette'

module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}",],
  theme: {
    extend: {
      animation:{
        'fadeIn' : 'fadeIn 0.3s forwards',
        'slideIn': 'slideIn 0.3s forwards'
      }
    },
    colors:{
      black:"#000000",
      white:"#E3E3E3",
      highlight:"#00F9F9",
      sucess:"#138636",
      accent:"#FFC328",
      warning:"#FFCC00",
      background:"#161616",
      danger:"#FF4444"
    },
    // colors: {collorPalette}
  },
  plugins: [],
}