using FileSystem.Models.DTOs;

namespace FileSystem.Models
{
    public class AppFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int FolderId { get; set; }
        public Folder Folder { get; set; } = null!;

        public AppFile(AppFileDTO dto)
        {
            Name = dto.Name;
            Path = dto.Path;
            FolderId = dto.FolderId;
        }

        public AppFile()
        {
            
        }
    }
}
