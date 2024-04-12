import { fetchPieChart } from './fetchUtils.js';

const pieChartOptions = {
  'expenses': {
    canvas: document.querySelector('canvas#expences.pie-chart'),
    chart: null,
  },
  'incomes': {
    canvas: document.querySelector('canvas#incomes.pie-chart'),
    chart: null,
  },
  'total': {
    canvas: document.querySelector('canvas#total.pie-chart'),
    chart: null,
  },
};

export async function updatePieChart() {
  // get the colors for the pie chart
  // they are stored in a json file, so that I can use the same
  // colors in the pie chart and the top categories
  const allPieChartData = await fetchPieChart('/finances/piechartdata');

  const totalExpensesData = allPieChartData.filter(d => d.Amount < 0);
  const expenseColors = await fetch('/expensecolors.json').then(res => res.json());
  createPieChart(totalExpensesData, expenseColors, pieChartOptions.expenses);

  const totalIncomeData = allPieChartData.filter(d => d.Amount > 0);
  const incomeColors = await fetch('/incomecolors.json').then(res => res.json());
  createPieChart(totalIncomeData, incomeColors, pieChartOptions.incomes);

  const colors = expenseColors.concat(incomeColors);
  createPieChart(allPieChartData, colors, pieChartOptions.total);
}

document.addEventListener('DOMContentLoaded', async () => updatePieChart());

function capitalizeFirstLetter(string) {
  return string.charAt(0).toUpperCase() + string.slice(1);
}

function createPieChart(data, colors, canvasType) {
  const piedata = {
    labels: data.map(d => capitalizeFirstLetter(d.Category)),
    datasets: [{
      data: data.map(d => d.Amount / 100 ),
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
      // make the chart responsive
      responsive: true,
    },
  };

  const ctx = canvasType.canvas.getContext('2d');
  if (canvasType.chart) canvasType.chart.destroy();
  canvasType.chart = new Chart(ctx, config);
  // show the total amount of expenses
  const totalElement = canvasType.canvas.parentElement.querySelector('p.chart-text');
  const total = data.reduce((acc, d) => acc + d.Amount, 0);
  totalElement.textContent = `${total / 100} €`;
}
