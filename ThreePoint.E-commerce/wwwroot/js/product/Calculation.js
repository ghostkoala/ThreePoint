//加载上次数据
if (typeof (Storage) !== "undefined") {
    $("#ExchangeRate").val(localStorage.getItem("ExchangeRate"));
    $("#ReturnAdvertisementPoint").val(localStorage.getItem("ReturnAdvertisementPoint"));

} else {
    alert("你的浏览器不支持本地存储数据");
}

// 利润计算按钮事件
function calcu() {
    var PurchasePrice = $("#PurchasePrice");
    if (PurchasePrice.val() == null || PurchasePrice.val() == "") {
        alert("请输入采购价！");
        PurchasePrice.focus();
        return false;
    }
    var Pcs = $("#Pcs");
    if (Pcs.val() == null || Pcs.val() == "") {
        alert("请输入货品数量！");
        Pcs.focus();
        return false;
    }
    var SellingPrice = $("#SellingPrice");
    if (SellingPrice.val() == null || SellingPrice.val() == "") {
        alert("请输入销售价！");
        SellingPrice.focus();
        return false;
    }
    var ExchangeRate = $("#ExchangeRate");
    if (ExchangeRate.val() == null || ExchangeRate.val() == "") {
        alert("请输入汇率！");
        ExchangeRate.focus();
        return false;
    }
    var SalesCommission = $("#SalesCommission");
    if (SalesCommission.val() == null || SalesCommission.val() == "") {
        alert("请输入销售佣金！");
        ExchangeRate.focus();
        return false;
    }
    var FBAshipping = $("#FBAshipping");
    if (FBAshipping.val() == null || FBAshipping.val() == "") {
        alert("请输入FBA费用！");
        FBAshipping.focus();
        return false;
    }
    var ReturnAdvertisementPoint = $("#ReturnAdvertisementPoint");
    if (ReturnAdvertisementPoint.val() == null || ReturnAdvertisementPoint.val() == "") {
        alert("请输入退货+广告费用率！");
        ReturnAdvertisementPoint.focus();
        return false;
    }
    var FreightCost = $("#FreightCost");
    if (FreightCost.val() == null || FreightCost.val() == "") {
        alert("请输入头程运费！");
        ReturnAdvertisementPoint.focus();
        return false;
    }

    //console.log(SellingPrice);
    var Profit = (SellingPrice.val() * (1 - ReturnAdvertisementPoint.val() / 100) - SalesCommission.val() - FBAshipping.val()) * ExchangeRate.val() - PurchasePrice.val() - FreightCost.val()
    var totalProfit = Pcs.val() * Profit;
    var Profitrate = parseInt((Profit / (SellingPrice.val() * ExchangeRate.val())) * 100);

    $("#profit").val(Profit.toFixed(2));
    $("#totalprofit").val(totalProfit.toFixed(2));
    $("#profitrate").val(Profitrate);

    localStorage.setItem("ExchangeRate", ExchangeRate.val());
    localStorage.setItem("ReturnAdvertisementPoint", ReturnAdvertisementPoint.val());
}

// 自动计算佣金
$("#SellingPrice").keyup(function () {
    var p = $("#SellingPrice").val() * 0.15;
    $("#SalesCommission").val(p.toFixed(2));
});

// 选择货币事件
$("#SellingCurrency").change(function () {
    var s = $(this).find("option:selected").text();
    $("#sc1").text(s);
    $("#sc2").text(s);
    $("#sc3").text(s);
});

// 广告+退货金额事件
$("#ReturnAdvertisementPoint").keyup(function () {
    s = $("#SellingPrice").val();
    if (s > 0) {
        p = s * $(this).val() / 100;
        $("#returnAdvLabel").text(p);
    }
    else {
        $("#returnAdvLabel").text("");
    }
    return true;
});

// 销售价格事件
$("#SellingPrice").keyup(function () {
    s = $("#ReturnAdvertisementPoint").val();
    if (s > 0) {
        p = s * $(this).val() / 100;
        $("#returnAdvLabel").text(p.toFixed(2));
    }
    else {
        $("#returnAdvLabel").text("");
    }
    return true;
});

// 重量事件
$("#Size_Weight").keyup(function () {
    var p = $("#Size_Weight").val();
    var t = $("#transport").val();

    // 国内运费计算
    if (t == 1) {
        $("#FreightCost").val(90 / 5000 * p);
    }
    else {
        $("#FreightCost").val(20 / 6000 * p);
    }

    // FBA计算
    if (p <= 170) {
        s = 2.7;
    }
    else if (p > 170 && p <= 340) {
        s = 2.84;
    }
    else if (p > 340 && p <= 453) {
        s = 3.32;
    }
    else {
        s = 0;
    }
    $("#FBAshipping").val(s.toFixed(2));
});



