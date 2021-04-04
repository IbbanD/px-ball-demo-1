// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Microsoft.MixedReality.OpenXR.ARFoundation
{
    [Preserve]
    public static class AnchorManagerExtensions
    {
        public static Task<Microsoft.MixedReality.OpenXR.XRAnchorStore> LoadAnchorStoreAsync(this ARAnchorManager anchorManager)
        {
            return Microsoft.MixedReality.OpenXR.XRAnchorStore.LoadAsync(anchorManager.subsystem);
        }
    }
}

namespace Microsoft.MixedReality.OpenXR.ARSubsystems
{
    [Preserve]
    public static class AnchorSubsystemExtensions
    {
        public static Task<XRAnchorStore> LoadAnchorStoreAsync(this XRAnchorSubsystem anchorSubsystem)
        {
            return Microsoft.MixedReality.OpenXR.XRAnchorStore.LoadAsync(anchorSubsystem);
        }
    }
}

namespace Microsoft.MixedReality.OpenXR
{
    [Preserve]
    public class XRAnchorStore
    {
        public IReadOnlyList<string> PersistedAnchorNames => m_openxrAnchorStore.PersistedAnchorNames;

        public TrackableId LoadAnchor(string name) => m_openxrAnchorStore.LoadAnchor(name);
        public bool TryPersistAnchor(TrackableId trackableId, string name) => m_openxrAnchorStore.TryPersistAnchor(name, trackableId);
        public void UnpersistAnchor(string name) => m_openxrAnchorStore.UnpersistAnchor(name);
        public void Clear() => m_openxrAnchorStore.Clear();

        public static async Task<XRAnchorStore> LoadAsync(XRAnchorSubsystem anchorSubsystem)
        {
            OpenXRAnchorStore openxrAnchorStore = await OpenXRAnchorStoreFactory.LoadAnchorStoreAsync(anchorSubsystem);
            return openxrAnchorStore == null ? null : new XRAnchorStore(openxrAnchorStore);
        }

        internal XRAnchorStore(OpenXRAnchorStore openxrAnchorStore)
        {
            m_openxrAnchorStore = openxrAnchorStore;
        }

        private readonly OpenXRAnchorStore m_openxrAnchorStore;
    }
}

namespace Microsoft.MixedReality.ARSubsystems
{
    [Preserve]
    public static class AnchorSubsystemExtensions
    {
        [System.Obsolete("The extension function LoadAnchorStoreAsync in namespace Microsoft.MixedReality.ARSubsystems is obsolete and will be removed in future releases. " +
            "Use the version in namespace Microsoft.MixedReality.OpenXR.ARSubsystems instead.")]
        public static Task<XRAnchorStore> LoadAnchorStoreAsync(this XRAnchorSubsystem anchorSubsystem)
        {
            return Microsoft.MixedReality.ARSubsystems.XRAnchorStore.LoadAsync(anchorSubsystem);
        }
    }

    [System.Obsolete("The type XRAnchorStore in namespace Microsoft.MixedReality.ARSubsystems is obsolete and will be removed in future releases. " +
        "Use the version in namespace Microsoft.MixedReality.OpenXR instead.")]
    public class XRAnchorStore : Microsoft.MixedReality.OpenXR.XRAnchorStore
    {
        internal new static async Task<XRAnchorStore> LoadAsync(XRAnchorSubsystem anchorSubsystem)
        {
            OpenXR.OpenXRAnchorStore openxrAnchorStore = await OpenXR.OpenXRAnchorStoreFactory.LoadAnchorStoreAsync(anchorSubsystem);
            return new XRAnchorStore(openxrAnchorStore);
        }

        private XRAnchorStore(OpenXR.OpenXRAnchorStore openxrAnchorStore)
            : base(openxrAnchorStore)
        {
        }

        [System.Obsolete("The TryPersistAnchor(string, TrackableId) function is obsolete and will be removed in future releases. " +
        "Use the version TryPersistAnchor(TrackableId, string) instead.")]
        public bool TryPersistAnchor(string name, TrackableId trackableId) => TryPersistAnchor(trackableId, name);
    }

} // namespace Microsoft.MixedReality.AnchorStore
