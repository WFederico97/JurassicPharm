export function showAlert(message, type = "info") {
  const alertContainer = document.getElementById("alertContainer");

  const alertDiv = document.createElement("div");
  alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
  alertDiv.role = "alert";
  let iconSrc;
  switch (type) {
    case "primary":
      iconSrc = "../../Assets/Images/bob_ok.webp";
      break;
    case "warning":
      iconSrc = "../../Assets/Images/bob_ups.webp";
      break;
    case "error":
      iconSrc = "../../Assets/Images/bob_400.webp";
      break;
    case "danger":
      iconSrc = "../../Assets/Images/bob_500.webp";
      break;
    case "success":
      iconSrc = "../../Assets/Images/bob_success.webp";
      break;
    default:
      iconSrc = "../../Assets/Images/bob_ok.webp";
      break;
  }

  alertDiv.innerHTML = `
          <img src="${iconSrc}" alt="${type}" style="width: 5em; height: 5em; margin-right: 10px;">
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
