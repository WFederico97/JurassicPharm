document.getElementById('login-form').addEventListener('submit', async function (e) {
    e.preventDefault();

    const CorreoElectronico = document.getElementById('floatingInput').value;
    const PasswordEmpleado = document.getElementById('floatingPassword').value;

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
            alert('Bienvenido');
            localStorage.setItem('jwtToken', data.token); 
            window.location = '../index.html';
        } else {
            alert('Fallo al autenticar');
        }

    } catch (err) {
        console.error('Error:', err);
        alert('Error de conexión o autenticación');
    }
});

async function fetchProtectedData() {
    const token = localStorage.getItem('jwtToken'); 
    
    try {
        const response = await fetch('https://localhost:7289/api/empleados', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const data = await response.json();
            console.log(data);
        } else {
            console.error('Error al obtener los datos:', response.status);
        }
    } catch (err) {
        console.error('Error al obtener los datos:', err);
    }
}