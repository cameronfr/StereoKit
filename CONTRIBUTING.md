# Contributing to StereoKit

StereoKit is Open Source, so if you have changes or improvements you'd like to add, we'll be happy to integrate it! This document is here to help smooth out the process, and point you in the right direction.

**Table of Contents**

- Documentation
    - General Notes
    - Commentdocs
    - Samples
    - Guides
    - Screenshots
- Project Overview
    - StereoKitC
    - StereoKit
- Coding Guidelines

## Documentation

If you want to add a sample, correct some documentation, or make a guide, it's helpful to know about StereoKit's documentation system! The [stereokit.net](https://stereokit.net) site (hosted from the [/docs](/docs) folder) is almost entirely generated by the [StereoKitDocumenter](/Tools/StereoKitDocumenter) project! So you probably should never be modifying things in /docs.

StereoKitDocumenter pulls documentation from a few different places when assembling the documentation site:

- Commentdocs from the C# code in [/StereoKit](/StereoKit)
- Samples and Guides from the code in the [StereoKitTest](/Examples/StereoKitTest) project
- Screenshots generated by running [StereoKitTest](/Examples/StereoKitTest) in test mode

### General Notes

For documentation and comments, it is strongly encouraged to limit line length to 80 characters! Lines that are too long become difficult for users to read. This is less of a problem for the final generated docs, but good to adhere to for people spelunking the code. I use the [Editor Guidelines](https://marketplace.visualstudio.com/items?itemName=PaulHarrington.EditorGuidelinesPreview) VS extension to make this easier for myself.

### Commentdocs

When writing commentdocs, try to tell the developer something they don't already know when looking at the function signature! Maybe some implementation details, performance implications, potential reasons for failure.

Feel free to use markdown in your commentdocs as well. Be aware of newlines (and the 80 char guideline) when doing this, as newlines tell markdown how to interpret the text.

### Samples

These are only available in the StereoKitTest project! You can set up samples on their own in  [/Examples/StereoKitTest/Docs](/Examples/StereoKitTest/Docs), or as part of an interactive demo, in [/Examples/StereoKitTest/Demos](/Examples/StereoKitTest/Demos). The documenter tool will search comments for a set of `:CodeSample:` and `:End:` tags that look like this:

`:CodeSample: [Class|Class.Method|Class.Property|...] [as many as you want]`

```csharp
/// :CodeSample: Vec3 Vec3.Distance
/// ### Distance between two points
/// 
/// Distance does use a Sqrt call, so only use it if you definitely
/// need the actual distance. Otherwise, consider DistanceSq.
Vec3  pointA   = new Vec3(3,2,5);
Vec3  pointB   = new Vec3(3,2,8);
float distance = Vec3.Distance(pointA, pointB);
/// :End:
```

`:CodeSample:` can be followed by one or more classes, methods, fields or properties separated by spaces, so they show up as samples in more than one place.

### Guides

Guides are standalone documents not attached to any particular part of the API. These are great for explaining a concept, making a tutorial, or whatever else! These are also only available in the StereoKitTest project, and mostly reside over here [/Examples/StereoKitTest/Guides](/Examples/StereoKitTest/Guides).

A guide uses tags similar to the way `:CodeSample:` does. It's good to note that matching samples _and_ guides are appended to eachother, so you can exclude parts of the code you don't want in the guide. Here's a small example, but check one of the existing guides as a more complete example.

`:CodeSample: [Category] [Sort Index] [Name]`

```csharp
/// :CodeDoc: Guides 5 Drawing
/// # Drawing content with StereoKit
/// 
/// ...
/// 
/// :End:

// doesn't include this in the guide
namespace Guides {
    /// :CodeDoc: Guides Drawing
    /// ...
    /// :End:
```

`:CodeSample:` should always be followed by a category and name, but subsequent `:CodeSample:`s can omit the sort index. Note that right now, categories are only partially implemented. I'd recommend using "Guides", and add an issue if you're interested in the option for a new folder.

### Screenshots

If you want to add a screenshot, these are generated via code every major release along with the docs! You should set up your scene in a ITest class, and make a call to `Tests.Screenshot`. Then you can refer to this screenshot with markdown like so:

```csharp
Tests.Screenshot("Drawing_MeshLooksLike.jpg", 600, 400, V.XYZ(0, 0, 0.12f), Vec3.Zero);
```

```markdown
![Meshes are made from triangles!]({{site.screen_url}}/Drawing_MeshLooksLike.jpg)
```

The StereoKitTest project does have some tools available in the debug menu behind the user (`Print Screenshot Pose`) that can help line up screenshots, and print out code you can use.

## Project Overview

If you want to work on features or bugfixes, you'll need to know where to look! StereoKit is composed of two major components: [StereoKitC](/StereoKitC), which contains the bulk of StereoKit's functionality written in C styled C++, and [StereoKit](/StereoKit), a C# netstandard2.0 library that provides 1st-class idiomatic C# bindings to the native library, as well as a few high-level features to make developer's lives easier.

When adding a feature, this often means working in two different languages, adding code to both StereoKitC as well as StereoKit!

### StereoKitC

At its core, StereoKit is really just a native C/C++ library. The public interface is pure C, and capable of talking with many different languages. While StereoKit only provides one official binding for C#, StereoKit is still designed to be 100% usable from the native level, for any future bindings or existing 3rd party bindings.

Most bugs should be fixable just by working with this project. There are occasionally issues that can arise when marshalling data up to the C# layer, but these are less common.

### StereoKit

This C# project should mostly be about making the best C# bindings possible to StereoKitC! This can mean a reasonable chunk of code sometimes, and occasionally adding a few things to take advantage of code features that C/C++ can't even dream of. But for the most part, this is just idiomatic wrappers, and extensive commentdocs.

There is a StereoKit.Framework namespace that allows for some room to add high-level functionality to StereoKit's C# layer, but this should generally be kept to a minimum.

## Coding Guidelines