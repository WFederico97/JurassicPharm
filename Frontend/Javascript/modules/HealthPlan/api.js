const token = localStorage.getItem("jwtToken");

export const getAllHealthPlan = async () => {
  const response = await fetch("http://localhost:3000/api/HealthPlan", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });

  return response.ok ? await response.json() : [];
};
