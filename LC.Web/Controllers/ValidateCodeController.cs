using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.Web.Framework.Core.VerifyCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LC.Web.Controllers
{
    public class ValidateCodeController : Controller
    {
        public ActionResult GetValidateCode()
        {
            string code = string.Empty;
            var stream = ValidateCode.Create(out code);

            HttpContext.Session.SetString("ValidateCode", code);
            return File(stream.ToArray(), @"image/jpeg");
        }
    }
}