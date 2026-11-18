# RandomUtilities

<xref:Espale.Utilities.RandomUtilities> wraps `UnityEngine.Random` with higher-level helpers.

```csharp
// A random value between a and b. Overloads exist for float, Vector2 (as a range), Vector2, Vector3 and Color.
float speed = RandomUtilities.RandomLerp(2f, 4f);
float pitch = RandomUtilities.RandomLerp(new Vector2(0.9f, 1.1f));
Color tint  = RandomUtilities.RandomLerp(Color.white, Color.yellow);

// Chance rolls.
bool crit = RandomUtilities.RandomPercentageChance(15f);   // true about 15% of the time
bool hit  = RandomUtilities.RandomChance(1f, 3f);          // true about 1 in 3 times

// Random unit vectors. min and max bound each axis before normalizing.
Vector2 dir = RandomUtilities.RandomVector2();
Vector3 dir3 = RandomUtilities.RandomVector3();

// Arrays of random numbers.
float[] weights = RandomUtilities.RandomFloatArray(5, 0f, 1f);
int[] rolls = RandomUtilities.RandomIntArray(3, 1, 7);     // uses Random.Range(int, int), so 7 is excluded

// Picking and shuffling (Fisher-Yates).
var item = RandomUtilities.PickRandomItemFrom(lootTable);  // List<T> or T[]
RandomUtilities.Shuffle(deck);                             // shuffles in place
var shuffledCopy = RandomUtilities.GetShuffled(deck);      // returns a new shuffled copy
```
