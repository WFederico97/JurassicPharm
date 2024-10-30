document.getElementById('login-form').addEventListener('submit', async function (e) {
    e.preventDefault();

    const CorreoElectronico = document.getElementById('floatingInput').value;
    const PasswordEmpleado = document.getElementById('floatingPassword').value;
    let tokenStored = localStorage.getItem('jwtToken');
    let storedUserEmail = localStorage.getItem('userEmail');

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
                        localStorage.setItem('userRole', loggedInEmployee.rol);
                        alert(`Bienvenido, su rol es: ${loggedInEmployee.rol}`);
                    } else {
                        alert('Empleado no encontrado.');
                    }

                    window.location = '../index.html';
                } else {
                    console.error('Error al obtener los empleados:', employeesResponse.status);
                    alert('Error al obtener la información del empleado.');
                }
            } else {
                alert('Fallo al autenticar');
            }

        } catch (err) {
            console.error('Error:', err);
            alert('Error de conexión o autenticación');
        }
    } else {
        alert(`Usted ya se encuentra logueado bajo el usuario: ${storedUserEmail}`);
        window.location = '../index.html';
    }
});