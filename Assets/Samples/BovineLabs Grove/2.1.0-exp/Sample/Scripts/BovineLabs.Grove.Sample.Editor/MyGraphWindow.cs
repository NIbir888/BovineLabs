// <copyright file="MyGraphWindow.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Editor
{
    using System;
    using BovineLabs.Grove.Editor;
    using Unity.GraphToolkit.Editor;
    using UnityEditor;

    [Serializable]
    [Graph(AssetExtension)]
    public class MyGraphGraph : GroveGraph
    {
        public const string AssetExtension = "mygraph";

        [MenuItem("Assets/Create/BovineLabs/Sample Graph")]
        private static void CreateAssetFile()
        {
            GraphDatabase.PromptInProjectBrowserToCreateNewAsset<MyGraphGraph>("MyGraph");
        }
    }
}
