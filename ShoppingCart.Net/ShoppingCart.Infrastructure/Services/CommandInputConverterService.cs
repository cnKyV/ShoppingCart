using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShoppingCart.Contract.Consts;
using ShoppingCart.Contract.Interfaces;
using ShoppingCart.Contract.RequestModels;
using ShoppingCart.Contract.ResponseModels;
using ShoppingCart.Core.Consts.ErrorMessages;
using ShoppingCart.Global.ResponseWrapper;


namespace ShoppingCart.Infrastructure.Services;

public static class CommandInputConverterService
{
    static CommandInputConverterService()
    {
        _cartService = new CartService();
    }

    private static readonly List<string> _commands = new()
    {
        "addItem",
        "addVasItemToItem",
        "removeItem",
        "resetCart",
        "displayCart"
    };

    private static readonly CartService _cartService;

    public static Response<CommandInput> Convert(string input)
    {
        using (JsonDocument doc = JsonDocument.Parse(input))
        {
            var root = doc.RootElement;

            if (root.TryGetProperty("command", out var commandProp) 
                && commandProp.ValueKind == JsonValueKind.String)
            {
                var command = commandProp.GetString();

                if (command is null)
                    return ResponseWrapper.Error<CommandInput>("");
                
                if (!_commands.Contains(command))
                    return ResponseWrapper.Error<CommandInput>("");

                Payload? payload;

                switch (command)
                {
                    case CommandInputTypeConsts.AddItem:
                        if (root.TryGetProperty("payload", out var addItemProp)
                            && addItemProp.ValueKind == JsonValueKind.Object)
                        {
                            payload = JsonConvert.DeserializeObject<ProductInsertRequestModel>(addItemProp.GetRawText());
                        }

                        payload = null;
                        
                        break;
                    
                    case CommandInputTypeConsts.DisplayCart:
                        payload = null;
                        break;
                    
                    case CommandInputTypeConsts.RemoveItem:
                        if (root.TryGetProperty("payload", out var removeItemProp)
                            && removeItemProp.ValueKind == JsonValueKind.Object)
                        {
                            payload = JsonConvert.DeserializeObject<ProductInsertRequestModel>(removeItemProp.GetRawText());
                        }

                        payload = null;
                        break;
                    
                    case CommandInputTypeConsts.ResetCart:
                        payload = null;
                        break;
                    
                    case CommandInputTypeConsts.AddVasItemToItem:
                        if (root.TryGetProperty("payload", out var addVasItemProp)
                            && addVasItemProp.ValueKind == JsonValueKind.Object)
                        {
                            payload = JsonConvert.DeserializeObject<ProductInsertRequestModel>(addVasItemProp.GetRawText());
                        }

                        payload = null;
                        break;
                    
                    default:
                        payload = null;
                        break;
                }

                var commandInput = new CommandInput
                {
                    Command = command,
                    Payload = payload
                };
                return ResponseWrapper.SuccessWithData(commandInput);
            }

        }
        
        return ResponseWrapper.Error<CommandInput>("");
    }    
    
    public static Response<DisplayCartResponseModel> ConvertAndProcess(string input)
    {
        
        var inputVal = CheckIfInputIsJson(input);

        if (!inputVal.Result)
        {
            return ResponseWrapper.Error<DisplayCartResponseModel>(inputVal.Message);
        }
        
        using (JsonDocument doc = JsonDocument.Parse(input))
        {
            var root = doc.RootElement;

            if (root.TryGetProperty("command", out var commandProp) 
                && commandProp.ValueKind == JsonValueKind.String)
            {
                var command = commandProp.GetString();

                if (command is null)
                    return ResponseWrapper.Error<DisplayCartResponseModel>(CommandInputConverterErrorConsts.CommandNotFound);
                
                if (!_commands.Contains(command))
                    return ResponseWrapper.Error<DisplayCartResponseModel>(CommandInputConverterErrorConsts.CommandNotRecognized);

                Payload? payload;
                Response<DisplayCartResponseModel> response = ResponseWrapper.Error<DisplayCartResponseModel>("Default");
                switch (command)
                {
                    case CommandInputTypeConsts.AddItem:
                        if (root.TryGetProperty("payload", out var addItemProp)
                            && addItemProp.ValueKind == JsonValueKind.Object)
                        {
                            payload = JsonConvert.DeserializeObject<ProductInsertRequestModel>(addItemProp.GetRawText());

                            if (payload is not null)
                            {
                                var req = _cartService.AddProduct((ProductInsertRequestModel)payload);

                                response = req.Result ? ResponseWrapper.SuccessWithData<DisplayCartResponseModel>(req.Message) : ResponseWrapper.Error<DisplayCartResponseModel>(req.Message);
                            }
                        }

                        payload = null;
                        
                        break;
                    
                    case CommandInputTypeConsts.DisplayCart:
                        response = _cartService.DisplayCart();
                        break;
                    
                    case CommandInputTypeConsts.RemoveItem:
                        if (root.TryGetProperty("payload", out var removeItemProp)
                            && removeItemProp.ValueKind == JsonValueKind.Object)
                        {
                            payload = JsonConvert.DeserializeObject<Payload>(removeItemProp.GetRawText());
                            var req = _cartService.RemoveProduct(payload.ItemId);
                            
                            response = req.Result ? ResponseWrapper.SuccessWithData<DisplayCartResponseModel>(req.Message) : ResponseWrapper.Error<DisplayCartResponseModel>(req.Message);
                        }

                        payload = null;
                        break;
                    
                    case CommandInputTypeConsts.ResetCart:
                        response = ResponseWrapper.SuccessWithData<DisplayCartResponseModel>(_cartService.ResetCart().Message);
                        break;
                    
                    case CommandInputTypeConsts.AddVasItemToItem:
                        if (root.TryGetProperty("payload", out var addVasItemProp)
                            && addVasItemProp.ValueKind == JsonValueKind.Object)
                        {
                            payload = JsonConvert.DeserializeObject<VasProductInsertRequestModel>(addVasItemProp.GetRawText());
                            var req = _cartService.AddVasProduct((VasProductInsertRequestModel)payload);
                            
                            response = req.Result ? ResponseWrapper.SuccessWithData<DisplayCartResponseModel>(req.Message) : ResponseWrapper.Error<DisplayCartResponseModel>(req.Message);
                        }

                        payload = null;
                        break;
                    
                    default:
                        payload = null;
                        break;
                }


                return response;
            }

        }
        
        return ResponseWrapper.Error<DisplayCartResponseModel>("");
    }

    private static Response CheckIfInputIsJson(string input)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return ResponseWrapper.Error("Input is null or empty.");
            }
            
            var parsed = JToken.Parse(input);
            
            if (parsed is JArray && parsed.All(token => token.Type == JTokenType.Integer || token.Type == JTokenType.Float))
            {
                throw new JsonReaderException("Input is a sequence of numbers.");
            }

            if (parsed.Type == JTokenType.Integer || parsed.Type == JTokenType.Float)
            {
                throw new JsonReaderException("Input is a single number.");
            }

            return ResponseWrapper.Success("");

        }
        catch (JsonReaderException e)
        {
            return ResponseWrapper.Error("Couldn't convert the input provided. Exception Message: "+e.Message);
        }
    }

}