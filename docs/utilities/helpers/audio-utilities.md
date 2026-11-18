# AudioUtilities

```csharp
// Convert a linear volume in [0, 1] to decibels for an AudioMixer. Very small values return -80 dB.
mixer.SetFloat("MasterVolume", AudioUtilities.FloatToDecibel(volumeSlider.value));

// And back.
float linear = AudioUtilities.DecibelToFloat(-6f);
```

<xref:Espale.Utilities.AudioUtilities> also defines two serializable structs you can use as inspector fields:

- `AudioUtilities.AudioData`: a clip and a volume.
- `AudioUtilities.RandomAudioData`: several clip options and a volume. `AudioUtilities.ChooseRandomAudioData(randomData)` turns it into an `AudioData` with a random clip.

```csharp
public AudioUtilities.RandomAudioData footsteps;

void PlayFootstep()
{
    var data = AudioUtilities.ChooseRandomAudioData(footsteps);
    source.PlayOneShot(data.clip, data.volume);
}
```
