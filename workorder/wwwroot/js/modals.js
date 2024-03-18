const modal = document.querySelector("#modal");
const modalHeader = document.querySelector("#modal_header");
const modalContent = document.querySelector("#modal_content");
const modalClose = document.querySelector("#modal_close");
const workersButtons = document.querySelectorAll(".workers-btn");

function toggleModal() {
  modal.parentElement.classList.toggle("hidden");
  modal.parentElement.classList.toggle("grid");
}

async function setCustomerInfo(h2Element) {
  const workId = h2Element.querySelector(".work_id").textContent;
  const customerId = h2Element.querySelector(".customer_id").textContent;
  const customerBusiness = h2Element.querySelector(".customer_business").textContent;
  // set the modal header to the customer name
  modalHeader.querySelector("div > p:first-child").textContent = `${customerBusiness} (${customerId})`;
  modalHeader.querySelector("div > p:last-child").textContent = `ID: ${workId}`;
}

async function setWorkers(workers) {
  const workersDict = {
    "hylatty": [],
    "vahvistettu": [],
    "odottaa": []
  };

  workers.forEach((worker) => {
    const status = worker.dataset.status;
    workersDict[status].push(worker.textContent.split(",")[0]);
  });

  const negativeList = modalContent.querySelector("#modal_negative > p");
  const confirmedList = modalContent.querySelector("#modal_confirmed > p");
  const pendingList = modalContent.querySelector("#modal_pending > p");

  negativeList.innerText = workersDict.hylatty.join(", ");
  confirmedList.innerText = workersDict.vahvistettu.join(", ");
  pendingList.innerText = workersDict.odottaa.join(", ");

  // set the counters
  const negativeCounter = modalContent.querySelector("#modal_negative > div > p:last-child");
  const confirmedCounter = modalContent.querySelector("#modal_confirmed > div > p:last-child");
  const pendingCounter = modalContent.querySelector("#modal_pending > div > p:last-child");

  negativeCounter.textContent = workersDict.hylatty.length;
  confirmedCounter.textContent = workersDict.vahvistettu.length;
  pendingCounter.textContent = workersDict.odottaa.length;
}

workersButtons.forEach((btn) => {
  btn.addEventListener("click", (event) => {
    const workInfo = event.target.previousElementSibling;
    setCustomerInfo(workInfo);
    const workerNames = event.target.closest(".workorder").querySelectorAll(".worker-name");
    setWorkers(workerNames);

    // get the parent element with the div of workorder
    toggleModal();
  });
})

modalClose.addEventListener("click", toggleModal);

modal.parentElement.addEventListener("click", (event) => {
  if (event.target === modal.nextElementSibling) {
    toggleModal();
  }
});
