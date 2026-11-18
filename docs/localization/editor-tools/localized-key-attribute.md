# LocalizedKeyAttribute

Put `[LocalizedKey]` on any serialized `string` field to turn it into a key picker in the Inspector:

```csharp
using Espale.Localization;
using UnityEngine;

public class DialogueLine : MonoBehaviour
{
    [LocalizedKey] public string lineKey;
}
```

The field then:

- shows a warning if the key doesn't exist,
- suggests matching keys as you type, which you can click to fill in,
- has a button to edit the key, or to create it if it doesn't exist, in the **Key Editor**.

`TextLocalizer` and several UI package components use this attribute.
