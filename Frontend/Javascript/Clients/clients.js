import { fetchClients } from "../modules/Clients/api.js";

// Get user's role and token from local storage
const userRole = localStorage.getItem("role");

// Array to store clients
let clients = [];

// Display user's full name in the navbar
document.addEventListener("DOMContentLoaded", () => {
  const fullName = localStorage.getItem("fullName");
  if (fullName) {
    document.getElementById("fullName").textContent = fullName;
  }
});

//Load clients when the page is loaded
addEventListener("load", async () => {
  await generateTable();
});

// Generate table with clients
const generateTable = async () => {
  clients = await fetchClients();
  let tableContent = "";

  clients.forEach((client) => {
    tableContent += `
            <tr>
                <td>${client.idClient}</td>
                <td>${client.name}, ${client.lastname}</td>
                <td>${client.email}</td>
                <td>${client.street} ${client.number}</td>
                <td>${client.city}</td>
                <td>${client.state}</td>
                <td>${client.healthPlan ? client.healthPlan : "No Registrado"}</td>
            </tr>
        `;
  });

  const tableBody = document.querySelector("#clients-table tbody");
  tableBody.innerHTML = tableContent;
};
