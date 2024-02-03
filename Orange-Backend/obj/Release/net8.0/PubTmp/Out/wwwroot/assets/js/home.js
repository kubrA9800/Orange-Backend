

$(function () {



  let products = document.querySelectorAll(".slider-products .product")

  products.forEach(productImage => {
    productImage.addEventListener("mouseover", function () {
      this.children[0].children[1].classList.remove("d-none")
    })

  })

  products.forEach(productImage => {
    productImage.addEventListener("mouseout", function () {
      this.children[0].children[1].classList.add("d-none")
    })

  })



  products.forEach(productIcon => {
    productIcon.addEventListener("mouseover", function () {
      this.children[1].classList.remove("d-none")
    })

  })

  products.forEach(productIcon => {
    productIcon.addEventListener("mouseout", function () {
      this.children[1].classList.add("d-none")
    })

  })


  let heartIcons = document.querySelectorAll(".product .icons .heart-icon");

  heartIcons.forEach(heartIcon => {
    heartIcon.addEventListener("click", function () {

      this.nextElementSibling.classList.remove("d-none")

      this.nextElementSibling.addEventListener("click", function () {
          this.classList.add("d-none")

      })

    })

  });


  
  let overlay = document.querySelector(".overlay")
  let cartSidebar = document.querySelector(".cart-sidebar")

  overlay.addEventListener("click", function () {
    this.style.display = "none";
    modal.classList.add("d-none")
    cartSidebar.classList.add("move-sidebar")

  })




  let eyeIcons = document.querySelectorAll(".product .icons .fa-eye")
  let modal = document.querySelector(".modals")

  eyeIcons.forEach(eyeIcon => {
    eyeIcon.addEventListener("click", function () {
      modal.classList.remove("d-none")
      overlay.style.display = "block";
    })
  })

  let closeModalIcon = document.querySelector(".modals .close")
  closeModalIcon.addEventListener("click", function () {
    modal.classList.add("d-none")
    overlay.style.display = "none";
  })

  
  
  let addBasketIcons = document.querySelectorAll(".slider-products .product .icons .basket-icon")
  addBasketIcons.forEach(addBasketIcon => {
    addBasketIcon.addEventListener("click", function () {
      cartSidebar.classList.remove("move-sidebar")
      overlay.style.display = "block";
    })
  })


})