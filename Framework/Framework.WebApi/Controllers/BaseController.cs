using Framework.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.WebApi
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private static readonly Dictionary<string, string> _actionMap = new()
        {
            { FrameworkConstant.POST,   FrameworkConstant.ADD },
            { FrameworkConstant.PUT,   FrameworkConstant.EDIT },
            { FrameworkConstant.DELETE,   FrameworkConstant.REMOVE },
        };

        /// <summary>
        /// Creates a JSON response containing the specified status code and message.
        /// </summary>
        /// <param name="code">The status code to include in the response. Typically used to indicate the result of the operation.</param>
        /// <param name="message">The message to include in the response. Provides additional information about the result.</param>
        /// <returns>A JSON-formatted action result containing the specified code and message.</returns>
        protected IActionResult CreateResponse(int code, string message)
        {
            return new JsonResult(new
            {
                code,
                message
            });
        }

        /// <summary>
        /// Creates a JSON-formatted HTTP response with a specified status code, message, and optional data payload.
        /// </summary>
        /// <remarks>The response object will omit the data field if the data parameter is null. This
        /// method is intended to standardize API responses across endpoints.</remarks>
        /// <typeparam name="T">The type of the data payload to include in the response.</typeparam>
        /// <param name="code">The status code to include in the response, typically representing the result of the operation.</param>
        /// <param name="message">A message describing the result or providing additional information about the response.</param>
        /// <param name="data">The optional data payload to include in the response. If not specified, the data field will be omitted from
        /// the JSON output.</param>
        /// <returns>A JSON result containing the specified status code, message, and data, suitable for use as an HTTP response.</returns>

        protected IActionResult CreateResponse<T>(int code, string message, T? data = default)
        {
            var response = new
            {
                code,
                data,
                message
            };

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return new JsonResult(response, options);
        }

        /// <summary>
        /// Creates a successful HTTP response with the specified message.
        /// </summary>
        /// <param name="message">The message to include in the response body. If not specified, a default success message is used.</param>
        /// <returns>An <see cref="IActionResult"/> representing a successful response containing the provided message.</returns>
        protected IActionResult Success(string message = ResponseCode.SUCCESS_MESSAGE) => CreateResponse(ResponseCode.SUCCESS, message);

        /// <summary>
        /// Creates a successful HTTP response containing the specified data payload.
        /// </summary>
        /// <typeparam name="T">The type of the data to include in the response body.</typeparam>
        /// <param name="data">The data to include in the response body. Can be null if no data is required.</param>
        /// <returns>An IActionResult representing a successful response with the provided data.</returns>
        protected IActionResult Success<T>(T data) => CreateResponse(ResponseCode.SUCCESS, ResponseCode.SUCCESS_MESSAGE, data);

        /// <summary>
        /// Creates a successful HTTP response with the specified message and data payload.
        /// </summary>
        /// <typeparam name="T">The type of the data to include in the response body.</typeparam>
        /// <param name="message">The message to include in the response, typically used to convey additional information about the operation
        /// result.</param>
        /// <param name="data">The data to include in the response body. Can be any type representing the result of the operation.</param>
        /// <returns>An IActionResult representing a successful response containing the provided message and data.</returns>
        protected IActionResult Success<T>(string message, T data) => CreateResponse(ResponseCode.SUCCESS, message, data);

        /// <summary>
        /// Creates an error response with the specified message.
        /// </summary>
        /// <param name="message">The error message to include in the response. If not specified, a default error message is used.</param>
        /// <returns>An <see cref="IActionResult"/> representing the error response.</returns>
        protected IActionResult Fail(string message = ResponseCode.ERROR_MESSAGE) => CreateResponse(ResponseCode.ERROR, message);

        /// <summary>
        /// Creates an error response with the specified data payload.
        /// </summary>
        /// <typeparam name="T">The type of the data to include in the error response.</typeparam>
        /// <param name="data">The data to include in the error response. Can be null if no additional information is required.</param>
        /// <returns>An IActionResult representing an error response containing the specified data.</returns>
        protected IActionResult Fail<T>(T data) => CreateResponse(ResponseCode.ERROR, ResponseCode.ERROR_MESSAGE, data);

        /// <summary>
        /// Creates an error response with the specified message and data payload.
        /// </summary>
        /// <typeparam name="T">The type of the data to include in the response payload.</typeparam>
        /// <param name="message">The error message to include in the response. Cannot be null.</param>
        /// <param name="data">The data to include in the response payload. May be null if no additional data is required.</param>
        /// <returns>An IActionResult representing an error response containing the specified message and data.</returns>
        protected IActionResult Fail<T>(string message, T data) => CreateResponse(ResponseCode.ERROR, message, data);

        protected IActionResult R(int code, string message)
        {
            return new JsonResult(new
            {
                code,
                message
            });
        }

        protected IActionResult R<T>(int code, string message, T data)
        {
            return new JsonResult(new
            {
                code,
                data,
                message
            });
        }

        /// <summary>
        /// 判断是否成功后返回
        /// </summary>
        /// <returns></returns>
        protected IActionResult BoolResult(bool isSuccess)
        {
            var action = _actionMap.TryGetValue(HttpContext.Request.Method, out var act)
                 ? act
                 : FrameworkConstant.ADD;

            var suffix = isSuccess ? FrameworkConstant.SUCCESS : FrameworkConstant.FAIL;
            var key = $"{action}_{suffix}";

            var message = CommonLocalization.GetString(key) ?? key;
            return Success(message);
        }

        protected IActionResult BadResult(int httpResponseCode, int code, string message)
        {
            var result = new BadRequestObjectResult(new
            {
                code,
                message
            });

            result.StatusCode = httpResponseCode;
            return result;
        }

        protected IActionResult BadResult<T>(int httpResponseCode, int code, string message, T data)
        {
            var result = new BadRequestObjectResult(new
            {
                code,
                data,
                message
            });

            result.StatusCode = httpResponseCode;
            return result;
        }


    }
}