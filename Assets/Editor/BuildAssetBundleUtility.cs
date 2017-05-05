using UnityEditor;
using UnityEngine;
using System.IO;

namespace Vire
{
	public class BuildAssetBundleUtility
	{
		static void BuildAssetBundle (string assetBundlePath, string objectPath, BuildTarget buildTarget)
		{
			AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

			AssetBundleBuild buildInfo = new AssetBundleBuild ();
			buildInfo.assetBundleName = Path.GetFileName (assetBundlePath);
			string[] prefabs = new string[1];

			prefabs [0] = objectPath;
			buildInfo.assetNames = prefabs;

			buildMap [0] = buildInfo;

			BuildPipeline.BuildAssetBundles (Path.GetDirectoryName (assetBundlePath), buildMap, BuildAssetBundleOptions.None, buildTarget);
		}

		static void BuildAssetBundleFor (BuildTarget buildTarget)
		{
			Object[] selection = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
			if (selection.Length != 1) {
				EditorUtility.DisplayDialog (
					"Error", 
					"Please select one object in the Project tab that you like to export as an asset bundle.",
					"Close");
			} else {
				Object obj = selection [0];
				string objectPath = AssetDatabase.GetAssetPath (obj);
				string assetBundlePath = EditorUtility.SaveFilePanel ("Save Asset Bundle to",
					                        "",
					                        Path.GetFileNameWithoutExtension (objectPath) + "-" + buildTarget.ToString (),
					                        "unity3d");
				if (assetBundlePath.Length != 0) {
					BuildAssetBundle (assetBundlePath, objectPath, buildTarget);
					EditorUtility.DisplayDialog (
						"Asset Bundle saved", 
						"Here: " + assetBundlePath,
						"Close");
				}
			}
		}

		[MenuItem ("Assets/Build Asset Bundle/for Android")]
		private static void BuildAllAssetBundlesAndroid ()
		{
			BuildAssetBundleFor (BuildTarget.Android);
		}

		[MenuItem ("Assets/Build Asset Bundle/for iOS")]
		private static void BuildAllAssetBundlesIOS ()
		{
			BuildAssetBundleFor (BuildTarget.iOS);
		}

		[MenuItem ("Assets/Build Asset Bundle/for Linux")]
		private static void BuildAllAssetBundlesLinux ()
		{
			BuildAssetBundleFor (BuildTarget.StandaloneLinux);
		}

		[MenuItem ("Assets/Build Asset Bundle/for Linux (x64)")]
		private static void BuildAllAssetBundlesLinux64 ()
		{
			BuildAssetBundleFor (BuildTarget.StandaloneLinux64);
		}

		[MenuItem ("Assets/Build Asset Bundle/for Mac")]
		private static void BuildAllAssetBundlesMac ()
		{
			BuildAssetBundleFor (BuildTarget.StandaloneOSXIntel);
		}

		[MenuItem ("Assets/Build Asset Bundle/for Mac (x64)")]
		private static void BuildAllAssetBundlesMac64 ()
		{
			BuildAssetBundleFor (BuildTarget.StandaloneOSXIntel64);
		}

		[MenuItem ("Assets/Build Asset Bundle/for PS4")]
		private static void BuildAllAssetBundlesPS4 ()
		{
			BuildAssetBundleFor (BuildTarget.PS4);
		}

		[MenuItem ("Assets/Build Asset Bundle/for WebGL")]
		private static void BuildAllAssetBundlesWebGL ()
		{
			BuildAssetBundleFor (BuildTarget.WebGL);
		}

		[MenuItem ("Assets/Build Asset Bundle/for Windows")]
		private static void BuildAllAssetBundlesWindows ()
		{
			BuildAssetBundleFor (BuildTarget.StandaloneWindows);
		}

		[MenuItem ("Assets/Build Asset Bundle/for Windows (x64)")]
		private static void BuildAllAssetBundlesWindows64 ()
		{
			BuildAssetBundleFor (BuildTarget.StandaloneWindows64);
		}
	}
}