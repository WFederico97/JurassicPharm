import { showAlert } from "../../helpers/showAlert.js";
const token = localStorage.getItem("jwtToken");

export const getBranches = async () => {
  try {
    const response = await fetch("http://localhost:3000/api/Branches", {
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
};
