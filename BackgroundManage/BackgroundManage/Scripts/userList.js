//角色选择Id
var checkId = "";
//当前用户Id
var forTreeId = 0;
$(document).ready(function () {
    DataTable();
   // GetAdminTree();
});
//加载用户列表
function DataTable(datas) {
    $("#usertable").dataTable({
        "columns": [
            { "data": "" },
            { "data": "Id" },
            { "data": "Account" },
            { "data": "RealName" },
            { "data": "Sex" },
            { "data": "TelPhone" },
            { "data": "OfficePhone" },
            { "data": "Email" },
            { "data": "Enable" },
            { "data": "Founder" },
            { "data": "FoundTime" },
            { "data": "ModifyPerson" },
            { "data": "ModifyTime" }
        ],
        "bSort": true,
        "columnDefs": [
            {
                "targets": [0],
                "data": "Id",
                "render": function (data, type, full) {
                    return "<a class='update'data-toggle='modal' href='javascript:void(0)' onclick='UpdateUser(" + full.Id + ")'>编辑</a>|<a class='delete' href='javascript:void(0)' onclick='Delete(" + full.Id + ")' >删除</a>";
                }
            },
            { "bVisible": false, "aTargets": [1,4, 8, 9,10,11,12] },
            { "bSortable": false, "aTargets": [0,3, 5, 6, 7] }
        ],
        "destory": true,
        "searching":false,
        "bProcessing": false,
        "bServerSide": true,
        "sAjaxSource": "/User/GetUserList",
        "sDom": "Tflt<'row DTTTFooter'<'col-sm-6'i><'col-sm-6'p>>",
        drawCallback: function(backSettings) {
        },
        "fnServerData": function(sSource, aoData, fnCallback) {
            var data = {};
            for (var i = 0; i < aoData.length; i++) {
                if (aoData[i].name === "iDisplayStart") {
                    data.PageIndex = aoData[i].value;
                }
                if (aoData[i].name === "iDisplayLength") {
                    data.PageSize = aoData[i].value;
                }
                if (aoData[i].name === "sEcho") {
                    data.Draw = aoData[i].value;
                }
                if (aoData[i].name === "sSearch") {
                    data.Search = aoData[i].value;
                }
                if (aoData[i].name === "iSortCol_0") {
                    data.SortCol = aoData[i].value;
                }
                if (aoData[i].name === "sSortDir_0") {
                    data.SortDir = aoData[i].value;
                }
            }
            if (datas !== null && datas !== undefined) {
                data.Acount = datas.acount;
                data.Rname = datas.rname;
                data.Mail = datas.mail;
            }
            $.ajax({
                "type": "post",
                "url": sSource,
                "data": { "pageInfo": data },
                "success": function(resp) {
                    fnCallback(resp);
                }
            });
        },
        oLanguage: {
            "sProcessing": "正在加载中......",
            "sLengthMenu": "每页显示 _MENU_ 条记录",
            "sZeroRecords": "对不起，查询不到相关数据！",
            "sEmptyTable": "表中无数据存在！",
            "sInfo": "当前显示 _START_ 到 _END_ 条，共 _TOTAL_ 条记录",
            "sInfoFiltered": "数据表中共为 _MAX_ 条记录",
            "sSearchPlaceholder": " 查找", //搜索框内占位符
            "sSearch": "", //搜索框前的字体
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上一页",
                "sNext": "下一页",
                "sLast": "末页"
            }
        }
    });
}

function ShowModal() {
    $("input[type=reset]").trigger("click");
    $("#Id").val("0");
    $("#myUsersLabel").text("新增");
    $('#myUsers').modal({
        backdrop: 'static',
        scrollY: true,
        keyboard: false,
        width: 1100,
        maxHeight: 600
    });
    forTreeId = "0";
    $("#AdminTree").jstree("destroy");
    GetAdminTree();
}

//获取单个用户编辑信息   
function UpdateUser(id) {
    $("#myUsersLabel").text("编辑");
    $('#myUsers').modal({
        backdrop: 'static',
        scrollY: true,
        keyboard: false,
        width: 1100,
        maxHeight: 600
    });
    forTreeId = id;
        $.ajax({
            url: "User/GetSingleUser",
            data: { "id": id },
            type: "post",
            success: function (data) {
                $("#myUsersLabel").text("编辑");
                $('#myUsers').modal();
                $("#AdminTree").jstree("destroy");
                GetAdminTree();
                //$("#AdminTree").jstree(true).refresh();
                $("#Account").val(data.Account);
                $("#RealName").val(data.RealName);
                $("#Enable").val(data.Enable);
                $("#TelPhone").val(data.TelPhone);
                $("#OfficePhone").val(data.OfficePhone);
                $("#Email").val(data.Email);
                $("#Id").val(id);
            }
    });
}
//保存用户信息（编辑、新增）
function SaveUser() {
    var isEdit;
    if ($("#Id").val() !== "0" && $("#Id").val() !== null) {
        isEdit = true;
    } else {
        isEdit = false;
    }
    var nodes = $("#AdminTree").jstree("get_checked"); //使用get_checked方法 
    checkId = nodes.toString();
    AddValidate(isEdit);
    //将form表单数据转换为json格式
    var datas = {};
    var dataCollect = $("#user-form").serializeArray();
    $.each(dataCollect, function () {
        if (datas[this.name] !== undefined) {
            if (!datas[this.name].push) {
                datas[this.name] = [datas[this.name]];
            }
            datas[this.name].push(this.value || "");
        } else {
            datas[this.name] = this.value || "";
        }
    });
    if (AddValidate(isEdit).form()) {//检查是否通过表单验证，通过提交表单
        $.ajax({
            url: "/User/SaveUser",
            type: "post",
            data: { "user": datas, "checkId": checkId },
            success: function (data) {
                alert(JSON.stringify(data));
                if (data === "保存成功") {
                    $("input[type=reset]").trigger("click");
                    $("#Id").val("0");
                    $("#myUsers").modal("hide");
                    checkId = "";
                    forTreeId = "";
                    $('#usertable').dataTable().fnDestroy();
                    DataTable();
                }
            }
        });
    }
}
//查询用户信息
function Search() {
    var acount = $("#Acount").val();
    var rname = $("#Rname").val();
    var mail = $("#Mail").val();
    var data = {};
    data.acount = acount;
    data.rname = rname;
    data.mail = mail;
    $('#usertable').dataTable().fnDestroy();
    DataTable(data);
    }
//删除用户信息
function Delete(data) {
    var confim = confirm("确认要删除？");
    if (confim === true) {
        $.ajax({
            "type": "post",
            "url": "/User/DeteleUser",
            "data": { "id": data },
            "success": function (data) {
                alert(data);
                $('#usertable').dataTable().fnDestroy();//等同于在DataTable中设置属性"destroy": true,
                DataTable();
            }
        });
    } else {
        return false;
    }
    return false;
}
//验证数据
function AddValidate(isEdit) {
   return $("#user-form").validate({
        rules: {
            RealName: {
                maxlength: 50
            },
            TelPhone: {
                required: true,
                isTelPhone:true
            },
            Email: {
                required: true,
                email: true
            },
            OfficePhone: {
                isOfficePhone: true,
                maxlength:11
            },
            Account: {
                required: true,
                maxlength: 8,
                //异步验证
                remote: {
                    url: "/User/CheckAccount", //后台数据处理地址
                    type: "post", //数据发送的方式
                    data: { //要传递的数据
                        account: function () {
                            if (!isEdit) {
                                return { "account": $("#Account").val(), "id": $("#Id").val() } ;
                            }
                        }
                    },
                    dataType: "json"
                }
            }
        },
        messages: {
            RealName: "请输入用户真实姓名",
            Email: {
                required: "请输入邮箱",
                email: "请正确填写邮箱"
            },
            TelPhone: {
                required: "请输入手机号码"
            },
            OfficePhone: {
                maxlength:"长度不能超过11"
        },
        Account: {
                required: "请输入用户名",
                maxlength: "用户名不能超过8个字",
                remote: "用户名不可用"
            }
        }
    });
}
//管理员拥有的角色选择
function GetAdminTree() {
    $("#AdminTree").jstree({
        "core": {
            "data": {
                "url": "/User/GetAdminTree",
                "datatype": "json",
                "data": function (node) {
                    var pdata = { "id": forTreeId };
                    return pdata;
                },
                "success": function (data) {
                    //alert(JSON.stringify(data));
                }
            }
        },
        "plugins": ["types","checkbox"],
        "types": {
            "default": {
                "icon": "jstree-node-online" //glyphicon glyphicon-folder-close
            }
        },
        "checkbox": { "keep_selected_style": false }
    })
        .on("loaded.jstree", function (e, data) {
            $("#AdminTree").jstree("open_all");
    });    
}