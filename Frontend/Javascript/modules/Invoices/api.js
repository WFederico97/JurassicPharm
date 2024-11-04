const token = localStorage.getItem("jwtToken");

export const getInvoices = async () => {
  const response = await fetch("http://localhost:5017/api/invoice", {
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

  return await fetch(`http://localhost:5017/api/invoice/${invoiceNumber}`, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(body),
  });
};
