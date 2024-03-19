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
        'secondary': {
          '50': '#f7f8ed',
          '100': '#e4e6da',
          '200': '#dbe3b3',
          '300': '#c3d086',
          '400': '#aabb60',
          '500': '#8da042',
          '600': '#6e7f31',
          '700': '#546229',
          '800': '#454f25',
          '900': '#3b4423',
          '950': '#1e240f',
          DEFAULT: '#f7f8ed',
        },
        'luonnos': '#71641F',
        'hylatty': '#6E1D1D',
        'laskutettu': '#202E05',
        'odottaa': '#1A4963',
        'kuitattu': '#52564A',
      },
      fontFamily: {
        'sans': ['Inter', 'sans-serif'],
        'serif': ['Merriweather', 'serif'],
      },
    },
  },
  plugins: [],
}

