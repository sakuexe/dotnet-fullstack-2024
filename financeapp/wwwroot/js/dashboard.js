import { fetchExpenses, initializeDeleteButtons, } from './fetchUtils.js';
import { addNewExpense } from './addnewexpense.js';
import { updatePieChart } from './piechart.js';

// Dashboard.js is like the main file for the dashboard page
// it is used to fetch expenses and handle their modifications

document.addEventListener('DOMContentLoaded', async () => {
  await fetchExpenses();
  initializeDeleteButtons();
});

const addForm = document.getElementById("add_expense");
const errorElement = addForm.querySelector(".error");

addForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  // clear error message
  errorElement.textContent = "";
  await addNewExpense();
  fetchExpenses();
  initializeDeleteButtons();
  updatePieChart();
});

const addButtons = document.querySelectorAll('#add_expense_btn, #add_income_btn');

function toggleForm(button) {
  const form = document.querySelector('#add_expense');
  form.classList.toggle('hidden');
  form.classList.toggle('block');
  // toggle between expense and income, so we dont need to add a new form
  if (button.id === 'add_income_btn') {
    form.querySelector('input[type=checkbox]').checked = false;
    form.querySelector('button[type=submit]').innerText = 'Add Income';
  } else {
    form.querySelector('input[type=checkbox]').checked = true;
    form.querySelector('button[type=submit]').innerText = 'Add Expense';
  }
  document.addEventListener('click', (e) => {
    if (e.target === form) {
      form.classList.add('hidden');
      form.classList.remove('block');
    }
  });
}
addButtons.forEach((button) => {
  button.addEventListener('click', () => {
    toggleForm(button);
  });
});
