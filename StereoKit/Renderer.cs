﻿using System;
using System.Runtime.InteropServices;

namespace StereoKit
{
    public class Renderer
    {
        #region Imports
        [DllImport(NativeLib.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void render_add_mesh(IntPtr mesh, IntPtr material, IntPtr transform);
        [DllImport(NativeLib.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void render_add_model(IntPtr model, IntPtr transform);
        [DllImport(NativeLib.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void render_set_camera(IntPtr cam, IntPtr cam_transform);
        #endregion

        public static void Add(Mesh mesh, Material material, Transform transform)
        {
            render_add_mesh(mesh._meshInst, material._materialInst, transform._transformInst);
        }
        public static void Add(Model model, Transform transform)
        {
            render_add_model(model._modelInst, transform._transformInst);
        }
        public static void SetCamera(Camera camera, Transform cameraTransform)
        {
            render_set_camera(camera._cameraInst, cameraTransform._transformInst);
        }
    }
}