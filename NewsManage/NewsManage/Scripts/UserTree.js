$(document).ready(function () {
    GetTree();
});
function GetTree() {
    $("#myTree").jstree({
        "core": {
            "multiple": false,
            "animation": 0,
            "check_callback": true,
            "themes": {},
            "data": {
                "url": "/User/GetDeptTree",
                "datatype": "json",
                "data": function(node) {
                    return { "id": node.id };
                },
                "success": function() {
                }
            }
        },
        "plugins": ["contextmenu", "dnd", "search", "state", "types", "wholerow"],
        "types": {
            "default": {
                "icon": "glyphicon glyphicon-folder-close"
            }
        }
    }).bind("select_node.jstree",
        function(event, data) {
            var inst = data.instance;
            var selectedNode = inst.get_node(data.selected);
            GetUserList(selectedNode);
        });
}

function GetUserList(departId) {
    $("#newstable").dataTable({
        "columns": [
            { "data": "UserId" },
            { "data": "Account" },
            { "data": "RealName" },
            { "data": "Role" },
            { "data": "DepartId" },
            { "data": "DeptName" }
        ],
        "bSort": true,
        "columnDefs": [
            //{
            //    "targets": [4],
            //    "data": "NewsId",
            //    "render": function (data, type, full) {
            //        return "<a class='update' href='/News/UpdateNews?id=" + data + "'>编辑</a>|<a class='delete' href='javascript:void(0)' onclick='Delete(" + data + ")' >删除</a>";
            //    }
            //},
            { "bSortable": false, "aTargets": [1, 2, 3] }
        ],
        "destroy": true,
        "bProcessing": false,
        "bServerSide": true,
        "sAjaxSource": "/User/GetUerList",//url地址
        "sDom": "Tflt<'row DTTTFooter'<'col-sm-6'i><'col-sm-6'p>>",
        drawCallback: function (backSettings) {
        },
        "fnServerData": function (sSource, aoData, fnCallback) {
            var data = {};
            //alert(JSON.stringify(aoData));
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
            // alert(JSON.stringify(data));
            $.ajax({
                "type": "post",
                "url": sSource,
                "data": { "pageInfo": data, "deptid": departId },
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

