using RemoteExecuter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;

namespace UserInterface.Services
{
    class StorageService
    {
        public static async Task<bool> SavePcData(List<RemotePcInfo> computers)
        {
            string json = JsonSerializer.Serialize(computers);
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add("Json files", new List<string>() { ".json" });
            savePicker.SuggestedFileName = "Profile";

            StorageFile file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, json);
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);

                if (status == FileUpdateStatus.Complete)
                {
                    return true;
                }
            }


            return false;
        }

        public static async Task<RemotePcInfo[]> LoadPcDataFromFile()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".json");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string rawFile = await FileIO.ReadTextAsync(file);
                RemotePcInfo[] computers = JsonSerializer.Deserialize<RemotePcInfo[]>(rawFile);

                return computers;
            }

            return new RemotePcInfo[] { };
        }
    }
}
