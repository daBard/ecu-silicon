const formErrorHandler = (event, validationResult) => {
    let spanElememt = document.querySelector(`[data-valmsg-for="${event.target.name}"]`)

    if (validationResult) {
        event.target.classList.remove('input-validation-error')
        spanElememt.classList.remove('field-validation-error')
        spanElememt.classList.add('field-validation-valid')
        spanElememt.innerHTML = ''
    }
    else {
        event.target.classList.add('input-validation-error')
        spanElememt.classList.add('field-validation-error')
        spanElememt.classList.remove('field-validation-valid')
        spanElememt.innerHTML = event.target.dataset.valRequired
    }
}

const textLengthValidator = (value, minLength = 1) => {
    if (value.length >= minLength)
        return true
    else
        return false
}

const compareValidator = (event, compareValue) => {
    if (event.target.value === compareValue)
        return true

    return false
}

const textValidator = (event) => {
    formErrorHandler(event, textLengthValidator(event.target.value))
}

const emailValidator = (event) => {
    const regEx = /[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+/
    formErrorHandler(event, regEx.test(event.target.value))
}

const passwordValidator = (event) => {
    if (event.target.dataset.valEqualtoOther !== undefined) {
        formErrorHandler(event, compareValidator(event, document.getElementsByName(event.target.dataset.valEqualtoOther.replace('*', 'Form'))[0].value))
    }
    else {
        const regEx = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/
        formErrorHandler(event, regEx.test(event.target.value))
    }
}

const checkboxValidator = (event) => {
    if (event.target.checked)
        formErrorHandler(event, true)
    else
        formErrorHandler(event, false)
}

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {
        if (input.type === 'checkbox')
            input.addEventListener('change', (event) => {
                checkboxValidator(event);
            })
        else {
            input.addEventListener('keyup', (event) => {
                switch (event.target.type) {
                    case 'text':
                        textValidator(event)
                        break
                    case 'email':
                        emailValidator(event)
                        break
                    case 'password':
                        passwordValidator(event)
                        break
                } 
            })
        }
    }
})