# StringUtilities

```csharp
// Inserts line breaks between words once a line reaches maxLineLength characters (25 by default).
string wrapped = StringUtilities.IncreaseLinesIfNecessary(longText, 30);

// "1.5 × 10<sup>-05</sup>" for numbers that ToString() writes in E notation, otherwise the plain number.
string sci = StringUtilities.NumToScientificNotation(0.000015f);
```

The scientific notation output uses rich text `sup` tags, which TextMeshPro renders as superscript.
