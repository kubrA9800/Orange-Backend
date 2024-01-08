"use strict"

$(function () {

    (function() {

        var parent = document.querySelector(".range-slider");
        if(!parent) return;
      
        var
          rangeS = parent.querySelectorAll("input[type=range]"),
          numberS = parent.querySelectorAll("input[type=number]");
      
        rangeS.forEach(function(el) {
          el.oninput = function() {
            var slide1 = parseFloat(rangeS[0].value),
                  slide2 = parseFloat(rangeS[1].value);
      
            if (slide1 > slide2) {
                      [slide1, slide2] = [slide2, slide1];
              // var tmp = slide2;
              // slide2 = slide1;
              // slide1 = tmp;
            }
      
            numberS[0].value = slide1;
            numberS[1].value = slide2;
          }
        });
      
        numberS.forEach(function(el) {
          el.oninput = function() {
                  var number1 = parseFloat(numberS[0].value),
                          number2 = parseFloat(numberS[1].value);
                  
            if (number1 > number2) {
              var tmp = number1;
              numberS[0].value = number2;
              numberS[1].value = tmp;
            }
      
            rangeS[0].value = number1;
            rangeS[1].value = number2;
      
          }
        });
      
      })();



      let products = document.querySelectorAll(".product")

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


  let cartSidebar = document.querySelector(".cart-sidebar")

  let overlayCart = document.querySelector(".overlay-cart")
  let addBasketIcons = document.querySelectorAll(".product .icons .fa-bag-shopping")
  addBasketIcons.forEach(addBasketIcon => {
    addBasketIcon.addEventListener("click", function () {
      cartSidebar.classList.remove("move-sidebar")
      overlayCart.style.display = "block";
    })
  })
})