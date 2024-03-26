function getStonkSpan() {
	const currentActiveFilter = document.querySelector('button.active')
	return currentActiveFilter?.dataset.duration
}

// draw the stonks
export async function drawStonks(stockValues) {
	const ctx = document.getElementById('main_chart')
	const color = checkStonkDelta(stockValues) > 0 ? '#22c55e' : '#ef4444'
	// get the ten latest stonks
	const duration = getStonkSpan()
	const latestStonks = stockValues.slice(-duration)

	// if the chart already exists, destroy it
	let chartStatus = Chart.getChart('main_chart')
	if (chartStatus) {
		chartStatus.destroy()
	}

	new Chart(ctx, {
		type: 'line',
		data: {
			labels: latestStonks.map(stonk => stonk.Date),
			datasets: [
				{
					label: 'Stock Value',
					data: latestStonks.map(stonk => stonk.Value),
					backgroundColor: `${color}55`,
					borderColor: color,
					borderWidth: 1,
					fill: true,
				},
			],
		},
		options: {
			// make the line pointy
			elements: {
				line: {
					tension: 0,
				},
			},
			plugins: {
				legend: false,
			},
		},
	})
}

// check the delta of the stonks
function checkStonkDelta(stonkValues) {
	const lastValue = stonkValues[stonkValues.length - 1].Value
	const secondLastValue = stonkValues[stonkValues.length - 2].Value
	return ((lastValue - secondLastValue) / secondLastValue) * 100
}

// add the stonk data related to the company
export function addStonkData(stonks) {
	const [name, shortName] = document.querySelectorAll('#company_names > *')
	const businessNameField = document.querySelector(
		'input[type="hidden"]#business_name'
	)
	const logo = document.querySelector('#company_logo')
	const [percentage, value] = document.querySelectorAll(
		'#stonk_values > div > *'
	)
	const businessValueField = document.querySelector(
		'input[type="hidden"]#business_value'
	)
	const lastUpdated = document.querySelector('#stonk_values > p')
	// set the names
	name.textContent = stonks.Name
	shortName.textContent = stonks.ShortName
	businessNameField.value = stonks.Name
	// set the logo
	logo.src = `/images/${stonks.Name.toLowerCase()}.png`
	logo.alt = `${stonks.Name} logo`
	// set the current value
	const length = stonks.StockValues.length
	const currentValue = parseFloat(stonks.StockValues[length - 1].Value)
	value.textContent = `\$${currentValue.toFixed(2)}`
	businessValueField.value = parseInt(currentValue * 100)
	// set the percentage
	const delta = checkStonkDelta(stonks.StockValues)
	percentage.textContent = `${delta.toFixed(2)}%`
	percentage.style.backgroundColor = delta > 0 ? '#22c55e' : '#ef4444'
	// set the last updated
	lastUpdated.textContent = `Updated: ${new Date().toLocaleTimeString()}`
}
