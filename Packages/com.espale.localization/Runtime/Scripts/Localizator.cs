using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

namespace Espale.Localization
{
    public static class Localizator
    {
        private static List<string> keys;
        private static Dictionary<Language, List<string>> localizedValues;

        private static Language currentLanguage = Language.English;

        private const string LNG_FILES_PATH = "Assets/Localization/";
        private const string DEFAULT_LNG_FILES_PATH = "Packages/com.espale.localization/Default Localization Dir/";
        private const string DEFAULT_LNG_FILES_LOC_PATH = "Localizations/";
        private const string DEFAULT_LNG_FILES_HELPERS_PATH = "Helpers/";
        private const string KEYS_FILE_NAME = "_keys.txt";
        private const string AVAILABLE_LANGUAGES_NAME = "_languages.txt";
        
        private const string LINE_BREAK_TEXT = "<line_break>";
        public const string LNG_NAME_KEY = "lng_name";
        public const string LOCALIZATION_ERROR_KEY = "lng_error";

        /// <summary>
        /// An array which consists of all the keys.
        /// </summary>
        public static List<string> Keys
        {
            get
            {
                if (keys == null) ReadLocalizationFile();
                return keys;
            }
            private set
            {
                keys = value;
            }
        }

        /// <summary>
        /// Returns a dictionary of languages where each value is an array of all the localized values
        /// </summary>
        /// <returns>a dictionary of languages where each value is an array of all the localized values</returns>
        internal static Dictionary<Language, List<string>> LocalizedValues
        {
            get
            {
                if (localizedValues == null) ReadLocalizationFile();
                return localizedValues; 
            }
            private set
            {
                localizedValues = value;
            }
        }

        /// <summary>
        /// Returns <c>LNG_FILES_PATH</c>, but creates the directory if it doesn't already exist.
        /// </summary>
        /// <returns>path to the language files</returns>
        /// <exception cref="DirectoryNotFoundException">Throws an error if the localization directory template could not be found.</exception>
        private static string GetLanguageFilesPath()
        {
            // Check if the resources folder, containing the localized values already exists, if so, return it.
            if (Directory.Exists(LNG_FILES_PATH + "Resources")) return LNG_FILES_PATH + "Resources";
            
            // Read the default directory containing the localization and the key files, make sure it exists
            var defaultDir = Directory.CreateDirectory(DEFAULT_LNG_FILES_PATH + DEFAULT_LNG_FILES_LOC_PATH);
            if (!defaultDir.Exists) throw new DirectoryNotFoundException("Default Localization Files Directory was not found.");
            
            // Create the resources folder
            var files = defaultDir.GetFiles();
            var resourcesPath = Path.Combine(LNG_FILES_PATH, "Resources/");
            Directory.CreateDirectory(resourcesPath);
            
            // Copy each localization file to the newly created resources folder
            foreach (var file in files)
                file.CopyTo(Path.Combine(resourcesPath, file.Name), false);

            // Read the Python helper functions from the defaults dir, make sure they exist
            defaultDir = Directory.CreateDirectory(DEFAULT_LNG_FILES_PATH + DEFAULT_LNG_FILES_HELPERS_PATH);
            if (!defaultDir.Exists) throw new DirectoryNotFoundException("Default Localization Helpers Directory was not found.");
            
            // Copy the helpers to the localization dir but not to the resources, they don't need shipment.
            files = defaultDir.GetFiles();
            foreach (var file in files) file.CopyTo(Path.Combine(LNG_FILES_PATH, file.Name), false);
            
            // Recall this function, the files should exist now.
            return GetLanguageFilesPath();
        }

        /// <summary>
        /// Returns the localized value for the given key. If a value is not found, uses the fallback language. If that has
        /// no match, returns language error in the current language
        /// </summary>
        /// <param name="key">The localized key</param>
        /// <param name="fallBackLanguage">Language to use if the current language has no localization for the given key</param>
        /// <param name="returnErrorString">If set to <c>true</c>, this will return "localization error" in the current language, else it will return an empty <c>string</c></param>
        /// <returns></returns>
        public static string GetString(string key, Language fallBackLanguage=Language.English, bool returnErrorString = true)
        {
            var result = GetStringWithCurrentLanguage(key, returnErrorString: false);
            if (!string.IsNullOrWhiteSpace(result)) return result.Trim();

            result = GetStringWithLanguage(key, fallBackLanguage, returnErrorString: false);
            if (!string.IsNullOrWhiteSpace(result)) return result.Trim();

            return returnErrorString ? GetStringWithCurrentLanguage(LOCALIZATION_ERROR_KEY, false) : "";
        }
        
        /// <summary>
        ///  Returns the localized value of the given key in the current language without any fallback language.
        /// </summary>
        /// <param name="key">The localized key</param>
        /// <param name="returnErrorString">If set to <c>true</c>, this will return "localization error" in the current language, else it will return an empty <c>string</c></param>
        /// <returns></returns>
        public static string GetStringWithCurrentLanguage(string key, bool returnErrorString = true)
            => GetStringWithLanguage(key, currentLanguage, returnErrorString: returnErrorString);

        /// <summary>
        ///  Returns the localized value of the given key in the specified language.
        /// </summary>
        /// <param name="key">The localized key</param>
        /// <param name="language">Language to localize the given key</param>
        /// <param name="returnErrorString">If set to <c>true</c>, this will return "localization error" in the current language, else it will return an empty <c>string</c></param>
        /// <returns></returns>
        public static string GetStringWithLanguage(string key, Language language, bool returnErrorString = true)
        {
            if (!LocalizedValues.ContainsKey(language))
                return "";

            var languageArray = LocalizedValues[language];
            var index = GetIndexOfKey(key);

            var doesLngFileContainsKey = languageArray.Count > index && index >= 0;

            if (!doesLngFileContainsKey || index == -1)
                return returnErrorString ? SavedLocalizedValToDisplayLocalizedVal(languageArray[GetIndexOfKey(LOCALIZATION_ERROR_KEY)]) : "";

            return SavedLocalizedValToDisplayLocalizedVal(languageArray[index]);
        }

        /// <summary>
        /// Returns a list of booleans that corresponds to the localization status of the key in the available languages.
        /// </summary>
        public static bool[] GetLocalizationStatusOfKey(string key)
        {
            var keyIndex = GetIndexOfKey(key);

            return LocalizedValues.Keys
                .Select(language => !string.IsNullOrWhiteSpace(LocalizedValues[language][keyIndex]))
                .ToArray();
        }

        /// <summary>
        /// Returns the localized value in the raw format which it's saved in.
        /// </summary>
        /// <param name="key">key to return</param>
        /// <param name="language">language</param>
        /// <returns></returns>
        public static string GetStringRaw(string key, Language language=Language.English)
            => DisplayLocalizedValToSavedLocalizedVal(GetString(key, language, returnErrorString: false));

        /// <summary>
        /// Converts a given text into saveable key format.
        /// </summary>
        /// <param name="raw">raw text</param>
        /// <returns>raw text, in the key format.</returns>
        public static string ConvertRawToKey(string raw)
            => raw.Replace(' ', '_').Replace('\n', new char()).Replace(Environment.NewLine, "").ToLower();
        
        /// <summary>
        /// Replaces the line break special symbol with actual new lines.
        /// </summary>
        /// <param name="savedString">saved text</param>
        /// <returns>saved text with replaced special new line symbol</returns>
        private static string SavedLocalizedValToDisplayLocalizedVal(string savedString)
            => savedString.Trim().Replace(LINE_BREAK_TEXT, "\n");
        
        /// <summary>
        /// Replaces new lines with the special new line symbol.
        /// </summary>
        /// <param name="usableString">original text</param>
        /// <returns>original text with replaced new lines.</returns>
        private static string DisplayLocalizedValToSavedLocalizedVal(string usableString)
            => usableString.Replace(Environment.NewLine, LINE_BREAK_TEXT).Replace("\n", LINE_BREAK_TEXT).Trim();

        /// <summary>
        /// Tries adding the given key, returns true if successful
        /// </summary>
        /// <param name="key">Key to add</param>
        /// <returns>true if addition was successful, else false</returns>
        public static bool AddKey(string key)
        {
            key = ConvertRawToKey(key);
            if (DoesContainKey(key)) return false;

            Keys.Add(key);
            foreach (var lng in GetAvailableLanguages())
                LocalizedValues[lng].Add("");

            SaveLocalizationFile();
            
            return true;
        }

        /// <summary>
        /// Tries removing the given key, returns true if successful.
        /// </summary>
        /// <param name="key">key to remove</param>
        /// <returns>true if deletion was successful.</returns>
        public static bool RemoveKey(string key)
        {
            key = ConvertRawToKey(key);

            // Check for special keys
            if (key == LNG_NAME_KEY || key == LOCALIZATION_ERROR_KEY)
            {
                Debug.LogWarning($"You can not remove the \"{key}\" key");
                return false;
            }
            if (!DoesContainKey(key)) return false;
            var index = GetIndexOfKey(key);
            
            // Remove the key
            Keys.RemoveAt(index);
            
            // Remove the values
            foreach (var lng in GetAvailableLanguages())
                LocalizedValues[lng].RemoveAt(index);
            
            SaveLocalizationFile();
            return true;
        }

        /// <summary>
        /// Tries to rename the given key, returns true if successful.
        /// </summary>
        /// <param name="key">name of the key to rename</param>
        /// <param name="newName">new desired name of the current key</param>
        /// <returns>true, if rename was successful, else false</returns>
        public static bool RenameKey(string key, string newName)
        {
            // Check if the rename is valid.
            var oldKeyRowIndex = GetIndexOfKey(key);
            var newKeyRowIndex = GetIndexOfKey(newName);
            if (oldKeyRowIndex == -1)
            {
                Debug.LogError("Tried to rename a key that doesn't exist");
                return false;
            }
            if (newKeyRowIndex == -1)
            {
                Debug.LogError("Tried to change the name of a key to an in-use key");
                return false;
            }

            Keys[oldKeyRowIndex] = newName;
            SaveLocalizationFile();
            return true;
        }

        /// <summary>
        /// Adds multiple localized values for a single key. uses <c>Localizator.AddLocalizedValue</c>
        /// </summary>
        /// <param name="key">key which to add the localized value into</param>
        /// <param name="localizedValues">array of localized values to add</param>
        /// <param name="languages">array of languages to add the localized values into, indexes must match with <c>localizedValues</c></param>
        public static void AddLocalizedValues(string key, string[] localizedValues, Language[] languages)
        {
            if (localizedValues.Length != languages.Length)
            {
                Debug.LogError($"Error mass adding localized values to the key: {key}, provided array of localized values and languages must have the same size.");
                return;
            }
            for (var i = 0; i < localizedValues.Length; i++)
                AddLocalizedValue(key, localizedValues[i], languages[i], saveToFile:false);
            
            SaveLocalizationFile();
        }
        
        /// <summary>
        /// Adds the given localized value to the database
        /// </summary>
        /// <param name="key">key which to add the localized value into</param>
        /// <param name="localizedValue">localized value to add</param>
        /// <param name="language">language to add the localized value into</param>
        /// <param name="saveToFile">if true, will save to the file. Only use false if you are mass editing and call <c>Localizator.SaveLocalizationFile</c> afterwards</param>
        public static void AddLocalizedValue(string key, string localizedValue, Language language, bool saveToFile=true)
        {
            // If the user is trying to re-add the same value, return
            if (GetStringWithLanguage(key, language, false) == localizedValue) return;
            
            localizedValue = DisplayLocalizedValToSavedLocalizedVal(localizedValue);
            
            // If the key doesn't exists, create it.
            var keyIndex = GetIndexOfKey(key);
            if (keyIndex == -1) AddKey(key);
            
            // Update the value
            LocalizedValues[language][keyIndex] = localizedValue;

            if (saveToFile) SaveLocalizationFile();
        }

        /// <summary>
        /// Reads each line of the given file, trims it, removes the last line if it's empty, and returns it.
        /// </summary>
        /// <param name="fileName">name of the file to read</param>
        /// <returns>IEnumerable of strings, containing each line</returns>
        private static IEnumerable<string> ReadAllLines(string fileName)
        {
#if UNITY_EDITOR
            var lines = File.ReadAllText(fileName);
#else
            // Resources.Load takes in file name without extension so use .Name here.
            var lines = Resources.Load<TextAsset>(fileName.Replace(".txt", "")).text;
#endif 
            var linesArray = lines.Split('\n');
            return linesArray
                .Select(l => l.Trim())
                // removes the last line if it's empty
                .Where((l, i) => i != linesArray.Length - 1 || !string.IsNullOrWhiteSpace(l)); 
        }

        /// <summary>
        /// Reads the localization files and assigns the appropriate fields.
        /// </summary>
        private static void ReadLocalizationFile()
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            // Check all the languages available
            var fileNames = new List<string>();
#if UNITY_EDITOR
            // When checking the available languages, we can use File operations if we are running the code on build.
            // So we need to cache the languages available to a file in the Resources folder. we use `AVAILABLE_LANGUAGES_NAME` file for it.
            var dirInfo = new DirectoryInfo(GetLanguageFilesPath());
            var files = dirInfo.GetFiles("*.txt");
            fileNames = files.Select(f => f.FullName).ToList();

            // Save the file names without the path.
            File.WriteAllLines(
                Path.Combine(GetLanguageFilesPath(), AVAILABLE_LANGUAGES_NAME),
                files.Select(f => f.Name).Where(f => f != KEYS_FILE_NAME && f != AVAILABLE_LANGUAGES_NAME)
            );
#else
            fileNames = ReadAllLines(AVAILABLE_LANGUAGES_NAME).ToList();

            // AVAILABLE_LANGUAGES_NAME file does not contain the keys file's name so add it manually.
            fileNames.Add(KEYS_FILE_NAME);
#endif

            // Read each file
            localizedValues = new Dictionary<Language, List<string>>();
            foreach (var fileName in fileNames.Where(fileName => !fileName.Contains(AVAILABLE_LANGUAGES_NAME)))
            {
                if (fileName.Contains(KEYS_FILE_NAME))
                    keys = ReadAllLines(fileName).ToList();
                else
                {
                    // Get rid of the path for the ISO code search, if there is a path (there won't be a path before on builds.)
                    var lngName = fileName.Replace(".txt", "").Split("\\").Last();
                    localizedValues[LanguageTools.LanguageFromIsoCode(lngName)] = ReadAllLines(fileName).ToList();
                }
            }
        }

        private static void SaveLocalizationFile()
        {
            // Save to the file only if we are in editor
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            
            // Save the values for each language
            File.WriteAllLines(Path.Combine(GetLanguageFilesPath(), KEYS_FILE_NAME), Keys);
            foreach (var language in localizedValues.Keys)
            {
                File.WriteAllLines(
                    Path.Combine(GetLanguageFilesPath(), $"{LanguageTools.IsoCodeFromLanguage(language)}.txt"),
                    localizedValues[language]
                );
            }
            
            // Cache the languages to `AVAILABLE_LANGUAGES_NAME`
            File.WriteAllLines(
                Path.Combine(GetLanguageFilesPath(), AVAILABLE_LANGUAGES_NAME),
                LocalizedValues.Keys.Select(l => $"{LanguageTools.IsoCodeFromLanguage(l)}.txt")
            );
#endif
            
            // If the change was made in play mode, trigger the event.
            if (Application.isPlaying)
                OnLanguageChange?.Invoke();
        }

        /// <summary>
        /// Tries to change the current language
        /// </summary>
        /// <param name="language">desired language</param>
        /// <returns>returns true if change was successful, else returns false</returns>
        public static bool SetCurrentLanguage(Language language)
        {
            if (!LocalizedValues.ContainsKey(language)) return false;

            currentLanguage = language;
            OnLanguageChange?.Invoke();

            return true;
        }
        
        public static Language GetCurrentLanguage() => currentLanguage;

        /// <summary>
        /// Returns the count of languages that currently exist in the project
        /// </summary>
        /// <returns>Count of languages</returns>
        public static int GetLanguageCount() => LocalizedValues.Keys.Count;

        /// <summary>
        /// Returns a list of languages that currently exist in the project
        /// </summary>
        /// <returns>Array of languages</returns>
        public static Language[] GetAvailableLanguages() => LocalizedValues.Keys.ToArray();

        public static bool DoesContainKey(string key) => GetIndexOfKey(key) != -1;

        /// <summary>
        /// Returns the row index of the given key in the localization csv.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>row index of the key, -1 if could find the key</returns>
        private static int GetIndexOfKey(string key) => Keys.IndexOf(key);

        /// <summary>
        /// Tries adding a new language to the project, returns true if successful
        /// </summary>
        /// <param name="language">Language to add</param>
        /// <returns>true if successful</returns>
        public static bool AddLanguage(Language language)
        {
            if (GetAvailableLanguages().Contains(language)) return false;

            var newValues = new List<string>();
            for (var i = 0; i < Keys.Count; i++) newValues.Add("");
            
            LocalizedValues.Add(language, newValues);
            
            SaveLocalizationFile();
            return true;
        }

        /// <summary>
        /// Tries to removes the given language from the project, return true if successful
        /// </summary>
        /// <param name="language">language to remove</param>
        public static bool RemoveLanguage(Language language)
        {
            if (!GetAvailableLanguages().Contains(language)) return false;

            LocalizedValues.Remove(language);
            SaveLocalizationFile();
            return true;
        }
        
        /// <summary>
        /// Reads all files again and refreshes Unity's asset database.
        /// </summary>
        public static void RefreshAll(bool forceReadLocFile=false)
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
            if (forceReadLocFile) ReadLocalizationFile();
        }

        public delegate void LanguageChange();
        public static event LanguageChange OnLanguageChange;

        /// <summary>
        /// Returns dictionary where each key is the language and the value is the localization percentage of the language.
        /// </summary>
        /// <returns>a dictionary where each key is the language and the value is the localization percentage of the language.</returns>
        public static Dictionary<Language, float> GetLocalizationPercentages()
        {
            var dict = new Dictionary<Language, float>();
            
            foreach (var language in GetAvailableLanguages())
            {
                var lines = LocalizedValues[language];
                var localizationPercentage = 100f * lines.Count(line => !string.IsNullOrWhiteSpace(line)) / lines.Count;

                // Round `localizationPercentage` to 2 decimal places
                dict.Add(language, (float) Math.Round((decimal) localizationPercentage, 2));
            }

            return dict;
        }
    }
}
