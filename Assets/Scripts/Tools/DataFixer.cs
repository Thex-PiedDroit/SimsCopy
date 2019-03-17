
#if UNITY_EDITOR

using UnityEditor;
using System.Collections.Generic;


public class DataFixer
{
	[MenuItem("Tools/Reserialize all files")]
	static private void ReserializeAllFiles()
	{
		string[] allDataObjectsGUIDs = GetAllAssetsGUIDs();

		// ForceReserializeAssets takes a list, but we want to treat files one by one to show them in progress bar.
		List<string> dummyPathsList = new List<string>(1) { "" };

		for (int i = 0, n = allDataObjectsGUIDs.Length; i < n; ++i)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(allDataObjectsGUIDs[i]);
			ReserializeAsset(assetPath, dummyPathsList);

			float progress = (float)i / n;
			DisplayProgressBar("Reserializing all files...", assetPath, progress);
		}

		ClearProgressBar();
	}

	static private string[] GetAllAssetsGUIDs()
	{
		return AssetDatabase.FindAssets("", new string[] { "Assets" });
	}

	static private void ReserializeAsset(string assetPath, List<string> dummyPathsList)
	{
		dummyPathsList[0] = assetPath;
		AssetDatabase.ForceReserializeAssets(dummyPathsList, ForceReserializeAssetsOptions.ReserializeAssetsAndMetadata);
	}

	static private void DisplayProgressBar(string title, string info, float progress)
	{
		EditorUtility.DisplayProgressBar(title, info, progress);
	}

	static private void ClearProgressBar()
	{
		EditorUtility.ClearProgressBar();
	}
}

#endif
