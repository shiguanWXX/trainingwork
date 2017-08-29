$(document).ready(function() {
    GetModuleTree();
    ModuleDataTable();
});
//角色选择Id
var checkId = "";
//当前模块Id
var forTreeId = "0";
var fId = "";
//加载模块树
function GetModuleTree() {
    $("#moduleTree")
        .jstree({
            "core": {
                "multiple": false,
                "animation": 0,
                "check_callback": true,
                "themes": { "themes": "default", "dots": true, "icon": false },
                "data": {
                    "url": "/Module/GetModuleTree",
                    "datatype": "json",
                    "data": function (node) {
                        return { "id": node.id };
                    },
                    "success": function () {
                    }
                }
            },
            "plugins": ["contextmenu", "dnd", "search", "state", "types", "wholerow"],
            "types": {
                "default": {
                    "icon": "jstree-node-online" //glyphicon glyphicon-folder-close
                }
            }
        })
        .bind("select_node.jstree",
            function (event, data) {
                var inst = data.instance;
                var selectedNode = inst.get_node(data.selected);
                fId = selectedNode.id;
                $('#moduleTable').dataTable().fnDestroy();
                ModuleDataTable(selectedNode.id);
        });
   // $("#moduleTree").jstree().open_all();
}
//加载模块列表
function ModuleDataTable(mId) {
    $("#moduleTable").dataTable({
        "columns": [
            { "data": "" },
            { "data": "Id" },
            { "data": "MChName" },
            { "data": "MEnName" },
            { "data": "URL" },
            { "data": "Icon" },
            { "data": "MSort" },
            { "data": "Enable" },
            { "data": "Display" },
            { "data": "Description" },
            { "data": "FId" }
        ],
        "bSort": false,
        "columnDefs": [
            {
                "targets": [0],
                "data": "",
                "render": function (data, type, full) {
                    return "<a class='update'data-toggle='modal' href='javascript:void(0)' onclick='ShowModal(" + full.Id + ")'>编辑</a>";
                }
            },
            { "bVisible": false, "aTargets": [1,10] }
        ],
        "destory": true,
        "searching": false,
        "bProcessing": false,
        "bServerSide": true,
        "sAjaxSource": "/Module/ModuleList",
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
                "data": { "pageInfo": data, "mId": mId },
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
//显示模态框
function ShowModal(id) {
    //if (id === undefined || id === "" || id === null) {
    //    if (fId === "" || fId === null) {
    //        alert("请选择上级模块");
    //        return false;
    //    }
    //}
    forTreeId = id;
   
    var url = "/Module/GetModulePartial";
    $("#myModule").load(url,
        { "id": id },
        function() {
            if (id === null || id === "" || id === undefined) {
                $("#myModuleLabel").text("新增");
                if (fId !== "" && fId !== null && fId !== undefined) {
                    $("#FId").val(fId);
                } else {
                    $("#FId").val("0");
                }
                $("#Id").val("0");
            } else {
                $("#myModuleLabel").text("编辑");
            }
            $("#myModule").modal();
            GetRmoduleTree();
        });
}
//保存模块信息
function SaveModule() {
    var isEdit;
    if ($("#Id").val() !== "0" && $("#Id").val() !== null) {
        isEdit = true;
    } else {
        isEdit = false;
    }
    var nodes = $("#ModuleTree").jstree("get_checked"); //使用get_checked方法 
    checkId = nodes.toString();
    var datas = {};
    var dataCollect = $("#module-form").serializeArray();
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
   // alert(checkId);
    AddValidate(isEdit);
    if (AddValidate(isEdit).form()) {
        $.ajax({
            url: "/Module/SaveModule",
            type: "post",
            data: { "module": datas, "checkId": checkId},
            success: function (data) {
                alert(JSON.stringify(data));
                if (data === "保存成功") {
                    $("input[type=reset]").trigger("click");
                    $("#Id").val("0");
                    $("#myModule").modal("hide");
                    forTreeId = "";
                    checkId = "";
                    if (!isEdit) {
                        $("#moduleTree").jstree(true).refresh();
                        $('#moduleTable').dataTable().fnDestroy();
                        fId = "";
                        ModuleDataTable();

                    } else {
                        $('#moduleTable').dataTable().fnDestroy();
                        ModuleDataTable(fId);
                    }
                    
                }
            }
        });
    }
}
//验证数据
function AddValidate(isEdit) {
    return $("#module-form").validate({
        rules: {
            URL: {
                maxlength: 50
            },
            MSort: {
                required: true,
                maxlength: 20
            },
            Icon: {
                required: true,
                maxlength: 35 
            },
            Description: {
                maxlength: 600
            },
            MChName: {
                required: true,
                maxlength: 50,
                //异步验证
                remote: {
                    url: "/Module/CheckMChName", //后台数据处理地址
                    type: "post", //数据发送的方式
                    data: { //要传递的数据
                        account: function () {
                                return { "mChName": $("#MChName").val(),"id": $("#Id").val()};
                        }
                    },
                    dataType: "json"
                }
            },
            MEnName: {
                required: true,
                maxlength: 50,
                //异步验证
                remote: {
                    url: "/Module/CheckMEnName", //后台数据处理地址
                    type: "post", //数据发送的方式
                    data: { //要传递的数据
                        account: function () {
                                return { "mEnName": $("#MEnName").val(), "id": $("#Id").val() };
                        }
                    },
                    dataType: "json"
                }
            }
        },
        messages: {
            URL: "编码的长度不能超过50",
            MSort: {
                required: "请输入排序",
                maxlength: "排序的长度不能超过20"
            },
            Icon: {
                required: "请输入图标",
                maxlength: "图标的长度不能超过35" 
            },
            Description: "描述的长度不能超过600",
            MChName: {
                required: "请输入中文名称",
                maxlength: "中文名称的长度不能超过50",
                remote: "中文名称不可用"
            },
            MEnName: {
                required: "请输入英文名称",
                maxlength: "英文名称的长度不能超过50",
                remote: "英文名称不可用"
            }
        }
    });
}
//模块角色
function GetRmoduleTree() {
    //alert(forTreeId);
    $("#ModuleTree").jstree({
        "core": {
            //"multiple": false,
            "animation": 0,
            "check_callback": true,
            "themes": { "themes": "default", "dots": true, "icon": false },
            "data": {
                "url": "/Module/GetRModuleTree",
                "datatype": "json",
                "data": function (node) {
                    return { "id":forTreeId };
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