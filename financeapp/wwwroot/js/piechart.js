import { fetchPieChart } from './fetchUtils.js';

const pieChartElement = document.querySelector('canvas#pie-chart');

document.addEventListener('DOMContentLoaded', async () => {
  const pieChartData = await fetchPieChart();
  createPieChart(pieChartData);
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
        'rgb(255, 99, 132)',
        'rgb(54, 162, 235)',
        'rgb(255, 205, 86)'
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
