/*
 * @Author: your name
 * @Date: 2021-08-17 16:39:35
 * @LastEditTime: 2021-08-19 16:52:23
 * @LastEditors: Please set LastEditors
 * @Description: In User Settings Edit
 * @FilePath: \Logicore\Logicore.Web\wwwroot\js\ServerException\index.js
 */

$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

    $('#date_start').datetimepicker({
        format: 'yyyy-mm-dd',
        weekStart: 1,
        language: 'cn',
        minView: 'month',
        autoclose: true,
        todayBtn: true,
        endDate: new Date(),
    }).on("changeDate", function (ev) {
        if (ev.date) {
            $('#date_end').datetimepicker('setStartDate', new Date(ev.date.valueOf()))
        } else {
            $('#date_end').datetimepicker('setStartDate',);
        }
    });

    $('#date_end').datetimepicker({
        format: 'yyyy-mm-dd',
        weekStart: 1,
        language: 'cn',
        minView: 'month',
        autoclose: true,
        endDate: new Date(),
    }).on("changeDate", function (ev) {
        if (ev.date) {
            $('#date_start').datetimepicker('setEndDate', new Date(ev.date.valueOf()))
        } else {
            $('#date_start').datetimepicker('setEndDate', new Date());
        }
    });
});

//表格初始化
var TableInit = function () {
    var columns = [];
    columns.push({ checkbox: true });
    columns.push({ field: 'id', title: 'Id', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'code', title: '错误代码', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'url', title: 'Url', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'method', title: '类型', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'errMessage', title: '错误信息', align: 'center', valign: 'middle', width: 120 });
    columns.push({ field: 'createdatetime', title: '创建时间', align: 'center', valign: 'middle', width: 120 });

    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_ServerException').bootstrapTable({
            url: '/ServerException/GetServerExceptionForTable',     //请求后台的URL（*）
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

            return false;
        });

        //$("#btn_query").click(function () {
        //    $("#tb_ServerException").bootstrapTable('refresh');
        //});
    };

    return oInit;
};

//查询按钮事件
$("#btn_query").click(function () {
    //刷新bootstraptable
    console.log("refresh");
    $("#tb_ServerException").bootstrapTable('refresh');
    return false;
});