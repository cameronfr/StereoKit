﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StereoKit
{
    /// <summary>A line drawing class! This is an easy way to visualize lines or relationships 
    /// between objects. The current implementatation uses a quad strip that always faces the 
    /// user, via vertex shader manipulation.</summary>
    public static class Lines
    {
        /// <summary>Adds a line to the environment for the current frame.</summary>
        /// <param name="start">Starting point of the line.</param>
        /// <param name="end">End point of the line.</param>
        /// <param name="color">Color for the line, this is embedded in the vertex color of the line.</param>
        /// <param name="thickness">Thickness of the line in meters.</param>
        public static void Add(Vec3 start, Vec3 end, Color32 color, float thickness)
            =>NativeAPI.line_add(start, end, color, color, thickness);

        /// <summary>Adds a line to the environment for the current frame.</summary>
        /// <param name="start">Starting point of the line.</param>
        /// <param name="end">End point of the line.</param>
        /// <param name="colorStart">Color for the start of the line, this is embedded in the vertex color of the line.</param>
        /// <param name="colorEnd">Color for the end of the line, this is embedded in the vertex color of the line.</param>
        /// <param name="thickness">Thickness of the line in meters.</param>
        public static void Add(Vec3 start, Vec3 end, Color32 colorStart, Color32 colorEnd, float thickness)
            => NativeAPI.line_add(start, end, colorStart, colorEnd, thickness);

        /// <summary>Adds a line based on a ray to the environment for the current frame.</summary>
        /// <param name="ray">The ray we want to visualize!</param>
        /// <param name="length">How long should the ray be? Actual length will be ray.direction.Magnitude * length.</param>
        /// <param name="color">Color for the line, this is embedded in the vertex color of the line.</param>
        /// <param name="thickness">Thickness of the line in meters.</param>
        public static void Add(Ray ray, float length, Color32 color, float thickness)
            => NativeAPI.line_add(ray.position, ray.position+ray.direction*length, color, color, thickness);

        public static void Add(in LinePoint[] points)
            => NativeAPI.line_add_listv(points, points.Length);

        /// <summary>Displays an RGB/XYZ axis widget at the pose! Note that this draws lines
        /// along 'Forward' vectors for each axis, not necessarily in the axis positive direction.</summary>
        /// <param name="atPose">What position and orientation do we want this axis widget at?</param>
        /// <param name="size">How long should the widget lines be, in meters?</param>
        public static void AddAxis(Pose atPose, float size = U.cm)
        {
            Add(atPose.position, atPose.position + (atPose.orientation * Vec3.Forward) * size, new Color32(0, 0, 255, 255), size*0.1f);
            Add(atPose.position, atPose.position + (atPose.orientation * Vec3.Right)   * size, new Color32(255, 0, 0, 255), size*0.1f);
            Add(atPose.position, atPose.position + (atPose.orientation * Vec3.Up)      * size, new Color32(0, 255, 0, 255), size*0.1f);
        }
    }
}
