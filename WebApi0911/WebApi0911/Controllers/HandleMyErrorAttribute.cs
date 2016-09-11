using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Web.Http.Filters;

namespace WebApi0911.Controllers
{
    public class HandleMyErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response =
                actionExecutedContext.Request.CreateResponse(
                HttpStatusCode.InternalServerError, new MyError() { Error_Message = "Exception", SubStatusCode = "100" });

            base.OnException(actionExecutedContext);
        }

        
    }
}