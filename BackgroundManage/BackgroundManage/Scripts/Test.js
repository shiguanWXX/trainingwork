$(function(){
    $("#using_json").jstree({
        "core": {
            "data": ["Empty Folder", {
                    "id":1,
                    "text": "Resources",
                    "state": {
                        "opened": true
                    }
                },
                "Fonts", "Images", "Scripts", "Templates", ]
        },
        "checkbox" : {
            "keep_selected_style" : false
        },
        "plugins" : [ "wholerow", "checkbox" ]
    })
});
$("#modeltree").jstree("check_node", function () { alert($("#" + obj.nodeid).attr("class")); $("#" + obj.nodeid).removeClass("jstree-unchecked jstree-undetermined").addClass("jstree-checked"); }); } }); })
.jstree({
    "json_data": {
        "ajax": {
            "async": false,//设置是否异步,此处设置为同步 
            "url": "tree_data.jsp", "data": function (n) {//传递参数 
                return { "id": n.attr ? n.attr("id") : 0 }
            }
        }
    }, "plugins": ["themes", "json_data", "checkbox", "ui"]
}).bind("click.jstree", function (event) { var eventNodeName = event.target.nodeName; }); 
    //var obj = document.getElementByIdx_x("modeltree"); //alert(obj.innerHTML);
    //将内容复制到剪贴板中 //window.clipboardData.setData("Text", obj.innerHTML); });

Query(function () {
    jQuery('#jstree').jstree({
        "core": {
            "check_callback": true,
        },
        "plugins": ['contextmenu', "dnd", 'sort', "state"],
        contextmenu: { items: context_menu }
    });

    jQuery('#jstree').jstree().open_all();//展开所有节点
});

{
    "id":0,
        "text":"全选",
        "state":
    {
        "opened":
        false, "disabled":
        false, "selected":
        false
    },
    "children":[
        {
            "id": 2,
            "text": "管理员",
            "state": {
                "opened": false,
                "disabled": false,
                "selected": false
            },
            "children": null
        },
        {
            "id": 3,
            "text": "财务人员",
            "state": { "opened": false, "disabled": false, "selected": false },
            "children": null
        },
        {
            "id": 4,
            "text": "报事报修派单人员",
            "state": { "opened": false, "disabled": false, "selected": false },
            "children": null
        },
        {
            "id": 5,
            "text": "停车场管理员",
            "state": { "opened": false, "disabled": false, "selected": false },
            "children": null
        },
        {
            "id": 6,
            "text": "停车场客户端收费员",
            "state": { "opened": false, "disabled": false, "selected": false },
            "children": null
        },
        {
            "id": 7,
            "text": "停车场-物业",
            "state": { "opened": false, "disabled": false, "selected": false },
            "children": null
        },
        {
            "id": 8,
            "text": "维修人员",
            "state": { "opened": false, "disabled": false, "selected": false },
            "children": null
        }
    ]
}