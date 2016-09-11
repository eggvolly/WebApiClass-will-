using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi0911.Controllers
{
    public class MyError
    {
        public string Error_Message { get; set; }
        public string SubStatusCode { get; set; }

        public MyError()
        {

        }
    }
}