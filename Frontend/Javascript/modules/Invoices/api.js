const token = localStorage.getItem("jwtToken");

export const getInvoices = async () => {
  const response = await fetch("http://localhost:3000/api/GetInvoices", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });

  return response.ok ? await response.json() : [];
};
export const fetchBilingReport = async () => {
  const response = await fetch("http://localhost:3000/api/billingReport", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });

  return response.ok ? await response.json() : [];
};

export const createInvoice = async (payload) => {
  return await fetch("http://localhost:3000/api/NewInvoice", {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });
};
