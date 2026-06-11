// <copyright file="MyContext.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

#pragma warning disable CS9084 // Struct member returns 'this' or other instance members by reference
namespace BovineLabs.Grove.Sample
{
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Utility;
    using Unity.Transforms;

    public unsafe partial struct MyContext : IContext<MyContext>
    {
        public ComponentContainer<LocalTransform> LocalTransform;

        public BufferContainer<GroveState> GroveStates;

        ref BufferContainer<GroveState> IContext<MyContext>.GroveStates => ref this.GroveStates;
    }
}
