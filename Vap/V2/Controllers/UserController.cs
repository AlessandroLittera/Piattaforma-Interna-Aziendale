using Microsoft.AspNetCore.Mvc.Rendering;
using Helper.Web.HatHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net.Http.Headers;
using AutoMapper;
using Vap.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using System;
using Microsoft.Extensions.Configuration;

namespace Vap.V2.Controllers
{
    public class UserController : Controller
    {
        

        [HttpGet("Healthcheck")]
        public IActionResult Helthcheck(){
            return ($"UserController ready at {DateTime.UtcNow}");
        }
    }
}