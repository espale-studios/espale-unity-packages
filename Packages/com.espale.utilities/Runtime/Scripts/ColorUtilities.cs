using UnityEngine;

namespace Espale.Utilities
{
    public static class ColorUtilities
    {
        public static Color orange = new (1f, .5f, 0f);
        public static Color teal = new (0f, .5f, .5f);
        public static Color purple = new (.5f, 0f, 1f);
        public static Color pink = new (244f / 255, 143f / 255, 177f / 255);
        public static Color pineGreen = new (0f, .35f, 0f);

        public static Color transparent = Color.clear;
        
        public static Color emerald = new (46f / 255, 204f / 255, 113f / 255);
        public static Color amethyst = new (155f / 255f, 89f / 255f, 182f / 255f);
        public static Color silver = new (189f / 255f, 195f / 255f, 199f / 255f);
        public static Color gold = new (255f / 255f, 215f / 255f, 0f);
        public static Color bronze = new (205f / 255f, 127f / 255f, 50f / 255f);

        public static Color darkGray = new Color(.7f, .7f, .7f, 1f);
        public static Color darkGrey = darkGray;

        /// <summary>
        /// Changes the brightness of the color with the given value
        /// </summary>
        /// <param name="color"></param>
        /// <param name="brightness">Change in brightness of the color, must be between -1 and 1</param>
        /// <returns></returns>
        public static Color Brighten(Color color, float brightness)
        {
            var r = 1f + brightness;
            return new Color(
                Mathf.Clamp01(color.r * r),
                Mathf.Clamp01(color.g * r),
                Mathf.Clamp01(color.b * r),
                color.a
            );
        }
        
        /// <summary>
        /// Converts the given color to grayscale using Unity's <c>Color.grayscale</c> (<i>Luminosity Method</i>).
        /// Formula: <c>grayscale = 0.299 * R + 0.587 * G + 0.114 * B</c>
        /// </summary>
        /// <param name="color">Original color</param>
        /// <returns>Grayscale color with the original alpha</returns>
        public static Color Grayscale(Color color)
        {
            var grayscale = color.grayscale;
            return new Color(grayscale, grayscale, grayscale, color.a);
        }

        /// <summary>
        /// Changes the darkness of the color with the given value
        /// </summary>
        /// <param name="color"></param>
        /// <param name="darkness">Change in darkness of the color, must be between -1 and 1</param>
        /// <returns></returns>
        public static Color Darken(Color color, float darkness) => Brighten(color, -darkness);
        
        /// <summary>
        /// Replaces the alpha chanel of the color with the given value
        /// </summary>
        /// <param name="color">Original color</param>
        /// <param name="alpha">New alpha chanel value</param>
        /// <returns></returns>
        public static Color GetColorWithAlpha(Color color, float alpha) => new Color(color.r, color.g, color.b, alpha);
        
        /// <summary>
        /// Replaces the red chanel of the color with the given value
        /// </summary>
        /// <param name="color">Original color</param>
        /// <param name="red">New red chanel value</param>
        /// <returns></returns>
        public static Color GetColorWithRed(Color color, float red) => new Color(red, color.g, color.b, color.a);
        
        /// <summary>
        /// Replaces the green chanel of the color with the given value
        /// </summary>
        /// <param name="color">Original color</param>
        /// <param name="green">New green chanel value</param>
        /// <returns></returns>
        public static Color GetColorWithGreen(Color color, float green) => new Color(color.r, green, color.b, color.a);
        
        /// <summary>
        /// Replaces the blue chanel of the color with the given value
        /// </summary>
        /// <param name="color">Original color</param>
        /// <param name="blue">New blue chanel value</param>
        /// <returns></returns>
        public static Color GetColorWithBlue(Color color, float blue) => new Color(color.r, color.g, blue, color.a);
    }
}
