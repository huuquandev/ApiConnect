using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace GatewayApi.BLL
{
    public class CConfigAG
    {
        public readonly string HOST_DATA_SERVICE;
        public readonly string URL_LOGIN;
        public readonly string URL_GET_ALL_PRODUCT;
        public readonly string URL_GET_PRODUCT_BY_ID;
        public readonly string URL_ADD_PRODUCT;

        public CConfigAG(IConfiguration _configuration)
        {   
            HOST_DATA_SERVICE = _configuration["URL:HOST_DATA_SERVICE"];
            URL_LOGIN = _configuration["URL:URL_LOGIN"];
            URL_GET_ALL_PRODUCT = _configuration["URL:URL_GET_ALL_PRODUCT"];
            URL_GET_PRODUCT_BY_ID = _configuration["URL:URL_GET_PRODUCT_BY_ID"];
            URL_ADD_PRODUCT = _configuration["URL:URL_ADD_PRODUCT"];
        }
    }
}
 