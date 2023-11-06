using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShoppingCart.Global.ResponseWrapper
{
    public static class ResponseWrapper
    {
        public static Response Success(string message) => new()
        {
            Message = message,
            Result = true
        };

        public static Response<T> SuccessWithData<T>(T? message) => new()
        {
            Result = true,
            Message = JsonConvert.SerializeObject(message)
        };        
        public static Response<T> SuccessWithData<T>(string message) => new()
        {
            Result = true,
            Message = message
        };

        public static Response Error(string message) => new()
        {
            Result = false,
            Message = message
        };        
        
        public static Response Error(Exception ex) => new()
        {
            Result = false,
            Message = ex.Message
        };        
        public static Response<T> Error<T>(string message) => new()
        {
            Result = false,
            Message = message
        };        
        
        public static Response<T> Error<T>(Exception ex) => new()
        {
            Result = false,
            Message = ex.Message
        };
    }
}
