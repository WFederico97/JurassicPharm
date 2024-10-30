addEventListener("load", async (event) => {
    await generateTable();
});

document.getElementById('editEmployeeForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const index = document.querySelector('[data-bs-toggle="modal"][data-index]').getAttribute("data-index");
    employees[index].nombre = document.getElementById('editNombre').value;
    employees[index].apellido = document.getElementById('editApellido').value;
    employees[index].calle = document.getElementById('editCalle').value;
    employees[index].altura = document.getElementById('editAltura').value;
    employees[index].correoElectronico = document.getElementById('editCorreo').value;
    employees[index].rol = document.getElementById('editRol').value;

    const modalInstance = bootstrap.Modal.getInstance(document.getElementById('editEmployeeModal'));
    modalInstance.hide();


    document.getElementById("employees-table").innerHTML = '';
    generateTable(); 
});



const generateTable = async () => {
    const employees = await fetchEmployeesData();
    const userRole = localStorage.getItem('userRole');
    const table = document.getElementById("employees-table");
    let tableContent = `<tbody>`;
    
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
                    `<button 
                        type="button" 
                        class="btn btn-primary rounded-pill px-3"  
                        data-bs-toggle="modal" 
                        data-bs-target="#editEmployeeModal" 
                        data-index="${index}">
                        Editar
                    </button>`
                    : 
                    `<button 
                        type="button" 
                        class="btn btn-primary rounded-pill px-3"  
                        disabled>
                        Editar
                    </button>`
                }
                </td>
                </tr>
                `;
            });
            
            tableContent += `</tbody>`;
            table.innerHTML += tableContent;
            
            if (userRole === 'ADMIN') {
                document.querySelectorAll('[data-bs-toggle="modal"]').forEach((button) => {
                    button.addEventListener("click", function () {
                        const index = this.getAttribute("data-index");
                        const employee = employees[index];
                        
                        document.getElementById("editNombre").value = employee.nombre;
                        document.getElementById("editApellido").value = employee.apellido;
                        document.getElementById("editCalle").value = employee.calle;
                        document.getElementById("editAltura").value = employee.altura;
                        document.getElementById("editCorreo").value = employee.correoElectronico;
                        document.getElementById("editRol").value = employee.rol;
                    });
                });
            }
};

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
            console.error('Error al obtener los datos:', response.status);
            return [];
        }
    } catch (err) {
        console.error('Error al obtener los datos:', err);
        return [];
    }
}