document.getElementById('login-form').addEventListener('submit', async function (e) {
    e.preventDefault();

    const CorreoElectronico = document.getElementById('floatingInput').value;
    const PasswordEmpleado = document.getElementById('floatingPassword').value;
    let tokenStored = localStorage.getItem('jwtToken');
    let storedUserEmail = localStorage.getItem('userEmail');

    if (!CorreoElectronico || !PasswordEmpleado) {
        showAlert('Por favor, complete ambos campos.', 'warning');
        return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(CorreoElectronico)) {
        showAlert('Por favor, ingrese un correo electrónico válido.', 'warning');
        return;
    }

    if (PasswordEmpleado.length < 6) {
        showAlert('La contraseña debe tener al menos 6 caracteres.', 'warning');
        return;
    }

    if (tokenStored == null) {
        try {
            const res = await fetch('https://localhost:7289/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ CorreoElectronico, PasswordEmpleado })
            });

            if (res.ok) {
                const data = await res.json();
                localStorage.setItem('jwtToken', data.token);
                localStorage.setItem('userEmail', CorreoElectronico);

                // Decode payload from token
                const payload = JSON.parse(atob(data.token.split('.')[1]));

                // Store user data in local storage
                localStorage.setItem('userId', payload.UserId);
                localStorage.setItem('fullName', payload.FullName);
                localStorage.setItem('role', payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
                localStorage.setItem('tokenExpiration', payload.exp);

                const employeesResponse = await fetch('https://localhost:7289/GetAll', {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${data.token}`,
                        'Content-Type': 'application/json'
                    }
                });

                if (employeesResponse.ok) {
                    const employees = await employeesResponse.json();

                    const loggedInEmployee = employees.find(emp => emp.correoElectronico === CorreoElectronico);

                    if (loggedInEmployee) {
                        showAlert(`Bienvenido, ${payload.FullName}. Su rol es: ${payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]}`, 'success');
                    } else {
                        showAlert('Empleado no encontrado.', 'danger');
                    }

                    window.location = '../index.html';
                } else {
                    console.error('Error al obtener los empleados:', employeesResponse.status);
                    showAlert('Error al obtener la información del empleado.', 'danger');
                }
            } else if (res.status === 401) {
                showAlert('Credenciales incorrectas. Por favor, intente de nuevo.', 'danger');
            } else if (res.status >= 500) {
                showAlert('Error en el servidor. Por favor, intente más tarde.', 'danger');
            } else {
                showAlert('Fallo al autenticar.', 'danger');
            }

        } catch (err) {
            console.error('Error:', err);
            showAlert('Error de conexión o autenticación', 'danger');
        }
    } else {
        showAlert(`Usted ya se encuentra logueado bajo el usuario: ${storedUserEmail}`, 'info');
        window.location = '../index.html';
    }
});

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