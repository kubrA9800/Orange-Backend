$(function () {

    $(document).on("click", ".heart-icon", function () {
        console.log("hgvjb");

        let id = $(this).parent().parent().attr("data-id");;
        let count = $(".wishlist .count").text();
        $.ajax({
            url: `/shop/addtowishlist?id=${id}`,
            type: "Post",
            success: function (res) {

                $(".wishlist .count").text(res);

            }
        })


    })


    $(document).on("click", ".delete-product-from-wishlist", function (e) {

        let id = parseInt($(this).attr("data-id"));
        console.log(id);
        $.ajax({
            url: `wishlist/deleteproduct?id=${id}`,
            type: "Post",
            success: function (res) {
                res--

                $(".wishlist .count").text(res);
                $(e.target).closest("tr").remove();

                if (res === 0) {
                    $(".empty-cart").removeClass("d-none");
                    $(".full-cart").addClass("d-none");
                }


            }
        })


    })
})