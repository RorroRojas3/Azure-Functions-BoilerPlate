using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Services.Interface;
using Rodrigo.Tech.Model.Constants;
using Rodrigo.Tech.Model.Requests;

namespace Rodrigo.Tech.BoilerPlate.HttpTriggers
{
    public class ItemHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IItemService _itemService;

        public ItemHttpTrigger(ILogger<ItemHttpTrigger> logger,
                                IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_GETALL)]
        public async Task<IActionResult> GetAllEmails(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = HttpTriggerFunctionRouteConstants.ITEM)] HttpRequest req
            )
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_GETALL} - Started");

                var result = await _itemService.GetItems();

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_GETALL} - Finished");
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_GETALL} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_GET)]
        public async Task<IActionResult> GetEmail(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = HttpTriggerFunctionRouteConstants.ITEM_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_GET} - Started");

                var result = await _itemService.GetItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_GET} - Finished");
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_GET} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_POST)]
        public async Task<IActionResult> PostEmail(
            [HttpTrigger(AuthorizationLevel.Function, "post",
            Route = HttpTriggerFunctionRouteConstants.ITEM)] HttpRequest request)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_POST} - Started");

                var input = await request.ReadAsStringAsync();
                var emailBodyRequest = JsonConvert.DeserializeObject<ItemRequest>(input);
                var result = await _itemService.PostItem(emailBodyRequest);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_POST} - Finished");
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_POST} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_PUT)]
        public async Task<IActionResult> PutEmail(
            [HttpTrigger(AuthorizationLevel.Function, "put",
            Route = HttpTriggerFunctionRouteConstants.ITEM_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_PUT} - Started");

                var input = await request.ReadAsStringAsync();
                var emailBodyRequest = JsonConvert.DeserializeObject<ItemRequest>(input);
                var result = await _itemService.PutItem(id, emailBodyRequest);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_PUT} - Finished");
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_PUT} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_DELETE)]
        public async Task<IActionResult> DeleteEmail(
            [HttpTrigger(AuthorizationLevel.Function, "delete",
            Route = HttpTriggerFunctionRouteConstants.ITEM_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_DELETE} - Started");

                var result = await _itemService.DeleteItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.ITEM_DELETE} - Finished");
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.ITEM_DELETE} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
