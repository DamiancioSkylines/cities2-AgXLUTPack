using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgXLUTPack.Initializer
{
    internal class Bundle
    {
        public static void ValidateLUTSDirectory()
        {
            try
            {
                // Ensure the directory path for LUTs is valid
                string directoryPath = GlobalPaths.LuminaLUTSDirectory;

                if (string.IsNullOrWhiteSpace(directoryPath))
                {
                    throw new InvalidOperationException("The directory path for LUTs is not set.");
                }

                // Create the directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Copy all embedded resources to the directory
                var assembly = typeof(Bundle).Assembly; // Use typeof(Bundle) to access the assembly in a static method
                var resourceNamespace = assembly.GetName().Name + ".LUTS"; // Adjusted for proper format

                //Mod.log.Info($"Assembly Name: {assembly.GetName().Name}");
                //Mod.log.Info($"Resource Namespace Prefix: {resourceNamespace}");
                
                // Get all resource names
                var resourceNames = assembly.GetManifestResourceNames();

                foreach (var resourceName in resourceNames)
                {
                    Mod.log.Debug($"Validating resource '{resourceName}'.");
                    // Check if the resource belongs to the correct namespace
                    if (resourceName.StartsWith(resourceNamespace))
                    {
                        using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
                        {
                            if (resourceStream == null)
                            {
                                Mod.log.Error($"Embedded resource '{resourceName}' not found.");
                                continue;
                            }

                            // Determine the destination path
                            var relativePath = resourceName.Substring(resourceNamespace.Length + 1); // Remove namespace prefix
                            var destinationPath = Path.Combine(directoryPath, relativePath);

                            // Create the directory if it doesn't exist
                            var destinationDirectory = Path.GetDirectoryName(destinationPath);
                            if (!Directory.Exists(destinationDirectory))
                            {
                                Directory.CreateDirectory(destinationDirectory);
                            }

                            // Copy the resource to the destination
                            using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                            {
                                resourceStream.CopyTo(fileStream);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mod.log.Error($"Failed to validate or create the LUTs directory: {ex.Message}");
            }
        }
        
        public static void ValidatePresetsDirectory()
        {
            try
            {
                // Ensure the directory path for LUTs is valid
                string directoryPath = GlobalPaths.LuminaPresetsDirectory;

                if (string.IsNullOrWhiteSpace(directoryPath))
                {
                    throw new InvalidOperationException("The directory path for Presets is not set.");
                }

                // Create the directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Copy all embedded resources to the directory
                var assembly = typeof(Bundle).Assembly; // Use typeof(Bundle) to access the assembly in a static method
                var resourceNamespace = assembly.GetName().Name + ".LuminaPresets"; // Adjusted for proper format

                //Mod.log.Info($"Assembly Name: {assembly.GetName().Name}");
                //Mod.log.Info($"Resource Namespace Prefix: {resourceNamespace}");
                
                // Get all resource names
                var resourceNames = assembly.GetManifestResourceNames();

                foreach (var resourceName in resourceNames)
                {
                    // Check if the resource belongs to the correct namespace
                    if (resourceName.StartsWith(resourceNamespace))
                    {
                        using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
                        {
                            if (resourceStream == null)
                            {
                                Mod.log.Error($"Embedded resource '{resourceName}' not found.");
                                continue;
                            }

                            // Determine the destination path
                            var relativePath = resourceName.Substring(resourceNamespace.Length + 1); // Remove namespace prefix
                            var destinationPath = Path.Combine(directoryPath, relativePath);

                            // Create the directory if it doesn't exist
                            var destinationDirectory = Path.GetDirectoryName(destinationPath);
                            if (!Directory.Exists(destinationDirectory))
                            {
                                Directory.CreateDirectory(destinationDirectory);
                            }

                            // Copy the resource to the destination
                            using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                            {
                                resourceStream.CopyTo(fileStream);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mod.log.Error($"Failed to validate or create the Presets directory: {ex.Message}");
            }
        }

    }

}

