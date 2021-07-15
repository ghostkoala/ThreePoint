/*
 * @Author: your name
 * @Date: 2021-05-13 14:09:31
 * @LastEditTime: 2021-07-15 17:24:16
 * @LastEditors: Please set LastEditors
 * @Description: In User Settings Edit
 * @FilePath: \Logicore\Logicore.Web\wwwroot\js\menu\index.js
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
    columns.push({ field: 'title', title: '标题', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'contents', title: '内容', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'readednumber', title: '已读人数', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'total', title: '总接收人数', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'createdatetime', title: '创建时间', align: 'center', valign: 'middle', width: 120 });

    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_messages').bootstrapTable({
            url: '/Message/GetMessageForTable',     //请求后台的URL（*）
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
            search: true,                       //是否显示表格搜索
            strictSearch: true,
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
            Keywords: params.search,  //搜索的字符
            Order: "desc", //排序方式 desc / asc
            SearchTitle: $("#SearchTitle").val(),
            SearchUserId: $("#SearchUserId").val(),
        };
        return temp;
    };
    return oTableInit;
};

//toolbar按钮操作
var ButtonInit = function () {
    var oInit = new Object();

    oInit.Init = function () {
        //修改按钮事件
        $("#btn_edit").click(function () {
            var arrselections = $("#tb_messages").bootstrapTable('getSelections');
            //console.log(arrselections[0].id);
            if (arrselections.length > 1) {
                toastr.warning('只能选择一行进行编辑');
                return;
            }
            if (arrselections.length <= 0) {
                toastr.warning('请选择要编辑的数据');
                return;
            }

            var obj = {
                dataset: {
                    url: "/Message/Edit/" + arrselections[0].id,
                    id: "EditMessageFrame" + arrselections[0].id,
                    value: "修改 " + arrselections[0].title + " 站内信"
                }
            };
            parent.window.newTabs(obj);
            return false;
        });

        //站内信详情事件
        $("#btn_detail").click(function () {
            var arrselections = $("#tb_messages").bootstrapTable('getSelections');
            //console.log(arrselections[0].id);
            if (arrselections.length > 1) {
                toastr.warning('只能选择一行进行查看');
                return;
            }
            if (arrselections.length <= 0) {
                toastr.warning('请选择要查看的信息');
                return;
            }

            var obj = {
                dataset: {
                    url: "/Message/GetMessageDetails/" + arrselections[0].id,
                    id: "ShowMessageDetail" + arrselections[0].id,
                    value: arrselections[0].title + " 详细信息"
                }
            };
            parent.window.newTabs(obj);
            return false;
        });

        //删除按钮事件
        $("#btn_delete").click(function () {
            var arrselections = $("#tb_messages").bootstrapTable('getSelections');
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
                            url: "/message/Delete",
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
                                    $("#tb_menus").bootstrapTable('refresh');
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

//查询按钮事件
$("#btn_query").click(function () {
    //刷新bootstraptable
    console.log("refresh");
    $("#tb_messages").bootstrapTable('refresh');
    return false;
});