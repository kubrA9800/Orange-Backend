$(function () {

    function updateIcons() {
        var wishlist = JSON.parse(localStorage.getItem('wishlist')) || [];

        wishlist.forEach(function (productId) {
            var $icon = $('.heart-icon[data-id="' + productId + '"]');
            $icon.addClass('fa-solid').removeClass('fa-regular');
        });
    }

    $(document).on('click', ".heart-icon", function () {

        //let id = $(this).parent().parent().attr("data-id");
        var productId = $(this).data('id');
        let count = $(".wishlist .count").text();
        $.ajax({
            url: `/shop/addtowishlist?id=${productId}`,
            type: "Post",
            success: function (res) {

                $(".wishlist .count").text(res);

            }
        })

        var wishlist = JSON.parse(localStorage.getItem('wishlist')) || [];
        var index = wishlist.indexOf(productId);

        if (index === -1) {

            wishlist.push(productId);

            $(this).addClass('fa-solid').removeClass('fa-regular');
        } else
        {
            wishlist.splice(index, 1);

            $(this).addClass('fa-regular').removeClass('fa-solid');
        }

        localStorage.setItem('wishlist', JSON.stringify(wishlist));


    })

 

    $(document).on('click', ".delete-product-from-wishlist", function (e) {

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

        var wishlist = JSON.parse(localStorage.getItem('wishlist')) || [];
        var index = wishlist.indexOf(id);

        if (index !== -1) {
            wishlist.splice(index, 1);

            // Обновить иконку на первую
            $('.heart-icon[data-id="' + id + '"]').addClass('fa-regular').removeClass('fa-solid');

            // Сохранить информацию в localStorage
            localStorage.setItem('wishlist', JSON.stringify(wishlist));
        }


    })


    updateIcons();


    $(document).on("click", ".basket-icon.wishlist", function (e) {
        console.log(this);
        $(this).addClass("d-none");
        $(this).next().removeClass("d-none");
    })
})