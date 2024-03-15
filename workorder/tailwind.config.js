/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    // get all the cshtml files
    './Pages/**/*.cshtml',
    './Views/**/*.cshtml',
    // get all the static js files
    './wwwroot/**/*.js',
  ],
  theme: {
    extend: {
      colors: {
        'primary': {
          '50': '#fbfee7',
          '100': '#f3fccb',
          '200': '#e7f99d',
          '300': '#d2f25c',
          '400': '#bee734',
          '500': '#a0cd15',
          '600': '#7ca40c',
          '700': '#5e7c0f',
          '800': '#4b6212',
          '900': '#405314',
          '950': '#202e05',
          DEFAULT: '#202e05',
        },
      }
    },
  },
  plugins: [],
}

