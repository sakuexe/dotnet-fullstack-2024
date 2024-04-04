const addForm = document.getElementById("add_expense");
const errorElement = addForm.querySelector(".error");

async function addData() {
  const url = "/Finance/Add";
  let data;
  try {
    const formData = new FormData(addForm);
    const response = await fetch(url, {
      method: "POST",
      body: formData,
    });
    // check if bad request, then show error messages
    if (response.status === 400) {
      data = await response.json();
      showErrorMessages(data);
      return;
    }
    // unknown error
    if (!response.ok) {
      throw new Error(`Unknown error occurred: ${response.status} \n ${response.statusText}`);
    }
    data = await response.json();
  } catch (error) {
    console.error(error);
    errorElement.textContent = error.message;
  }
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
