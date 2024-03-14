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
    extend: {},
  },
  plugins: [],
}

