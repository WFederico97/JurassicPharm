const token = localStorage.getItem("jwtToken");

export const getInvoices = async () => {
  const response = await fetch("http://localhost:3000/api/invoice", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });

  return response.ok ? await response.json() : [];
};
export const fetchBilingReport = async () => {
  const response = await fetch("http://localhost:3000/api/billing-report", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });

  return response.ok ? await response.json() : [];
};

export const updateInvoice = async (payload) => {
  const { invoiceNumber, ...body } = payload;

  return await fetch(`http://localhost:3000/api/invoice/${invoiceNumber}`, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(body),
  });
};

export const createInvoice = async (payload) => {
  return await fetch("http://localhost:3000/api/invoice", {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });
};
