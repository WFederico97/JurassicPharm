const activePage = window.location.pathname;

const navLinks = document.querySelectorAll('.nav-link');

//Activate sidebar items
navLinks.forEach(link => {  
  if(link.href.includes(`${activePage}`)){
    document.querySelector(".active").classList.remove('active')
    link.classList.add('active')
  }  
});

