const token = localStorage.getItem("jwtToken");

export const getAllCities = async () => {
  const res = await fetch("http://localhost:3000/api/City", {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });
  return res.ok ? res.json() : [];
};
