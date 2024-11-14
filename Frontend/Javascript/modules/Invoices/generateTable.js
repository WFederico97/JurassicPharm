export const generateTable = async (invoices) => {
  const role = localStorage.getItem("role");

  const tbody = document.getElementById("invoices-tbody-table");
  //Reset tbody
  tbody.innerHTML = "";

  invoices.forEach(
    (
      {
        invoiceNumber,
        clientName,
        clienLastName,
        date,
        branch: { address },
        details,
      },
      index
    ) => {

      tbody.innerHTML += `
            <tr>
              <td>${invoiceNumber}</td>
              <td>${clienLastName}, ${clientName}</td>
              <td>${new Date(date).toLocaleDateString()}</td>
              <td>${address}</td>
              <td>
                <button 
                  type="button" 
                  name="btn-detail"
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
};

const getTotal = (details) => {
  let total = 0;

  details.forEach(({ unitPrice, amount }) => {
    total += unitPrice * amount;
  });

  return total;
};
