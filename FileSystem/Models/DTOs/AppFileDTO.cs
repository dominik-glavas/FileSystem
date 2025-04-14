namespace FileSystem.Models.DTOs
{
    public class AppFileDTO
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int FolderId { get; set; }

        public AppFileDTO(string name, string path, int folderId)
        {
            Name = name;
            Path = path;
            FolderId = folderId;
        }
    }
}
