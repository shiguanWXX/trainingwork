$(function() {
    $("#login-form").validate({
        rules: {
            Account: {
                required: true,
                maxlength: 8
            },
            Password: "required"
        },
        messages: {
            Account: {
                required: "请输入用户名",
                maxlength: "用户名不能超过8个字符"
            },
            Password: "请输入密码"

        }
    });
})