// <copyright file="MyGraph.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Data.Core
{
    using Unity.Entities;

    public struct MyGraph : IComponentData, IGraphReference
    {
        /// <inheritdoc />
        public BlobAssetReference<GraphData> Graph { get; set; }
    }
}
