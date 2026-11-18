# CollectionUtilities

<xref:Espale.Utilities.CollectionUtilities> works on any `IEnumerable<T>`:

```csharp
int[] unique = CollectionUtilities.RemoveDuplicatesFrom(new[] { 1, 2, 2, 3 }); // { 1, 2, 3 }, order kept
var randomEnemy = CollectionUtilities.GetRandomItemFrom(enemies);
```
