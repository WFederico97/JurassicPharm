export function showAlert(message, type = "info") {
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
