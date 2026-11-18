# UIAudioManager

<xref:Espale.UI.UIAudioManager> has one static method that plays a clip once on a temporary `AudioSource`, which is destroyed when the clip ends. `SelectableAudioSource` uses it, and you can call it from your own UI code:

```csharp
using Espale.UI;

UIAudioManager.PlayOneShotAudio(confirmClip, 0.8f, uiMixerGroup);   // the mixer group can be null
```

The temporary audio source is created at the scene origin with default settings, so it plays as 2D sound.
