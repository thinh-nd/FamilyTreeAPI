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
            if (context.Exception.GetType().Name == nameof(EntityNotFoundException))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Error = context.Exception.Message
                });
            }
        }
    }
}
