//菜单添加提交事件
$("#submit").click(function (obj) {
    //console.log(obj);
    var t;
    var url = obj.currentTarget.dataset.url;
    //验证表单
    if (!IsValid()) { return false; }
    var d = {};
    d["OldPassWord"] = $("#OldPassWord").val();
    d["NewPassWord"] = $("#NewPassWord").val();
    d["ConfirmPassword"] = $("#ConfirmPassword").val();
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
    //旧密码验证
    var name = $("#OldPassWord").val();
    var i = name.length;
    var nameValid = $("span[data-valmsg-for=OldPassWord]");
    //不为空
    if (name == "") {
        nameValid.text("旧密码不能为空");
        return false;
    }
    else {
        nameValid.text("");
    }
    //大于6位
    if (i < 6) {
        nameValid.text("旧密码应大于6个字符");
        return false;
    }
    else {
        nameValid.text("");
    }

    //新密码验证
    var name = $("#NewPassWord").val();
    var i = name.length;
    var nameValid = $("span[data-valmsg-for=NewPassWord]");
    //不为空
    if (name == "") {
        nameValid.text("新密码不能为空");
        return false;
    }
    else {
        nameValid.text("");
    }
    //大于6位
    if (i < 6) {
        nameValid.text("新密码应大于6个字符");
        return false;
    }
    else {
        nameValid.text("");
    }

    //确认密码验证
    var checkpassword = $("#ConfirmPassword").val();
    var nameValid = $("span[data-valmsg-for=ConfirmPassword]");
    //与新密码相同
    if (name != checkpassword) {
        nameValid.text("两次输入密码不相同");
        return false;
    }
    else {
        nameValid.text("");
    }
    
    //结束验证
    return true;
};