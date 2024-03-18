// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//// Mobile menu
let domMenu = document.getElementById("menu")

function MenuButton_Clicked() {
    domMenu.classList.toggle("active")
}

//// Light/Dark Toggle
let domCheckbox = document.querySelector('input[name=lightdarkcheckbox]')

// sessionStorage match window theme
if (sessionStorage.getItem('theme') === null) {
    if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
        sessionStorage.setItem('theme', 'dark')
    } else {
        sessionStorage.setItem('theme', 'light')
    }
}

//set theme
if (sessionStorage.getItem('theme') == 'dark') {
    document.documentElement.setAttribute('data-theme', 'dark')
    domCheckbox.checked = true
} else {
    document.documentElement.setAttribute('data-theme', 'light')
    domCheckbox.checked = false
}

// switch theme if checkbox is engaged
domCheckbox.addEventListener('change', (cb) => {
    document.documentElement.setAttribute(
        'data-theme',
        cb.target.checked ? 'dark' : 'light'

    )
    sessionStorage.setItem('theme', cb.target.checked ? 'dark' : 'light')
})