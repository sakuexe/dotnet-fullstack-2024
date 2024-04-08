import { fetchExpenses, initializeDeleteButtons, fetchPieChart } from './fetchUtils.js';

// Dashboard.js is like the main file for the dashboard page
// only it is directly linked to the HTML file and is using
// type="module" in the script tag.

document.addEventListener('DOMContentLoaded', async () => {
  await fetchExpenses();
  initializeDeleteButtons();
  fetchPieChart();
});

const addButtons = document.querySelectorAll('#add_expense_btn, #add_income_btn');
function toggleForm(button) {
  const form = document.querySelector('form#add_expense');
  form.classList.toggle('hidden');
  form.classList.toggle('flex');
  // toggle between expense and income, so we dont need to add a new form
  if (button.id === 'add_income_btn') {
    form.querySelector('input[type=checkbox]').checked = false;
  } else {
    form.querySelector('input[type=checkbox]').checked = true;
  }
  document.addEventListener('click', (e) => {
    if (e.target === button) return;
    if (e.target !== form && !form.contains(e.target)) {
      form.classList.add('hidden');
      form.classList.remove('flex');
    }
  });
}
addButtons.forEach((button) => {
  button.addEventListener('click', () => {
    toggleForm(button);
  });
});
