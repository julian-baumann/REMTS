using RemoteExecuter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Win32;
using System.IO;

namespace Gui
{
    class StorageService
    {
        public static void SavePcData(List<RemotePcInfo> computers)
        {
            string json = JsonSerializer.Serialize(computers);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Computers.json";
            saveFileDialog.Filter = "Json files (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {

                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }

        public static RemotePcInfo[] LoadPcDataFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                string rawJson = File.ReadAllText(openFileDialog.FileName);

                RemotePcInfo[] computers = JsonSerializer.Deserialize<RemotePcInfo[]>(rawJson);

                return computers;
            }

            return new RemotePcInfo[] { };
        }
    }
}
