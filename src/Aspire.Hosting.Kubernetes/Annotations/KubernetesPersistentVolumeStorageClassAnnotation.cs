// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting.Kubernetes.Annotations;

/// <summary>
/// Represents an annotation that enables the explicit ommission of storageClassName from a PVC.
/// </summary>

internal sealed class KubernetesPersistentVolumeStorageClassAnnotation : IResourceAnnotation
{
    /// <summary>
    /// Instantiates an instance of <see cref="KubernetesPersistentVolumeStorageClassAnnotation"/>.
    /// </summary>
    /// <param name="omitStorageClassName">Whether or not to omit storageClassName.</param>
    /// <param name="storageClassName">The storageClassName to set for the PVC. Set this to null to fallback to the environment's defaults.</param>
    public KubernetesPersistentVolumeStorageClassAnnotation(bool omitStorageClassName, ReferenceExpression? storageClassName)
    {
        if (omitStorageClassName && storageClassName is {})
        {
            throw new ArgumentException($"A Persistent Volume Claim was configured with {nameof(omitStorageClassName)} = false, but a {nameof(storageClassName)} was provided.");
        }

        OmitStorageClassName = omitStorageClassName;
        StorageClassName = storageClassName;
    }

    /// <summary>
    /// Gets or sets whether or not to include a storage class in the <see cref="KubernetesPersistentVolumeResource"/> generated spec.
    /// This is configured as an annotation to force explicit omission. This maintains the existing fallback behavior when a storage class
    /// is not otherwise specified. The default behavior is to fallback to the <see cref="KubernetesEnvironmentResource"/>'s
    /// <see cref="KubernetesEnvironmentResource.DefaultStorageClassName"/>.
    /// </summary>
    public bool OmitStorageClassName { get; }

    /// <summary>
    /// Gets or sets the 
    /// </summary>
    public ReferenceExpression? StorageClassName { get; }
}