using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevGames.Models;
using DevGames.Services;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DevGames.Controllers
{
    [Route("api/[controller]")]
    public class UploadController: ControllerBase
    {
        IHostingEnvironment _appEnvironment;

        public UploadController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }

        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult> Post(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }
            if (!Directory.Exists(_appEnvironment.WebRootPath + "\\uploads\\"))
            {
                Directory.CreateDirectory(_appEnvironment.WebRootPath + "\\uploads\\");
            }
            string fileName = "Game_File_" + Guid.NewGuid();

            if(file.FileName.Contains(".jpg"))
                fileName += ".jpg";
            else if(file.FileName.Contains(".gif"))
                fileName += ".gif";
            else if(file.FileName.Contains(".png"))
                fileName += ".png";
            else
                fileName+= ".tmp";

            string pathFile = _appEnvironment.WebRootPath + "\\uploads\\" + fileName;

            using (var stream = new FileStream(pathFile, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new {path = pathFile});
            
        }
    }
     
}