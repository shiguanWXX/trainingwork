$(function () {
    $('#jstree_div').jstree({
        'core': {
            'check_callback': true,
            "data": function (obj, callback) {
                $.ajax({
                    url: "/Webs/Services",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        console.info(data);
                        if (data) {
                            callback.call(this, data);
                        } else {
                            $("#jstree_div").html("暂无数据！");
                        }
                    }
                });
            }
        },
        "plugins": ["sort"]
    }).bind("select_node.jstree", function (event, data) {
        var inst = data.instance;
        var selectedNode = inst.get_node(data.selected);
        //console.info(selectedNode.aria-level);  
        var level = $("#" + selectedNode.id).attr("aria-level");
        if (parseInt(level) <= 3) {
            loadConfig(inst, selectedNode);
        }
    });
});  
function loadConfig(inst, selectedNode) {

    var temp = selectedNode.text;
    //inst.open_node(selectedNode);  
    //alert(temp);  
    $.ajax({
        url: "/Webs/LoadConfig",
        dataType: "json",
        type: "POST",
        success: function (data) {
            if (data) {
                selectedNode.children = [];
                $.each(data, function (i, item) {
                    var obj = { text: item };
                    //$('#jstree_div').jstree('create_node', selectedNode, obj, 'last');  
                    inst.create_node(selectedNode, item, "last");
                });
                inst.open_node(selectedNode);
            } else {
                $("#jstree_div").html("暂无数据！");
            }
        }
    });
}  