using System.Text.Json.Serialization;

namespace FileSystem.Models
{
    public class Folder
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Folder? Parent { get; set; }
        [JsonIgnore]
        public ICollection<AppFile> Files { get; set; } = new List<AppFile>();

        public Folder(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public Folder()
        {
            
        }
    }
}
