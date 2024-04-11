import { getTotalSavings } from "./fetchUtils.js";

const canvas = document.querySelector('canvas#savings');
const ctx = canvas.getContext('2d');
let chart; // make the chart a global variable so we can destroy it when we need to update it

async function updateSavingsInfo(savings){
  const savingsInfo = document.querySelector('#savings_info');
  const delta = savingsInfo.querySelector('span#delta');
  const avg = savingsInfo.querySelector('span#avg_daily');

  const total = savings.totalByDays[savings.totalByDays.length - 1].Total / 100;
  const currentDate = new Date(savings.totalByDays[savings.totalByDays.length - 1].Date);
  const daysUntilEndOfMonth = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0).getDate() - currentDate.getDate();
  
  delta.textContent = (total - savings.savingsGoal).toFixed(2);
  avg.textContent = ((total - savings.savingsGoal) / daysUntilEndOfMonth).toFixed(2);
}

export async function updateSavingsChart() {
  const data = await getTotalSavings();
  updateSavingsInfo(data);
  const labels = data.totalByDays.map((day) => day.Date);
  const values = data.totalByDays.map((day) => (day.Total / 100).toFixed(2));
  const savingsGoal = data.savingsGoal;
  // make a values length list of just savings goals
  const savingsGoalList = Array(values.length).fill(savingsGoal);

  // if the current total is above the savings goal, make the line green
  // otherwise make it orange
  const savingsLineColor = values[values.length - 1] > savingsGoal ? '#a1c181' : '#F77F00';

  if (chart) {
    chart.destroy();
  }

  chart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: labels,
      datasets: [
        {
          label: 'Savings',
          data: values,
          borderColor: savingsLineColor,
        },
        {
          label: 'Goal',
          data: savingsGoalList,
          borderColor: '#eae2b78f',
        },
      ],
    },
    options: {
      responsive: true,
      // when hovering, show the value of all nodes
      // on the nearest x axis
      interaction: {
        mode: 'index',
        intersect: false,
      },
    },
  });
}
