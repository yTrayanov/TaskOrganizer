
const popupWindow = document.querySelectorAll(".popup");


const showCloseWindow = () => {
    const burgerMenu = document.querySelector(".burger-menu");

    if (burgerMenu.classList.contains("hidden")) {
        burgerMenu.classList.remove("hidden");
    }
    else {
        burgerMenu.classList.add("hidden");
    }
}

popupWindow.forEach(element => {
    element.addEventListener('click', showCloseWindow);
})
