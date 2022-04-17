using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.Infrastructure;
using BLL.Services;
using Common.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace API.Controllers
{
    public class MediasController : BaseController
    {
        private MediaService MediaService { get; }

        private IWebHostEnvironment WebHostEnvironment { get; }

        public MediasController(MediaService mediaService, IWebHostEnvironment webHostEnvironment)
        {
            MediaService = mediaService;
            WebHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task<Guid> Add(IFormFile file)
        {
            string uploads = Path.Combine(WebHostEnvironment.WebRootPath, "uploads");
            return await MediaService.Add(file, uploads);
        }

        [HttpGet]
        [Route("list")]
        [AuthorizeRoles(RoleType.Admin, RoleType.DepartmentAdmin, RoleType.User)]
        public async Task<List<MediaModels.ListOut>> GetList(List<Guid> ids)
        {
            string uploads = Path.Combine(WebHostEnvironment.WebRootPath, "uploads");
            return await MediaService.GetList(ids, uploads);
        }
    }
}
