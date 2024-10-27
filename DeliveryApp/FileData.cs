using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp
{
    public static class FileData
    {
        public static List<Order>? ReadData(string? pathFile)
        {
            List<Order> orders = [];
            Log.Information($"Чтение данных из файла: {pathFile}");

            if (!File.Exists(pathFile))
            {
                Console.WriteLine("Неверный путь к файлу с заказами.");
                Log.Error($"Файл не прочитан, неверный путь к файлу: {pathFile}");
                return null;
            }
            string[] orderString = File.ReadAllLines(pathFile);
            foreach (string order in orderString)
            {
                string[] strings = order.Split(new char[] { ',', ';' });
                bool flag1 = int.TryParse(strings[0].Trim(), out int id);
                bool flag2 = int.TryParse(strings[1].Trim(), out int weight);
                string district = strings[2];
                bool flag3 = DateTime.TryParseExact(strings[3].Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);
                if (flag1 && flag2 && flag3) orders.Add(new Order(id, weight, district, dateTime));
                else Log.Warning($"Строка из файла не прочитана.");
            }
            return orders;
        }

        public static void WriteData(List<Order> orders, string? deliveryOrder)
        {
            Log.Information($"Запись результатов фильтрации в файл: {deliveryOrder}");
            string[] ordersArray = orders.Select(o => $"{o.Id}, {o.Weight}, {o.District}, {o.DeliveryTime}").ToArray();
            try
            {
                File.WriteAllLines(deliveryOrder, ordersArray);
            }
            catch(Exception ex){
                Log.Error($"Файл не записан, неверный путь: {ex.Message}.");
            }
        }
    }
}
