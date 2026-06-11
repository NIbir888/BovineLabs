// <copyright file="ExecuteSetDirectionNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Editor
{
    using System;
    using BovineLabs.Grove.Editor;
    using BovineLabs.Grove.Sample.Authoring.Nodes.Execution;
    using Unity.GraphToolkit.Editor;

    [Serializable]
    [UseWithGraph(typeof(MyGraphGraph))]
    public sealed class ExecuteSetDirectionNode : GroveExecutionEmptyNode<ExecuteSetDirectionAuth>
    {
    }
}
