# ScrollViewAutoScroller

Add <xref:Espale.UI.ScrollViewAutoScroller> to a GameObject with a `ScrollRect`. When the selected object changes to one of the scroll view's items, the content scrolls smoothly until that item is in view.

It expects the default Scroll View hierarchy, **Scroll View > Viewport > Content > items**, and treats each direct child of **Content** that has a `Selectable` as an item.

| Field | Description |
| --- | --- |
| **Is Active** | Turn auto scrolling on or off. |
| **Update Vertical Nav On Enable** | When enabled, set explicit up and down navigation between the items, so navigation follows the list order. |
| **Loopable Navigation** | With the option above, pressing down on the last item selects the first one, and up on the first selects the last. |
| **Use Depth Based Search For Selectables** | Also find a `Selectable` inside each item's children, for items whose button isn't the item itself. |
| **Use Unscaled Time** | Keep scrolling while `Time.timeScale` is 0, for example in a pause menu. |
| **Auto Scroll Speed** | How fast the list scrolls (1 to 100). |

Call `UpdateChildren()` after adding or removing items at runtime, and `UpdateVerticalNav()` if you also want to rebuild the navigation. `ScrollToIndex(i)` scrolls to an item yourself.

> [!NOTE]
> Auto scrolling is vertical only.
