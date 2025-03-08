using Microsoft.AspNetCore.Mvc;

using Vira.App.Core.Helpers;
using Vira.App.Core.Interfaces;
using Vira.App.Core.Models;
using Vira.Network.Utilities;

namespace Vira.Web.Controllers
{
    [Route("api/[controller]")]
    public class FileSystemController : Controller
    {
        private readonly ISystemFilesService _fileService;

        public FileSystemController(ISystemFilesService fileService)
        {
            _fileService = fileService;
        }

        // POTS: /api/filesystem/directories
        [HttpPost("directories")]
        public async Task<IActionResult> GetDirectoryAsync([FromBody] string path = "")
        {
            try
            {
                if (path.StartsWith("/") || string.IsNullOrEmpty(path))
                {
                    path = PathHelper.GetEnvironmentPath();
                }

                var directories = await _fileService.GetDirectoryAsync(path);

                return Ok(directories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: "/api/filesystem/subdirectories"
        [HttpPost("subdirectories")]
        public async Task<IActionResult> GetSubdirectoryAsync([FromBody] DirectoryItemModel directory)
        {
            try
            {
                if (directory != null)
                {
                    await _fileService.GetSubdirectoryAsync(directory);
                }
                else
                {
                    throw new Exception("Нет Директории для lazy загрузки");
                }

                return Ok(JsonConverter.Serialize(directory));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}