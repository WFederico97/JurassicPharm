import { showAlert } from "../helpers/showAlert.js";
import { getBranches } from "../modules/Branches/api.js";
import { fetchClients } from "../modules/Clients/api.js";
import { createInvoice, fetchBilingReport } from "../modules/Invoices/api.js";
import { fetchSupplies } from "../modules/Supplies/api.js";

const details = [];
let selectedSupply = {};

async function fetchEmployeesByStore() {
  const token = localStorage.getItem("jwtToken");
  try {
    const response = await fetch("http://localhost:3000/api/GetStores", {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    if (!response.ok) throw new Error("Error al obtener las sucursales");

    const stores = await response.json();
    const tableBody = document.querySelector("#employeeTable tbody");
    tableBody.innerHTML = "";

    stores.forEach((store) => {
      const activeEmployees = store.empleados.filter((emp) => emp.active);
      const activeAdmins = store.empleados.filter(
        (emp) => emp.active && emp.role === "ADMIN"
      );
      const activeRepositores = store.empleados.filter(
        (emp) => emp.active && emp.role === "REPOSITOR"
      );
      const activeCashiers = store.empleados.filter(
        (emp) => emp.active && emp.role === "CAJERO"
      );

      if (activeEmployees.length > 0) {
        const row = document.createElement("tr");
        row.innerHTML = `
                    <td>${store.provincia}</td>
                    <td>${store.localidad}</td>
                    <td>${store.ciudad}</td>
                    <td>${store.calle}, ${store.altura}</td>
                    <td>${activeEmployees
                      .map((emp) => `${emp.nombre} ${emp.apellido}`)
                      .join(", ")}</td>
                    <td>${activeEmployees.length}</td>
                    <td>${activeAdmins.length}</td>
                    <td>${activeCashiers.length}</td>
                    <td>${activeRepositores.length}</td>
                `;
        tableBody.appendChild(row);
      }
    });
    createStoreChart(stores);
  } catch (error) {
    console.error("Error fetching stores:", error);
  }
}

const createSalesChart = async (sales) => {
  if (sales.length === 0) return;

  const years = sales.map((sale) => sale.year);

  const yearsWithotDuplicates = new Set(years);
  const yearsArray = Array.from(yearsWithotDuplicates);

  const totalsByYear = yearsArray.map((year) =>
    sales.reduce((acc, item) => {
      return item.year === year ? acc + item.total : acc;
    }, 0)
  );

  const salesChart = document.getElementById("salesChart").getContext("2d");

  new Chart(salesChart, {
    type: "bar",
    data: {
      labels: yearsArray,
      datasets: [
        {
          label: "Facturacion por año ($)",
          data: totalsByYear,
          backgroundColor: "#d17d0f",
          borderColor: "#d17d0f",
          borderWidth: 1,
        },
      ],
    },
    options: {
      responsive: true,
      scales: {
        y: {
          beginAtZero: true,
        },
      },
      plugins: {
        tooltip: {
          callbacks: {
            label: function (context) {
              return `Facturacion: $${context.raw.toFixed(2)}`;
            },
          },
        },
      },
    },
  });
};

const createSalesBySuppliesChart = async (sales) => {
  if (sales.length === 0) return;
  const currentYear = new Date().getFullYear();

  const currentYearSales = sales;

  const salesBySupplyChart = document
    .getElementById("salesBySuppliesChart")
    .getContext("2d");

  new Chart(salesBySupplyChart, {
    type: "bar",
    data: {
      labels: currentYearSales.map((sale) => sale.supply),
      datasets: [
        {
          label: `Facturacion ${currentYear} por suministro ($)`,
          data: currentYearSales.map((sale) => sale.total),
          fill: false,
          borderColor: "rgb(75, 192, 192)",
          backgroundColor: [
            "rgb(255, 99, 132)",
            "rgb(255, 159, 64)",
            "rgb(255, 205, 86)",
            "rgb(75, 192, 192)",
            "rgb(54, 162, 235)",
            "rgb(153, 102, 255)",
            "rgb(201, 203, 207)",
          ],
          tension: 0.1,
        },
      ],
    },
    options: {
      responsive: true,
      scales: {
        y: {
          beginAtZero: true,
        },
      },
      plugins: {
        tooltip: {
          callbacks: {
            label: function (context) {
              return `Facturacion: $${context.raw.toFixed(2)}`;
            },
          },
        },
      },
    },
  });
};

function createStoreChart(stores) {
  const ctx = document.getElementById("storeChart").getContext("2d");
  const labels = stores.map(
    (store) => `${store.provincia} - ${store.localidad}`
  );
  const data = stores.map(
    (store) => store.empleados.filter((emp) => emp.active).length
  );

  new Chart(ctx, {
    type: "bar",
    data: {
      labels: labels,
      datasets: [
        {
          label: "Cantidad de empleados",
          data: data,
          backgroundColor: "#007bff",
          borderColor: "#007bff",
          borderWidth: 1,
        },
      ],
    },
    options: {
      responsive: true,
      scales: {
        y: {
          beginAtZero: true,
        },
      },
    },
  });
}

const populateDataToCreateInvoice = async () => {
  const clients = await fetchClients();
  const branches = await getBranches();
  const supplies = await fetchSupplies();

  const clientSelectInput = document.getElementById(
    "create-invoice-client-select"
  );
  const branchSelectInput = document.getElementById(
    "create-invoice-branch-select"
  );
  const supplySelectInput = document.getElementById(
    "create-invoice-supply-select"
  );

  // Agregar opción vacía al inicio de cada select
  clientSelectInput.innerHTML = `<option value="" disabled selected></option>`;
  branchSelectInput.innerHTML = `<option value="" disabled selected></option>`;
  supplySelectInput.innerHTML = `<option value="" disabled selected></option>`;

  clients?.forEach(({ idClient, name, lastname }) => {
    clientSelectInput.innerHTML += `<option value="${idClient}">${lastname}, ${name}</option>`;
  });

  branches?.forEach(({ id, address, streetLevel, city }) => {
    branchSelectInput.innerHTML += `<option value="${id}">${city}, ${address} ${streetLevel}</option>`;
  });

  supplies?.forEach(({ idSupply, name, price, stock }) => {
    supplySelectInput.innerHTML += `<option value="${idSupply}" data-price="${price}" data-stock="${stock}">${name}</option>`;
  });

  // Evento para actualizar el precio en el campo de precio del suministro seleccionado
  supplySelectInput.addEventListener("change", (e) => {
    selectedSupply = e.target.selectedOptions[0];

    const amountInput = document.getElementById(
      "create-invoice-supply-amount-input"
    );

    const stock = selectedSupply.getAttribute("data-stock");

    amountInput.setAttribute("max", stock);

    const price = selectedSupply.getAttribute("data-price");

    const priceInput = document.getElementById(
      "create-invoice-supply-price-input"
    );
    priceInput.value = price;
  });
};

document.addEventListener("DOMContentLoaded", async () => {
  const role = localStorage.getItem("role");
  const sales = await fetchBilingReport();

  if (role == "CAJERO") {
    document.getElementById("employeesByStoreContainer").style.display = "none";
    document.getElementById("storesChartContainer").style.display = "none";
    document.getElementById("annualSalesChartContainer").style.display = "none";
    document.getElementById("salesBySuppliesChartContainer").style.display =
      "none";
  }

  fetchEmployeesByStore();
  populateDataToCreateInvoice();
  createSalesChart(sales);
  createSalesBySuppliesChart(sales);
});

document
  .getElementById("create-invoice-form")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const supplySelect = document.getElementById(
      "create-invoice-supply-select"
    );
    const priceInput = document.getElementById(
      "create-invoice-supply-price-input"
    );
    const amountInput = document.getElementById(
      "create-invoice-supply-amount-input"
    );

    const tbody = document.getElementById("invoice-details-tbody-table");

    // Agregar el nuevo detalle al arreglo
    details.push({
      supplyId: supplySelect.value,
      name: selectedSupply.textContent,
      salePrice: priceInput.value,
      amount: amountInput.value,
    });

    // Limpiar los campos del formulario
    supplySelect.value = "";
    priceInput.value = "";
    amountInput.value = "";

    // Actualizar la tabla
    renderDetailsTable();
  });

/**
 * Función para renderizar la tabla de detalles
 */
function renderDetailsTable() {
  const tbody = document.getElementById("invoice-details-tbody-table");
  tbody.innerHTML = ""; // Limpiar la tabla

  details.forEach(({ supplyId, salePrice, name, amount }, index) => {
    tbody.innerHTML += ` <tr>
                  <td>${supplyId}</td>
                  <td>${name}</td>
                  <td>${salePrice}</td>
                  <td>${amount}</td>
                  <td>
                      <button 
                        type="button" 
                        class="btn btn-danger rounded-pill px-3 m-1 bin-button"  
                        data-index=${index}
                      >
                        <svg
                          class="bin-top"
                          viewBox="0 0 39 7"
                          fill="none"
                          xmlns="http://www.w3.org/2000/svg"
                        >
                          <line y1="5" x2="39" y2="5" stroke="white" stroke-width="4"></line>
                          <line
                          x1="12"
                          y1="1.5"
                          x2="26.0357"
                          y2="1.5"
                          stroke="white"
                          stroke-width="3"
                          ></line>
                        </svg>
                        <svg
                            class="bin-bottom"
                            viewBox="0 0 33 39"
                            fill="none"
                            xmlns="http://www.w3.org/2000/svg"
                        >
                            <mask id="path-1-inside-1_8_19" fill="white">
                            <path
                                d="M0 0H33V35C33 37.2091 31.2091 39 29 39H4C1.79086 39 0 37.2091 0 35V0Z"
                            ></path>
                            </mask>
                            <path
                            d="M0 0H33H0ZM37 35C37 39.4183 33.4183 43 29 43H4C-0.418278 43 -4 39.4183 -4 35H4H29H37ZM4 43C-0.418278 43 -4 39.4183 -4 35V0H4V35V43ZM37 0V35C37 39.4183 33.4183 43 29 43V35V0H37Z"
                            fill="white"
                            mask="url(#path-1-inside-1_8_19)"
                            ></path>
                            <path d="M12 6L12 29" stroke="white" stroke-width="4"></path>
                            <path d="M21 6V29" stroke="white" stroke-width="4"></path>
                        </svg>
                      </button>              
                  </td>
              </tr>`;
  });

  // Agregar evento a los botones de eliminación
  const deleteInvoiceBtn = document.querySelectorAll(".btn-danger");
  deleteInvoiceBtn.forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const index = e.target.closest("button").getAttribute("data-index");
      details.splice(index, 1); // Eliminar del arreglo
      renderDetailsTable(); // Volver a renderizar la tabla
    });
  });
}

const confirmButton = document.getElementById("confirm");
confirmButton.addEventListener("click", async () => {
  const clientId = document.getElementById(
    "create-invoice-client-select"
  ).value;
  const branchId = document.getElementById(
    "create-invoice-branch-select"
  ).value;
  const supplyId = document.getElementById(
    "create-invoice-supply-select"
  ).value;
  const price = document.getElementById(
    "create-invoice-supply-price-input"
  ).value;
  const amount = document.getElementById(
    "create-invoice-supply-amount-input"
  ).value;
  const date = document.getElementById("create-invoice-date-input").value;

  if (
    (clientId === "" ||
      branchId === "" ||
      supplyId === "" ||
      price === "" ||
      amount === "" ||
      date === "") &&
    details.length === 0
  ) {
    showAlert(
      "Debe agregar al menos un detalle para realizar la transacción",
      "warning"
    );
    return;
  }

  const payload = {
    clientId,
    branchId,
    date,
    details,
  };

  try {
    await createInvoice(payload);

    showAlert("Factura creada exitosamente!", "success");

    setTimeout(() => {
      window.location.reload();
    }, 1000);
  } catch (error) {
    showAlert("Error inesperado al crear una factura", "danger");
  }
});
