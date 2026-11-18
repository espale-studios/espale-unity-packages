# Debug displays

Two small overlays for development builds and playtests. Both are [singletons](../../utilities/singleton.md): they survive scene loads, and extra copies in other scenes destroy themselves. Place one in your first scene.

> [!NOTE]
> Their toggle keys use the legacy Input Manager (`UnityEngine.Input`), which is fine for debug tools. In **Project Settings > Player > Active Input Handling**, choose **Both** if your project otherwise only uses the new Input System, or the toggle keys won't work.

| Component | Description |
| --- | --- |
| [FPSDisplayer](fps-displayer.md) | Shows the frame rate and its averages. |
| [VersionDisplayer](version-displayer.md) | Shows the game version on a colored label. |
