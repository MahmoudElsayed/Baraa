
function onUserChange() {
    var treeview = $("#treeview").data("kendoTreeView");
    treeview.dataSource.read();
}function onRoleChange() {
    var treeview = $("#treeview").data("kendoTreeView");
    treeview.dataSource.read();
}

function additionalInfo() {
    return {
        roleId: $("#RoleID").val(),
        UserId: $("#UserId").val(),
    }
}

function DisplayUserMessage(data) {
    toastr[data.ResultType](data.Message);
}

// function that gathers IDs of checked nodes
function checkedNodeIds(nodes, checkedNodes) {
    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].checked) {
            checkedNodes.push(nodes[i].id);
        }

        if (nodes[i].hasChildren) {
            checkedNodeIds(nodes[i].children.view(), checkedNodes);
        }
    }
}

// show checked node IDs on datasource change
function onCheck(e) {
    var checkedNodes = [], message, treeView = $("#treeview").data("kendoTreeView");

    SetModelCheckedItems(treeView, checkedNodes, message);
}

function SetModelCheckedItems(treeView, checkedNodes, message) {
    checkedNodeIds(treeView.dataSource.view(), checkedNodes);

    if (checkedNodes.length > 0) {
        message = checkedNodes.join(",");
    } else {
        message = "";
    }

    $("#CheckedItems").val(message);
}

function OnDataBound(e) {
    $(".remove-btn").removeClass('k-button');
    treeView = $("#treeview").data("kendoTreeView");

    treeView.expand(".k-item");

    var checkedNodes = [], message;
    SetModelCheckedItems(treeView, checkedNodes, message);

    setTimeout(function () {
        treeView.collapse(".k-item");
    }, 150);
}