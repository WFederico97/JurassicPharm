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
            <td>${getTotal(details)}</td>
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

const getInvoices = () => {
  //TODO: FETCH REAL DATA FROM BACKEND

  return [
    {
      clientName: "Jorge",
      clienLastName: "Bayer",
      date: "2023-09-01T00:00:00",
      branch: "Monroe, 742",
      details: [
        {
          supplyName: "Ibuprofeno",
          unitPrice: 150,
          amount: 2,
        },
        {
          supplyName: "Insulina",
          unitPrice: 1500,
          amount: 1,
        },
        {
          supplyName: "Viagra",
          unitPrice: 200,
          amount: 2,
        },
      ],
    },
    {
      clientName: "Jorge",
      clienLastName: "Suspenso",
      date: "2023-09-02T00:00:00",
      branch: "Boulevard Chacabuco, 360",
      details: [
        {
          supplyName: "Amoxicilina",
          unitPrice: 500,
          amount: 1,
        },
      ],
    },
    {
      clientName: "Juan Roman",
      clienLastName: "Tristelme",
      date: "2023-09-03T00:00:00",
      branch: "Olleros, 501",
      details: [
        {
          supplyName: "Paracetamol",
          unitPrice: 120,
          amount: 3,
        },
      ],
    },
    {
      clientName: "Jorge",
      clienLastName: "Bayer",
      date: "2022-09-01T00:00:00",
      branch: "Monroe, 742",
      details: [
        {
          supplyName: "Paracetamol",
          unitPrice: 120,
          amount: 15,
        },
      ],
    },
    {
      clientName: "Jorge",
      clienLastName: "Suspenso",
      date: "2019-09-02T00:00:00",
      branch: "Boulevard Chacabuco, 360",
      details: [
        {
          supplyName: "Paracetamol",
          unitPrice: 120,
          amount: 33,
        },
      ],
    },
    {
      clientName: "Juan Roman",
      clienLastName: "Tristelme",
      date: "2015-09-03T00:00:00",
      branch: "Olleros, 501",
      details: [
        {
          supplyName: "Ibuprofeno",
          unitPrice: 150,
          amount: 12,
        },
      ],
    },
    {
      clientName: "Jorge",
      clienLastName: "Bayer",
      date: "2024-09-21T00:00:00",
      branch: "Olleros, 501",
      details: [
        {
          supplyName: "Ibuprofeno",
          unitPrice: 150,
          amount: 14,
        },
      ],
    },
    {
      clientName: "Juan Roman",
      clienLastName: "Tristelme",
      date: "2024-09-15T00:00:00",
      branch: "Monroe, 742",
      details: [
        {
          supplyName: "Paracetamol",
          unitPrice: 120,
          amount: 3,
        },
      ],
    },
    {
      clientName: "Juan Roman",
      clienLastName: "Tristelme",
      date: "2024-09-04T00:00:00",
      branch: "Monroe, 742",
      details: [
        {
          supplyName: "Paracetamol",
          unitPrice: 120,
          amount: 7,
        },
      ],
    },
    {
      clientName: "Jorge",
      clienLastName: "Suspenso",
      date: "2024-10-22T00:00:00",
      branch: "Boulevard Chacabuco, 360",
      details: [
        {
          supplyName: "Ibuprofeno",
          unitPrice: 150,
          amount: 3,
        },
      ],
    },
    {
      clientName: "Jorge",
      clienLastName: "Bayer",
      date: "2024-10-22T00:00:00",
      branch: "Monroe, 742",
      details: [],
    },
    {
      clientName: "Jorge",
      clienLastName: "Bayer",
      date: "2024-10-22T00:00:00",
      branch: "Monroe, 742",
      details: [],
    },
    {
      clientName: "Jorge",
      clienLastName: "Suspenso",
      date: "2024-10-22T00:00:00",
      branch: "Boulevard Chacabuco, 360",
      details: [],
    },
    {
      clientName: "Juan Roman",
      clienLastName: "Tristelme",
      date: "2024-10-22T00:00:00",
      branch: "Olleros, 501",
      details: [],
    },
    {
      clientName: "Jorge",
      clienLastName: "Suspenso",
      date: "2024-10-22T00:00:00",
      branch: "Boulevard Chacabuco, 360",
      details: [],
    },
  ];
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
