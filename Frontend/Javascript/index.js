let tokenStored = localStorage.getItem("jwtToken");
const activePage = window.location.pathname;

const navLinks = document.querySelectorAll(".nav-link");

//check user email for navbar
document.addEventListener("DOMContentLoaded", () => {
  const fullName = localStorage.getItem("fullName");

  if (fullName) {
    document.getElementById("fullName").textContent = fullName;
  }
});

//Activate sidebar items
navLinks.forEach(link => {  
  if(link.href.includes(`${activePage}`)){
    document.querySelector(".active").classList.remove('active')
    link.classList.add('active')
  }  
});

if(tokenStored == null){
  window.location = '../Pages/login.html';
}

document.getElementById('logout').addEventListener('click', logout);
async function logout() {
  localStorage.removeItem('jwtToken');
  localStorage.removeItem('userEmail');
  localStorage.removeItem('userRole');
  window.location = '../Pages/login.html';
}