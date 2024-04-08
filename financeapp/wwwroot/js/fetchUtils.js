export async function fetchExpenses() {
  const financesElement = document.querySelector('div#finances');
  const url = '/finances';
  let data;
  try {
    const response = await fetch(url, {
      method: 'POST',
    });
    // because the endpoint is returning HTML
    // we need to use the text() method to get the data
    data = await response.text();
  } catch (error) {
    console.error(error);
  }
  financesElement.innerHTML = data;
}

export async function fetchPieChart() {
  const pieChartElement = document.querySelector('.pie-chart');
  const url = '/finances/piechart';
  let data;
  try {
    const response = await fetch(url, {
      method: 'POST',
    });
    // because the endpoint is returning HTML
    // we need to use the text() method to get the data
    data = await response.text();
  } catch (error) {
    console.error(error);
  }
  pieChartElement.innerHTML = data;
}

export async function initializeDeleteButtons() {
  // allow the user to delete an expense
  const deleteButtons = document.querySelectorAll('#all_finances button[type="button"]');
  deleteButtons.forEach(button => {
    button.addEventListener('click', async () => {
      const form = button.parentElement;
      const response = await fetch('Finances/Delete', {
        method: form.method,
        body: new FormData(form),
      });
      if (response.ok) {
        form.parentElement.parentElement.remove();
        return;
      }
      // if the expense could not be deleted, show an alert
      const message = await response.text();
      const alert = document.createElement('div');
      alert.classList.add('text-red-400', 'px-4');
      alert.textContent = message;
      form.parentElement.parentElement.appendChild(alert);
    });
  });
}
