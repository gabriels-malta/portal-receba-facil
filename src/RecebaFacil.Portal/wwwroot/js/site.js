function initScroll() {
    const linksInternos = document.querySelectorAll(".js-option a[href^='#'");

    function scrollToSection(event) {
        event.preventDefault();
        const href = event.currentTarget.getAttribute('href');
        const div = document.querySelector(href);
        div.scrollIntoView({
            behavior: "smooth",
            block: "start",
        });
    }
    linksInternos.forEach((link) => {
        link.addEventListener("click", scrollToSection);
    });
}

initScroll();


const divs = document.querySelectorAll(".js-left-animation, .js-right-animation");
const windowMetade = window.innerHeight * 0.6;

function animaScroll() {
    divs.forEach((div) => {
        const divTop = div.getBoundingClientRect().top;
        const isDivVisible = (divTop - windowMetade) < 0;
        if(isDivVisible) {
            div.classList.add("ativo");
        }
    })
}

window.addEventListener("scroll", animaScroll)