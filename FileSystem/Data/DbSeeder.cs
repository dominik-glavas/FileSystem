using FileSystem.Models;

namespace FileSystem.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext dbContext)
        {
            if (!dbContext.Folders.Any())
            {
                var folders = new List<Folder>
                {
                    new Folder { Name = "C:", Path = "" },
                    new Folder { Name = "Users", Path = "C:" },
                    new Folder { Name = "User", Path = "C:/Users" },
                    new Folder { Name = "Desktop", Path = "C:/Users/User" }
                };

                for(int i = 1; i < folders.Count; i++)
                {
                    folders[i].Parent = folders[i-1];
                }

                dbContext.Folders.AddRange(folders);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
