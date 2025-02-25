// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.TemplateEngine.Abstractions.TemplatePackage
{
    /// <summary>
    /// Defines the scope that managed by built-in providers.
    /// </summary>
    public enum InstallationScope
    {
        /// <summary>
        /// Template packages are visible to all template hosts.
        /// </summary>
        Global = 0,

        /// <summary>
        /// Template packages are visible to all versions of certain template host.
        /// </summary>
        //        Host = 1,         //not supported at the moment

        /// <summary>
        /// Template packages are visible to only to specific version of the host.
        /// </summary>
        //        Version = 2       //not supported at the moment
    }

    /// <summary>
    /// Manages all <see cref="ITemplatePackageProvider"/>s available to the host.
    /// </summary>
    public interface ITemplatePackageManager
    {
        /// <summary>
        /// Triggered every time when the list of <see cref="ITemplatePackage"/>s changes, this is triggered by <see cref="ITemplatePackageProvider.TemplatePackagesChanged"/>.
        /// </summary>
        event Action TemplatePackagesChanged;

        /// <summary>
        /// Returns combined list of <see cref="ITemplatePackage"/>s that all <see cref="ITemplatePackageProvider"/>s and <see cref="IManagedTemplatePackagesProvider"/>s return.
        /// <see cref="ITemplatePackageManager"/> caches the responses from <see cref="ITemplatePackageProvider"/>s, to get non-cached response <paramref name="invalidateCache"/> should be set to true.
        /// </summary>
        /// <param name="invalidateCache">Useful when <see cref="ITemplatePackageProvider"/> doesn't trigger <see cref="ITemplatePackageProvider.TemplatePackagesChanged"/> event.</param>
        /// <returns>The list of <see cref="ITemplatePackage"/>s.</returns>
        Task<IReadOnlyList<ITemplatePackage>> GetTemplatePackagesAsync(bool invalidateCache = false);

        /// <summary>
        /// Same as <see cref="GetTemplatePackagesAsync"/> but filters only <see cref="IManagedTemplatePackage"/> packages.
        /// </summary>
        /// <param name="invalidateCache">Useful when <see cref="IManagedTemplatePackage"/> doesn't trigger <see cref="ITemplatePackageProvider.TemplatePackagesChanged"/> event.</param>
        /// <returns>The list of <see cref="IManagedTemplatePackage"/>.</returns>
        Task<IReadOnlyList<IManagedTemplatePackage>> GetManagedTemplatePackagesAsync(bool invalidateCache = false);

        /// <summary>
        /// Returns built-in <see cref="IManagedTemplatePackageProvider"/> of specified <see cref="InstallationScope"/>.
        /// </summary>
        /// <param name="scope">scope managed by built-in provider.</param>
        /// <returns><see cref="IManagedTemplatePackageProvider"/> which manages packages of <paramref name="scope"/>.</returns>
        IManagedTemplatePackageProvider GetBuiltInManagedProvider(InstallationScope scope);

        /// <summary>
        /// Returns <see cref="IManagedTemplatePackageProvider"/> with specified name.
        /// </summary>
        /// <param name="name">Name from <see cref="ITemplatePackageProviderFactory.DisplayName"/>.</param>
        /// <returns></returns>
        /// <remarks>For default built-in providers use <see cref="GetBuiltInManagedProvider"/> method instead.</remarks>
        IManagedTemplatePackageProvider GetManagedProvider(string name);

        /// <summary>
        /// Returns <see cref="IManagedTemplatePackageProvider"/> with specified <see cref="Guid"/>.
        /// </summary>
        /// <param name="id"><see cref="Guid"/> from <see cref="IIdentifiedComponent.Id"/> of <see cref="ITemplatePackageProviderFactory"/>.</param>
        /// <returns></returns>
        /// <remarks>For default built-in providers use <see cref="GetBuiltInManagedProvider"/> method instead.</remarks>
        IManagedTemplatePackageProvider GetManagedProvider(Guid id);
    }
}
