import { fetchSupplies, getSuppliesSelectOptions, updateSupplies,addSupply } from "../modules/Supplies/api.js";
import { showAlert } from "../helpers/showAlert.js";

// Array to store supplies
let supplies = [];
const userRole = localStorage.getItem("role");

// Show "Add Supply" button only if the user's role is "CAJERO"
if (userRole === "REPOSITOR") {
  document.getElementById("addSupplyButtonContainer").style.display = "block";
}

// Display user's full name in the navbar
document.addEventListener("DOMContentLoaded", async () => {
  const fullName = localStorage.getItem("fullName");
  if (fullName) {
    document.getElementById("fullName").textContent = fullName;
  }
  await generateTable();
});

// Load supplies table when the page is loaded
window.addEventListener("load", async () => {
  await generateTable();
});

// Populate the Edit Supply Modal with selected supply data
document
  .getElementById("editSupplyModal")
  .addEventListener("show.bs.modal", async function (event) {
    const button = event.relatedTarget;
    const index = button.getAttribute("data-index");
    const supply = supplies[index];

    // Get select options
    const selectOptions = await getSuppliesSelectOptions();

    // Get the IDs of the selected options
    const brandId = selectOptions.marcas.find(option => option.nombre === supply.brand)?.id;
    const distributionId = selectOptions.tiposDistribucion.find(option => option.nombre === supply.distribution)?.id;
    const supplyTypeId = selectOptions.tiposSuministro.find(option => option.nombre === supply.supplyType)?.id;

    populateSelect("editMarcaSelect", selectOptions.marcas, brandId);
    populateSelect("editTipoSuministroSelect", selectOptions.tiposDistribucion, distributionId);
    populateSelect("editPresentacionSelect", selectOptions.tiposSuministro, supplyTypeId);
    
    // Set supply values in the form
    document.getElementById("editNombre").value = supply.name;
    document.getElementById("editPrecio").value = supply.price;
    document.getElementById("editStock").value = supply.stock;
    document.getElementById("editStockMinimo").value = supply.minimumStock;
    
    // Set the index of the supply in the form
    document.getElementById("editSupplyForm").setAttribute("data-index", index);
  });

//Populate the Add Supply Modal with select options
document
  .getElementById("addSupplyModal")
  .addEventListener("show.bs.modal", async function (event) {
    const selectOptions = await getSuppliesSelectOptions();
    populateSelect("addMarca", selectOptions.marcas);
    populateSelect("addTipoSuministro", selectOptions.tiposDistribucion);
    populateSelect("addPresentacion", selectOptions.tiposSuministro);

    // Reset form fields
    document.getElementById("addNombre").value = "";
    document.getElementById("addPrecio").value = "";
    document.getElementById("addStock").value = "";
    document.getElementById("addStockMinimo").value = "";

  });



  // Populate a select element with options and set the selected value
function populateSelect(selectId, options, selectedValue) {
  const selectElement = document.getElementById(selectId);
  selectElement.innerHTML = ""; // Reset options
  options.forEach(option => {
    const opt = document.createElement("option");
    opt.value = option.id;
    opt.textContent = option.nombre;
    selectElement.appendChild(opt);
  });
  // Set the selected value after populating options
  selectElement.value = selectedValue;
}

// Handle the submit event of the edit supply form
document
  .getElementById("editSupplyForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    const index = parseInt(this.getAttribute("data-index"));
    const originalSupply = supplies[index];

    // Collect updated values
    const updatedSupply = {
      idSupply: originalSupply.idSupply,
      name: document.getElementById("editNombre").value,
      price: parseFloat(document.getElementById("editPrecio").value),
      stock: parseInt(document.getElementById("editStock").value),
      minimumStock: parseInt(document.getElementById("editStockMinimo").value),
      brand: document.getElementById("editMarcaSelect").value,
      distribution: document.getElementById("editTipoSuministroSelect").value,
      supplyType: document.getElementById("editPresentacionSelect").value,
    };

    // Send updated data to the API
    await updateSupplies(updatedSupply);

    // Update the table and close the modal
    supplies[index] = { ...originalSupply, ...updatedSupply };
    const modalInstance = bootstrap.Modal.getInstance(
      document.getElementById("editSupplyModal")
    );
    modalInstance.hide();
    generateTable();
  });

//Handle the submit event of the add supply form
document
  .getElementById("addSupplyForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    // Collect new supply data
    const newSupply = {
      Name: document.getElementById("addNombre").value,
      Price: parseInt(document.getElementById("addPrecio").value),
      Stock: parseInt(document.getElementById("addStock").value),
      minimumStock: parseInt(document.getElementById("addStockMinimo").value),
      IdBrand: parseInt(document.getElementById("addMarca").value),
      IdDistribution: parseInt(document.getElementById("addTipoSuministro").value),
      IdSupplyType: parseInt(document.getElementById("addPresentacion").value),
    };

    // Send new data to the API
    await addSupply(newSupply);

    // Update the table and close the modal
    generateTable();
    const modalInstance = bootstrap.Modal.getInstance(
      document.getElementById("addSupplyModal")
    );
    modalInstance.hide();
  });

// Generate table with supplies data
const generateTable = async () => {
  supplies = await fetchSupplies();
  let tableContent = "";

  supplies.forEach((supply, index) => {
    tableContent += `
      <tr>
        <td>${supply.idSupply}</td>
        <td>${supply.name}</td>
        <td>${supply.brand}</td>
        <td>${supply.distribution}</td>
        <td>${supply.supplyType}</td>
        <td>${supply.price}</td>
        <td>${supply.stock}</td>
        <td class="d-flex">
          <button
            type="button" 
            class="btn btn-primary rounded-pill px-3 m-1 editBtn"  
            data-bs-toggle="modal" 
            data-bs-target="#editSupplyModal" 
            data-index="${index}"
            ${userRole !== "REPOSITOR" ? "disabled" : ""}
          >
            <svg height="1em" viewBox="0 0 512 512">
              <path
              d="M410.3 231l11.3-11.3-33.9-33.9-62.1-62.1L291.7 89.8l-11.3 11.3-22.6 22.6L58.6 322.9c-10.4 10.4-18 23.3-22.2 37.4L1 480.7c-2.5 8.4-.2 17.5 6.1 23.7s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L387.7 253.7 410.3 231zM160 399.4l-9.1 22.7c-4 3.1-8.5 5.4-13.3 6.9L59.4 452l23-78.1c1.4-4.9 3.8-9.4 6.9-13.3l22.7-9.1v32c0 8.8 7.2 16 16 16h32zM362.7 18.7L348.3 33.2 325.7 55.8 314.3 67.1l33.9 33.9 62.1 62.1 33.9 33.9 11.3-11.3 22.6-22.6 14.5-14.5c25-25 25-65.5 0-90.5L453.3 18.7c-25-25-65.5-25-90.5 0zm-47.4 168l-144 144c-6.2 6.2-16.4 6.2-22.6 0s-6.2-16.4 0-22.6l144-144c6.2-6.2 16.4-6.2 22.6 0s6.2 16.4 0 22.6z"
              ></path>
            </svg>
          </button>
        </td>
      </tr>
    `;
  });

  const tableBody = document.querySelector("#supplies-table tbody");
  tableBody.innerHTML = tableContent;

  document.querySelectorAll(".editBtn").forEach((button) =>
    button.addEventListener("click", handleEditButtonClick)
  );
};

// Function to handle the click on the edit button
const handleEditButtonClick = (event) => {
  const index = event.currentTarget.getAttribute("data-index");
  const supply = supplies[index];

  // Set the selected supply's index and prefill the form fields in the modal
  document.getElementById("editSupplyModal").setAttribute("data-index", index);
  document.getElementById("editNombre").value = supply.name;
  document.getElementById("editPrecio").value = supply.price;
  document.getElementById("editStock").value = supply.stock;
  document.getElementById("editStockMinimo").value = supply.minimumStock;
};