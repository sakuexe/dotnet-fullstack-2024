import { fetchPieChart } from './fetchUtils.js';

const pieChartElement = document.querySelector('canvas#pie-chart');
const totalElement = document.querySelector('p#total_expenses');

document.addEventListener('DOMContentLoaded', async () => {
  const pieChartData = await fetchPieChart();
  createPieChart(pieChartData);
  // show the total amount of expenses
  const total = pieChartData.reduce((acc, d) => acc + d.Amount, 0);
  totalElement.textContent = `${total / 100} €`;
});

function capitalizeFirstLetter(string) {
  return string.charAt(0).toUpperCase() + string.slice(1);
}

function createPieChart(data) {
  const piedata = {
    labels: data.map(d => capitalizeFirstLetter(d.Category)),
    datasets: [{
      data: data.map(d => d.Amount / -100 ),
      backgroundColor: [
        '#9333ea',
        '#e11d48',
        '#4f46e5',
        '#0891b2',
      ],
      borderWidth: 0,
      hoverOffset: 4
    }],
  };
  const config = {
    type: 'doughnut',
    data: piedata,
    options: {
      // remove the legend
      plugins: {
        legend: false,
      },
      // make the donut chart less thick
      cutout: '70%',
    },
  };

  const ctx = pieChartElement.getContext('2d');
  new Chart(ctx, config);
}
