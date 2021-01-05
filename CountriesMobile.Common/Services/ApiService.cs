using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CountriesMobile.Common.Models;

namespace CountriesMobile.Common.Services
{
    public class ApiService : IApiService
    {
        /// <summary>
        /// Task that converts the API response into a JSON format and then a .Net Type object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlBase"></param>
        /// <param name="servicePrefix"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public async Task<Response> GetListAsync<T>(
        string urlBase,
        string servicePrefix,
        string controller)
        {
            try
            {
                // Creates a new client with a given url
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                string url = $"{servicePrefix}{controller}";

                // Gets the response from the API
                HttpResponseMessage response = await client.GetAsync(url);

                // Serializes the response content into a string
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                // Defines custom settings for the JSON Serializer
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore//Handle nulls coming from api
                };

                // Deserializes the JSON into a list of strings
                var list = JsonConvert.DeserializeObject<List<T>>(result, jsonSettings);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

    }
}
