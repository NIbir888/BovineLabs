// <copyright file="DrawToolbarViewModel.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

#if UNITY_EDITOR || !APP_UI_EDITOR_ONLY
namespace BovineLabs.Quill.Debug
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using BovineLabs.Anchor;
    using BovineLabs.Core.Extensions;
    using Unity.AppUI.UI;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Properties;
    using UnityEngine;

    [Serializable]
    public partial class DrawToolbarViewModel : SystemObservableObject<DrawToolbarViewModel.Data>, ILoadable, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<string> categorySave = new();

        [SerializeField]
        private List<string> systemSave = new();

        [SerializeField]
        private bool enabledSave = true;

        [NonSerialized]
        private bool isRestoringSelections;

        public DrawToolbarViewModel()
        {
            this.PropertyChanged += this.OnPropertyChanged;
        }

        [CreateProperty]
        public bool Enabled
        {
            get => this.Value.Enabled.Value;
            set => this.Value.Enabled = value;
        }

        [CreateProperty]
        public UIArray<FixedString32Bytes> Categories => this.Value.Categories;

        [CreateProperty]
        public IEnumerable<int> CategoryValues
        {
            get => this.Value.CategoryValues.Value.AsArray();
            set => this.SetProperty(this.Value.CategoryValues, value);
        }

        [CreateProperty]
        public UIArray<int> Systems => this.Value.Systems;

        [CreateProperty]
        public IEnumerable<int> SystemValues
        {
            get => this.Value.SystemValues.Value.AsArray();
            set => this.SetProperty(this.Value.SystemValues, value);
        }

        public void BindCategoryItem(DropdownItem item, int index)
        {
            item.label = this.Value.Categories[index].ToString();
        }

        public void BindSystemItem(DropdownItem item, int index)
        {
            const string drawSystemPrefix = "DrawSystem";
            const string systemPrefix = "System";

            var type = this.Value.Systems[index];

            var name = TypeManager.GetSystemName(type).ToString();
            var nameIndex = name.LastIndexOf('.') + 1;
            name = nameIndex == 0 ? name : name.Substring(nameIndex, name.Length - nameIndex);

            var drawIndex = name.IndexOf(drawSystemPrefix, StringComparison.Ordinal);
            if (drawIndex != -1)
            {
                name = name.Remove(drawIndex, drawSystemPrefix.Length);
            }
            else
            {
                var systemIndex = name.IndexOf(systemPrefix, StringComparison.Ordinal);
                if (systemIndex != -1)
                {
                    name = name.Remove(systemIndex, systemPrefix.Length);
                }
            }

            item.label = name.ToSentence();
        }

        /// <inheritdoc/>
        void ILoadable.Load()
        {
            this.Value.Initialize();
            this.Value.Enabled = this.enabledSave;
        }

        /// <inheritdoc/>
        void ILoadable.Unload()
        {
            this.Value.Unload();
        }

        public void OnBeforeSerialize()
        {
            if (!this.Value.IsInitialized)
            {
                return;
            }

            this.enabledSave = this.Enabled;
            this.RefreshCategorySave(preserveMissing: true);
            this.RefreshSystemSave(preserveMissing: true);
        }

        public void OnAfterDeserialize()
        {
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Re-map selections when the source arrays change and keep save data synced with live selection.
            switch (e.PropertyName)
            {
                case nameof(this.Categories):
                {
                    this.RestoreCategoryValues();
                    break;
                }

                case nameof(this.Systems):
                {
                    this.RestoreSystemValues();
                    break;
                }

                case nameof(this.CategoryValues):
                {
                    if (!this.isRestoringSelections)
                    {
                        this.RefreshCategorySave();
                    }

                    break;
                }

                case nameof(this.SystemValues):
                {
                    if (!this.isRestoringSelections)
                    {
                        this.RefreshSystemSave();
                    }

                    break;
                }

                case nameof(this.Enabled):
                {
                    this.enabledSave = this.Enabled;
                    break;
                }
            }
        }

        private void RefreshCategorySave(bool preserveMissing = false)
        {
            List<string> unresolved = null;
            var categories = this.Value.Categories.AsArray();

            if (preserveMissing && this.categorySave.Count > 0)
            {
                unresolved = new List<string>();
                foreach (var categoryName in this.categorySave)
                {
                    if (categories.IndexOf(categoryName) == -1)
                    {
                        unresolved.Add(categoryName);
                    }
                }
            }

            this.categorySave.Clear();
            foreach (var selectedIndex in this.Value.CategoryValues.Value)
            {
                if ((uint)selectedIndex < (uint)categories.Length)
                {
                    var categoryName = categories[selectedIndex].ToString();
                    if (!this.categorySave.Contains(categoryName))
                    {
                        this.categorySave.Add(categoryName);
                    }
                }
            }

            if (unresolved != null)
            {
                foreach (var unresolvedCategory in unresolved)
                {
                    if (!this.categorySave.Contains(unresolvedCategory))
                    {
                        this.categorySave.Add(unresolvedCategory);
                    }
                }
            }
        }

        private void RefreshSystemSave(bool preserveMissing = false)
        {
            List<string> unresolved = null;
            var systems = this.Value.Systems.AsArray();

            if (preserveMissing && this.systemSave.Count > 0)
            {
                unresolved = new List<string>();
                foreach (var systemName in this.systemSave)
                {
                    if (FindSystemIndexByName(systems, systemName) == -1)
                    {
                        unresolved.Add(systemName);
                    }
                }
            }

            this.systemSave.Clear();
            foreach (var selectedIndex in this.Value.SystemValues.Value)
            {
                if ((uint)selectedIndex < (uint)systems.Length)
                {
                    var systemName = TypeManager.GetSystemName(systems[selectedIndex]).ToString();
                    if (!this.systemSave.Contains(systemName))
                    {
                        this.systemSave.Add(systemName);
                    }
                }
            }

            if (unresolved != null)
            {
                foreach (var unresolvedSystem in unresolved)
                {
                    if (!this.systemSave.Contains(unresolvedSystem))
                    {
                        this.systemSave.Add(unresolvedSystem);
                    }
                }
            }
        }

        private void RestoreCategoryValues()
        {
            var remapped = new List<int>(this.categorySave.Count);
            var categories = this.Value.Categories.AsArray();

            foreach (var categoryName in this.categorySave)
            {
                var index = categories.IndexOf(categoryName);
                if (index != -1)
                {
                    remapped.Add(index);
                }
            }

            this.isRestoringSelections = true;
            this.SetProperty(this.Value.CategoryValues, remapped, nameof(this.CategoryValues));
            this.isRestoringSelections = false;
        }

        private void RestoreSystemValues()
        {
            var remapped = new List<int>(this.systemSave.Count);
            var systems = this.Value.Systems.AsArray();

            foreach (var systemName in this.systemSave)
            {
                var index = FindSystemIndexByName(systems, systemName);
                if (index != -1)
                {
                    remapped.Add(index);
                }
            }

            this.isRestoringSelections = true;
            this.SetProperty(this.Value.SystemValues, remapped, nameof(this.SystemValues));
            this.isRestoringSelections = false;
        }

        private static int FindSystemIndexByName(NativeArray<int>.ReadOnly systems, string systemName)
        {
            for (var i = 0; i < systems.Length; i++)
            {
                if (string.Equals(TypeManager.GetSystemName(systems[i]).ToString(), systemName, StringComparison.Ordinal))
                {
                    return i;
                }
            }

            return -1;
        }

        private static void ConvertToDisplayNames(List<string> list, NativeArray<int> types)
        {
            const string drawSystemPrefix = "DrawSystem";
            const string systemPrefix = "System";

            foreach (var type in types)
            {
                var name = TypeManager.GetSystemName(type).ToString();
                var index = name.LastIndexOf('.') + 1;
                name = index == 0 ? name : name.Substring(index, name.Length - index);

                var drawIndex = name.IndexOf(drawSystemPrefix, StringComparison.Ordinal);
                if (drawIndex != -1)
                {
                    name = name.Remove(drawIndex, drawSystemPrefix.Length);
                }
                else
                {
                    var systemIndex = name.IndexOf(systemPrefix, StringComparison.Ordinal);
                    if (systemIndex != -1)
                    {
                        name = name.Remove(systemIndex, systemPrefix.Length);
                    }
                }

                list.Add(name.ToSentence());
            }
        }

        [Serializable]
        public partial struct Data
        {
            [SystemProperty]
            private Changed<bool> enabled;

            [SystemProperty]
            private NativeList<FixedString32Bytes> categories;

            [SystemProperty]
            private ChangedList<int> categoryValues;

            [SystemProperty]
            private NativeList<int> systems;

            [SystemProperty]
            private ChangedList<int> systemValues;

            internal bool IsInitialized => this.categories.IsCreated && this.systems.IsCreated;

            internal void Initialize()
            {
                this.categories = new NativeList<FixedString32Bytes>(Allocator.Persistent);
                this.categoryValues = new NativeList<int>(Allocator.Persistent);
                this.systems = new NativeList<int>(Allocator.Persistent);
                this.systemValues = new NativeList<int>(Allocator.Persistent);
            }

            internal void Unload()
            {
                this.categories.Dispose();
                this.categoryValues.Value.Dispose();
                this.systems.Dispose();
                this.systemValues.Value.Dispose();
            }
        }
    }
}
#endif
