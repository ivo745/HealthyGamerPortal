
using System;
using System.Diagnostics;
using System.Reflection;

namespace HealthyGamerPortal.Common.Helpers
{
    /// <summary>
    /// Helper class that can get different kinds of version numbers for an assembly.
    /// </summary>
    public static class VersionHelper
    {
        /// <summary>
        /// Gets the version defined by the &lt;AssemblyVersion&gt;&lt;/AssemblyVersion&gt; tag in the .csproj file.
        /// This for the currently executing assembly.
        /// </summary>
        public static string GetAssemblyVersionForExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Gets the version defined by the &lt;AssemblyVersion&gt;&lt;/AssemblyVersion&gt; tag in the .csproj file.
        /// This for assembly that defines the specified <paramref name="type"/>.
        /// </summary>
        public static string GetAssemblyVersionForType(Type type)
        {
            return Assembly.GetAssembly(type).GetName().Version.ToString();
        }

        /// <summary>
        /// Gets the version defined by the &lt;FileVersion&gt;&lt;/FileVersion&gt; tag in the .csproj file.
        /// This for the currently executing assembly.
        /// </summary>
        public static string GetFileVersionForExecutingAssembly()
        {
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return fvi.FileVersion;
        }

        /// <summary>
        /// Gets the version defined by the &lt;FileVersion&gt;&lt;/FileVersion&gt; tag in the .csproj file.
        /// This for assembly that defines the specified <paramref name="type"/>.
        /// </summary>
        public static string GetFileVersionForType(Type type)
        {
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(type).Location);
            return fvi.FileVersion;
        }

        /// <summary>
        /// Gets the version defined by the &lt;Version&gt;&lt;/Version&gt; tag in the .csproj file.
        /// This for the currently executing assembly.
        /// </summary>
        public static string GetProductVersionForExecutingAssembly()
        {
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return fvi.ProductVersion;
        }

        /// <summary>
        /// Gets the version defined by the &lt;Version&gt;&lt;/Version&gt; tag in the .csproj file.
        /// This for assembly that defines the specified <paramref name="type"/>.
        /// </summary>
        public static string GetProductVersionForType(Type type)
        {
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(type).Location);
            return fvi.ProductVersion;
        }
    }
}