﻿$(function () {


    $(document).on("click", ".basket-icon", function (e) {

        let id = parseInt($(this).closest(".item").attr("data-id"))
        let basketCount = $(".basket .count").text();
        console.log("fghj");
        //let cartSidebar = $(".cart-products")
        //console.log(cartSidebar);
        $.ajax({

            url: `/shop/addtobasket?id=${id}`,
            type: "Post",
            success: function (res) {
                basketCount++;
                $(".basket .count").text(basketCount)

                //$(cartSidebar).append(
                //    `<div class="item" >
                //    <div class="content d-flex align-items-center">
                //        <div class="img">
                //            <img src="~/assets/img/product/${res.Image}" alt="">

                //        </div>
                //        <div class="text">
                //            <h1>${res.Name}</h1>
                //            <h2>$ ${res.Price}x <span>${res.Count}</span></h2>
                //            <div class="count">
                //                <i class="fa-solid fa-minus"></i>
                //                <span>${res.Count}</span>
                //                <i class="fa-solid fa-plus"></i>
                //            </div>
                //        </div>
                //    </div>
                //    <div class="button">
                //        <a asp-controller="Cart" asp-action="DeleteProduct">Remove</a>
                //    </div>
                //</div >`
                //)



            }
        })
    })


    $(document).on("click", ".delete-product", function (e) {
        let id = parseInt($(this).attr("data-id"));
        $.ajax({
            url: `cart/deleteproduct?id=${id}`,
            type: "Post",
            success: function (res) {

                $(".basket .count").text(res.count);
                $(e.target).closest("#cart tr").remove();
                $(e.target).closest(".item").remove();
                $(".grand-total span").text(res.grandTotal.toFixed(2));

                if (res.count === 0) {
                    $(".empty-cart").removeClass("d-none");
                    $(".full-cart").addClass("d-none");
                }


            }
        })

    })


    $(document).on("click", ".plus-icon", function (e) {

        let id = parseInt($(this).attr("data-id"))
        let count = $(".basket .count").text();
        $.ajax({

            url: `cart/IncreaseProductCount?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).prev().text(res.countItem)
                $(".up-count").text(res.countItem)
                $(".grand-total span").text(res.grandTotal.toFixed(2));
                $(e.target).parent().next().next().children().text(res.productTotalPrice.toFixed(2))
                count++;
                $(".basket .count").text(count);
            }
        })

    })


    $(document).on("click", ".minus-icon", function (e) {
        let id = parseInt($(this).attr("data-id"))
        let count = $(".basket .count")
        let a = 0;

        $.ajax({

            url: `cart/DecreaseProductCount?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).next().text(res.countItem)
                $(".up-count").text(res.countItem)
                $(".grand-total span").text(res.grandTotal.toFixed(2));
                $(e.target).parent().next().next().children().text(res.productTotalPrice.toFixed(2))
                $(count).text(res.countBasket)

            }
        })

    })

})