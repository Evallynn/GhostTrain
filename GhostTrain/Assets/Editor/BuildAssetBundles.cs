using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;


public class BuildAssetBundles {
    // Command line argument names.
    private const string ARG_BUILD_DIR = "-buildDir";


    // Main build entrypoint.
    [MenuItem("Build pipeline/BuildWin64")]
    public static void PerformWin64Build() => RunBuild(BuildTarget.StandaloneWindows64, "GhostTrain-PC", "GhostTrain.exe");

    [MenuItem("Build pipeline/WebGL")]
    public static void PerformWebGLBuild() => RunBuild(BuildTarget.WebGL, "GhostTrain-WebGL", "GhostTrain");


    // Helper function for core build.
    private static void RunBuild(BuildTarget target, string buildName, string exeName) {
        // Eye catcher.
        Debug.Log(GenerateHeader("Building Asset Bundles - " + target.ToString()));

        // Obtain and process command line arguements.
        Dictionary<string, string> args = GetArgs();

        // Ensure we're running against the windows build platform (note that this doesn't work in command line batch mode, it's only for the editor menu copy).
        BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(target);
        EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, target);

        // Addressables.
        //AddressableAssetSettings.CleanPlayerContent();
        //AddressableAssetSettings.BuildPlayerContent();

        // Main player. 
        string[] levels = new string[] {
            "Assets/Scenes/Splash.unity",
            "Assets/Scenes/MainMenu.unity"
        };
        BuildReport report = BuildPipeline.BuildPlayer(levels, args[ARG_BUILD_DIR] + "/" + buildName + "/" + exeName, target, BuildOptions.Development);


        // Print the result.
        Debug.Log(GenerateHeader("    Build Complete    "));
        Debug.Log(" - Result : " + report.summary.result.ToString());
        Debug.Log(" - Errors: " + report.summary.totalErrors);
        Debug.Log(" - Warnings: " + report.summary.totalWarnings);
        Debug.Log(" - Output Path: '" + report.summary.outputPath + "'");
    }


    // Helper function to generate proper logging padding.
    private static string GenerateHeader(string stringToPad) {
        // Pad the header line.
        stringToPad = "= " + stringToPad + " =";

        // Generate the header eye catcher.
        string eyeCatcher = "";
        eyeCatcher.PadLeft(stringToPad.Length, '=');

        // Wrap the main string and return it.
        return eyeCatcher + "\n" + stringToPad + "\n" + eyeCatcher;
    }


    // Helper function for getting the command line arguments
    private static Dictionary<string, string> GetArgs() {
        // Obtain the arguements and setup our key/value pairs.
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        string[] args = System.Environment.GetCommandLineArgs();


        // Iterate over the command line entries.
        for (int i = 0; i < args.Length - 1; i++) {
            // Print feedback.
            Debug.Log("Arg: " + args[i]);


            // Check for arguents we care about.
            if (args[i] == ARG_BUILD_DIR) {
                // Check for any issues.
                if (i == args.Length - 1) {
                    Debug.LogError("No value provided for build flag '" + ARG_BUILD_DIR + "'.");
                    continue;
                }

                // Store this and the next argument.
                keyValuePairs.Add(ARG_BUILD_DIR, args[i + 1]);
                i++;
            }
        }


        // Check to see if we found the build directory or not.
        if (!keyValuePairs.ContainsKey(ARG_BUILD_DIR)) {
            // Default the value, but issue a warning.
            string defaultPath = System.IO.Directory.GetCurrentDirectory() + "\\Builds\\";
            Debug.LogWarning("The build flag '" + ARG_BUILD_DIR + "' was not passed, defaulting to '" + defaultPath + "'.");
            keyValuePairs.Add(ARG_BUILD_DIR, defaultPath);
        }


        // Pass back the key/value pairs.
        return keyValuePairs;
    }
}
