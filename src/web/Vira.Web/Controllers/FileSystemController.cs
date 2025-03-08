using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Vira.App.Core.Models;
using Vira.App.Core.Services;
using Vira.Network.Requests;
using Vira.Network.Responces;

namespace Vira.Web.Controllers
{
    [Route("api/[controller]")]

    public class FileSystemController : Controller
    {
        private SystemFilesService _fileService = new SystemFilesService();

        // POTS: /api/filesystem/directories
        [HttpPost("directories")]
        public async Task<IActionResult> GetDirectories([FromBody] string path = "")
        {
            try
            {
                var directories = await _fileService.GetRootDirectory("C:\\");

                return Ok(new FileSystemResponce() 
                { 
                    Data = JsonConvert.SerializeObject(directories),
                    Type = Network.Interfaces.TypeRequest.Compleate
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new FileSystemResponce()
                {
                    Message = ex.Message,
                    Type = Network.Interfaces.TypeRequest.Error
                });
            }
        }

        // POST: "/api/filesystem/subdirectories"
        [HttpPost("subdirectories")]
        public async Task<IActionResult> GetFiles([FromBody] string json)
        {
            try
            {
                var directory = JsonConvert.DeserializeObject<DirectoryItemModel>(json);

                if (directory != null)
                {
                    await _fileService.LoadDirectory(directory);
                    await _fileService.LoadFiles(directory);
                }
                else
                {
                    throw new Exception("Нет Директории для lazy загрузки");
                }

                return Ok(new FileSystemResponce()
                {
                    Data = JsonConvert.SerializeObject(directory),
                    Type = Network.Interfaces.TypeRequest.Compleate
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new FileSystemResponce()
                {
                    Message = ex.Message,
                    Type = Network.Interfaces.TypeRequest.Error
                });
            }
        }
    }
}