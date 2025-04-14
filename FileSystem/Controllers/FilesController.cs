using FileSystem.Data;
using FileSystem.Models;
using FileSystem.Models.DTOs;
using FileSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSystem.Controllers
{
    [ApiController]
    [Route("files")]
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly AppFileService _appFileService;

        public FilesController(AppDbContext dbContext, AppFileService appFileService)
        {
            _dbContext = dbContext;
            _appFileService = appFileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFileAsync(AppFileDTO dto)
        {
            try
            {
                var file = await _appFileService.CreateFileAsync(dto);

                _dbContext.Add(file);
                await _dbContext.SaveChangesAsync();

                return Ok($"Successfully created new file with ID: {file.Id}");
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"File with name {dto.Name} already exists at specified path. {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAppFilesDTO>>> GetAllFilesAsync()
        {
            try
            {
                return await _appFileService.GetAllFilesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFileAsync(int fileId)
        {
            try
            {
                await _appFileService.DeleteFileAsync(fileId);

                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByName")]
        public async Task<ActionResult<List<GetAppFilesDTO>>> SearchFilesByNameAsync(string? name)
        {
            try
            {
                return await _appFileService.SearchFilesByNameAsync(name);
            }
            catch (AggregateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
