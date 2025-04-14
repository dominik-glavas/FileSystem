namespace FileSystem.Models.DTOs
{
    public class GetAppFilesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Folder Folder { get; set; }

        public GetAppFilesDTO(AppFile file)
        {
            Id = file.Id;
            Name = file.Name;
            Path = file.Path;
            Folder = file.Folder;
        }
    }
}
