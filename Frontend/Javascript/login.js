import { forgotPassword } from "./modules/Auth/api.js";
import { showAlert } from "./helpers/showAlert.js";

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

document
  .getElementById("login-form")
  .addEventListener("submit", async function (e) {
    e.preventDefault();

    showBtnLoading();
    const CorreoElectronico = document.getElementById("floatingInput").value;
    const PasswordEmpleado = document.getElementById("floatingPassword").value;
    let tokenStored = localStorage.getItem("jwtToken");
    let storedUserEmail = localStorage.getItem("userEmail");

    if (!CorreoElectronico || !PasswordEmpleado) {
      showAlert("Por favor, complete ambos campos.", "warning");
      showBtnLoading();
      return;
    }

    if (!emailRegex.test(CorreoElectronico)) {
      showAlert("Por favor, ingrese un correo electrónico válido.", "warning");
      showBtnLoading();
      return;
    }

    if (PasswordEmpleado.length < 6) {
      showAlert("La contraseña debe tener al menos 6 caracteres.", "warning");
      showBtnLoading();
      return;
    }

    if (tokenStored == null) {
      try {
        const res = await fetch("http://localhost:3000/api/auth/login", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ CorreoElectronico, PasswordEmpleado }),
        });

        if (res.ok) {
          const data = await res.json();
          localStorage.setItem("jwtToken", data.token);
          localStorage.setItem("userEmail", CorreoElectronico);

          // Decode payload from token
          const payload = JSON.parse(atob(data.token.split(".")[1]));

          // Store user data in local storage
          localStorage.setItem("userId", payload.UserId);
          localStorage.setItem("fullName", payload.FullName);
          localStorage.setItem(
            "role",
            payload[
              "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            ]
          );
          localStorage.setItem("tokenExpiration", payload.exp);

          const employeesResponse = await fetch(
            "http://localhost:3000/api/GetAll",
            {
              method: "GET",
              headers: {
                Authorization: `Bearer ${data.token}`,
                "Content-Type": "application/json",
              },
            }
          );

          if (employeesResponse.ok) {
            const employees = await employeesResponse.json();

            const loggedInEmployee = employees.find(
              (emp) => emp.correoElectronico === CorreoElectronico
            );

            if (loggedInEmployee) {
              showAlert(
                `Bienvenido, ${payload.FullName}. Su rol es: ${payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]}`,
                "success"
              );
            } else {
              showAlert("Empleado no encontrado.", "danger");
            }

            window.location = "../index.html";
          } else {
            console.error(
              "Error al obtener los empleados:",
              employeesResponse.status
            );
            showAlert(
              "Error al obtener la información del empleado.",
              "danger"
            );
          }
        } else if (res.status === 401) {
          showAlert(
            "Credenciales incorrectas. Por favor, intente de nuevo.",
            "danger"
          );
        } else if (res.status >= 500) {
          showAlert(
            "Error en el servidor. Por favor, intente más tarde.",
            "danger"
          );
        } else {
          showAlert("Fallo al autenticar.", "danger");
        }
        showBtnLoading();
      } catch (err) {
        console.error("Error:", err);
        showAlert("Error de conexión o autenticación", "danger");
        showBtnLoading();
      }
    } else {
      showAlert(
        `Usted ya se encuentra logueado bajo el usuario: ${storedUserEmail}`,
        "info"
      );
      window.location = "../index.html";
    }
  });

const showBtnLoading = (selector = ".btn-login") => {
  const btnLogin = document.querySelector(selector);
  const btnText = btnLogin.querySelector("p");
  const spinner = btnLogin.querySelector("span");

  btnText.classList.toggle("d-none");
  spinner.classList.toggle("d-none");
};

//Handle forgot password
document
  .getElementById("forgot-password-form")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const email = document.getElementById("forgot-password-input").value;

    if (!emailRegex.test(email)) {
      showAlert("Por favor, ingrese un correo electrónico válido.", "warning");
      return;
    }

    try {
      showBtnLoading(".send-email-btn");

      const res = await forgotPassword(email);

      if (res.status === 500) {
        showBtnLoading(".send-email-btn");
        document.querySelector(".btn-close").click();

        showAlert(
          "Error inesperado. Comuniquese con servicio al cliente",
          "danger"
        );
      } else {
        showBtnLoading(".send-email-btn");
        document.querySelector(".btn-close").click();
        showAlert(
          "Si su correo está registrado, recibirás un mensaje con instrucciones para recuperar tu contraseña. Por favor, revisa tu bandeja de entrada o carpeta de spam.",
          "success"
        );
      }
    } catch (error) {
      console.error(error);
      showBtnLoading(".send-email-btn");
      document.querySelector(".btn-close").click();
    }
  });
