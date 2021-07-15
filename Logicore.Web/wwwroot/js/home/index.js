/*
 * @Author: your name
 * @Date: 2021-05-16 13:57:04
 * @LastEditTime: 2021-07-14 13:51:22
 * @LastEditors: Please set LastEditors
 * @Description: In User Settings Edit
 * @FilePath: \Logicore\Logicore.Web\wwwroot\js\home\index.js
 */

//tabs操作
var nthTabs;
$(function () {
    //一个低门槛的演示,更多需求看源码
    //基于bootstrap tab的自定义多标签的jquery实用插件
    nthTabs = $("#main-tabs").nthTabs();

    // 新建选项卡示例
    nthTabs.addTab({
        id: 'home',
        title: '首页',
        url: "/Home/Welcome",
        //content: '这是首页',
        active: true, // 是否激活状态，默认是
        allowClose: false, //是否可关闭，默认是
        location: true, //是否自动定位，默认是
        fadeIn: true //是否开启淡入淡出效果，默认是
    });
});

function newTabs(obj) {
    //openModel 1、text/html 2、Iframe打开
    //console.log(obj);
    //<a>标签数据连接
    url = obj.dataset.url;
    //<a>标签Index
    index = obj.dataset.id;
    //<a>标签内容
    title = obj.dataset.value;

    nthTabs.addTab({
        id: index,
        title: title,
        url: url,
        //content: content,
        active: true,
    });
}

//选项卡操作
//左侧导航栏操作
jQuery(document).ready(function () {
    jQuery("#jquery-accordion-menu").jqueryAccordionMenu();

});

//列表项背景颜色切换
$(function () {
    $("#demo-list li").click(function () {
        $("#demo-list li.active").removeClass("active")
        $(this).addClass("active");
    });
});

//列表样式初始化
(function ($) {
    $.expr[":"].Contains = function (a, i, m) {
        return (a.textContent || a.innerText || "").toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
    };
    function filterList(header, list) {
        //header 头部元素
        //list 无序列表
        //创建一个搜素表单
        var form = $("<form>").attr({
            "class": "filterform",
            action: "#"
        }), input = $("<input>").attr({
            "class": "filterinput",
            type: "text",
            placeholder: "search"
        });
        $(form).append(input).appendTo(header);
        $(input).change(function () {
            var filter = $(this).val();
            if (filter) {
                $matches = $(list).find("a:Contains(" + filter + ")").parent();
                $("li", list).not($matches).slideUp();
                $matches.slideDown();
            } else {
                $(list).find("li").slideDown();
            }
            return false;
        }).keyup(function () {
            $(this).change();
        });
    }
    $(function () {
        filterList($("#form"), $("#demo-list"));
    });
})(jQuery);

//监听模态框关闭事件开始
$("#editInfoModalLabel").on("hidden.bs.modal", function () {
    // 这个#showModal是模态框的id
    $(this).removeData("bs.modal");
    $(this).find(".modal-content").children().remove();
});

$("#editPasswordModalLabel").on("hidden.bs.modal", function () {
    // 这个#showModal是模态框的id
    $(this).removeData("bs.modal");
    $(this).find(".modal-content").children().remove();
});

$("#loginLogLabel").on("hidden.bs.modal", function () {
    // 这个#showModal是模态框的id
    $(this).removeData("bs.modal");
    $(this).find(".modal-content").children().remove();
});
//监听模态框关闭事件结束

//打开用户信息模态框
function openInfoModal() {
    $("#editInfoModal").load("/Admin/EditGeneralInfo", function (responseTxt, statusTxt, xhr) {
        $('#editInfoModal').modal('show');
    });
    return false;
};

//打开修改密码模态框
function openEditPasswordModal() {
    // 打开模态框
    $("#editPasswordModal").load("/Admin/EditPassword", function (responseTxt, statusTxt, xhr) {
        $('#editPasswordModal').modal('show');
    });
    return false;
};

//打开要读的信息
function readMessage() {

};