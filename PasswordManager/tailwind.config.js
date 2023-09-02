const defaultTheme = require('tailwindcss/defaultTheme')
const colors = require('tailwindcss/colors')

/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Pages/**/*.razor",
        "./Shared/**/*.razor"
    ],
  theme: {
      extend: {
          fontFamily: {
              sans: [
                  "Open Sans Regular",
                  ...defaultTheme.fontFamily.sans,
              ],
          },
          colors: {
              primary: "#008dd5",
              secondary: "#373f51",
              neutral: "#f4f7f5",
          }
      },   
  },
  plugins: [
    require("@tailwindcss/typography"),
    require("@tailwindcss/forms"),
    require("@tailwindcss/aspect-ratio"),
    require("@tailwindcss/container-queries"),
  ],
};
