using System.Threading.Tasks;

namespace Vira.Network.Interfaces
{
    /// <summary>
    /// Интерфейс для службы HTTP клиента
    /// </summary>
    public interface IHttpClientService
    {
        /// <summary>
        /// Выполнить POST запрос
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        Task<string> PostAsync(string url, string jsonContent);
    }
}