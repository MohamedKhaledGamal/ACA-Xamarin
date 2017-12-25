using ACA.Common;
using Java.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace ACA.Common
{
    class SaveAndLoad : ISaveAndLoad
    {
        public void SaveText(string filename, string text)
        {
            //var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //var filePath = Path.Combine(documentsPath, filename);
            //File.WriteAllText(filePath, text);
        }
        public string LoadText(string filename)
        {
            //var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //var filePath = Path.Combine(documentsPath, filename);
            //return File.ReadAllText(filePath);

            return "";
        }
        public bool FileExist(string filename)
        {
            //var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //var filePath = Path.Combine(documentsPath, filename);
            //return File.Exists(filePath);
            return false;
        }
    }
}
