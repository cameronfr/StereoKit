﻿namespace StereoKit
{
    /// <summary>A collection of functions for rendering and working with text.</summary>
    class Text
    {
        /// <summary>Create a text style for use with other text functions! A text style
        /// is a font plus size/color/material parameters, and are used to keep text looking
        /// more consistent through the application by encouraging devs to re-use styles 
        /// throughout the project.</summary>
        /// <param name="font">Font asset you want attached to this style.</param>
        /// <param name="characterHeight">Height of a text glpyh in meters. TODO: find what text glyph specifically!</param>
        /// <param name="material">Which material should be used to render the text with?</param>
        /// <param name="color">The color of the text style. This will be embedded in the vertex color of the text mesh.</param>
        public static void MakeStyle(Font font, float characterHeight, Material material, Color32 color)
            => NativeAPI.text_make_style(font._fontInst, characterHeight, material._materialInst, color);

        /// <summary>Renders text at the given location! Must be called every frame you want this text to be visible.</summary>
        /// <param name="text">What text should be drawn?</param>
        /// <param name="transform">A Matrix representing the transform of the text mesh! Try Matrix.TRS.</param>
        /// <param name="style">Style information for rendering, see Text.MakeStyle or the TextStyle object.</param>
        /// <param name="position">How should the text's bounding rectangle be positioned relative to the transform?</param>
        /// <param name="align">How should the text be aligned within the text's bounding rectangle?</param>
        /// <param name="offX">An additional offset on the X axis.</param>
        /// <param name="offY">An additional offset on the Y axis.</param>
        /// <param name="offZ">An additional offset on the Z axis.</param>
        public static void Add(string text, Matrix transform, TextStyle style, TextAlign position = TextAlign.Center, TextAlign align = TextAlign.Center, float offX=0, float offY=0, float offZ=0) 
            => NativeAPI.text_add_at(text, ref transform, style.id, position, align, offX, offY, offZ);

        /// <summary>Renders text at the given location! Must be called every frame you want this text to be visible.</summary>
        /// <param name="text">What text should be drawn?</param>
        /// <param name="transform">A Matrix representing the transform of the text mesh! Try Matrix.TRS.</param>
        /// <param name="position">How should the text's bounding rectangle be positioned relative to the transform?</param>
        /// <param name="align">How should the text be aligned within the text's bounding rectangle?</param>
        /// <param name="offX">An additional offset on the X axis.</param>
        /// <param name="offY">An additional offset on the Y axis.</param>
        /// <param name="offZ">An additional offset on the Z axis.</param>
        public static void Add(string text, Matrix transform, TextAlign position = TextAlign.Center, TextAlign align = TextAlign.Center, float offX = 0, float offY = 0, float offZ = 0)
            => NativeAPI.text_add_at(text, ref transform, -1, position, align, offX, offY, offZ);

        /// <summary>Sometimes you just need to know how much room some text takes up! This finds
        /// the size of the text in meters, when using the indicated style!</summary>
        /// <param name="text">Text you want to find the size of.</param>
        /// <param name="style">The visual style of the text, see Text.MakeStyle or the TextStyle object for more details.</param>
        /// <returns>The width and height of the text in meters.</returns>
        public static Vec2 Size(string text, TextStyle style)
            => NativeAPI.text_size(text, style.id);

        /// <summary>Sometimes you just need to know how much room some text takes up! This finds
        /// the size of the text in meters, when using the indicated style!</summary>
        /// <param name="text">Text you want to find the size of.</param>
        /// <returns>The width and height of the text in meters.</returns>
        public static Vec2 Size(string text)
            => NativeAPI.text_size(text, -1);
    }
}