import { showAlert } from "../../helpers/showAlert.js";

const token = localStorage.getItem("jwtToken");


export async function fetchSupplies() {
  try {
    const response = await fetch("http://localhost:3000/api/Supplies", {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    if (response.ok) return await response.json();
  } catch (error) {
    showAlert(`Error: ${error}`, "danger");
    return [];
  }
}

export async function updateSupplies(supply) {
  try {
    const response = await fetch(
      `http://localhost:3000/api/UpdateSupply/${supply.idSupply}`,
      {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(supply),
      }
    );
    if(response.ok){
      showAlert("Supply updated successfully", "success");
      return await response.json();
    }
    else{
      showAlert("Error updating supply", "danger");
      return [];
    }
  }
  catch (error) {
    showAlert(`Error: ${error}`, "danger");
    return [];
  }
}

export async function getSuppliesSelectOptions() {
  try {
    const response = await fetch("http://localhost:3000/api/selectOptions", {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    if (response.ok) return await response.json();
  }
  catch (error) {
    showAlert(`Error: ${error}`, "danger");
    return [];
  }
}

export async function addSupply(supply) {
  try {
    const response = await fetch("http://localhost:3000/api/NewSupply", {
      method: "POST", 
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(supply),
    });
    if(response.ok){
      showAlert("Supply added successfully", "success");
      return await response.json();
    }
    else{
      showAlert("Error adding supply", "danger");
      return [];
    }
  }
  catch (error) {
    showAlert(`Error: ${error}`, "danger");
    return [];
  }
}

export async function salesBySupply() {
  try {
    const response = await fetch("http://localhost:3000/api/salesBySupply", {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    if (response.ok) return await response.json();
  }
  catch (error) {
    showAlert(`Error: ${error}`, "danger");
    return [];
  }
}