import { showAlert } from "../helpers/showAlert.js";
import { resetPassword } from "../modules/Auth/api.js";
const urlParams = new URLSearchParams(window.location.search);
const tokenQueryParam = urlParams.get("token");

function checkTokenExpiration() {
  if (window.location.pathname === "/Pages/reset-password.html") {
    //Disabled reset password button if token does not exist

    if (tokenQueryParam === null || tokenQueryParam === "") {
      const resetBtn = document.querySelector(".reset-form button");
      resetBtn.setAttribute("disabled", "");
      resetBtn.classList.add("opacity-50");
      resetBtn.classList.add("pe-none");
    }
  }

  const token = localStorage.getItem("jwtToken");
  if (!token) {
    return;
  }

  const payload = JSON.parse(atob(token.split(".")[1]));
  const expirationTime = payload.exp;
  const currentTime = Math.floor(Date.now() / 1000);

  const timeLeft = expirationTime - currentTime;

  if (timeLeft > 0) {
    if (timeLeft <= 60) {
      showAlert(
        "Su sesión expirará pronto. Será deslogueado automáticamente.",
        "warning"
      );
      setTimeout(() => {
        localStorage.clear();
        window.location = "../../Pages/login.html";
      }, timeLeft * 1000);
    }
  } else {
    localStorage.clear();
    showAlert(
      "Su sesión ha expirado. Será redirigido al inicio de sesión.",
      "danger"
    );
    setTimeout(() => {
      window.location = "../../Pages/login.html";
    }, 3000);
  }
}

document?.addEventListener("DOMContentLoaded", checkTokenExpiration);

//RESET PASSWORD
const resetForm = document.querySelector(".reset-form");
const dangerAlert = document.querySelector(".alert-danger");
const newPassword = document.getElementById("password");
const confirmedPassword = document.getElementById("confirm-password");

resetForm?.addEventListener("submit", async (e) => {
  e.preventDefault();

  if (newPassword.value !== confirmedPassword.value) {
    dangerAlert.classList.remove("d-none");
    return;
  }
  dangerAlert.classList.add("d-none");

  showBtnLoading();
  try {
    const response = await resetPassword(tokenQueryParam, newPassword.value);

    if (response.status === 500) {
      dangerAlert.classList.remove("d-none");
      dangerAlert.textContent =
        " Error inesperado. Pongase en contacto con servicio al cliente";
      showBtnLoading();
      return;
    }
    showAlert(
      "Contraseña cambiada exitosamente. Esta siendo redirigido hacia el login",
      "success"
    );
    setTimeout(() => {
      window.location = "../Pages/login.html";
    }, 5000);
  } catch (error) {
    dangerAlert.classList.remove("d-none");
    dangerAlert.textContent =
      " Error inesperado. Pongase en contacto con servicio al cliente";
    showBtnLoading();
  }
});

confirmedPassword?.addEventListener("change", (e) => {
  if (newPassword.value === e.target.value) {
    dangerAlert.classList.add("d-none");
  }
});

const showBtnLoading = () => {
  const resetPassBtn = document.getElementById("reset-password-btn");

  const btnText = resetPassBtn.querySelector("span");
  const spinner = resetPassBtn.querySelector("div");

  btnText.classList.toggle("d-none");
  spinner.classList.toggle("d-none");
};
