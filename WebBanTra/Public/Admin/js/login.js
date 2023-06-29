$(document).ready(function () {
    $('#btn-login').click(function (e) {
        e.preventDefault();
        $('.overlay').addClass('d-flex');
        const user = $('input[name=username]').val().trim();
        const pass = $('input[name=password]').val().trim();
        if (user == '' || pass == '') {
            alert("Vui lòng nhập đủ thông tin trước khi nhấn đăng nhập!");
            $('.overlay').removeClass('d-flex');
        } else {
            $.ajax({
                url: "Login",
                data: { User: user, Pass: pass },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.s == 1) {
                        alert("Tài khoản không chính xác!");
                        $('.overlay').removeClass('d-flex');
                    } else if (res.s == 2) {
                        alert("Mật khẩu không chính xác!");
                        $('.overlay').removeClass('d-flex');
                    } else {
                        window.location.href = "/Admin/Dashboard";
                    }
                }
            })
        }
    })
});