const token = localStorage.getItem("jwtToken");

export const forgotPassword = async (email) => {
  return await fetch("http://localhost:3000/api/Auth/forgot-password", {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(email),
  });
};

export const resetPassword = async (token, newPassword) => {
  return await fetch(
    `http://localhost:3000/api/Auth/reset-password?token=${token}`,
    {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newPassword),
    }
  );
};
