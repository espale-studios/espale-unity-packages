# FPSDisplayer

<xref:Espale.UI.FPSDisplayer> shows the current frame rate, optionally with the average since the game started and the average since the current scene loaded, for example `FPS: 60 | Avg: 58 | Scene Avg: 59`.

| Field | Description |
| --- | --- |
| **Toggled** | Show the counter when the game starts. |
| **Toggle Key** | Key that shows and hides the counter. `F6` by default. |
| **Display Average** | Add the average since the game started. |
| **Display Scene Specific** | With **Display Average**, also add the average for the current scene. |
| **Text Color** | Color of the text. Change it from code with `UpdateColor(color)`. |
