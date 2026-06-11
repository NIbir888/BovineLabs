// <copyright file="MyGraphInstanceEditor.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Editor
{
    using BovineLabs.Grove.Editor;
    using BovineLabs.Grove.Sample.Authoring.Core;
    using UnityEditor.AssetImporters;

    [ScriptedImporter(1, MyGraphGraph.AssetExtension)]
    public class MyGraphImporter : GroveImporter<MyGraphAuth>
    {
    }
}
