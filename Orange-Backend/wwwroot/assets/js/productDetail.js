// $('.slider-for').slick({
//     slidesToShow: 1,
//     slidesToScroll: 1,
//     arrows: false,
//     fade: true,
//     asNavFor: '.slider-nav'
//   });
//   $('.slider-nav').slick({
//     slidesToShow: 4,
//     slidesToScroll: 1,
//     vertical:true,
//     asNavFor: '.slider-for',
//     dots: false,
//     focusOnSelect: true,
//     verticalSwiping:true
//   });

"use strict"

$(function () {


  // $(".swinger-container").swinger();

  // $(".product").hover(function () {
  //   let dataId = $(this).data("id");

  //   $(".product-icons").each(function () {
  //     if ($(this).data("id") === dataId) {
  //       $(this).toggleClass("transform-product-icons");
  //     }
  //   });
  // });








  if ( $('.product__slider-main').length ) {
    var $slider = $('.product__slider-main')
        .on('init', function(slick) {
            $('.product__slider-main').fadeIn();
        })
        .slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: true,
            autoplay: false,
            lazyLoad: 'ondemand',
            
            asNavFor: '.product__slider-thmb'
        });

var $slider2 = $('.product__slider-thmb')
        .on('init', function(slick) {
            $('.product__slider-thmb').fadeIn();
        })
        .slick({
            slidesToShow: 4,
            slidesToScroll: 1,
            lazyLoad: 'ondemand',
            asNavFor: '.product__slider-main',
            dots: false,
            centerMode: false,
            focusOnSelect: true
        });

//remove active class from all thumbnail slides
$('.product__slider-thmb .slick-slide').removeClass('slick-active');

//set active class to first thumbnail slides
$('.product__slider-thmb .slick-slide').eq(0).addClass('slick-active');

// On before slide change match active thumbnail to current slide
$('.product__slider-main').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
var mySlideNumber = nextSlide;
$('.product__slider-thmb .slick-slide').removeClass('slick-active');
$('.product__slider-thmb .slick-slide').eq(mySlideNumber).addClass('slick-active');
});


// init slider
require(['js-sliderWithProgressbar'], function(slider) {

$('.product__slider-main').each(function() {

    me.slider = new slider($(this), options, sliderOptions, previewSliderOptions);

    // stop slider
    //me.slider.stop();

    // start slider
    //me.slider.start(index);

    // get reference to slick slider
    //me.slider.getSlick();

});
});
var options = {
progressbarSelector    : '.bJS_progressbar'
, slideSelector        : '.bJS_slider'
, previewSlideSelector : '.bJS_previewSlider'
, progressInterval     : ''
    // add your own progressbar animation function to sync it i.e. with a video
    // function will be called if the current preview slider item (".b_previewItem") has the data-customprogressbar="true" property set
, onCustomProgressbar : function($slide, $progressbar) {}
}

// slick slider options
// see: https://kenwheeler.github.io/slick/
var sliderOptions = {
slidesToShow   : 1,
slidesToScroll : 1,
arrows         : false

}

// slick slider options
// see: https://kenwheeler.github.io/slick/
var previewSliderOptions = {
slidesToShow   : 3,
slidesToScroll : 1,
dots           : false,
focusOnSelect  : true,
centerMode     : true
}
}

  
})

let addToWishlistIcon=document.querySelector("#product-detail .text .add")

addToWishlistIcon.onclick=function(){
    this.parentElement.nextElementSibling.children[0].classList.remove("d-none")
    this.classList.add("d-none")
    // this.parentElement.nextElementSibling.children[0].onclick=function(){
    //     this.classList.add("d-none")
    //     this.parentElement.previousElementSibling.children[0].classList.remove("d-none")
    // }
}



let tabHeaders=document.querySelectorAll(".tab-header .item")
let tabContents=document.querySelectorAll(".tab-content .item")
tabHeaders.forEach(tabHeader => {
    tabHeader.addEventListener("click", function(){
        document.querySelector(".active-tab").classList.remove("active-tab")
        this.classList.add("active-tab")


        tabContents.forEach(tabContent => {
            if(tabContent.getAttribute("data-id") ==this.getAttribute("data-id")){
                tabContent.classList.remove("d-none")
            }else{
                tabContent.classList.add("d-none")
            }
        });
    })
});