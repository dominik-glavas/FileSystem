using FileSystem.Data;
using FileSystem.Models;
using FileSystem.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Services
{
    public class FolderService
    {
        private readonly AppDbContext _dbContext;

        public FolderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Folder CreateFolder(FolderDTO dto)
        {
            dto.Path = dto.Path.Replace("\\", "/");
            var parentName = dto.Path.Split('/');
            var parentPath = String.Join('/', parentName.SkipLast(1));
            var parent = _dbContext.Folders.Where(f => f.Name.ToLower().Equals(parentName.Last().ToLower()) && f.Path.ToLower().Equals(parentPath.ToLower())).FirstOrDefault();

            if(parent == null) throw new AggregateException("Cannot create new folder at this location. Parent folder does not exist");

            var folder = new Folder(dto.Name, dto.Path);
            folder.Parent = parent;

            return folder;
        }

        public async Task DeleteFolderAsync(int folderId)
        {
            var folder = await _dbContext.Folders.FindAsync(folderId) ?? throw new AggregateException($"Folder with ID {folderId} doesn't exist.");

            _dbContext.Folders.Remove(folder);
        }
    }
}
