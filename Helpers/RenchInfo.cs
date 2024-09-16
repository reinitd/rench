using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WrenchRealm.Models;

namespace WrenchRealm.Helpers
{
    public class RenchInfoService
    {
        public Rench? Info { get; private set; }
        private const string FilePath = "./Rench.json";

        public RenchInfoService()
        {
            if (!File.Exists(FilePath))
            {
                InitializeNewRench();
            }
            else
            {
                LoadExistingRench();
            }
        }

        private void InitializeNewRench()
        {
            Info = new Rench
            {
                FirstOpenTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                PromptedToInstallGD = null,
                IsGDInstalled = null,
                GDRealmPath = null,
                WrenchSavePath = null
            };

            SaveToFile();
        }

        private void LoadExistingRench()
        {
            string fileContent = File.ReadAllText(FilePath);
            Info = JsonConvert.DeserializeObject<Rench>(fileContent);
        }

        public void Update(Rench updatedRench)
        {
            EnsureFileExists();
            Info = updatedRench;
            SaveToFile();
        }

        public async Task UpdateAsync(Rench updatedRench)
        {
            EnsureFileExists();
            Info = updatedRench;
            await SaveToFileAsync();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(Info, Formatting.None);
        }

        public string GetSerializedInfo()
        {
            return Serialize();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException($"The file '{FilePath}' was not found.");
            }
        }

        private void SaveToFile()
        {
            File.WriteAllText(FilePath, Serialize());
        }

        private async Task SaveToFileAsync()
        {
            await File.WriteAllTextAsync(FilePath, Serialize());
        }
    }
}
