using FileSystem.Data;
using FileSystem.Models;
using FileSystem.Models.DTOs;
using FileSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileSystem.Controllers
{
    [ApiController]
    [Route("folders")]
    public class FoldersController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly FolderService _folderService;

        public FoldersController(AppDbContext dbContext, FolderService folderService)
        {
            _dbContext = dbContext;
            _folderService = folderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFolderAsync(FolderDTO dto)
        {
            try
            {
                var folder = _folderService.CreateFolder(dto);

                _dbContext.Add(folder);
                await _dbContext.SaveChangesAsync();

                return Ok($"Successfully created new folder with ID: {folder.Id}");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Folder with name {dto.Name} already exists at specified path. {ex.Message}");
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

        [HttpDelete]
        public async Task<IActionResult> DeleteFolderAsync(int folderId)
        {
            try
            {
                await _folderService.DeleteFolderAsync(folderId);

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
    }
}
