using System.Text.Json;

namespace DeveBlockStacker.Core.State
{
    public static class SaveGameLoaderSaver
    {
        public const string SaveGameFileName = "deveblockstacker.sav";
        public static string TotalSaveGameFileName
        {
            get
            {
#if WINDOWS_UAP
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#else
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
#endif
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
