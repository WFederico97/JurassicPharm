let employees = [];
const userRole = localStorage.getItem("role");

//check user fullname for navbar
document.addEventListener("DOMContentLoaded", () => {
  const fullName = localStorage.getItem("fullName");
  console.log("Rol de usuario:", userRole);

  if (fullName) {
    document.getElementById("fullName").textContent = fullName;
  }
});

if (userRole === "ADMIN") {
  document.getElementById("addEmployeeButtonContainer").style.display = "block";
}

addEventListener("load", async (event) => {
  await generateTable();
}); //Load table when page is loaded

//Prepoluate Edit Employee Modal
document
  .getElementById("editEmployeeModal")
  .addEventListener("show.bs.modal", function (event) {
    let button = event.relatedTarget;

    let index = button.getAttribute("data-index");

    let employee = employees[index];

    let domicilio = employee.domicilio;

    let match = domicilio.match(/^(.+?)\s(\d+)$/);

    console.log(domicilio);

    document.getElementById("editNombre").value = employee.nombre || "";
    document.getElementById("editApellido").value = employee.apellido || "";
    if (match) {
      let calle = match[1];
      let altura = match[2];
      document.getElementById("editCalle").value = calle || "";
      document.getElementById("editAltura").value = altura || "";
    }
    document.getElementById("editCorreo").value =
      employee.correoElectronico || "";
    document.getElementById("editRol").value = employee.rol || "";

    document
      .getElementById("editEmployeeForm")
      .setAttribute("data-index", index);
  });

//Edit Employee Form treatment
document
  .getElementById("editEmployeeForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    const index = parseInt(this.getAttribute("data-index"));
    const originalEmployee = employees[index];

    const email = document.getElementById("editCorreo").value;

    // Validaciones de correo
    if (!validateEmail(email)) {
      showAlert("Ingrese un correo electrónico válido.", "warning");
      return;
    }

    let updatedFields = {};

    //Get the actual fields values
    const nombre = document.getElementById("editNombre").value;
    const apellido = document.getElementById("editApellido").value;
    const calle = document.getElementById("editCalle").value;
    const altura = document.getElementById("editAltura").value;
    const correoElectronico = email;
    const rol = document.getElementById("editRol").value;

    //Check if the fields are different from the original values
    if (nombre !== originalEmployee.nombre) updatedFields.nombre = nombre;
    if (apellido !== originalEmployee.apellido)
      updatedFields.apellido = apellido;
    if (calle !== originalEmployee.calle) updatedFields.calle = calle;
    if (parseInt(altura) !== originalEmployee.altura)
      updatedFields.altura = parseInt(altura);
    if (correoElectronico !== originalEmployee.correoElectronico)
      updatedFields.correoElectronico = correoElectronico;
    if (rol !== originalEmployee.rol) updatedFields.rol = rol;

    //Send the updated fields to the API
    if (Object.keys(updatedFields).length > 0) {
      updatedFields.legajoEmpleado = originalEmployee.legajoEmpleado;
      await updateEmployee(updatedFields);
    } else {
      showAlert("No se han realizado cambios.", "info");
    }

    //Update the employee in the local array
    employees[index] = { ...originalEmployee, ...updatedFields };

    const modalInstance = bootstrap.Modal.getInstance(
      document.getElementById("editEmployeeModal")
    );
    modalInstance.hide();

    generateTable();
  });

document
  .getElementById("addEmployeeModal")
  .addEventListener("show.bs.modal", async function () {
    const citySelect = document.getElementById("addIdCiudad");
    await populateCitySelect(citySelect);
  });

//Add Employee Form treatment
document
  .getElementById("addEmployeeForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    const email = document.getElementById("addCorreo").value;
    const password = document.getElementById("addPassword").value;

    //Validate email and password
    if (!validateEmail(email)) {
      showAlert("Ingrese un correo electrónico válido.", "warning");
      return;
    }

    if (!validatePassword(password)) {
      showAlert("La contraseña debe tener al menos 8 caracteres.", "warning");
      return;
    }

    //Prepare data to send to the API
    const newEmployee = {
      nombre: document.getElementById("addNombre").value,
      apellido: document.getElementById("addApellido").value,
      calle: document.getElementById("addCalle").value,
      altura: parseInt(document.getElementById("addAltura").value),
      correoElectronico: email,
      rol: document.getElementById("addRol").value,
      passwordEmpleado: password,
      idSucursal: parseInt(document.getElementById("addIdSucursal").value),
      idCiudad: parseInt(document.getElementById("addIdCiudad").value),
    };

    await addEmployee(newEmployee);

    const modalInstance = bootstrap.Modal.getInstance(
      document.getElementById("addEmployeeModal")
    );
    modalInstance.hide();

    generateTable();

    document.getElementById("addEmployeeForm").reset();
  });

//Delete Employee Modal treatment
function prepareDeleteModal(index) {
  const employee = employees[index];

  document.getElementById("deleteLegajo").textContent = employee.legajoEmpleado;
  document.getElementById("deleteNombre").textContent = employee.nombre;
  document.getElementById("deleteApellido").textContent = employee.apellido;
  document.getElementById("deleteCorreo").textContent =
    employee.correoElectronico;

  document.getElementById("confirmDeleteButton").onclick = function () {
    deleteEmployee(employee.legajoEmpleado);
  };
}

//Populate City Select
async function populateCitySelect(selectElement) {
  const cities = await getCities();
  selectElement.innerHTML = "";
  if (cities.length === 0) {
    console.error("No se encontraron ciudades");
    return;
  }

  cities.forEach((city) => {
    const option = document.createElement("option");
    option.value = city.idCiudad;
    option.textContent = city.nombre;
    selectElement.appendChild(option);
  });
}

const generateTable = async () => {
  employees = await fetchEmployeesData();
  const userRole = localStorage.getItem("role");
  let tableContent = ``;
  console.log(employees);

  employees.forEach(
    (
      {
        nombre,
        apellido,
        legajoEmpleado,
        rol,
        correoElectronico,
        ciudad,
        direccionSucursal,
        domicilio,
      },
      index
    ) => {
      tableContent += `
        <tr>
            <td>${legajoEmpleado}</td>
            <td>${direccionSucursal}</td>
            <td>${ciudad}</td>
            <td>${nombre}, ${apellido}</td>
            <td>${domicilio}</td>
            <td>${correoElectronico}</td>
            <td>${rol}</td>
            <td>
                ${
                  userRole === "ADMIN"
                    ? `<div class="d-flex">
                        <button 
                            type="button" 
                            class="btn btn-primary rounded-pill px-3 m-1 editBtn"  
                            data-bs-toggle="modal" 
                            data-bs-target="#editEmployeeModal" 
                            data-index="${index}"
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
                            data-bs-target="#deleteEmployeeModal" 
                            onclick="prepareDeleteModal(${index})"
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
                    </div>`
                    : ""
                }
            </td>
        </tr>
        `;
    }
  );

  const tableBody = document.querySelector("#employees-table tbody");
  tableBody.innerHTML = tableContent;
};

//Get all employees data
async function fetchEmployeesData() {
  const token = localStorage.getItem("jwtToken");

  try {
    const response = await fetch("https://localhost:7289/GetAll", {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });

    if (response.ok) {
      const data = await response.json();
      return data;
    } else {
      showAlert(`Error al obtener los datos: ${response.status}`, "danger");
      return [];
    }
  } catch (err) {
    showAlert(`Error al obtener los datos: ${err}`, "danger");
    return [];
  }
}

//Update Employee Endpoint
async function updateEmployee(updatedFields) {
  const token = localStorage.getItem("jwtToken");

  try {
    const response = await fetch(
      `https://localhost:7289/UpdateEmployee/${updatedFields.legajoEmpleado}`,
      {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(updatedFields),
      }
    );
    if (response.ok) {
      const message = await response.text();
      showAlert(message, "success");
      return message;
    } else {
      const errorText = await response.text();
      showAlert(`Error al modificar los datos: ${errorText}`, "danger");
      return [];
    }
  } catch (err) {
    showAlert("Error de conexión o modificación", "danger");
    return [];
  }
}

//Add Employee Endpoint
async function addEmployee(newEmployee) {
  const token = localStorage.getItem("jwtToken");

  try {
    const response = await fetch("https://localhost:7289/NewEmployee", {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newEmployee),
    });

    if (response.ok) {
      const message = await response.text();
      showAlert(message, "success");
      return message;
    } else {
      const errorText = await response.text();
      showAlert(`Error al agregar el empleado: ${errorText}`, "danger");
      return [];
    }
  } catch (err) {
    showAlert("Error de conexión o agregado", "danger");
    return [];
  }
}

//Delete Employee Endpoint
async function deleteEmployee(legajoEmpleado) {
  const token = localStorage.getItem("jwtToken");

  try {
    const response = await fetch(
      `https://localhost:7289/DeleteEmployee/${legajoEmpleado}`,
      {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );

    if (response.ok) {
      const message = await response.text();
      showAlert(message, "success");

      const deleteModal = bootstrap.Modal.getInstance(
        document.getElementById("deleteEmployeeModal")
      );
      deleteModal.hide();

      await generateTable();
    } else {
      const errorText = await response.text();
      showAlert(`Error al eliminar el empleado: ${errorText}`, "danger");
    }
  } catch (err) {
    showAlert("Error de conexión o eliminación", "danger");
  }
}

//GetCities Endpoint
async function getCities() {
  const token = localStorage.getItem("jwtToken");

  try {
    const response = await fetch("https://localhost:7289/GetCities", {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });

    if (response.ok) {
      const data = await response.json();
      return data;
    } else {
      return [];
    }
  } catch (err) {
    return [];
  }
}

//Email and Password validation
function validateEmail(email) {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}

function validatePassword(password) {
  return password.length >= 8;
}

function showAlert(message, type = "info") {
  const alertContainer = document.getElementById("alertContainer");

  const alertDiv = document.createElement("div");
  alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
  alertDiv.role = "alert";
  alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;

  alertContainer.appendChild(alertDiv);

  setTimeout(() => {
    alertDiv.classList.remove("show");
    alertDiv.classList.add("hide");
    setTimeout(() => alertDiv.remove(), 500);
  }, 5000);
}
