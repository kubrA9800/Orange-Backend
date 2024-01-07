"use strict"
let placeholders = document.querySelectorAll(".form .input span")

placeholders.forEach(placeholder =>
    placeholder.onclick = function () {
        this.nextElementSibling.focus()
    })


let inputs = document.querySelectorAll(".input input")
inputs.forEach(input => {
    input.onfocus = function () {
        this.previousElementSibling.style.top = "-14px"
        this.previousElementSibling.style.fontSize = "14px"
    }
})

inputs.forEach(input => {
    input.onblur = function () {
        this.previousElementSibling.style.top = "5px"
        this.previousElementSibling.style.fontSize = "16px"
    }

})



let links = document.querySelectorAll("#login-register .link a")
links.forEach(link =>

    link.onclick = function () {

        let id = this.getAttribute("data-id")
        let boxs = document.querySelectorAll(".box")
        for (const box of boxs) {

            if (box.getAttribute("data-id") === id) {
                box.classList.remove("d-none")
            } else {
                box.classList.add("d-none")

            }
        }
    })