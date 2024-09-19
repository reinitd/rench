using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rench.Models;

namespace Rench.Helpers
{
    public class RenchInfoService
    {
        public RenchInfo Info { get; private set; }
        private const string FilePath = "./Rench.json";

        public RenchInfoService()
        {
            if (!File.Exists(FilePath))
            {
                InitializeNewRenchForm();
            }
            else
            {
                LoadExistingRenchForm();
            }
        }

        private void InitializeNewRenchForm()
        {
            Info = new RenchInfo
            {
                FirstOpenTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                PromptedToInstallGD = null,
                IsGDInstalled = null,
                GDRealmPath = null,
                WrenchSavePath = null
            };

            SaveToFile();
        }

        private void LoadExistingRenchForm()
        {
            string fileContent = File.ReadAllText(FilePath);
            Info = JsonConvert.DeserializeObject<RenchInfo>(fileContent);
        }

        public void Update(RenchInfo updatedRenchForm)
        {
            EnsureFileExists();
            Info = updatedRenchForm;
            SaveToFile();
        }

        public async Task UpdateAsync(RenchInfo updatedRenchForm)
        {
            EnsureFileExists();
            Info = updatedRenchForm;
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
