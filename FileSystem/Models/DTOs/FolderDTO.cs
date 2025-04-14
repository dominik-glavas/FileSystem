namespace FileSystem.Models.DTOs
{
    public class FolderDTO
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public FolderDTO(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
