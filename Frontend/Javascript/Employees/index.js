let employees = [];
const userRole = localStorage.getItem('userRole');

//check user email for navbar
document.addEventListener("DOMContentLoaded", () => {
    const userEmail = localStorage.getItem("userEmail");
  
    if (userEmail) {
      document.getElementById("userEmail").textContent = userEmail;
    }
  });

if (userRole === 'ADMIN') {
    document.getElementById('addEmployeeButtonContainer').style.display = 'block';
}

addEventListener("load", async (event) => {
    await generateTable();

}); //Load table when page is loaded

//Prepoluate Edit Employee Modal
document.getElementById('editEmployeeModal').addEventListener('show.bs.modal', function (event) {

    let button = event.relatedTarget;

    let index = button.getAttribute('data-index');

    let employee = employees[index];

    document.getElementById('editNombre').value = employee.nombre || '';
    document.getElementById('editApellido').value = employee.apellido || '';
    document.getElementById('editCalle').value = employee.calle || '';
    document.getElementById('editAltura').value = employee.altura || '';
    document.getElementById('editCorreo').value = employee.correoElectronico || '';
    document.getElementById('editRol').value = employee.rol || '';

    document.getElementById('editEmployeeForm').setAttribute('data-index', index);
});

//Edit Employee Form treatment
document.getElementById('editEmployeeForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const index = parseInt(this.getAttribute('data-index'));
    const originalEmployee = employees[index];

    const email = document.getElementById('editCorreo').value;

    // Validaciones de correo 
    if (!validateEmail(email)) {
        showAlert("Ingrese un correo electrónico válido.", 'warning');
        return;
    }

    let updatedFields = {};

    //Get the actual fields values
    const nombre = document.getElementById('editNombre').value;
    const apellido = document.getElementById('editApellido').value;
    const calle = document.getElementById('editCalle').value;
    const altura = document.getElementById('editAltura').value;
    const correoElectronico = email;
    const rol = document.getElementById('editRol').value;
    
    //Check if the fields are different from the original values
    if (nombre !== originalEmployee.nombre) updatedFields.nombre = nombre;
    if (apellido !== originalEmployee.apellido) updatedFields.apellido = apellido;
    if (calle !== originalEmployee.calle) updatedFields.calle = calle;
    if (parseInt(altura) !== originalEmployee.altura) updatedFields.altura = parseInt(altura);
    if (correoElectronico !== originalEmployee.correoElectronico) updatedFields.correoElectronico = correoElectronico;
    if (rol !== originalEmployee.rol) updatedFields.rol = rol;


    //Send the updated fields to the API
    if (Object.keys(updatedFields).length > 0) {
        updatedFields.legajoEmpleado = originalEmployee.legajoEmpleado;
        await updateEmployee(updatedFields);
    } else {
        showAlert("No se han realizado cambios.", 'info');
    }

    //Update the employee in the local array
    employees[index] = { ...originalEmployee, ...updatedFields };

    const modalInstance = bootstrap.Modal.getInstance(document.getElementById('editEmployeeModal'));
    modalInstance.hide();

    generateTable();
});

document.getElementById('addEmployeeModal').addEventListener('show.bs.modal', async function () {
    const citySelect = document.getElementById('addIdCiudad');
    await populateCitySelect(citySelect);
});

//Add Employee Form treatment
document.getElementById('addEmployeeForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const email = document.getElementById('addCorreo').value;
    const password = document.getElementById('addPassword').value;

    //Validate email and password
    if (!validateEmail(email)) {
        showAlert("Ingrese un correo electrónico válido.", 'warning');
        return;
    }

    if (!validatePassword(password)) {
        showAlert("La contraseña debe tener al menos 8 caracteres.", 'warning');
        return;
    }

    //Prepare data to send to the API
    const newEmployee = {
        nombre: document.getElementById('addNombre').value,
        apellido: document.getElementById('addApellido').value,
        calle: document.getElementById('addCalle').value,
        altura: parseInt(document.getElementById('addAltura').value),
        correoElectronico: email,
        rol: document.getElementById('addRol').value,
        passwordEmpleado: password,
        idSucursal: parseInt(document.getElementById('addIdSucursal').value),
        idCiudad: parseInt(document.getElementById('addIdCiudad').value)
    };
    
    await addEmployee(newEmployee);

    const modalInstance = bootstrap.Modal.getInstance(document.getElementById('addEmployeeModal'));
    modalInstance.hide();

    generateTable();

    document.getElementById('addEmployeeForm').reset();
});

//Delete Employee Modal treatment
function prepareDeleteModal(index) {
    const employee = employees[index];

    document.getElementById('deleteLegajo').textContent = employee.legajoEmpleado;
    document.getElementById('deleteNombre').textContent = employee.nombre;
    document.getElementById('deleteApellido').textContent = employee.apellido;
    document.getElementById('deleteCorreo').textContent = employee.correoElectronico;

    document.getElementById('confirmDeleteButton').onclick = function() {
        deleteEmployee(employee.legajoEmpleado);
    };
}

//Populate City Select
async function populateCitySelect(selectElement) {
    const cities = await getCities();
    selectElement.innerHTML = '';  
    if (cities.length === 0) {
        console.error("No se encontraron ciudades");
        return;
    }

    cities.forEach(city => {
        const option = document.createElement('option');
        option.value = city.idCiudad;
        option.textContent = city.nombre;
        selectElement.appendChild(option);
    });
}

const generateTable = async () => {
    employees = await fetchEmployeesData();
    const userRole = localStorage.getItem('userRole');
    let tableContent = ``;

    employees.forEach(({ nombre, apellido, calle, altura, idSucursal, legajoEmpleado, rol, correoElectronico }, index) => {
        tableContent += `
        <tr>
            <td>${legajoEmpleado}</td>
            <td>${idSucursal}</td>
            <td>${nombre}, ${apellido}</td>
            <td>${calle} ${altura}</td>
            <td>${correoElectronico}</td>
            <td>${rol}</td>
            <td>
            ${
                userRole === 'ADMIN'
                ? 
                
                `<div class="d-flex">
                    <button 
                        type="button" 
                        class="btn btn-primary rounded-pill px-3 m-1"  
                        data-bs-toggle="modal" 
                        data-bs-target="#editEmployeeModal" 
                        data-index="${index}">
                        Editar
                    </button>
                    <button 
                        type="button" 
                        class="btn btn-danger rounded-pill px-3 m-1"  
                        data-bs-toggle="modal" 
                        data-bs-target="#deleteEmployeeModal" 
                        onclick="prepareDeleteModal(${index})">
                        Eliminar
                    </button>
                </div>
                `
                : '' 
            }
            </td>
        </tr>
        `;
    });

    const tableBody = document.querySelector("#employees-table tbody");
    tableBody.innerHTML = tableContent; 
};

//Get all employees data
async function fetchEmployeesData() {
    const token = localStorage.getItem('jwtToken'); 
    
    try {
        const response = await fetch('https://localhost:7289/GetAll', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const data = await response.json();
            return data;
        } else {
            showAlert(`Error al obtener los datos: ${response.status}`, 'danger');
            return [];
        }
    } catch (err) {
        showAlert(`Error al obtener los datos: ${err}`, 'danger');
        return [];
    }
}

//Update Employee Endpoint
async function updateEmployee(updatedFields) {
    const token = localStorage.getItem('jwtToken'); 
    
    try {
        const response = await fetch(`https://localhost:7289/UpdateEmployee/${updatedFields.legajoEmpleado}`, {
            method: 'PUT',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedFields)
        }); 
        if (response.ok) {
            const message = await response.text();
            showAlert(message, 'success');
            return message;
        } else {
            const errorText = await response.text();
            showAlert(`Error al modificar los datos: ${errorText}`, 'danger');
            return [];
        }
    }
    catch (err) {
        showAlert('Error de conexión o modificación', 'danger');
        return [];
    }
}

//Add Employee Endpoint
async function addEmployee(newEmployee) {
    const token = localStorage.getItem('jwtToken'); 
    
    try {
        const response = await fetch('https://localhost:7289/NewEmployee', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newEmployee)
        });

        if (response.ok) {
            const message = await response.text();
            showAlert(message, 'success');
            return message;
        } else {
            const errorText = await response.text();
            showAlert(`Error al agregar el empleado: ${errorText}`, 'danger');
            return [];
        }
    } catch (err) {
        showAlert('Error de conexión o agregado', 'danger');
        return [];
    }
}

//Delete Employee Endpoint
async function deleteEmployee(legajoEmpleado) {
    const token = localStorage.getItem('jwtToken'); 
    
    try {
        const response = await fetch(`https://localhost:7289/DeleteEmployee/${legajoEmpleado}`, {
            method: 'PUT',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const message = await response.text();
            showAlert(message, 'success');

            const deleteModal = bootstrap.Modal.getInstance(document.getElementById('deleteEmployeeModal'));
            deleteModal.hide();

            await generateTable();
        } else {
            const errorText = await response.text();
            showAlert(`Error al eliminar el empleado: ${errorText}`, 'danger');
        }
    } catch (err) {
        showAlert('Error de conexión o eliminación', 'danger');
    }
}

//GetCities Endpoint
async function getCities() {
    const token = localStorage.getItem('jwtToken'); 
    
    try {
        const response = await fetch('https://localhost:7289/GetCities', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
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

function showAlert(message, type = 'info') {
    const alertContainer = document.getElementById('alertContainer');
    
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
    alertDiv.role = 'alert';
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;

    alertContainer.appendChild(alertDiv);

    setTimeout(() => {
        alertDiv.classList.remove('show');
        alertDiv.classList.add('hide');
        setTimeout(() => alertDiv.remove(), 500); 
    }, 5000);
}