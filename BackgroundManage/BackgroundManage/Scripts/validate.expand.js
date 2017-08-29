//if (!$("#TelPhone").val().match(/^(((13[0-9]{1})|159|153)+\d{8})$/)) {
//    $("#TelPhone").html("<font>手机号码格式不正确！请重新输入！</font>");
//    $("#TelPhone").focus();
//    return false;
//} else {
//    return true;
//}
//if (!$("#OfficePhone").val().match(/^((0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/)) {
//    $("#OfficePhone").html("<font>bangong格式不正确！请重新输入！</font>");
//    $("#OfficePhone").focus();
//    return false;
//} else {
//    return true;
//}

jQuery.validator.addMethod("isTelPhone", function (value, element) {
    var tel = /^((13[0-9])|(15[^4,\D])|(18[0,5-9]))\d{8}$/;
    return this.optional(element) || (tel.test(value));
}, "请正确填写您的手机号码");

jQuery.validator.addMethod("isOfficePhone", function (value, element) {
    var tel = /^((0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/;
    return this.optional(element) || (tel.test(value));
}, "请正确填写您的办公电话");