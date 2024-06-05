// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system, system-ui, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

// Pie Chart Example
var ctx = document.getElementById("myPieChart").getContext('2d');
var myPieChart = new Chart(ctx, {
    type: 'pie',
    data: {
        labels: ["Clientes", "Ventas", "Productos"],
        datasets: [{
            data: [12.21, 15.58, 11.25],
            backgroundColor: ['#007bff', '#dc3545', '#ffc107'],
        }],
    },
});

// AJAX request to update dashboard values
$.ajax({
    url: '@Url.Action("VistaDashBoard", "Home")',
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    success: function (data) {
        var objeto = data.resultado;
        $("#totalcliente").text(objeto.TotalCliente);
        $("#totalventa").text(objeto.TotalVenta);
        $("#totalproducto").text(objeto.TotalProducto);
    },
    error: function (xhr, status, error) {
        console.error("AJAX error:", status, error);
    }
});