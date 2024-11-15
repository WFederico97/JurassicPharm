import { showAlert } from "../../helpers/showAlert.js";
const token = localStorage.getItem("jwtToken");

export async function fetchClients() {
  try {
    const response = await fetch("http://localhost:3000/api/GetClients", {
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

export const createClient = async (payload) => {
  return await fetch("http://localhost:3000/api/NewClient", {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });
};

export const updateClient = async (payload, clientId) => {
  if (payload === null) return;

  return await fetch(`http://localhost:3000/api/Client/${clientId}`, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });
};

export const deleteClient = async (clientId) => {
  return await fetch(`http://localhost:3000/api/DeleteClient/${clientId}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });
};
