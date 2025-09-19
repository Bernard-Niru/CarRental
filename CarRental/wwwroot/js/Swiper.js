
        flatpickr("#pickup-date-input", {
            dateFormat: "Y-m-d",
            minDate: "today"
        });
        flatpickr("#dropoff-date-input", {
            dateFormat: "Y-m-d",
            minDate: "today"
        });

            var swiper = new Swiper(".swiper", {
                effect: "coverflow",
                grabCursor: true,
                centeredSlides: true,
                slidesPerView: 1.1,   // one image per slide
                speed: 400,
                coverflowEffect: {
                    rotate: 10,
                    stretch: 0,
                    depth: 100,
                    modifier: 3,
                    slideShadows: true,
                },
                loop: true,
                autoplay: {
                    delay: 3000,       // 3000ms = 3 seconds per slide
                    disableOnInteraction: false,  // keep autoplay even after user interacts
                },
                navigation: {
                    nextEl: ".swiper-button-next",
                    prevEl: ".swiper-button-prev",
                },

                pagination: {
                    el: ".swiper-pagination",
                    clickable: true,
                },
            });