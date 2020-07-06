$(document).ready(function () {
    $("#adminName").text($("#custno").val());
    setupEnterToNext();
});

function setupEnterToNext() {

    // add keydown event for all inputs
    $(':input').keydown(function (e) {

        if (e.keyCode == 13 /*Enter*/) {

            // focus next input elements
            $(':input:visible:enabled:eq(' + ($(':input:visible:enabled').index(this) + 1) + ')').focus();
            e.preventDefault();
        }

    });
}


function changpage(folder, page) {
    document.location.href = "../" + folder + "/" + page + ".aspx";
    document.cookie = "page_=0";
    //window.open("../" + folder + "/" + page + ".aspx");data_ser
}

function cookingClaer() {
    //document.cookie = "page_=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
    document.cookie = "page_=0";
}



var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};


function openNav() {
    document.getElementById("sidebarMenu").style.width = "250px";
    //document.getElementById("adminMain").style.marginLeft = "250px";
    //document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
}

function closeNav() {
    document.getElementById("sidebarMenu").style.width = "0";
    //document.getElementById("adminMain").style.marginLeft = "0";
    //document.body.style.backgroundColor = "white";
}


function loading(type) {
    if (type) {
        $("#loading_div").css("display", "block");
    }
    else {
        $("#loading_div").css("display", "none");
    }
}

function chenull(str) {
    if (str == null) {
        return "";
    }
    else {
        return str;
    }
}

function chenull_zero(str) {
    if (str == null) {
        return "0";
    }
    else {
        return str;
    }
}

//數值千分位
var thousandComma = function (number) {
    var num = "";
    if (number != null) {
        num = number.toString();
        var pattern = /(-?\d+)(\d{3})/;

        while (pattern.test(num)) {
            num = num.replace(pattern, "$1,$2");

        }
    }

    return num;

}

//字串全部取代
function ReplaceAll(strSource, strFind, strRepl) {
    var str5 = new String(strSource);
    while (str5.indexOf(strFind) != -1) {
        str5 = str5.replace(strFind, strRepl);
    }
    return str5;
}

function data_ser(file, id) {
    $("#ContentPlaceHolder1_ser").click();
}

function data_ins(file, cmd) {
    $.ajax({
        type: 'post',
        url: "../../ashx/" + file + "/" + file + "_ins.ashx",
        data: {
            custno: $("#custno").val(),
            cmd: cmd
        },
        success: function (data) {
            if (data != null && data.length > 0) {
                if (data == "OK") {
                    swal({
                        title: "儲存成功"
                    }, function () {
                        document.location.href = file + ".aspx";
                    });
                } else {
                    swal("儲存失敗");
                }
            }
        },
        error: function (data) {
            load_close();
        }
    });
}

function data_del(file,listid) {
   

        swal({
            title: "確認刪除!?",
            showCancelButton: true,
            confirmButtonColor: '#d33',
            confirmButtonText: "刪除",
            cancelButtonText: "取消"
        },
        function (value) {
            if (value) {
                $("#ContentPlaceHolder1_del").click();
            }
        });
    

}

function save() {
    var type = getUrlParameter("type");
    //if (type == "edit") {
    //    data_edit('update', file);
    //} else {
    //    data_edit('ins', file);
    //}

    $("#ContentPlaceHolder1_save").click();
}

function edit() {
    $("#ContentPlaceHolder1_ins").click();
}

function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

function paddingLeft(str, lenght) {
    if (str.length >= lenght)
        return str;
    else
        return paddingLeft("0" + str, lenght);
}
function paddingRight(str, lenght) {
    if (str.length >= lenght)
        return str;
    else
        return paddingRight(str + "0", lenght);
}


function Set_Table(file, id) {

    var ca = document.cookie.split(';');
    var page = ca[0].split('=');

    var oTable;
    if (file == "Employee") {
        console.log("1");
        oTable = $("#ContentPlaceHolder1_GridView1").dataTable();
    }
    else {
        console.log("2");
        oTable = $("#ContentPlaceHolder1_GridView1").dataTable({
            "scrollX": true
        });
    }
    

    try {

        //總頁數
        //console.log("1:");
        var oSettings = oTable.fnSettings();
        //console.log("2:");
        var paging_length = oSettings._iDisplayLength;//当前每页显示多少 
        //console.log("3:");
        var SSpage = Math.ceil(oTable.fnGetData().length / paging_length);//oTable.fnGetData().length
        //console.log("4:");
        if (page[1] != "NaN" && page[1] != null) {
            //console.log("SSpage:" + SSpage);
            //console.log("page:" + (parseInt(page[1]) + parseInt(1)));
            if (SSpage < (parseInt(page[1]) + parseInt(1))) {
                //console.log("1-1");
                oTable.fnPageChange(parseInt(page[1] - 1));
            }
            else {
                //console.log("cooking");
                if (!isNaN(page[1])) {
                    //console.log("int:" + page[1]);
                    oTable.fnPageChange(parseInt(page[1]));
                }
                else {
                    //console.log("no_int");
                    oTable.fnPageChange(0);
                }
            }

        }
        else {
            //console.log("頁數給一");
            oTable.fnPageChange(0);
        }
    }
    catch (e) {
        oTable.fnPageChange(0);
        //console.log("錯誤訊息:" + e);
    }
    $("#ContentPlaceHolder1_GridView1_filter").css("display", "none");
}