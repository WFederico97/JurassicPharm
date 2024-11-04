import { showAlert } from "../helpers/showAlert.js";
function checkTokenExpiration() {
    const token = localStorage.getItem('jwtToken');
    if (!token) {
        return; 
    }

    const payload = JSON.parse(atob(token.split('.')[1]));
    const expirationTime = payload.exp;
    const currentTime = Math.floor(Date.now() / 1000);

    const timeLeft = expirationTime - currentTime;

    if (timeLeft > 0) {
        if (timeLeft <= 60) {
            showAlert('Su sesión expirará pronto. Será deslogueado automáticamente.', 'warning');
            setTimeout(() => {
                localStorage.clear();
                window.location = '../../Pages/login.html'; 
            }, timeLeft * 1000);
        }
    } else {
        localStorage.clear();
        showAlert('Su sesión ha expirado. Será redirigido al inicio de sesión.', 'danger');
        setTimeout(() => {
            window.location = '../../Pages/login.html'; 
        }, 3000); 
    }
}

document.addEventListener('DOMContentLoaded', checkTokenExpiration);