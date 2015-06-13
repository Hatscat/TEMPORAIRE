using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem.UnityGUI;

namespace PixelCrushers.DialogueSystem.UFPS {
	
	/// <summary>
	/// This script was written to be attached to the example Weapon Inventory Menu GUI.
	/// When the player presses a key (F10 by default), it opens the inventory menu.
	/// </summary>
	public class FPWeaponInventoryMenu : MonoBehaviour {
		
		/// <summary>
		/// The FPFreezePlayer is used to freeze the FPS player while the menu is being shown.
		/// </summary>
		public FPFreezePlayer fpFreezePlayer;
		
		/// <summary>
		/// The vp_Inventory is used to determine what weapons the player has.
		/// </summary>
		public vp_Inventory fpInventory;
		
		/// <summary>
		/// The key that brings up the weapon inventory menu.
		/// </summary>
		public KeyCode key = KeyCode.F10;
		
		/// <summary>
		/// The weapon panels. You must assign these before using the menu.
		/// </summary>
		public GUIControl[] weaponPanels;
		
		public bool IsOpen { get { return (guiRoot != null) && guiRoot.gameObject.activeSelf; } }
		
		private GUIRoot guiRoot;
		
		/// <summary>
		/// On Awake, make sure that all properties have been set up properly.
		/// </summary>
		void Awake() {
			if (fpFreezePlayer == null) {
				Debug.LogError("You must assign FPFreezePlayer to " + name);
				enabled = false;
			}
			if (fpInventory == null) {
				Debug.LogError("You must assign vp_Inventory to " + name);
				enabled = false;
			}
			guiRoot = GetComponentInChildren<GUIRoot>();
			if (guiRoot == null) {
				Debug.LogError("No GUIRoot found under " + name);
				enabled = false;
			} else {
				guiRoot.gameObject.SetActive(false);
			}
			if (weaponPanels.Length <= 0) {
				Debug.LogError("You must assign at least one weapon panel to " + name);
				enabled = false;
			}
		}
	
		/// <summary>
		/// Handle key input to open and close the window.
		/// </summary>
		void Update() {
			if (Input.GetKeyDown(key) && !IsOpen) Open();
			if (Input.GetKeyDown(KeyCode.Escape) && IsOpen) Close();
		}
		
		/// <summary>
		/// Open the window, freezing the player, and going to the first panel.
		/// </summary>
		public void Open() {
			fpFreezePlayer.Freeze();
			guiRoot.gameObject.SetActive(true);
			GotoPanel(weaponPanels[0].name);
		}
		
		/// <summary>
		/// Close the window, unfreezing the player.
		/// </summary>
		public void Close() {
			guiRoot.gameObject.SetActive(false);
			fpFreezePlayer.Unfreeze();
		}
		
		/// <summary>
		/// Goes to the panel.
		/// </summary>
		/// <param name='targetPanelName'>
		/// Target panel name.
		/// </param>
		public void GotoPanel(string targetPanelName) {
			
			// Go through all of the panels:
			foreach (GUIControl panel in weaponPanels) {
				
				// Is this the requested panel? If yes, activate it. Otherwise deactivate it.
				bool isTargetPanel = string.Equals(panel.name, targetPanelName);
				panel.gameObject.SetActive(isTargetPanel);
				
				// If it's the requested panel, set the status text ("Owned: Yes/No"):
				if (isTargetPanel) {
					string weaponName = targetPanelName.Replace(" Panel", string.Empty);
					bool owned = false;
					foreach (vp_ItemInstance item in fpInventory.ItemInstances) {
						Debug.Log(item.Type.name + " ?= " + weaponName);
						if (string.Equals(item.Type.name, weaponName)) owned = true;
					}
					foreach (vp_UnitBankInstance unit in fpInventory.UnitBankInstances) {
						if (string.Equals(unit.Type.name, weaponName)) owned = true;
					}
					foreach (GUILabel label in panel.GetComponentsInChildren<GUILabel>()) {
						if (string.Equals(label.name, "Weapon Status")) {
							label.text = "Owned: " + (owned ? "Yes" : "No");
						}
					}
				}
			}
		}
		
	}

}
