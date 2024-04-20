using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Espale.Utilities
{
    public static class SaveUtilities
    {
        public static void SaveValue<T>(string fileName, T value)
        {
            if (value == null)
            {
                BetterDebug.LogWarning("Tried to save a null value to the \"" + fileName + "\" save file");
                return;
            }

            CreateSaveDirectoryIfNecessary(fileName);

            var path = GetPathFromFileName(fileName);

            var binaryFormatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Create);

            binaryFormatter.Serialize(stream, value);
            stream.Close();
        }

        public static T ReadValue<T>(string fileName, T defaultValue)
        {
            var path = GetPathFromFileName(fileName);

            if (File.Exists(path))
            {
                var binaryFormatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);

                var value = (T) binaryFormatter.Deserialize(stream);
                stream.Close();

                return value;
            }
            
            return defaultValue;
        }

        public static void ClearValue(string fileName)
        {
            var path = GetPathFromFileName(fileName);

            if (File.Exists(path))
                File.Delete(path);
        }

        private static string GetPathFromFileName(string fileName) => Application.persistentDataPath + "/saves/" + fileName + ".save";

        private static void CreateSaveDirectoryIfNecessary(string fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");

            // Create the subdirectories if there are any.
            if (fileName.Contains("/"))
            {
                var dirs = fileName.Split('/');
                
                // Last index is the name of the file so we skip it.
                for (var i = 0; i < dirs.Length - 1; i++)
                {
                    var currentPathToCheck = Application.persistentDataPath + "/saves";
                    for (var j = 0; j <= i; j++)
                        currentPathToCheck += $"/{dirs[j]}";
                    
                    if (!Directory.Exists(currentPathToCheck))
                        Directory.CreateDirectory(currentPathToCheck);
                }
            }
        }
        
        public class SemanticVersionData : IEquatable<SemanticVersionData>, IComparable<SemanticVersionData>
        {
            public string semVer;

            public int Major => int.Parse(semVer.Split('.')[0]);
            public int Minor => int.Parse(semVer.Split('.')[1]);
            public int Patch => int.Parse(semVer.Split('.')[2]);
    
            public SemanticVersionData(string semVer) => this.semVer = semVer;

            public bool Equals(SemanticVersionData other) => other != null && semVer == other.semVer;

            public int CompareTo(SemanticVersionData other)
            {
                if (Major < other.Major) return -1;
                if (Major > other.Major) return 1;
        
                if (Minor < other.Minor) return -1;
                if (Minor > other.Minor) return 1;
        
                if (Patch < other.Patch) return -1;
                if (Patch > other.Patch) return 1;

                return 0;
            }
        }
    }
}
