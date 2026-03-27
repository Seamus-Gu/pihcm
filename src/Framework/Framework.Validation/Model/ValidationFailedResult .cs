using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Framework.Validation
{
    internal class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
       : base(null!)
        {
            // 异常列错误
            var errors = modelState
                         .Where(ms => ms.Value?.Errors.Count > 0)
                         .SelectMany(ms => ms.Value!.Errors
                             .Select(e => new ValidationError
                             {
                                 Field = ms.Key,
                                 Message = e.ErrorMessage
                             }))
                         .ToList();

            var code = (int)HttpStatusCode.UnprocessableEntity;

            this.Value = new ValidationResult
            {
                Code = code,
                //Message = HttpLocalization.GetString(code.ToString())!,
                Data = errors
            };

            this.StatusCode = code;
        }
    }
}