using CountriesMobile.Common.Models;
using System.Threading.Tasks;

namespace CountriesMobile.Common.Services
{
    public interface IApiService
    {
        /// <summary>
        /// Get a list of objects from API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlBase"></param>
        /// <param name="servicePrefix"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

    }
}
