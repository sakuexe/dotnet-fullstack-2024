const addForm = document.querySelector("#add_expense > form");
const errorElement = addForm.querySelector(".error");

export async function addNewExpense() {
  const url = "/Finances/Add";
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
  if (response.status === 400 || response.status === 500) {
    const data = await response.json();
    console.log(data);
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
  addForm.parentElement.classList.add("hidden");
}

async function showErrorMessages(errors) {
  for (const error in errors) {
    const errorSpan = document.createElement("p");
    errorSpan.classList.add("text-red-500", "text-center");
    errorSpan.textContent = errors[error].ErrorMessage;
    errorElement.appendChild(errorSpan);
  }
}
