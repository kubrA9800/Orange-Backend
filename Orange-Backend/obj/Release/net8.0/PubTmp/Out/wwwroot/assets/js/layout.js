$(function () {
    
  let openSidebarIcon = document.querySelector(".header-all .hamburger-icon")
  let sidebar = document.querySelector(".sidebar")
  let closeSidebarIcon = document.querySelector(".sidebar .close-icon")
  let overlay = document.querySelector(".overlay")

  openSidebarIcon.addEventListener("click", function () {
    sidebar.classList.remove("move-sidebar")
    overlay.style.display = "block";

  })

  closeSidebarIcon.addEventListener("click", function () {
    sidebar.classList.add("move-sidebar")
    overlay.style.display = "none"
  })

  overlay.addEventListener("click", function () {
    sidebar.classList.add("move-sidebar")
    this.style.display = "none";
    seacrhSidebar.classList.add("move-sidebar")
    cartSidebar.classList.add("move-sidebar")

  })

  let seacrhSidebar = document.querySelector(".search-sidebar")
  let searchIcon = document.querySelector(".icons .search-icon")
  let closeSearchIcon = document.querySelector(".search-sidebar .fa-xmark")
  searchIcon.addEventListener("click", function () {
    seacrhSidebar.classList.remove("move-sidebar")
    overlay.style.display = "block";

  })
  closeSearchIcon.addEventListener("click", function () {
    seacrhSidebar.classList.add("move-sidebar")
    overlay.style.display = "none"
  })


  let basketIcon = document.querySelector("header .basket")
  let cartSidebar = document.querySelector(".cart-sidebar")
  let closeCartSidebar = document.querySelector(".cart-sidebar .close-icon")

  basketIcon.addEventListener("click", function () {
    cartSidebar.classList.remove("move-sidebar")
    overlay.style.display = "block";
  })
  closeCartSidebar.addEventListener("click", function () {
    cartSidebar.classList.add("move-sidebar")
    overlay.style.display = "none"
  })



  let footerInput = document.querySelector("footer .input input")
  footerInput.addEventListener("focus", function () {
    this.previousElementSibling.style.top = "-13px"
    this.previousElementSibling.style.fontSize = "12px"
  })
  footerInput.addEventListener("blur", function () {
    this.previousElementSibling.style.top = "0px"
    this.previousElementSibling.style.fontSize = "16px"
  })

    $('#topbtn').click(function () {
        $('html').animate({
            scrollTop: 0
        }, 100)

    })

})