export const generateDetails = (details) => {
  const modalTable = document.getElementById("facturas-modal-table");

  let modalTableBody = modalTable.querySelector("tbody");
  if (modalTableBody) {
    modalTableBody.remove();
  }

  modalTableBody = document.createElement("tbody");

  details.forEach(({ supplyName, salePrice, amount }) => {
    const row = document.createElement("tr");
    row.innerHTML = `
            <td>${supplyName}</td>
            <td>$${salePrice}</td>
            <td>${amount}</td>
          `;
    modalTableBody.appendChild(row);
  });

  modalTable.appendChild(modalTableBody);
};
