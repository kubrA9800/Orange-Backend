





$(function () {

    $(document).on("click", ".open-modal", function (e) {

        let id = $(this).attr("data-id");
        let name=$(".modals .product-name")
        let description = $(".modals .product-desc")
        let price = $(".modals .product-price")
        let category = $(".modals .product-category")

        $.ajax({
            type: "Get",
            url: `/Home/GetProductDatasModal/${id}`,
            success: function (res) {
                description.text(res.description)
                name.text(res.name)
                price.text(res.price.toFixed(2))
                category.text(res.categoryName)
                let src = "/assets/img/product/" + res.image;
                console.log(src);
                $(".modals .img img").attr("src", src);
                $(".text").attr("data-id", res.id);
                $(".item").attr("data-id", res.id);
                $(".info a").attr("href", `/Shop/ProductDetail/${res.id}`);


            }
        })


    })


})