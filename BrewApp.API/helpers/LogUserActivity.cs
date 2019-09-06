using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BrewApp.API.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BrewApp.API.helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        // action filter to log last active
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            var user_Id = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContext.HttpContext.RequestServices.GetService<IBrewRepository>();
            var user = await repo.GetUser(user_Id);
            user.LastActive = DateTime.Now;
            await repo.SaveAll();
        }
    }
}