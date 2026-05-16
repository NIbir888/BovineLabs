// <copyright file="PhysicsToolbarSystem.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

#if UNITY_PHYSICS
namespace BovineLabs.Quill.Debug.Physics
{
    using BovineLabs.Anchor.Debug.Toolbar;
    using BovineLabs.Anchor.Debug.ViewModels;
    using BovineLabs.Anchor.Debug.Views;
    using Unity.Burst;
    using Unity.Entities;
    using Unity.Physics.Systems;

    [UpdateInGroup(typeof(ToolbarSystemGroup))]
    public partial struct PhysicsToolbarSystem : ISystem, ISystemStartStop
    {
        private ToolbarHelper<PhysicsToolbarView, PhysicsToolbarViewModel, PhysicsToolbarViewModel.Data> toolbar;

        /// <inheritdoc />
        public void OnCreate(ref SystemState state)
        {
            if (state.World.GetExistingSystem<BuildPhysicsWorld>() == SystemHandle.Null)
            {
                state.Enabled = false;
                return;
            }

            this.toolbar = new ToolbarHelper<PhysicsToolbarView, PhysicsToolbarViewModel, PhysicsToolbarViewModel.Data>(ref state, "Physics");

            state.EntityManager.AddComponent<PhysicsDebugDraw>(state.SystemHandle);
        }

        /// <inheritdoc />
        public void OnStartRunning(ref SystemState state)
        {
            this.toolbar.Load();

            this.UpdateData(ref state, ref this.toolbar.Binding);
        }

        /// <inheritdoc />
        public void OnStopRunning(ref SystemState state)
        {
            this.toolbar.Unload();
        }

        /// <inheritdoc />
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (!this.toolbar.IsVisible())
            {
                return;
            }

            this.UpdateData(ref state, ref this.toolbar.Binding);
        }

        private void UpdateData(ref SystemState state, ref PhysicsToolbarViewModel.Data data)
        {
            var c = SystemAPI.GetSingleton<PhysicsDebugDraw>();
            if (c.DrawColliderEdges != data.DrawColliderEdges || c.DrawColliderAabbs != data.DrawColliderAabbs ||
                c.DrawCollisionEvents != data.DrawCollisionEvents || c.DrawTriggerEvents != data.DrawTriggerEvents ||
                c.DrawMeshColliderEdges != data.DrawMeshColliderEdges || c.DrawTerrainColliderEdges != data.DrawTerrainColliderEdges)
            {
                ref var rw = ref SystemAPI.GetSingletonRW<PhysicsDebugDraw>().ValueRW;
                rw.DrawColliderEdges = data.DrawColliderEdges;
                rw.DrawColliderAabbs = data.DrawColliderAabbs;
                rw.DrawCollisionEvents = data.DrawCollisionEvents;
                rw.DrawTriggerEvents = data.DrawTriggerEvents;
                rw.DrawMeshColliderEdges = data.DrawMeshColliderEdges;
                rw.DrawTerrainColliderEdges = data.DrawTerrainColliderEdges;
            }
        }
    }
}
#endif
