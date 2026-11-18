# ArcCollider2D

<xref:Espale.Utilities.Components.Colliders.ArcCollider2D> (`Espale.Utilities.Components.Colliders`) generates an arc-shaped `EdgeCollider2D`, such as a curved wall or a ring segment.

It requires an `EdgeCollider2D` on the same GameObject. The collider updates live in edit mode while you change the values. In play mode, call `UpdateCollider()` after changing them from code.

| Field | Description |
| --- | --- |
| **Arc Offset** | Center of the arc, relative to the GameObject. |
| **Distance Between Points** | Spacing of the collider's points along the arc. Smaller values give a smoother arc. |
| **Start Angle** | Angle in degrees where the arc starts (0 is to the right). |
| **Arc Angle** | How far the arc goes, in degrees. Negative values go clockwise. |
| **Arc Radius** | Radius of the arc. |
| **Thickness** | Thickness of the arc. The collider's edge radius is set to half of it. |
