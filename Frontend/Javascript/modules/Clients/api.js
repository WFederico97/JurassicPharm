import { showAlert } from "../../helpers/showAlert.js";
const token = localStorage.getItem("jwtToken");

export async function fetchClients() {
  try {
    const response = await fetch("http://localhost:3000/api/Client", {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    if (response.ok) return await response.json();
  } catch (error) {
    showAlert("Error fetching clients", "danger");
    return [];
  }
}
