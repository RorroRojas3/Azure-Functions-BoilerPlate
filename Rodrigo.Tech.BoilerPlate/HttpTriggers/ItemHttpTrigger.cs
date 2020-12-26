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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Collections.Generic;
using Rodrigo.Tech.Model.Responses;

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

        [OpenApiOperation(operationId: "getItems", tags: new[] { "item" }, Summary = "Gets the list of dummies", Description = "This gets the list of dummies.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<ItemResponse>), Summary = "List of the dummy responses", Description = "This returns the list of dummy responses")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_GETALL)]
        public async Task<IActionResult> GetAllItems(
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

        [OpenApiOperation(operationId: "getItem", tags: new[] { "item" }, Summary = "Gets the list of dummies", Description = "This gets the list of dummies.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ItemResponse), Summary = "List of the dummy responses", Description = "This returns the list of dummy responses")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_GET)]
        public async Task<IActionResult> GetItem(
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

        [OpenApiOperation(operationId: "postItem", tags: new[] { "item" }, Summary = "Gets the list of dummies", Description = "This gets the list of dummies.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ItemResponse), Summary = "List of the dummy responses", Description = "This returns the list of dummy responses")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_POST)]
        public async Task<IActionResult> PostItem(
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

        [OpenApiOperation(operationId: "putItem", tags: new[] { "item" }, Summary = "Gets the list of dummies", Description = "This gets the list of dummies.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<ItemResponse>), Summary = "List of the dummy responses", Description = "This returns the list of dummy responses")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_PUT)]
        public async Task<IActionResult> PutItem(
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

        [OpenApiOperation(operationId: "deleteItem", tags: new[] { "item" }, Summary = "Gets the list of dummies", Description = "This gets the list of dummies.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Summary = "List of the dummy responses", Description = "This returns the list of dummy responses")]
        [FunctionName(HttpTriggerFunctionNameConstants.ITEM_DELETE)]
        public async Task<IActionResult> DeleteItem(
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
