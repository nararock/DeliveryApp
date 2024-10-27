// See https://aka.ms/new-console-template for more information

using DeliveryApp;
using System.Configuration;
using Serilog;


//Параметры из App.config
var pathFile = ConfigurationManager.AppSettings["PathFile"];
var deliveryOrder = ConfigurationManager.AppSettings["DeliveryOrder"];
var cityDistrict = ConfigurationManager.AppSettings["CityDistrict"];
var firstDeliveryDateTime = ConfigurationManager.AppSettings["FirstDeliveryDateTime"];
var deliveryLog = ConfigurationManager.AppSettings["DeliveryLog"];

//Параметры из командной строки
for(int i = 0; args.Length > i; i++)
{
    if (args[i] == "_cityDistrict") cityDistrict = args[i];
    else if (args[i] == "_firstDeliveryDateTime") firstDeliveryDateTime = args[i];  
    else if (args[i] == "_deliveryLog") deliveryLog = args[i];
    else if (args[i] == "_deliveryOrder") deliveryOrder = args[i];
}

//Логирование
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(deliveryLog,
                rollingInterval: RollingInterval.Day)
            .CreateLogger();

Log.Information("Пуск");

List<Order>? orders = FileData.ReadData(pathFile);
if (orders == null)
{
    return;
} else if (orders.Count == 0)
{
    Log.Information("Файл не содержит данных.");
    return;
}
List<Order>? answer = FilterData.FilterOrder(orders, cityDistrict, firstDeliveryDateTime);
if (answer == null)
{
    return;
}
FileData.WriteData(answer, deliveryOrder);

Log.Information("Стоп");
