using Serilog;
using System.Globalization;

namespace DeliveryApp
{
    public static class FilterData
    {
        public static List<Order>? FilterOrder(List<Order> orders, string? cityDistrict, string? firstDeliveryDateTime)
        {
            Log.Information($"Фильтрация данных с параметрами cityDistrict = {cityDistrict}, firstDeliveryDateTime = {firstDeliveryDateTime}");
            bool flag = DateTime.TryParseExact(firstDeliveryDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);
            DateTime addDateTime = dateTime.AddMinutes(30);
            if (flag == false || String.IsNullOrEmpty(cityDistrict) || String.IsNullOrWhiteSpace(cityDistrict)) {
                Log.Error("Ошибка фильтрации: неправильно заданы параметры.");
                return null;
            }
            List<Order>  answer = orders.Where(o => (o.District == cityDistrict) &&
                                     o.DeliveryTime >= dateTime &&
                                     o.DeliveryTime <= addDateTime)
                                .OrderByDescending(o => o.DeliveryTime)
                                .ToList();
            return answer;
        }
       
    }
}
