/*
 * @Author: your name
 * @Date: 2021-05-16 13:57:04
 * @LastEditTime: 2021-05-27 11:44:26
 * @LastEditors: Please set LastEditors
 * @Description: In User Settings Edit
 * @FilePath: \Logicore\ThreePoint.Web\wwwroot\js\menu\add.js
 */

//菜单添加提交事件
$("#submit").click(function (obj) {
    //console.log(obj);
    var t;
    var url = obj.currentTarget.dataset.url;
    //验证表单
    if (!IsValid()) { return false; }
    var d = {};
    if (url.indexOf("add") > 0) {
        t = $("#addform").serializeArray();
    }
    else {
        t = $("#editform").serializeArray();
    }
    //t的值为[{name: "a1", value: "xx"},
    //{name: "a2", value: "xx"}...]
    $.each(t, function () {
        d[this.name] = this.value;
    });

    BootstrapDialog.confirm({
        title: '确认',
        message: '是否要添加此数据',
        type: BootstrapDialog.TYPE_SUCCESS, // <-- Default value is
        // BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is false，点击对话框以外的页面内容可关闭
        draggable: true, // <-- Default value is false，可拖拽
        btnCancelLabel: '取消', // <-- Default value is 'Cancel',
        btnOKLabel: '确定', // <-- Default value is 'OK',
        btnOKClass: 'btn-warning', // <-- If you didn't specify it, dialog type
        size: BootstrapDialog.SIZE_SMALL,
        // 对话框关闭的时候执行方法
        //onhide: funcclose,
        callback: function (result) {
            if (result) {
                console.log(url);
                $.ajax({
                    url: url,
                    data: d,
                    type: 'post',
                    success: function (data, status) {
                        //console.log(data);
                        //console.log(status);
                        if (data.status == true) {
                            BootstrapDialog.show({
                                type: BootstrapDialog.TYPE_SUCCESS,  //BootstrapDialog.TYPE_DANGER, BootstrapDialog.TYPE_WARNING
                                title: '成功 ',
                                message: "操作成功",
                                size: BootstrapDialog.SIZE_SMALL,
                                buttons: [{
                                    label: '确定',
                                    action: function (dialogItself) {
                                        dialogItself.close();
                                    }
                                }],
                                // 指定时间内可自动关闭
                                onshown: function (dialogRef) {
                                    setTimeout(function () {
                                        dialogRef.close();
                                    }, 2000);
                                },
                                // 对话框关闭时带入callback方法
                                //onhide: function () { }
                            });
                            $('#addModal').modal('hide');
                            // if (d.type = 1) {
                            //     $("#tb_menus").bootstrapTable('refresh');
                            // }                        
                        }
                        else {
                            BootstrapDialog.show({
                                type: BootstrapDialog.TYPE_WARNING,  //BootstrapDialog.TYPE_DANGER, BootstrapDialog.TYPE_WARNING
                                title: '失败 ',
                                message: data.message,
                                size: BootstrapDialog.SIZE_SMALL,
                                buttons: [{
                                    label: '确定',
                                    action: function (dialogItself) {
                                        dialogItself.close();
                                    }
                                }],
                                // 指定时间内可自动关闭
                                onshown: function (dialogRef) {
                                    setTimeout(function () {
                                        dialogRef.close();
                                    }, 2000);
                                },
                                // 对话框关闭时带入callback方法
                                //onhide: function () { }
                            });
                        }
                    },
                    error: function () {
                        toastr.error('执行添加操作失败');
                    },
                    complete: function () {
                    }
                });
            }
        }
    });
});

//菜单下拉框点击事件
$("#btn_dropDownMenu").click(function () {
    var type = $("#Type").val();
    if (type <= 1 || type >= 4) { return false; } //无效数值
    //console.log(type);
    $("#list_dropDownMenu").load("/Menu/DropDownMenuSeach", { type: type });
});

//菜单下拉框选中事件
function selectMenu(obj) {
    var id = obj.dataset.id;
    // var name = $(obj).text();
    var name = obj.dataset.name;
    $("#ParentName").val(name);
    $("#ParentId").val(id);
};

//图标选择
$("#btn_selectIcons").click(function () {
    $("#selectIconModal").load("/Menu/FontGlyphicons", function (responseTxt, statusTxt, xhr) {
        $('#selectIconModal').modal('show');
    });
    return false;
});

//关闭图标模态框
function closeIconModal() {
    $("#selectIconModal").modal('hide');
    return false;
};

//表单验证
function IsValid() {
    //Name验证
    var name = $("#Name").val();
    var i = name.length;
    var nameValid = $("span[data-valmsg-for=Name]");
    //Name不为空
    if (name == "") {
        nameValid.text("菜单名不能为空");
        return false;
    }
    else {
        nameValid.text("");
    }
    //Name长度2-20
    if (i < 2 || i > 20) {
        nameValid.text("菜单名在2-20个字符");
        return false;
    }
    else {
        nameValid.text("");
    }

    //Url验证
    var url = $("#Url").val();
    i = url.length;
    var urlValid = $("span[data-valmsg-for=Url]");
    //Url不为空
    if (url == "") {
        urlValid.text("Url不能为空");
        return false;
    }
    else {
        urlValid.text("");
    }
    //Url少于300个字符
    if (i > 300) {
        urlValid.text("Url需少于300个字符");
        return false;
    }
    else {
        urlValid.text("");
    }

    //类型验证
    var type = $("#Type").val();
    console.log(type);
    var typeValid = $("span[data-valmsg-for=Type]");
    if (type == null) {
        typeValid.text("请选择类型");
        return false;
    }
    else {
        typeValid.text("");
    }
    if (type < 1 || type > 3) {
        typeValid.text("请选择正常的类型");
        return false;
    }
    else {
        typeValid.text("");
    }



    //结束验证
    return true;
};