const addForm = document.getElementById("add_expense");
const errorElement = addForm.querySelector(".error");

async function addData() {
  const url = "/Finances/AddExpense";
  let response;
  try {
    const formData = new FormData(addForm);
    response = await fetch(url, {
      method: "POST",
      body: formData,
    });
  } catch (error) {
    console.error(error);
    errorElement.textContent = error.message;
  }
  // check if bad request, then show error messages
  if (response.status === 400) {
    const data = await response.json();
    showErrorMessages(data);
    return;
  }
  // unknown error
  if (!response.ok) {
    errorElement.textContent = `Unknown error occurred: ${response.status} \n ${response.statusText}`;
    return
  }
  // success
  addForm.reset();
  addForm.classList.add("hidden");
  addForm.classList.remove("flex");
  // we added fetchExpenses to the global scope in home/index.cshtml
  // so that we can refresh the data after adding a new expense
  fetchExpenses();
}

async function showErrorMessages(errors) {
  for (const error in errors) {
    const errorSpan = document.createElement("p");
    errorSpan.classList.add("text-red-500", "text-center");
    errorSpan.textContent = errors[error].ErrorMessage;
    errorElement.appendChild(errorSpan);
  }
}

addForm.addEventListener("submit", (event) => {
  event.preventDefault();
  // clear error message
  errorElement.textContent = "";
  addData();
});
