# AudioSourceRandomizer

<xref:Espale.Utilities.Components.Randomizers.AudioSourceRandomizer> (`Espale.Utilities.Components.Randomizers`) gives an `AudioSource` a random clip, volume and pitch, so repeated sounds don't sound identical.

| Field | Description |
| --- | --- |
| **Randomize On Start** | Randomize once in `Start`. |
| **Randomize Clip**, **Clip Options** | Pick a random clip from the list. |
| **Randomize Volume**, **Volume Range** | Pick a random volume in the range. |
| **Randomize Pitch**, **Pitch Range** | Pick a random pitch in the range. |

Call `Randomize()` to pick new values, for example right before each `Play()`.
