import { showAlert } from "../../helpers/showAlert.js";

const token = localStorage.getItem("token");

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
