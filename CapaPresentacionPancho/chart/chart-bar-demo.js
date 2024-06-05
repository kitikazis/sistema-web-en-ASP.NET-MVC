$(document).ready(function () {
    $("#txtfechainicio").datepicker({ dateFormat: 'dd/mm/yy' }).datepicker('setDate', new Date());
    $("#txtfechafin").datepicker({ dateFormat: 'dd/mm/yy' }).datepicker('setDate', new Date());

    $("#btnbuscar").on("click", function () {
        // Get the date range and transaction ID
        var fechaInicio = $("#txtfechainicio").val();
        var fechaFin = $("#txtfechafin").val();
        var idTransaccion = $("#txtidtransaccion").val();

        // Construct the URL with the date range and transaction ID
        var Url = '@Url.Action("ListaReporte", "Home")' +
            "?fechainicio=" + fechaInicio +
            "&fechafin=" + fechaFin +
            "&idtransaccion=" + idTransaccion;

        // Make an AJAX call to get the sales data
        $.ajax({
            url: Url,
            type: "GET",
            dataType: "json",
            success: function (data) {
                // Aggregate total sales for each month
                var monthlySales = Array(12).fill(0); // Initialize array to hold monthly sales totals
                data.forEach(function (sale) {
                    var month = new Date(sale.FechaVenta).getMonth(); // Get month (0-indexed)
                    monthlySales[month] += sale.Total; // Add sale total to corresponding month
                });

                // Update DataTable with the aggregated sales data
                tabladata.clear().draw();
                data.forEach(function (sale) {
                    tabladata.row.add([
                        sale.FechaVenta,
                        sale.Cliente,
                        sale.Producto,
                        sale.Precio,
                        sale.Cantidad,
                        sale.Total,
                        sale.IdTransaccion
                    ]).draw();
                });

                // Update the chart with the aggregated monthly sales data
                updateBarChart(monthlySales);
            }
        });
    });

    // Function to update the bar chart with monthly sales data
    function updateBarChart(monthlySales) {
        // Bar Chart Example
        var ctx = document.getElementById("myBarChart");
        var myBarChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                datasets: [{
                    label: "Total Ventas",
                    backgroundColor: "rgba(2,117,216,1)",
                    borderColor: "rgba(2,117,216,1)",
                    data: monthlySales, // Update with monthly sales data
                }],
            },
            options: {
                scales: {
                    xAxes: [{
                        time: {
                            unit: 'month'
                        },
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            maxTicksLimit: 12 // Adjust max ticks to 12 for twelve months
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: Math.max(...monthlySales) + 5000, // Adjust max value dynamically
                            maxTicksLimit: 5
                        },
                        gridLines: {
                            display: true
                        }
                    }],
                },
                legend: {
                    display: false
                }
            }
        });
    }

    // Initialize DataTable
    tabladata = $("#tabla").DataTable({
        responsive: true,
        ordering: false,
        "columns": [
            { "data": "FechaVenta" },
            { "data": "Cliente" },
            { "data": "Producto" },
            { "data": "Precio" },
            { "data": "Cantidad" },
            { "data": "Total" },
            { "data": "IdTransaccion" }
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.3/i18n/es_es.json"
        }
    });
});
