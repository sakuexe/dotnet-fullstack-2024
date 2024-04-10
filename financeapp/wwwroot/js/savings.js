import { getTotalSavings } from "./fetchUtils.js";

const canvas = document.querySelector('canvas#savings');
const ctx = canvas.getContext('2d');
let chart;

export async function updateSavingsChart() {
  const data = await getTotalSavings();
  const labels = data.totalByDays.map((day) => day.Date);
  const values = data.totalByDays.map((day) => (day.Total / 100).toFixed(2));
  const savingsGoal = data.savingsGoal;
  // make a values length list of just savings goals
  const savingsGoalList = Array(values.length).fill(savingsGoal);

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
          borderColor: 'rgb(75, 192, 192)',
        },
        {
          label: 'Goal',
          data: savingsGoalList,
          borderColor: 'rgb(255, 99, 132)',
        },
      ],
    },
  });
}
