// <copyright file="SampleStateNodes.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Editor
{
    using System;
    using BovineLabs.Grove.Editor.States;
    using BovineLabs.Grove.Sample.Data.Core;
    using Unity.GraphToolkit.Editor;

    [Serializable]
    [UseWithGraph(typeof(MyGraphGraph))]
    public sealed class SampleStateSetNode : StateSetNode<SampleState>
    {
        protected override short CurrentStateKey => (short)StateKeys.CurrentState;
    }

    [Serializable]
    [UseWithGraph(typeof(MyGraphGraph))]
    public sealed class SampleStateIfNode : StateIfNode<SampleState>
    {
        protected override short CurrentStateKey => (short)StateKeys.CurrentState;
    }

    [Serializable]
    [UseWithGraph(typeof(MyGraphGraph))]
    public sealed class SampleStateSelectNode : StateSelectNode<SampleState>
    {
        protected override short CurrentStateKey => (short)StateKeys.CurrentState;

        protected override short PreviousStateKey => (short)StateKeys.PreviousState;
    }
}
