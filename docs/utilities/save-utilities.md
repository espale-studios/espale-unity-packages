# SaveUtilities

<xref:Espale.Utilities.SaveUtilities> saves single values to files and reads them back. It's meant for small amounts of data such as settings, unlocked levels or a high score.

```csharp
using Espale.Utilities;

// Save
SaveUtilities.SaveValue("settings/music_volume", 0.8f);

// Load, with a default for when the file doesn't exist yet
float volume = SaveUtilities.ReadValue("settings/music_volume", 1f);

// Delete
SaveUtilities.ClearValue("settings/music_volume");
```

## Where the files go

Each value is written to its own file at:

```
<Application.persistentDataPath>/saves/<fileName>.save
```

- `fileName` can contain `/` to group saves into folders. Missing folders are created when you save.
- Saving `null` logs a warning in the editor and writes nothing.
- `ReadValue` returns `defaultValue` when the file doesn't exist.

## What you can save

Values are written with .NET's `BinaryFormatter`, so the type must be serializable: built-in types such as `int`, `float`, `bool` and `string`, arrays and lists of them, or your own classes and structs marked `[System.Serializable]`.

```csharp
[System.Serializable]
public class Progress
{
    public int level;
    public float bestTime;
    public string[] unlockedSkins;
}

SaveUtilities.SaveValue("progress", progress);
var loaded = SaveUtilities.ReadValue("progress", new Progress());
```

> [!WARNING]
> Unity types like `Vector2`, `Color` or `GameObject` are not marked serializable for `BinaryFormatter`. Store their fields (for example `x` and `y`) in your own serializable type instead.

> [!CAUTION]
> `BinaryFormatter` can run code while it reads a file. Only load save files your game wrote itself, and never load files downloaded from other players or the internet.

## Keep save files compatible

Players keep their save files when they update your game. If you change a saved class, old files must still load:

- Add new fields instead of renaming or removing old ones.
- Keep the class name and namespace the same.
- Use a version field if you need to migrate data. `SaveUtilities.SemanticVersionData` can compare `major.minor.patch` version strings (each part must be a whole number, so `Application.version` needs that format too):

```csharp
var saved = new SaveUtilities.SemanticVersionData("1.2.0");
var current = new SaveUtilities.SemanticVersionData(Application.version);

if (saved.CompareTo(current) < 0)
{
    // The save was written by an older version of the game.
}
```
