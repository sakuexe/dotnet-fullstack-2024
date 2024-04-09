import { fetchPieChart } from './fetchUtils.js';

const pieChartElement = document.querySelector('canvas#pie-chart');
const totalElement = document.querySelector('p#total_expenses');
let chart;

export async function updatePieChart() {
  console.log("updating piechart");
  const pieChartData = await fetchPieChart();
  // get the colors for the pie chart
  // they are stored in a json file, so that I can use the same
  // colors in the pie chart and the top categories
  const colors = await fetch('/colors.json').then(res => res.json());
  createPieChart(pieChartData, colors);
  // show the total amount of expenses
  const total = pieChartData.reduce((acc, d) => acc + d.Amount, 0);
  totalElement.textContent = `${total / 100} €`;
}

document.addEventListener('DOMContentLoaded', async () => updatePieChart());

function capitalizeFirstLetter(string) {
  return string.charAt(0).toUpperCase() + string.slice(1);
}

function createPieChart(data, colors) {
  const piedata = {
    labels: data.map(d => capitalizeFirstLetter(d.Category)),
    datasets: [{
      data: data.map(d => d.Amount / -100 ),
      backgroundColor: colors,
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
  if (chart) chart.destroy();
  chart = new Chart(ctx, config);
}
