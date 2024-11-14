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
