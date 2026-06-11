// <copyright file="MyGraphAuthoring.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Authoring.Core
{
    using BovineLabs.Grove.Authoring;
    using BovineLabs.Grove.Sample.Data.Core;
    using Unity.Entities;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class MyGraphAuthoring : GraphAuthoring<MyGraphAuth, MyGraph>
    {
        protected override TransformUsageFlags EntityTransformUsageFlags => TransformUsageFlags.Dynamic;
    }
}
