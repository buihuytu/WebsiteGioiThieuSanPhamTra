var username = document.querySelector('#username');
var email = document.querySelector('#email');
var password = document.querySelector('#password');
var confirmPassword = document.querySelector('#confirmPassword');
var form = document.querySelector('form');

function showError(input, message){
    let parent = input.parentElement;
    let small = parent.querySelector('small');
    parent.classList.add('error');
    small.innerText = message;
}

function showSuccess(input){
    let parent = input.parentElement;
    let small = parent.querySelector('small');
    parent.classList.remove('error');
    small.innerText = '';
}

function checkEmptyError(listInput){
    let isEmptyError = false;
    listInput.forEach(input => {
        input.value = input.value.trim();

        if(input.value == ''){
            isEmptyError = true;
            showError(input, "Không được để trống");
        }else{
            showSuccess(input);
        }
    });

    return isEmptyError;
}

function checkEmailError(input){
    const regexEmail =
  /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    input.value = input.value.trim();

    let isEmailError = !regexEmail.test(input.value);
    if(regexEmail.test(input.value)){
        showSuccess(input);
    } else {
        showError(input, "Email không hợp lệ");
    }

    return isEmailError;
}

function checkLengthError(input, min, max){
    input.value = input.value.trim();

    if(input.value.length < min){
        showError(input, `Phải có ít nhất ${min} kí tự`);
        return true;
    } 

    if(input.value.length > max){
        showError(input, `Không được quá ${max} kí tự`);
        return true;
    }

    return false;
}

function checkMatchPasswordError(passwordInput, confirmPasswordInput){
    if(passwordInput.value !== confirmPasswordInput.value){
        showError(confirmPasswordInput, 'Mật khẩu không trùng khớp');
        return true;
    }
    return false;
}


function checkValidate(){
    let isValid = true;

    // check account from DB
    // validate username
    if(!checkEmptyError([username])){
        if (checkLengthError(username, 3, 10)){
            isValid = false;
        } 
    } else {
        isValid = false;
    }


    // validate email
    if(!checkEmptyError([email])){
        if (checkEmailError(email)){
            isValid = false;
        } 
    } else {
        isValid = false;
    }

    // validate password
    if(!checkEmptyError([password])){
        if (checkLengthError(password, 3, 10)){
            isValid = false;
        } 
    } else {
        isValid = false;
    }

    // validate confirm password
    if(!checkEmptyError([confirmPassword])){
        if (checkMatchPasswordError(password, confirmPassword)){
            isValid = false;
        } 
    } else {
        isValid = false;
    }

    return isValid;
}

form.addEventListener('submit', function(e){
    e.preventDefault();

    // let isEmptyError = checkEmptyError([ email, password, confirmPassword]);
    // let isEmailError = checkEmailError(email);
    // let isUsernameLengthError = checkLengthError(username, 3, 10);
    // let isPasswordLengthError = checkLengthError(password, 3, 10);
    // let isMatchError = checkMatchPasswordError(password, confirmPassword);
    
    // if(isEmptyError || isEmailError || isUsernameLengthError || isPasswordLengthError || isMatchError){
    //     // do nothing
    // }
    // else{
    //     // check account from DB 
    // }
    
    let isValid = checkValidate();

    if(isValid){
        alert("Log in")
        
    }

})