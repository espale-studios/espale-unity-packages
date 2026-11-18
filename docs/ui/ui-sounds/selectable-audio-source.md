# SelectableAudioSource

Add <xref:Espale.UI.SelectableAudioSource> to any `Selectable` (a button, toggle, slider, dropdown and so on) to play a sound when it's highlighted, selected or pressed. The **Button**, **Toggle** and **Multi Select Dropdown** prefabs already have one, set up with the included click sounds.

| Field | Plays when |
| --- | --- |
| **Highlighted Audio Data** | The pointer moves over the element. |
| **Selected Audio Data** | The element is selected, for example by gamepad or keyboard navigation. |
| **Pressed Audio Data** | The element is clicked or submitted. |
| **Mixer Group** | Optional `AudioMixerGroup` to route the sounds through, such as a UI or SFX group. |

Each audio data field is an `AudioUtilities.AudioData` from [Utilities](../../utilities/helpers/audio-utilities.md): a clip and a volume. Leave a clip empty to play nothing for that event. Nothing plays while the element isn't interactable.

The package includes click and whoosh sounds in `Runtime/UI SFXs` that you can use or replace.
