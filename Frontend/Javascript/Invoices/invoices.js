import { showAlert } from "../helpers/showAlert.js";
import { generateTable } from "../modules/Invoices/generateTable.js";
import { getInvoices } from "../modules/Invoices/api.js";
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

const setDefaultValue = ({ clientId, date, branch: { id: branchId } }) => {
  const clientOptions = document.querySelectorAll("#edit-client-select option");
  const branchOptions = document.querySelectorAll("#edit-branch-select option");
  const [ISOdate] = date.split("T");

  document.getElementById("edit-date-input").value = ISOdate;

  //To change the value of the selected item
  setSelectedOption(clientOptions, String(clientId));
  setSelectedOption(branchOptions, String(branchId));
};
