/*
 * @Author: your name
 * @Date: 2021-07-06 13:02:22
 * @LastEditTime: 2021-07-07 12:23:13
 * @LastEditors: Please set LastEditors
 * @Description: In User Settings Edit
 * @FilePath: \Logicore\ThreePoint.Web\wwwroot\js\department\index.js
 */

$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});

//表格初始化
var TableInit = function () {
    var columns = [];
    columns.push({ checkbox: true });
    columns.push({ field: 'id', title: 'Id', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'name', title: '名称', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'fullname', title: '全称', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'parentid', title: '父级Id', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'enable', title: '是否启用', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'createtime', title: '创建日期', align: 'center', valign: 'middle', width: 120 });

    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_departments').bootstrapTable({
            url: '/Department/GetDepartmentForTable',     //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "desc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索
            strictSearch: false,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: columns
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            Name: $("#searchName").val(),
            ParentId: $("#searchParentId").val(),
            IsEnabled: $("#IsEnable").val() == 0 ? "" : $("#IsEnable").val() > 0 ? true : false,
        };
        return temp;
    };
    return oTableInit;
};

//toolbar按钮操作
var ButtonInit = function () {
    var oInit = new Object();

    oInit.Init = function () {
        //添加按钮事件
        $("#btn_add").click(function () {
            $("#addModal").load("/Department/Add", function (responseTxt, statusTxt, xhr) {
                $('#addModal').modal('show');
            });
            return false;
        });

        //修改按钮事件
        $("#btn_edit").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            //console.log(arrselections[0].id);
            if (arrselections.length > 1) {
                toastr.warning('只能选择一行进行编辑');
                return;
            }
            if (arrselections.length <= 0) {
                toastr.warning('请选择要编辑的数据');
                return;
            }

            $("#editModal").load("/Department/Edit/" + arrselections[0].id, function (responseTxt, statusTxt, xhr) {
                $('#editModal').modal('show');
            });
            return false;
        });

        //删除按钮事件
        $("#btn_delete").click(function () {
            var arrselections = $("#tb_departments").bootstrapTable('getSelections');
            //console.log(arrselections);
            var ids = [];
            if (arrselections.length <= 0) {
                toastr.warning('请选择要删除的数据!');
                return;
            }
            else {
                arrselections.forEach(element => {
                    ids.push(element.id);
                    //console.log(ids);
                });
            }
            BootstrapDialog.confirm({
                title: '确认',
                message: '是否要删除选中数据？',
                type: BootstrapDialog.TYPE_WARNING, // <-- Default value is
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
                    // 点击确定按钮时，result为true
                    if (result) {
                        //console.log(ids);
                        // 执行方法
                        $.ajax({
                            type: "post",
                            url: "/Department/Delete",
                            data: { ids: ids },
                            success: function (data, status) {
                                if (data.status == true) {
                                    BootstrapDialog.show({
                                        type: BootstrapDialog.TYPE_SUCCESS,  //BootstrapDialog.TYPE_DANGER, BootstrapDialog.TYPE_WARNING
                                        title: '成功 ',
                                        message: "删除数据成功",
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
                                    $("#tb_departments").bootstrapTable('refresh');
                                }
                                else {
                                    BootstrapDialog.show({
                                        type: BootstrapDialog.TYPE_WARNING,  //BootstrapDialog.TYPE_DANGER, BootstrapDialog.TYPE_WARNING
                                        title: '失败 ',
                                        message: "删除数据失败",
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
                                toastr.error('执行删除操作失败');
                            },
                            complete: function () {

                            }
                        });
                    }
                    else {
                        console.log("你取消了删除操作!");
                    }
                }
            });
            return false;
        });

        //$("#btn_query").click(function () {
        //    $("#tb_departments").bootstrapTable('refresh');
        //});
    };

    return oInit;
};

//监听模态框关闭事件开始
$("#addModal").on("hidden.bs.modal", function () {
    // 这个#showModal是模态框的id
    $(this).removeData("bs.modal");
    $(this).find(".modal-content").children().remove();
});

$("#editModal").on("hidden.bs.modal", function () {
    // 这个#showModal是模态框的id
    $(this).removeData("bs.modal");
    $(this).find(".modal-content").children().remove();
});
//监听模态框关闭事件结束

//查询按钮
$("#btn_query").click(function () {
    //刷新bootstraptable
    $("#tb_departments").bootstrapTable('refresh');
    return false;
});