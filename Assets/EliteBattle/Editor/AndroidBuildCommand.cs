
#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace EliteBattle.EditorTools
{
    public static class AndroidBuildCommand
    {
        public static void BuildAndroid()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(
                BuildTargetGroup.Android, BuildTarget.Android);

            EliteBattleSceneBuilder.CreatePrototypeScene();

            string scenePath = "Assets/EliteBattle/Scenes/Prototype.unity";

            if (!File.Exists(scenePath))
                throw new Exception("Prototype scene was not created.");

            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(scenePath, true)
            };

            PlayerSettings.productName = "ELITE BATTLE OFFLINE";
            PlayerSettings.companyName = "Big Boys Elite";
            PlayerSettings.SetApplicationIdentifier(
                BuildTargetGroup.Android, "com.bigboys.elitebattleoffline");

            PlayerSettings.defaultInterfaceOrientation =
                UIOrientation.LandscapeLeft;

            PlayerSettings.Android.minSdkVersion =
                AndroidSdkVersions.AndroidApiLevel23;

            string output = "Build/ELITE_BATTLE_OFFLINE.apk";
            Directory.CreateDirectory("Build");

            BuildReport report = BuildPipeline.BuildPlayer(
                new BuildPlayerOptions
                {
                    scenes = new[] { scenePath },
                    locationPathName = output,
                    target = BuildTarget.Android,
                    options = BuildOptions.None
                });

            if (report.summary.result != BuildResult.Succeeded)
                throw new Exception(
                    "Android build failed: " + report.summary.result);

            Debug.Log("APK created at: " + output);
        }
    }
}
#endif
