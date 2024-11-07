import {showAlert} from '../helpers/showAlert.js';

// Get user's role and token from local storage
const userRole = localStorage.getItem("role");
const token = localStorage.getItem("token");

// Array to store clients
let supplies = [];

// https://localhost:3000/api/Supplies'

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
    let tableContent = '';

    supplies.forEach((supply)=>{
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
        `
    })

    const tableBody = document.querySelector("#supplies-table tbody");
    tableBody.innerHTML = tableContent;
};

async function fetchSupplies(){
    try {
        const response = await fetch('https://localhost:3000/api/Supplies',{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        
        });
        if(response.ok) return await response.json();
    }
    catch (error) {
        showAlert("error", `Error: ${error}`);
        return [];
    }
}