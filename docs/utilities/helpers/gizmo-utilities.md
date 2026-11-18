# GizmoUtilities

Call these from `OnDrawGizmos` or `OnDrawGizmosSelected`. They use the current `Gizmos.color`.

```csharp
void OnDrawGizmosSelected()
{
    Gizmos.color = Color.yellow;
    GizmoUtilities.DrawArrow2D(transform.position, target.position, arrowHeadLength: 0.25f);
    GizmoUtilities.DrawCross2D(spawnPoint, length: 0.2f);
}
```

`DrawArrow` and `DrawCross` are the 3D versions and take a normal vector that sets the plane they're drawn on.
