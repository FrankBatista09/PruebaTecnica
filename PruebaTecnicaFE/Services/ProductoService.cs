using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PruebaTecnicaFE.Models;
using System.Net.Http.Headers;
using System.Text;

namespace PruebaTecnicaFE.Services
{
    public class ProductoService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ProductoService> _logger;

        public ProductoService(HttpClient client, ILogger<ProductoService> logger)
        {
            _client = client;
            _logger = logger;

            _client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
        }

        public async Task<List<ProductModel>> GetAll()
        {
            try
            {
                var response = await _client.GetAsync("producto");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<List<ProductModel>>(content);
                    return products;
                }
                return new List<ProductModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos");
                return new List<ProductModel>();
            }
        }

        public async Task<ProductModel> GetById(int id)
        {
            try
            {
                var response = await _client.GetAsync($"producto/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductModel>(content);
                    return product;
                }
                return new ProductModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el producto con ID {id}");
                return new ProductModel();
            }
        }

        public async Task<bool> Add(ProductModel productModel)
        {
            try
            {
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(productModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("producto", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar producto");
                return false;
            }
        }

        public async Task<bool> Update(ProductModel productModel)
        {
            try
            {
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(productModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"producto/{productModel.ProductoID}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el producto con ID {productModel.ProductoID}");
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"producto/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el producto con ID {id}");
                return false;
            }
        }
    }
}
