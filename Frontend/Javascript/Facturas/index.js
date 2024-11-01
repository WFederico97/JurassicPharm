import { showAlert } from "../helpers/showAlert.js";

addEventListener("load", async (event) => {
  await generateTable();
});

const generateTable = async () => {
  const invoices = await getInvoices();
  const table = document.getElementById("facturas-table");

  let tableContent = `<tbody>`;

  invoices.forEach(
    ({ clientName, clienLastName, date, branch, details }, index) => {
      tableContent += `
          <tr>
            <td>${clienLastName}, ${clientName}</td>
            <td>${new Date(date).toLocaleDateString()}</td>
            <td>${branch}</td>
            <td>
              <button 
                type="button" 
                class="btn btn-outline-primary btn-sm"  
                data-bs-toggle="modal" 
                data-bs-target="#exampleModal" 
                data-index="${index}">
                  Detalle
              </button>
            </td>
            <td>$${getTotal(details)}</td>
          </tr>
        `;
    }
  );

  tableContent += `</tbody>`;
  table.innerHTML += tableContent;

  document.querySelectorAll('[data-bs-toggle="modal"]').forEach((button) => {
    button.addEventListener("click", function () {
      const index = this.getAttribute("data-index");
      const selectedDetails = invoices[index].details;

      generateDetails(selectedDetails);
    });
  });
};

const getTotal = (details) => {
  let total = 0;

  details.forEach(({ unitPrice }) => {
    total += unitPrice;
  });

  return total;
};

const getInvoices = async () => {
  const token = localStorage.getItem("jwtToken");

  try {
    const response = await fetch("https://localhost:7289/api/invoice", {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });

    if (response.ok) {
      const data = await response.json();
      return data;
    } else {
      showAlert(`Error al obtener los datos: ${response.status}`, "danger");
      return [];
    }
  } catch (err) {
    showAlert(`Error al obtener los datos: ${err}`, "danger");
    return [];
  }
};

const generateDetails = (details) => {
  const modalTable = document.getElementById("facturas-modal-table");

  let modalTableBody = modalTable.querySelector("tbody");
  if (modalTableBody) {
    modalTableBody.remove();
  }

  modalTableBody = document.createElement("tbody");

  details.forEach(({ supplyName, unitPrice, amount }) => {
    const row = document.createElement("tr");
    row.innerHTML = `
          <td>${supplyName}</td>
          <td>$${unitPrice}</td>
          <td>${amount}</td>
        `;
    modalTableBody.appendChild(row);
  });

  modalTable.appendChild(modalTableBody);
};
