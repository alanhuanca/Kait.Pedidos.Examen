$('#btnGuardar').click(function () {
    var btn = $(this);
    btn.button('loading');
    setTimeout(function () {
        btn.button('reset');
    }, 1000);
});  
 
$("#addToList").click(function (e) {
    e.preventDefault(); 

    if ($.trim($("#price").val()) == "") {
        swal({
            title: "Validación",
            text: "Ingrese un valor correcto para el precio de producto",
            type: "error",
            timer: 5000
        });
        return;
    }

    if ($.trim($("#quantity").val()) == "") {
        swal({
            title: "Validación",
            text: "Ingresee un valor correcto para la cantidad de producto",
            type: "error",
            timer: 5000
        });
        return;
    }

    var Descripcion = $("#idProducto option:selected").text(),
        price = $("#price").val(),
        quantity = $("#quantity").val(),
        detailsTableBody = $("#detailsTable tbody"),
        idProducto = $("#idProducto").val();

    var subtotal = (parseFloat(price) * parseFloat(quantity)).toFixed(2);

    var productItem = '<tr><td>' + idProducto + '</td><td>' + Descripcion + '</td><td>' + price + '</td><td>' + quantity + '</td><td>' + subtotal + '</td><td><a data-itemId="0" href="#" class="deleteItem">Quitar</a></td></tr>';
    detailsTableBody.append(productItem);
    clearItem();
    sumartotal();
});

function clearItem() {
    $("#price").val('0.0');
    $("#quantity").val('1');
}
 
$(document).on('click', 'a.deleteItem', function (e) {
    e.preventDefault();
    var $self = $(this);
    if ($(this).attr('data-itemId') == "0") {
        $(this).parents('tr').css("background-color", "#ff6347").fadeOut(800, function () {
            $(this).remove();
            sumartotal();
        });
    }
});


//After Click Save Button Pass All Data View To Controller For Save Database
function saveOrder(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: "/Pedido/SaveOrder",
        data: data,
        success: function (result) {
            if (result == 'Error') {
                swal({
                    title: "Validación",
                    text: "Error en el registro de datos",
                    type: "error",
                    timer: 5000
                }); 
            }
            else {
                swal({
                    title: "Conforme",
                    text: "Los datos se guardaron correctamente",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonClass: "btn-success",
                    confirmButtonText: "Cerrar",
                    closeOnConfirm: false,
                    timer: 3000
                },
                    function () {
                        window.location.href = '/Pedido/Index';
                    });
            }

        },
        error: function () {

            swal({
                title: "Validación",
                text: "Error en el registro de datos",
                type: "error",
                timer: 5000
            });
        }
    });
}
         
$("#btnGuardar").click(function (e) {
    e.preventDefault();

    var orderArr = [];
    orderArr.length = 0;

    $.each($("#detailsTable tbody tr"), function () {

        orderArr.push({
            idProducto: $(this).find('td:eq(0)').html(),
            DescripcionProducto: $(this).find('td:eq(1)').html(),
            PrecioUnitario: $(this).find('td:eq(2)').html(),
            Cantidad: $(this).find('td:eq(3)').html(),
            SubTotal: $(this).find('td:eq(4)').html()
        });
    });


    var data = JSON.stringify({
        idCliente: $("#idCliente").val(), 
        fechaOrden: $("#FechaOrden").val(),
        detalle: orderArr
    });

    $.when(saveOrder(data)).then(function (response) {
        console.log(response);
    }).fail(function (err) {
        console.log(err);
    });
});
  
function sumartotal() {
    var totalDeuda = 0.0;
    $.each($("#detailsTable tbody tr"), function () {
        totalDeuda = totalDeuda + parseFloat($(this).find('td:eq(4)').html());
    });
    $('#suma').val(totalDeuda.toFixed(2))
}
$(document).ready(function () {


    $('#FechaOrden').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        dateFormat: 'dd/mm/yy',
        language: 'es'
    });

});
   
 