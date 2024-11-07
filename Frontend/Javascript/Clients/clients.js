import {showAlert} from '../helpers/showAlert.js';

// Get user's role and token from local storage
const userRole = localStorage.getItem("role");
const token = localStorage.getItem("token");

// Array to store clients
let clients = [];


// Display user's full name in the navbar
document.addEventListener("DOMContentLoaded", () => {
    const fullName = localStorage.getItem("fullName");
    console.log("User role:", userRole);
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
    let tableContent = '';

    clients.forEach((client)=>{
        tableContent += `
            <tr>
                <td>${client.idClient}</td>
                <td>${client.name}, ${client.lastname}</td>
                <td>${client.email}</td>
                <td>${client.street} ${client.number}</td>
                <td>${client.city}</td>
                <td>${client.state}</td>
                <td>${client.healthPlan}</td>

            </tr>
        `
    })

    const tableBody = document.querySelector("#clients-table tbody");
    tableBody.innerHTML = tableContent;
}; 


//Fetch Clients from API
async function fetchClients(){
    try {
        const response = await fetch('https://localhost:3000/api/Client',{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        if(response.ok) return await response.json();
    } catch (error) {
        showAlert('Error fetching clients', 'danger');
        return [];
    }
}