import { drawStonks, addStonkData } from "./stonkutils.js";

async function getStonks(businessName = "Apple") {
  const url = "/stonks/_stonks";

  const formData = new FormData();
  formData.append("businessName", businessName);

  const response = await fetch(url, {
    method: "POST",
    body: formData
  });

  if (!response.ok) {
    return;
  }
  return await response.json();
}

// on load of the page
document.addEventListener("DOMContentLoaded", async () => {
  const stonks = await getStonks();
  drawStonks(stonks.StockValues);
  addStonkData(stonks);

  const businessElements = document.querySelectorAll(".business-btn");
  businessElements.forEach((element) => {
    element.addEventListener("click", async () => {
      const businessName = element.querySelector(".small-business-name");
      const stonks = await getStonks(businessName.textContent);
      drawStonks(stonks.StockValues);
      addStonkData(stonks);
    });
  });

  const filters = document.querySelectorAll("button.filter");
  const activeFilterStyles = ["active", "bg-neutral-700", "text-neutral-50"];
  filters.forEach((filter) => {
    filter.addEventListener("click", async () => {
      filters.forEach((filter) => filter.classList.remove(...activeFilterStyles));
      filter.classList.add(...activeFilterStyles);
      drawStonks(stonks.StockValues);
    });
  })
});
