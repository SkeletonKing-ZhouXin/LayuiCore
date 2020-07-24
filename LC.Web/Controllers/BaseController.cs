using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Dictionary<string, object> GetResult(bool success, string message = null, object data = null)
        {

            var dict = new Dictionary<string, object>
            {
                { "success",success }
            };
            if (!string.IsNullOrWhiteSpace(message))
            {
                dict.Add("msg", message);
            }
            if (data != null)
                dict.Add("data", data);

            return dict;
        }
    }
}
