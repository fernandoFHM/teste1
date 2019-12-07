using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevGames.Models;
using DevGames.Services;

namespace DevGames.Controllers
{
    [Route("api/[controller")]
    public class UploadController Controller
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
            if (file == null || file.length == 0)
            {
                return BadRequest();
            }
            if (!Directory.Exists(_appEnvironment.WebRootPath + "\\uploads\\"))
            {
                Directory.CreateDirectory(_appEnvironment.WebRootPath + "\\uploads\\";
            }
            string fileName = "Game_File_" + Guid.NewGuid();

            if(file.fileName.Contains(".jpg"))
                fileName += ".jpg";
            else if(file.fileName.Contains(".gif"))
                fileName += ".gif";
            else if(file.fileName.Contains(".png"))
                fileName += ".png";
            else
                fileName+= ".tmp";

            string pathFile = _appEnvironment.WebRootPath + "\\uploads\\" + fileName;

            using (var stream = new FileStream(pathFile, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return OK(new {path = pathFile});
            
        }
    }
     
}