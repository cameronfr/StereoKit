﻿using System;
using System.Runtime.InteropServices;

namespace StereoKit
{
    public class Tex2D
    {
        #region Imports
        [DllImport(NativeLib.DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr tex2d_find(string id);
        [DllImport(NativeLib.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr tex2d_create();
        [DllImport(NativeLib.DllName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr tex2d_create_file(string file);
        [DllImport(NativeLib.DllName, CallingConvention = CallingConvention.Cdecl)]
        static extern void tex2d_release(IntPtr tex);
        #endregion

        internal IntPtr _texInst;
        public Tex2D()
        {
            _texInst = tex2d_create();
        }
        private Tex2D(IntPtr tex)
        {
            _texInst = tex;
            if (_texInst == IntPtr.Zero)
                Log.Write(LogLevel.Warning, "Received an empty texture!");
        }
        public Tex2D(string file)
        {
            _texInst = tex2d_create_file(file);
        }
        ~Tex2D()
        {
            if (_texInst != IntPtr.Zero)
                tex2d_release(_texInst);
        }

        public static Tex2D Find(string id)
        {
            IntPtr tex = tex2d_find(id);
            return tex == IntPtr.Zero ? null : new Tex2D(tex);
        }
    }
}