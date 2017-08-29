$(document).ready(function () {
    GetDicTree();
    DataTableDic();
    DataTableDetail();
});
//获取字典树
var dataDicId = "";
function GetDicTree() {
        $("#myTree").jstree({
                "core": {
                    "multiple": false,
                    "animation": 0,
                    "check_callback": true,
                    "themes": { "themes": "default", "dots": true, "icon": false},
                    "data": {
                        "url":"/DataDic/GetDicTree",
                        "datatype": "json",
                        "data": function (node) {
                            return { "id": "#" };
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
                    dataDicId = selectedNode.id;
                    $('#detailtable').dataTable().fnDestroy();
                    $('#dictable').dataTable().fnDestroy();
                    DataTableDic(selectedNode.id);
                    DataTableDetail(selectedNode.id);
                });
}
//加载数据字典列表
function DataTableDic(dicId) {
    $("#dictable").dataTable({
        "columns": [
            { "data": "" },
            { "data": "Id" },
            { "data": "DChName" },
            { "data": "DEnName" },
            { "data": "DReadonly" },
            { "data": "Description" }
        ],
        "bSort": false,
        "columnDefs": [
            {
                "targets": [0],
                "data": "",
                "render": function (data, type, full) {
                    return "<a class='update' data-toggle='modal' href='javascript:void(0)' onclick='ShowModal(" + full.Id + ")'>编辑</a>";
                }
            },
            { "bVisible": false, "aTargets": [1] }
        ],
        "destory": true,
        "searching": false,
        "bProcessing": false,
        "bServerSide": true,
        "sAjaxSource": "/DataDic/GetDicList",
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
                "data": { "pageInfo": data, "dicId": dicId },
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
//加载数据字典详细
function DataTableDetail(dicId) {
    $("#detailtable").dataTable({
        "columns": [
            { "data": "" },
            { "data": "Id" },
            { "data": "DeChName" },
            { "data": "DeEnName" },
            { "data": "Sort" },
            { "data": "Enable" },
            { "data": "RealName"},
            { "data": "ModifyPerson" },
            { "data": "ModifyTime" },
            { "data": "Description" },
            { "data": "DId" }
        ],
        "bSort": false,
        "columnDefs": [
            {
                "targets": [0],
                "data": "",
                "render": function (data, type, full) {
                    return "<a class='update' href='javascript:void(0)' onclick='ShowModalDetail(" + full.Id + ")'>编辑</a>";
                }
            },
            { "bVisible": false, "aTargets": [1,7,10] }
        ],
        "destory": true,
        "searching": false,
        "bProcessing": false,
        "bServerSide": true,
        "sAjaxSource": "/DataDic/GetDicDetailList",
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
                "data": { "pageInfo": data, "dicId": dicId },
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

//字典详细操作
//显示模态框（新增、编辑）
function ShowModalDetail(id) {
    if (id === undefined || id === "" || id === null) {
        if (dataDicId === "" || dataDicId === null) {
            alert("请选择字典");
            return false;
        }
    }
    var url = "/DataDic/GetPartialModal";
    $("#myDetail").load(url,
        { "id": id},
        function () {
            if (id !== null && id !== "" && id !== undefined) {
                $("#myDetailLabel").text("编辑");
            } else {
                $("#myDetailLabel").text("新增");
                $("#Id").val("0");
            }
            $("#DId").val(dataDicId);
            $('#myDetail').modal();
        });
    return null;
}
//保存字典详细信息（编辑、新增）
function SaveDataDetail() {
    var id = $("#Id").val();
    alert(id);
    //将form表单数据转换为json格式
    var datas = {};
    var dataCollect = $("#detail-form").serializeArray();
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
    alert(JSON.stringify(datas));
    if (DetailValidate(id).form()) {
        $.ajax({
            url: "/DataDic/SaveDataDetail",
            type: "post",
            datatype:"json",
            data: $("form").serialize(),
            success: function (data) {
                alert(JSON.stringify(data));
                if (data === "保存成功") {
                    $("input[type=reset]").trigger("click");
                    $("#Id").val("0");
                    $("#myDetail").modal("hide");
                    $('#detailtable').dataTable().fnDestroy();
                    DataTableDetail(dataDicId);
                }
            }
        });
    }
}
//数据字典详细验证
function DetailValidate(id) {
    alert(id);

    return $("#detail-form").validate({
        rules: {
            DeChName: {
                required: true,
                maxlength: 50,
                //异步验证
                remote: {
                    url: "/DataDic/CheckDeChName", //后台数据处理地址
                    type: "post", //数据发送的方式
                    data: { //要传递的数据
                        account: function () {
                            alert(888);
                            alert(id);
                                return { "deChName": $("#DeChName").val(),"id": id}
                        }
                    },
                    dataType: "json"
                }
            },
            DeEnName: {
                required: true,
                maxlength: 50,
                //异步验证
                remote: {
                    url: "/DataDic/CheckDeEnName", //后台数据处理地址
                    type: "post", //数据发送的方式
                    data: { //要传递的数据
                        account: function () {
                                return { "deEnName":$("#DeEnName").val(), "id": id} ;
                        }
                    },
                    dataType: "json"
                }
            },
            Sort: {
                required: true
            },
            Description: {
                maxlength:600
            }
        },
        messages: {
            DeChName: {
                required: "请输入中文名称",
                maxlength: "中文名称不能超过50个字",
                remote: "中文名称不可用"
            },
            DeEnName: {
                required: "请输入英文名称",
                maxlength: "英文名称不能超过50个字",
                remote: "英文名称不可用"
            },
            Sort: {
                required: "请输入排序"
            },
            Description: {
                maxlength: "描述长度不能超过600"
            }
        }
    });
}
//数据字典操作
//显示模态框（新增、编辑）
function ShowModal(id) {
    var url = "/DataDic/GetDicPartialModal";
    $("#myDic").load(url,
        { "id": id },
        function () {
            if (id !== null && id !== "" && id !== undefined) {
                $("#myDicLabel").text("编辑");
            } else {
                $("#myDicLabel").text("新增");
                $("#Id").val("0");
            }
            $('#myDic').modal();
        });
}
//保存字典信息（编辑、保存）
function SaveDataDic() {
    var isEdit = "";
    if ($("#Id").val() !== "0" && $("#Id").val() !== null) {
        isEdit = true;
    } else {
        isEdit = false;
    }
    if (DicValidate(isEdit).form()) {
        $.ajax({
            url: "/DataDic/SaveDataDic",
            type: "post",
            data: $("form").serialize(),
            success: function (data) {
                alert(JSON.stringify(data));
                if (data === "保存成功") {
                    $("input[type=reset]").trigger("click");
                    $("#Id").val("0");
                    $("#myDic").modal("hide");
                    if (!isEdit) {
                        //$("#myTree").data("jstree", false).empty();
                        //GetDicTree();
                        dataDicId=""
                        $('#dictable').dataTable().fnDestroy();
                        $('#detailtable').dataTable().fnDestroy();
                        DataTableDic();
                        DataTableDetail();
                        $("#myTree").jstree(true).refresh();
                    } else {
                        $('#dictable').dataTable().fnDestroy();
                        DataTableDic(dataDicId);
                    }
                }
            }
        });
    }
}
//数据字典验证
function DicValidate(isEdit) {
    return $("#dic-form").validate({
        rules: {
            DChName: {
                required: true,
                maxlength: 50,
                //异步验证
                remote: {
                    url: "/DataDic/CheckDChName", //后台数据处理地址
                    type: "post", //数据发送的方式
                    data: { //要传递的数据
                        account: function () {
                           return { "dChName": $("#DChName").val(), "id": $("#Id").val()}
                        }
                    },
                    dataType: "json"
                }
            },
            DEnName: {
                required: true,
                maxlength: 50,
                //异步验证
                remote: {
                    url: "/DataDic/CheckDEnName", //后台数据处理地址
                    type: "post", //数据发送的方式
                    data: { //要传递的数据
                        account: function () {
                            if (!isEdit) {
                                return { "dChName": $("#DEnName").val(), "id": $("#Id").val() } ;
                            } else {
                                return "";
                            }
                        }
                    },
                    dataType: "json"
                }
            },
            Description: {
                maxlength: 600
            }
        },
        messages: {
            DChName: {
                required: "请输入中文名称",
                maxlength: "中文名称不能超过50个字",
                remote: "中文名称不可用"
            },
            DEnName: {
                required: "请输入英文名称",
                maxlength: "英文名称不能超过50个字",
                remote: "英文名称不可用"
            },
            Description: {
                maxlength: "描述长度不能超过600"
            }
        }
    });
}