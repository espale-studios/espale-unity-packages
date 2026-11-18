# Singleton&lt;T&gt;

<xref:Espale.Utilities.Singleton`1> is a base class for `MonoBehaviour`s that should exist only once and survive scene loads, such as an audio manager or a game state manager.

```csharp
using Espale.Utilities;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static GameManager Instance => GetInstance();

    public int score;

    protected override void OnSceneChange(Scene current, Scene next)
    {
        // Find scene objects again here: references to objects in the old scene are gone.
    }
}
```

## How it behaves

- In `Awake`, the first instance becomes the singleton and is marked `DontDestroyOnLoad`.
- Any later instance (for example the copy in a scene you load again) logs a warning in the editor and destroys its own `GameObject`.
- `GetInstance()` is `protected`, so you decide how to expose it. A static `Instance` property, as above, is the usual way. If no instance exists it logs an error in the editor and returns `null`.
- `OnSceneChange(current, next)` runs once in `Awake` and again every time the active scene changes. Override it to refresh scene references.

> [!IMPORTANT]
> `Singleton<T>` uses `Awake`, `OnEnable` and `OnDisable`. If your class needs them too, hide them with `new` and call the base version first:
>
> ```csharp
> private new void Awake()
> {
>     base.Awake();
>     // your code
> }
> ```

## Creating the instance

Place the singleton in your first scene, or in every scene you might start the game from while testing. The singleton doesn't create itself: if no instance has been placed, `GetInstance()` returns `null`.
