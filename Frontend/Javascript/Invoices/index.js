import { showAlert } from "../helpers/showAlert.js";
import { generateTable } from "../modules/Invoices/generateTable.js";
import { getInvoices, updateInvoice } from "../modules/Invoices/api.js";
import { generateDetails } from "../modules/Invoices/generateDetails.js";
import { setSelectedOption } from "../helpers/setSelectedOption.js";

let selectedInvoiceNumber = 0;

addEventListener("DOMContentLoaded", async (event) => {
  const invoices = await getInvoicesAndGenerateTable();

  document.querySelectorAll('[data-bs-toggle="modal"]').forEach((button) => {
    button.addEventListener("click", (event) =>
      handleTableActions(event, invoices)
    );
  });
});

const getInvoicesAndGenerateTable = async () => {
  let invoices = [];
  try {
    invoices = await getInvoices();

    if (!invoices.length > 0) return;

    await generateTable(invoices);
  } catch (error) {
    showAlert(`Error al obtener los datos: ${error.message}`, "danger");
  } finally {
    return invoices;
  }
};

const handleTableActions = (event, invoices) => {
  const index = event.target.getAttribute("data-index");

  const selectedInvoice = invoices[index];
  selectedInvoiceNumber = selectedInvoice.invoiceNumber;

  switch (event.target.name) {
    case "btn-detail":
      generateDetails(selectedInvoice?.details);
      break;
    case "btn-edit":
      prepareInvoiceEdit(selectedInvoice, invoices);
      break;

    default:
      console.log(event.target.name);
      break;
  }
};

const prepareInvoiceEdit = (selectedInvoice, invoices) => {
  //Populate select inputs
  setClients(invoices);
  setBranches(selectedInvoice.branch);

  //Set values of selected invoice
  setDefaultValue(selectedInvoice);
};

const setClients = (invoices) => {
  const editClientSelect = document.getElementById("edit-client-select");
  //Reset options
  editClientSelect.innerHTML = "";

  //Remove duplicated clients
  const mapFromInvoices = new Map(
    invoices.map((invoice) => [invoice.clientId, invoice])
  );
  const uniqueClients = [...mapFromInvoices.values()];

  uniqueClients.forEach(({ clienLastName, clientName, clientId }) => {
    editClientSelect.innerHTML += `<option value="${clientId}" >${clienLastName}, ${clientName}</option>`;
  });
};

const setBranches = ({ id, address }) => {
  const editBranchSelect = document.getElementById("edit-branch-select");

  //Reset options
  editBranchSelect.innerHTML = "";
  editBranchSelect.innerHTML += `<option value="${id}">${address}</option>`;
};

const setDefaultValue = ({
  clientId,
  date,
  branch: { id: branchId, address },
}) => {
  const clientOptions = document.querySelectorAll("#edit-client-select option");
  const branchOptions = document.querySelectorAll("#edit-branch-select option");

  //To change the value of the selected item
  setSelectedOption(clientOptions, String(clientId));
  setSelectedOption(branchOptions, String(branchId));

  document.getElementById("edit-date-input").value = new Date(
    date
  ).toLocaleDateString();
};

const handleEdit = async (e) => {
  e.preventDefault();
  const clientId = document.getElementById("edit-client-select").value;
  const branchId = document.getElementById("edit-branch-select").value;
  const date = document.getElementById("edit-date-input").value;

  const body = {
    clientId,
    branchId,
    date: new Date(date).toISOString(),
    invoiceNumber: selectedInvoiceNumber,
  };

  try {
    //Close edit modal
    document.querySelector(".btn-close").click();

    await updateInvoice(body);

    await getInvoicesAndGenerateTable();

    showAlert("Factura actualizada con éxito", "success");
  } catch (error) {
    showAlert(`Error al actualizar factura: ${error.message}`, "danger");
  }
};

const editInvoiceForm = document.getElementById("edit-invoice-form");
editInvoiceForm.addEventListener("submit", handleEdit);
