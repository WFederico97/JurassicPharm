import { fetchSupplies } from "../modules/Supllies/api.js";

// Get user's role and token from local storage
const userRole = localStorage.getItem("role");

// Array to store clients
let supplies = [];

// Display user's full name in the navbar
document.addEventListener("DOMContentLoaded", () => {
  const fullName = localStorage.getItem("fullName");
  console.log("User role:", userRole);
  if (fullName) {
    document.getElementById("fullName").textContent = fullName;
  }
});

//Load supplies when the page is loaded
addEventListener("load", async () => {
  await generateTable();
});

// Generate table with clients
const generateTable = async () => {
  supplies = await fetchSupplies();
  console.log(supplies);
  let tableContent = "";

  supplies.forEach((supply) => {
    tableContent += `
            <tr>
                <td>${supply.idSupply}</td>
                <td>${supply.name}</td>
                <td>${supply.brand}</td>
                <td>${supply.distribution}</td>
                <td>${supply.supplyType}</td>
                <td>${supply.price}</td>
                <td>${supply.stock}</td>
            </tr>
        `;
  });

  const tableBody = document.querySelector("#supplies-table tbody");
  tableBody.innerHTML = tableContent;
};
