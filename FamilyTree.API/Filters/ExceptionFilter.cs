using FamilyTree.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
