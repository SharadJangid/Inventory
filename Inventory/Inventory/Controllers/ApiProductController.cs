using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class ApiProductController : ApiController
    {
        [HttpPost]
        [Route("api/apiproduct/getproduct")]
        public IEnumerable<dynamic> getproduct([FromBody] Products _objpro)
        {
            // get the list of the responses for the supplied parameters
                
            List<dynamic> _listProd = new List<dynamic>();
            _objpro.GetApiProductData(ref _listProd, _objpro);
            return _listProd;
        }

        [HttpPost]
        [Route("api/apiproduct/addupdateproduct")]
        public IEnumerable<string> addupdateproduct([FromBody] Products _objpro)
        {           
            List<string> ProductId = new List<string>();
            _objpro.AddEditDeleteProductData(ref ProductId, _objpro);
            return ProductId;
        }

        [HttpPost]
        [Route("api/apiproduct/deleteproduct")]
        public IEnumerable<string> deleteproduct([FromBody] Products _objpro)
        {
            
            List<string> ProductId = new List<string>();
            _objpro.AddEditDeleteProductData(ref ProductId, _objpro);
            return ProductId;
        }
    }
}
