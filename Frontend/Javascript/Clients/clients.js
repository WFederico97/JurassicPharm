import { getAllHealthPlan } from "../modules/HealthPlan/api.js";
import { getAllCities } from "../modules/City/api.js";
import { showAlert } from "../helpers/showAlert.js";
import {
  createClient,
  deleteClient,
  fetchClients,
  updateClient,
} from "../modules/Clients/api.js";

let selectedClientId = 0;

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
addEventListener("DOMContentLoaded", async () => {
  await generateTable();
  setSelectValues("edit");
});

// Generate table with clients
const generateTable = async () => {
  clients = await fetchClients();
  let tableContent = "";

  clients.forEach((client, index) => {
    tableContent += `
            <tr>
                <td>${client.idClient}</td>
                <td>${client.name}, ${client.lastname}</td>
                <td>${client.email}</td>
                <td>${client.street} ${client.number}</td>
                <td>${client.city}</td>
                <td>${client.state}</td>
                <td>${
                  client.healthPlan ? client.healthPlan : "No Registrado"
                }</td>
                 <td class="d-flex">
                <button
                  id="btn-edit"
                  type="button" 
                  class="btn btn-primary rounded-pill px-3 m-1 editBtn"  
                  data-bs-toggle="modal" 
                  data-bs-target="#editClientModal" 
                  data-client-id="${client.idClient}"
                  data-index="${index}"
                  ${userRole != "ADMIN" ? "disabled" : ""}
                >
                  <svg height="1em" viewBox="0 0 512 512">
                      <path
                      d="M410.3 231l11.3-11.3-33.9-33.9-62.1-62.1L291.7 89.8l-11.3 11.3-22.6 22.6L58.6 322.9c-10.4 10.4-18 23.3-22.2 37.4L1 480.7c-2.5 8.4-.2 17.5 6.1 23.7s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L387.7 253.7 410.3 231zM160 399.4l-9.1 22.7c-4 3.1-8.5 5.4-13.3 6.9L59.4 452l23-78.1c1.4-4.9 3.8-9.4 6.9-13.3l22.7-9.1v32c0 8.8 7.2 16 16 16h32zM362.7 18.7L348.3 33.2 325.7 55.8 314.3 67.1l33.9 33.9 62.1 62.1 33.9 33.9 11.3-11.3 22.6-22.6 14.5-14.5c25-25 25-65.5 0-90.5L453.3 18.7c-25-25-65.5-25-90.5 0zm-47.4 168l-144 144c-6.2 6.2-16.4 6.2-22.6 0s-6.2-16.4 0-22.6l144-144c6.2-6.2 16.4-6.2 22.6 0s6.2 16.4 0 22.6z"
                      ></path>
                  </svg>
                </button>
                <button 
                  type="button" 
                  class="btn btn-danger rounded-pill px-3 m-1 bin-button"  
                  data-bs-toggle="modal" 
                  data-bs-target="#deleteClientModal" 
                  onclick="prepareDeleteModal(${index})"
                  ${userRole != "ADMIN" ? "disabled" : ""}
                >
                  <svg
                    class="bin-top"
                    viewBox="0 0 39 7"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <line y1="5" x2="39" y2="5" stroke="white" stroke-width="4"></line>
                    <line
                    x1="12"
                    y1="1.5"
                    x2="26.0357"
                    y2="1.5"
                    stroke="white"
                    stroke-width="3"
                    ></line>
                  </svg>
                  <svg
                      class="bin-bottom"
                      viewBox="0 0 33 39"
                      fill="none"
                      xmlns="http://www.w3.org/2000/svg"
                  >
                      <mask id="path-1-inside-1_8_19" fill="white">
                      <path
                          d="M0 0H33V35C33 37.2091 31.2091 39 29 39H4C1.79086 39 0 37.2091 0 35V0Z"
                      ></path>
                      </mask>
                      <path
                      d="M0 0H33H0ZM37 35C37 39.4183 33.4183 43 29 43H4C-0.418278 43 -4 39.4183 -4 35H4H29H37ZM4 43C-0.418278 43 -4 39.4183 -4 35V0H4V35V43ZM37 0V35C37 39.4183 33.4183 43 29 43V35V0H37Z"
                      fill="white"
                      mask="url(#path-1-inside-1_8_19)"
                      ></path>
                      <path d="M12 6L12 29" stroke="white" stroke-width="4"></path>
                      <path d="M21 6V29" stroke="white" stroke-width="4"></path>
                  </svg>
                </button>              
              </td>
            </tr>
        `;
  });

  const tableBody = document.querySelector("#clients-table tbody");
  tableBody.innerHTML = tableContent;

  document.querySelectorAll("#btn-edit").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const button = e.target.closest("#btn-edit");
      if (button) {
        selectedClientId = button.getAttribute("data-client-id");
      }
    });
  });
};

// Populate the Edit Employee Modal with selected employee data
document
  .getElementById("editClientModal")
  .addEventListener("show.bs.modal", function (event) {
    const button = event.relatedTarget;
    const index = button.getAttribute("data-index");
    const client = clients[index];
    const healthPlanSelect = document.getElementById("edit-health-plan-select");

    if (!client) return;

    const { name, lastname, street, number, email, healthPlan } = client;

    document.getElementById("edit-input-name").value = name;

    document.getElementById("edit-input-lastname").value = lastname;
    document.getElementById("edit-input-street").value = street;
    document.getElementById("edit-input-street-height").value = number;
    document.getElementById("edit-input-email").value = email;

    Array.from(healthPlanSelect.options).forEach((option) => {
      option.selected = option.textContent === healthPlan;
    });
  });

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

const setSelectValues = async (mode) => {
  const healthPlanSelect = document.getElementById(
    `${mode === "edit" && "edit-"}health-plan-select`
  );
  const citySelect = document.getElementById(
    `${mode === "edit" && "edit-"}city-select`
  );
  const healtPlans = await getAllHealthPlan();
  const cities = await getAllCities();

  if (healtPlans.length < 0 || cities.length < 0) return;

  healtPlans.forEach(({ id, name }) => {
    healthPlanSelect.innerHTML += `<option value="${id}">${name}</option>`;
  });

  cities.forEach(({ idCiudad, nombre, provincia }) => {
    citySelect.innerHTML += `<option value="${idCiudad}">${nombre}, ${provincia}</option>`;
  });
};

const handleClientActions = async (e, action) => {
  const prefix = action === "edit" ? "edit-" : "";
  e.preventDefault();
  const healthPlanSelect = document.getElementById(
    `${prefix}health-plan-select`
  );
  const citySelect = document.getElementById(`${prefix}city-select`);
  const name = document.getElementById(`${prefix}input-name`);
  const lastname = document.getElementById(`${prefix}input-lastname`);
  const street = document.getElementById(`${prefix}input-street`);
  const streetHeight = document.getElementById(`${prefix}input-street-height`);
  const email = document.getElementById(`${prefix}input-email`);

  const idHealthPlan =
    healthPlanSelect.options[healthPlanSelect.selectedIndex].value;
  const idCity = citySelect.options[citySelect.selectedIndex].value;

  if (!emailRegex.test(email.value)) {
    document.querySelector(".btn-close").click();
    showAlert("Por favor, ingrese un correo electrónico válido.", "warning");
    return;
  }

  try {
    const payload = {
      idHealthPlan: Number(idHealthPlan),
      idCity: Number(idCity),
      name: name.value,
      lastname: lastname.value,
      email: email.value,
      street: street.value,
      number: Number(streetHeight.value),
    };

    if (action === "create") {
      await createClient(payload);
    } else {
      await updateClient(payload, selectedClientId);
    }

    const modalInstance = bootstrap.Modal.getInstance(
      document.getElementById(
        action === "edit" ? "editClientModal" : "addClientModal"
      )
    );

    modalInstance.hide();

    showAlert(
      `Cliente ${
        action === "create" ? "creado" : "editado"
      } exitosamente", "success`
    );

    await generateTable();
  } catch (error) {
    console.log(error);
    document.querySelector(".btn-close").click();

    showAlert("Ocurrió un error inesperado.", "warning");
  }
};

// Prepare the Delete Client Modal
window.prepareDeleteModal = function (index) {
  const client = clients[index];

  document.getElementById("deleteNombre").textContent = client.name;
  document.getElementById("deleteApellido").textContent = client.lastname;
  document.getElementById("deleteCorreo").textContent = client.email;

  document.getElementById("confirmDeleteButton").onclick = async function () {
    try {
      const response = await deleteClient(client.idClient);
      if (response.ok) {
        const message = await response.text();
        showAlert(message, "success");
        await generateTable();
      } else {
        const errorText = await response.text();
        showAlert(`Error al eliminar el cliente: ${errorText}`, "danger");
      }
    } catch (err) {
      showAlert("Error inesperado. Consulte con atención al cliente", "danger");
    } finally {
      const deleteModal = bootstrap.Modal.getInstance(
        document.getElementById("deleteClientModal")
      );
      deleteModal.hide();
    }
  };
};

//handle add client
const addClientBtn = document.querySelector(".btnAgregarEmpleado");
addClientBtn.addEventListener("click", () => setSelectValues("create"));

const addClientform = document.getElementById("addClientForm");
addClientform.addEventListener("submit", (e) =>
  handleClientActions(e, "create")
);

//handle edit form submit
const editClientform = document.getElementById("editClientForm");
editClientform.addEventListener("submit", (e) =>
  handleClientActions(e, "edit")
);

//
