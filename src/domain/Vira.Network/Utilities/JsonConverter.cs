using Newtonsoft.Json;

namespace Vira.Network.Utilities
{
    /// <summary>
    /// Класс для сериализации и десериализации JSON
    /// </summary>
    public static class JsonConverter
    {
        /// <summary>
        /// Сериализовать объект в JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Десериализовать JSON в объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}