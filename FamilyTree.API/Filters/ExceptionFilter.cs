using FamilyTree.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace FamilyTree.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var clientExceptions = new List<string>()
            {
                nameof(EntityNotFoundException),
                nameof(FamilyStructureException)
            };

            if (clientExceptions.Contains(context.Exception.GetType().Name))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Error = context.Exception.Message
                });
            }
            else
            {
                context.Result = new StatusCodeResult(500);
            }
        }
    }
}
