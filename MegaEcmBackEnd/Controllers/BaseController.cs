using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SasReportService.Controllers
{
    public class BaseController : ControllerBase
    {
        protected string GetModelAttributeErrorMessage()
        {
            var message = string.Join(" | ", ModelState.Values
                .SelectMany(values => values.Errors)
                .Select(e => e.ErrorMessage));
            return message;
        }

    }
}