using Inveon.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;

namespace Inveon.Admin.Filters
{
    public class NotFoundFilter: ActionFilterAttribute
    {
        private readonly IProductService productService;

        public NotFoundFilter(IProductService productService)
        {
            this.productService = productService;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string id = context.ActionArguments.Values.FirstOrDefault()?.ToString() ?? null;
            if (!ObjectId.TryParse(id, out ObjectId _))
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                var result = await productService.GetByIdAsync(id);
                if (result == null)
                {
                    context.Result = new NotFoundResult();
                }
                else await next();
            }

        }
    }
}
