using FileSystem.Data;
using FileSystem.Models;
using FileSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FileSystem.Services
{
    public class AppFileService
    {
        private readonly AppDbContext _dbContext;

        public AppFileService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppFile> CreateFileAsync(AppFileDTO dto)
        {
            dto.Path = dto.Path.Replace("\\", "/");
            var folders = dto.Path.Split("/");
            var parentFolderPath = String.Join('/', folders.SkipLast(1));
            var parentFolder = await _dbContext.Folders.FindAsync(dto.FolderId) ?? throw new AggregateException("Cannot create new file at this location. Parent folder does not exist");

            if(!parentFolder.Path.ToLower().Equals(parentFolderPath?.ToLower())) throw new AggregateException("Cannot create new file at this location. Parent folder path is wrong");

            var file = new AppFile(dto);

            return file;
        }

        public async Task<List<GetAppFilesDTO>> GetAllFilesAsync()
        {
            return await _dbContext.Files.Include(x => x.Folder)
                .Select(f => new GetAppFilesDTO(f))
                .ToListAsync();
        }

        public async Task DeleteFileAsync(int fileId)
        {
            var file = await _dbContext.Files.FindAsync(fileId) ?? throw new AggregateException($"File with ID {fileId} doesn't exist.");

            _dbContext.Files.Remove(file);
        }

        public async Task<List<GetAppFilesDTO>> SearchFilesByNameAsync(string? name)
        {
            return await _dbContext.Files.Include(x => x.Folder)
                .Where(f => name != null ? f.Name.ToLower().StartsWith(name!.ToLower()) : true)
                .Select(f => new GetAppFilesDTO(f))
                .Take(10)
                .ToListAsync();
        }
    }
}
