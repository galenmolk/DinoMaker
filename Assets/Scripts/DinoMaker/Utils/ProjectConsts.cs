using System.IO;
using UnityEngine;

namespace DinoMaker.Utils
{
    public static class ProjectConsts
    {
        public const string CUSTOM_ASSET_MENU = "Custom/";

        public const int OPEN_TWEEN_INDEX = 1;
        public const int CLOSE_TWEEN_INDEX = 0;

        private const string JPG_EXT = ".jpg";
        
        private static string SavePath => Application.persistentDataPath + "/Dinos/";
        
        public static string NewFilePath(string fileName)
        {
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
            
            return SavePath + fileName + JPG_EXT;
        }
    }
}
