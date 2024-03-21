function getStonkSpan() {
  const currentActiveFilter = document.querySelector('button.active');
  return currentActiveFilter?.dataset.duration;
}

// draw the stonks
export async function drawStonks(stockValues) {
  const ctx = document.getElementById('main_chart');
  const color = checkStonkDelta(stockValues) > 0 ? '#22c55e' : '#ef4444';
  // get the ten latest stonks
  const duration = getStonkSpan();
  const latestStonks = Object.entries(stockValues).slice(-duration);

  // if the chart already exists, destroy it
  let chartStatus = Chart.getChart('main_chart');
  if (chartStatus) {
    chartStatus.destroy();
  }

  new Chart(ctx, {
    type: 'line',
    data: {
      datasets: [{
        label: 'Stock Value',
        data: latestStonks,
        backgroundColor: `${color}55`,
        borderColor: color,
        borderWidth: 1,
        fill: true,
      }]
    },
    options: {
      // make the line pointy
      elements: {
        line: {
          tension: 0
        }
      },
      plugins: {
        legend: false,
      }
    }
  });
}

// check the delta of the stonks
function checkStonkDelta(stonkValues) {
  const length = Object.keys(stonkValues).length;
  const lastValue = Object.values(stonkValues)[length - 1]
  const secondLastValue = Object.values(stonkValues)[length - 2]
  return (lastValue - secondLastValue) / secondLastValue * 100;
}

// add the stonk data related to the company
export function addStonkData(stonks) {
  const [name, shortName] = document.querySelectorAll('#company_names > *');
  const logo = document.querySelector('#company_logo');
  const [percentage, value] = document.querySelectorAll('#stonk_values > div > *');
  const lastUpdated = document.querySelector('#stonk_values > p');
  // set the names
  name.textContent = stonks.Name;
  shortName.textContent = stonks.ShortName;
  // set the logo
  logo.src = `/images/${stonks.Name.toLowerCase()}.png`;
  logo.alt = `${stonks.Name} logo`;
  // set the current value
  const length = Object.keys(stonks.StockValues).length;
  value.textContent = `\$${Object.values(stonks.StockValues)[length - 1]}`;
  // set the percentage
  const delta = checkStonkDelta(stonks.StockValues);
  percentage.textContent = `${delta.toFixed(2)}%`;
  percentage.style.backgroundColor = delta > 0 ? '#22c55e' : '#ef4444';
  // set the last updated
  lastUpdated.textContent = `Updated: ${new Date().toLocaleTimeString()}`;
}

