//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
    clearData();
});
//Load Data function
function loadData() {
    //create a data object for by default getting active record
    var empObj = {
        tb_pro_status: 1
    };
    $.ajax({
        url: "api/apiproduct/getproduct",
        data: JSON.stringify(empObj),
        type: "Post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            var count = 1;

            $.each(result, function (key, item) {

                var proId = "'" + item.Id + "'";
                var proName = "'" + item.tb_pro_name + "'";
                var proDes = "'" + item.tb_pro_des + "'";
                var proPrice = "'" + item.tb_pro_price + "'";

                html += '<tr>';
                html += '<td>' + count + '</td>';
                html += '<td><a href="#" onclick="getDatabyID(' + proId + ',' + proName + ',' + proDes + ',' + proPrice+')">Edit</a></td>';
                html += '<td><a href="#" onclick="DeleleProduct(' + proId + ')">Delete</a></td>';     
                html += '<td>' + item.tb_pro_name + '</td>';
                html += '<td>' + item.tb_pro_des + '</td>';
                html += '<td>' + item.tb_pro_price + '</td>';               
                html += '</tr>';
                count = count + 1;
            });
            $('#tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function
function SaveUpdateProduct() {

  //create a data object
    var empObj = {
        Id: $('#txt_pro_id').val(),
        tb_pro_name: $('#txt_tb_pro_name').val(),
        tb_pro_des: $('#txt_tb_pro_des').val(),
        tb_pro_price: $('#txt_tb_pro_price').val(),
        Mode: $('#txt_Mode').val()
    };
    $.ajax({
        url: "api/apiproduct/addupdateproduct",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            clearData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function clearData() {

    $('#txt_tb_pro_name').val("");
    $('#txt_tb_pro_des').val("");
    $('#txt_tb_pro_price').val("");
    $('#txt_pro_id').val("");
    $('#txt_Mode').val("I");
}
//Function for getting the Data Based upon Employee ID
function getDatabyID(ProId, ProName, ProDes, ProPrice) {

   
    $('#txt_pro_id').val(ProId);
    $('#txt_Mode').val("U");
    $('#txt_tb_pro_name').val(ProName);
    $('#txt_tb_pro_des').val(ProDes);
    $('#txt_tb_pro_price').val(ProPrice);

         
}

//function for deleting employee's record
function DeleleProduct(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    //create a data object
    var empObj = {
        Id: ID,      
        Mode: "D"
    };

    if (ans) {
        
        $.ajax({
            url: "api/apiproduct/deleteproduct",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
                clearData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
