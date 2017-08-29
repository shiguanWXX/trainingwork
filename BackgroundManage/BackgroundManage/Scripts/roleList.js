var forTreeId = "0";
var forTreeIdM = "0";
var checkAId = "";
var checkMId = "";
$(document).ready(function () {
    //GetAdminTree();
    RoleDataTable();
});
//加载角色列表
function RoleDataTable(roleName) {
    $("#roletable").dataTable({
        "columns": [
            { "data": "" },
            { "data": "Id" },
            { "data": "RName" },
            { "data": "Code" },
            { "data": "Description" }
        ],
        "bSort": true,
        "columnDefs": [
            {
                "targets": [0],
                "data": "",
                "render": function (data, type, full) {
                    return "<a class='update'data-toggle='modal' href='javascript:void(0)' onclick='ShowModal(" + full.Id + ")'>编辑</a>|<a class='delete' href='javascript:void(0)' onclick='Delete(" + full.Id + ")' >删除</a>";
                }
            },
            { "bVisible": false, "aTargets": [1] },
            { "bSortable": false, "aTargets": [1,3,4] }
        ],
        "destory": true,
        "searching": false,
        "bProcessing": false,
        "bServerSide": true,
        "sAjaxSource": "/Role/RoleList",
        "sDom": "Tflt<'row DTTTFooter'<'col-sm-6'i><'col-sm-6'p>>",
        drawCallback: function (backSettings) {
        },
        "fnServerData": function (sSource, aoData, fnCallback) {
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
            $.ajax({
                "type": "post",
                "url": sSource,
                "data": { "pageInfo": data, "roleName": roleName },
                "success": function (resp) {
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
//搜索方法
function Search() {
    var roleName = $("#RoleName").val();
    $('#roletable').dataTable().fnDestroy();
    RoleDataTable(roleName);
}
//显示模态框
function ShowModal(id) {
    forTreeId = id;
    var url = "/Role/GetRolePartial";
    $("#myRole").load(url,
        { "id": id },
        function () {
            if (id === null || id === "" || id===undefined) {
                $("#myRoleLabel").text("新增");
                $("#Id").val("0");
            } else {
                $("#myRoleLabel").text("编辑");
            }
            $("#myRole").modal();
            GetModuleTree();
            GetAdminTree();
            // $("#AdminTrees").jstree(true).refresh();
        });
}
//保存角色信息
function SaveRole() {
    var isEdit;
    if ($("#Id").val() !== "0" && $("#Id").val() !== null) {
        isEdit = true;
    } else {
        isEdit = false;
    }
    var nodeA = $("#AdminTrees").jstree("get_checked"); //使用get_checked方法 
    checkAId = nodeA.toString();
    var nodeM = $("#ModuleTree").jstree("get_checked"); //使用get_checked方法 
    checkMId = nodeM.toString();
    //将form表单数据转换为json格式
    var datas = {};
    var dataCollect = $("#role-form").serializeArray();
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
    if (AddValidate(isEdit).form()) {
        $.ajax({
            url: "/Role/SaveRole",
            type: "post",
            data: { "role": datas, "checkAId": checkAId, "checkMId": checkMId},
            success: function (data) {
                alert(JSON.stringify(data));
                if (data === "保存成功") {
                    $("input[type=reset]").trigger("click");
                    $("#Id").val("0");
                    $("#myRole").modal("hide");
                    checkAId = "";
                    checkMId = "";
                    $('#roletable').dataTable().fnDestroy();
                    RoleDataTable();
                }
            }
        });
    }
}
//删除角色信息
function Delete(id) {
    var confim = confirm("是否删除");
    if (confim === true) {
        $.ajax({
            url: "/Role/DeleteRole",
            type: "post",  
            data: { "id": id },
            success: function(data) {
                alert(data);
                $("#roletable").dataTable().fnDestroy();
                RoleDataTable();
            }
        });
    }
}
//数据验证
function AddValidate(isEdit) {
    return $("#role-form").validate({
        rules: {
            Code: {
                maxlength: 50
            },
            Description: {
                maxlength: 600
            },
            RName: {
                required: true,
                maxlength: 50,
                //异步验证
                remote: {
                    url: "/Role/CheckRoleName", //后台数据处理地址
                    type: "post", //数据发送的方式
                    data: { //要传递的数据
                        account: function () {
                            if (!isEdit) {
                                return { "roleName": $("#RName").val(), "id": $("#Id").val() } ;
                            }
                        }
                    },
                    dataType: "json"
                }
            }
        },
        messages: {
            Code: "编码的长度不能超过80",
            Description: "描述的长度不能超过600",
            RName: {
                required: "请输入角色名",
                maxlength: "角色名的长度不能超过50",
                remote: "角色名不可用"
            }
        }
    });
}
//管理员权限树
function GetAdminTree() {
    $("#AdminTrees").jstree({
        "core": {
            //"multiple": false,
            "animation": 0,
            "check_callback": true,
            "themes": { "themes": "default", "dots": true, "icon": false },
            "data": {
                "url": "/Role/GetAdminTree",
                "datatype": "json",
                "data": function (node) {
                    return { "id": forTreeId };
                },
                "success": function (data) {
                    //forTreeId = "";
                    //alert(JSON.stringify(data));
                }
            }
        },
        "plugins": ["contextmenu", "dnd", "search", "types", "wholerow", "checkbox"],
        "types": {
            "default": {
                "icon": "jstree-node-online" //glyphicon glyphicon-folder-close
            }
        },
        "checkbox": { "keep_selected_style": false }
    });
}
//模块权限树
function GetModuleTree() {
    $("#ModuleTree").jstree({
        "core": {
            "animation": 0,
            "check_callback": true,
            "themes": { "themes": "default", "dots": true, "icon": false },
            "data": {
                "url": "/Role/GetModuleTrees",
                "datatype": "json",
                "data": function(node) {
                    return { "id": forTreeId === undefined ? "0" : forTreeId }
                },
                "success": function(data) {
                    //alert(JSON.stringify(data));
                }
            }
        },
        "plugins": ["types", "checkbox"],
        "types": {
            "default": {
                "icon": "jstree-node-online"
            }
        },
        "checkbox": { "keep_selected_style": false }
    })
   .on("loaded.jstree", function (e, data) {
        $("#ModuleTree").jstree("open_all");
    });    
}