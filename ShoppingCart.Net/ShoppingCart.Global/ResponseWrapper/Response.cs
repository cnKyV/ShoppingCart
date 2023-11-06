using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingCart.Global.CustomJsonConverters;

namespace ShoppingCart.Global.ResponseWrapper
{
    public class Response<T> 
    {
        public bool Result { get; set; }
        [JsonConverter(typeof(ResponseJsonConverter))]
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{{Result: {Result}, Message: {Message}}}";
        }
    }
    public class Response
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        
        public override string ToString()
        {
            return $"{{Result: {Result}, Message: {Message}}}";
        }
    }
}
