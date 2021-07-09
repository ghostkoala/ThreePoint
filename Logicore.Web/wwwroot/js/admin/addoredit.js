/*
 * @Author: your name
 * @Date: 2021-05-16 13:57:04
 * @LastEditTime: 2021-06-14 11:04:08
 * @LastEditors: Please set LastEditors
 * @Description: In User Settings Edit
 * @FilePath: \Logicore\Logicore.Web\wwwroot\js\menu\add.js
 */

//菜单添加提交事件
$("#submit").click(function (obj) {
    //console.log(obj);
    var t;
    var url = obj.currentTarget.dataset.url;
    //验证表单
    if (!IsValid()) { return false; }
    var d = {};
    d["Id"] = $("#Id").val();
    d["LoginName"] = $("#LoginName").val();
    d["RealName"] = $("#RealName").val();
    d["Email"] = $("#Email").val();
    d["Password"] = $("#Password").val();
    d["DepartmentId"] = $("#DepartmentId").val();
    d["RoleId"] = $("#RoleId").val();
    d["Enabled"] = $("#Enabled").prop("checked");
    //console.log(d);

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
                //console.log(url);
                $.ajax({
                    contentType: "application/x-www-form-urlencoded",
                    url: url,
                    data: d,
                    type: 'post',
                    success: function (data, status) {
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
                            $('#editModal').modal('hide');
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

//表单验证
function IsValid() {
    //LoginName验证
    var name = $("#LoginName").val();
    //console.log(name);
    var i = name.length;
    var nameValid = $("span[data-valmsg-for=LoginName]");
    //Name不为空
    if (name == "") {
        nameValid.text("登陆帐号不能为空");
        return false;
    }
    else {
        nameValid.text("");
    }
    //LoginName长度2-20
    if (i < 2 || i > 20) {
        nameValid.text("登陆帐号在2-20个字符");
        return false;
    }
    else {
        nameValid.text("");
    }

    //RealName验证
    var name = $("#RealName").val();
    var i = name.length;
    var nameValid = $("span[data-valmsg-for=RealName]");
    //Name不为空
    if (name == "") {
        nameValid.text("真实姓名不能为空");
        return false;
    }
    else {
        nameValid.text("");
    }
    //RealName长度2-20
    if (i < 2 || i > 20) {
        nameValid.text("真实姓名在2-20个字符");
        return false;
    }
    else {
        nameValid.text("");
    }

    //email验证
    var email = $("#Email").val();
    var emailValid = $("span[data-valmsg-for=Email]");
    if (email != "") {
        var reg1 = /([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)/;
        if (reg1.test(email) == false) {
            emailValid.text("Email格式不对");
            return false;
        }
        else {
            emailValid.text("");
        }
    }


    //结束验证
    return true;
};