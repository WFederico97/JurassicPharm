let tokenStored = localStorage.getItem('jwtToken')
const activePage = window.location.pathname;

const navLinks = document.querySelectorAll('.nav-link');

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
  window.location = '../Pages/login.html';
}