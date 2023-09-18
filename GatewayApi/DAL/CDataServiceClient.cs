using GatewayApi.BLL;
using Microsoft.Extensions.Configuration;
using ProjectApiFpts.Models;
using Newtonsoft.Json ;
using System.Text;

namespace GatewayApi.DAL
{
    public class CDataServiceClient
    {
        private readonly IConfiguration _configuration;

        public CDataServiceClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<ProductModel> api_get_all_product()
        {
            CConfigAG config = new CConfigAG(_configuration);
            try
            {
                string URL = $"{config.HOST_DATA_SERVICE}/{config.URL_GET_ALL_PRODUCT}";
                Task<string> tsk = (new HttpClient()).GetStringAsync(URL);
                string strReponse = tsk.Result;
                List<ProductModel> products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(strReponse);
                return products;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public ProductModel api_get_product_by_id(string id)
        {
            CConfigAG config = new CConfigAG(_configuration);
            try
            {
                string URL = $"{config.HOST_DATA_SERVICE}/{config.URL_GET_PRODUCT_BY_ID.Replace("{Id}", id).Replace("{productId}", id)}";
                Task<string> tsk = (new HttpClient()).GetStringAsync(URL);
                string strReponse = tsk.Result;
                ProductModel product = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(strReponse);
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ProductModel api_add_product(ProductModel productModel)
        {
            CConfigAG config = new CConfigAG(_configuration);
            try
            {
                string URL = $"{config.HOST_DATA_SERVICE}/{config.URL_ADD_PRODUCT}";
                string jsonContent = JsonConvert.SerializeObject(productModel);

                // Tạo một yêu cầu HTTP POST
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(URL, content).Result;

                string strReponse = response.Content.ReadAsStringAsync().Result;

                // Chuyển đổi dữ liệu thành đối tượng ProductModel
                ProductModel product = JsonConvert.DeserializeObject<ProductModel>(strReponse);

                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
