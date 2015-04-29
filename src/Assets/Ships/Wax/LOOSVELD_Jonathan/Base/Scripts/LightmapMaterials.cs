using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class LightmapMaterials : ScriptableObject {

	private static string targetFolder = "Assets/LightmapMats";

	private static bool CreateTargetFolder()
	{
		try
		{
			System.IO.Directory.CreateDirectory(targetFolder);
		}
		catch
		{
			EditorUtility.DisplayDialog("Error", "Failed to create folder", "");
			return false;
		}
		
		return true;
	}

	[MenuItem ("BenTools/Create Local Materials")]


	static void SaveMaterials () {

		List <Material> createdMats = new List<Material>();

		if (!CreateTargetFolder())
			return;

		Transform[] selection = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);

		if (selection.Length > 0 &&
			EditorUtility.DisplayDialog("Create per lightmap materials", "Warning : All objects in selection will lose connection to shared materials",
			                            "Create", "Cancel")){

			int objCounter = 0;

			for (int i = 0; i < selection.Length; i++)
			{
				Component[] renderers = selection[i].GetComponentsInChildren(typeof(Renderer));
				
				for (int r = 0; r < renderers.Length; r++){

					int lIndex = renderers[r].renderer.lightmapIndex;

					if (lIndex > -1){

						Material[] mats = renderers[r].renderer.sharedMaterials;

						for (int m = 0; m < mats.Length; m++){

							bool foundMat = false;

							string newMatName = mats[m].name + "_" + lIndex + ".mat";

							foreach (Material mt in createdMats){

								string testName = mats[m].name + "_" + lIndex;

								if (mt.name == testName){
									mats[m] = mt;
									foundMat = true;
								}
							}

							if (!foundMat){
								Material newMat = new Material(mats[m]);
								AssetDatabase.CreateAsset(newMat, "Assets/LightmapMats/" + newMatName);
								AssetDatabase.Refresh();
								createdMats.Add(newMat);
								mats[m] = newMat;
							}
						}

						renderers[r].renderer.materials = mats;
					}
				}
			}

			EditorUtility.DisplayDialog("Create per lightmap materials", createdMats.Count + " materials created in Assets/LightmapMats",
			                            "Ok");
		}

	}
}
