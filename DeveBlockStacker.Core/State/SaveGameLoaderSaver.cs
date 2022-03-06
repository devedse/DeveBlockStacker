#if NETFRAMEWORK
using System.Threading.Tasks;

namespace DeveBlockStacker.Core.State
{
    public static class SaveGameLoaderSaver
    {
        public const string SaveGameFileName = "deveblockstacker.sav";
        public static string TotalSaveGameFileName
        {
            get
            {
                return "empty";
            }
        }

        public static async Task<SaveGame> LoadSaveGame()
        {
            return null;
        }

        public static async Task SaveSaveGame(SaveGame saveGame)
        {

        }
    }
}
#else
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeveBlockStacker.Core.State
{
    public static class SaveGameLoaderSaver
    {
        public const string SaveGameFileName = "deveblockstacker.sav";
        public static string TotalSaveGameFileName
        {
            get
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var totPath = Path.Combine(folder, SaveGameFileName);
                return totPath;
            }
        }

        public static async Task<SaveGame> LoadSaveGame()
        {
            var totPath = TotalSaveGameFileName;
            if (!File.Exists(totPath))
            {
                return null;
            }
            else
            {
                try
                {
                    using (var reader = new StreamReader(new FileStream(totPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        var readText = await reader.ReadToEndAsync();
                        var parsed = JsonSerializer.Deserialize<SaveGame>(readText);
                        return parsed;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occured: {ex}");
                    return null;
                }
            }
        }

        public static Task SaveSaveGame(SaveGame saveGame)
        {
            var totPath = TotalSaveGameFileName;
            using (var writer = new StreamWriter(new FileStream(totPath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                var serializedText = JsonSerializer.Serialize(saveGame);
                return writer.WriteAsync(serializedText);
            }
        }
    }
}
#endif