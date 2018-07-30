using System;
using System.IO;
using UnityEngine;


public class BuildInfo: ScriptableObject {
	// Returning installed mod after version number
	public string VersionNumber {
		get {
			return this.versionNumber + " - novid_mod - Barichello";
		}
	}

	public string BuildFolder {
		get {
			return this.buildFolder;
		}
	}

	public string ExecutableName {
		get {
			return this.executableName;
		}
	}

	public string MacAppStoreExecutableName {
		get {
			return this.macAppStoreExecutable;
		}
	}

	public bool SteamWorksEnabled {
		get {
			return this.SteamIntegration;
		}
	}

	public bool GogIntergration {
		get {
			return this.goGIntegration;
		}
	}

	public bool MacAppStore {
		get {
			return this.macAppStore;
		}
	}

	public bool CensorSexualContent {
		get {
			return this.censorSexualContent;
		}
	}

	public bool DebugOverlayEnabled {
		get {
			return this.DebugOverlay;
		}
	}

	// Return false for ShowIntro
	public bool ShowIntro {
		get {
			return false;
		}
	}

	public bool SandboxMode {
		get {
			return this.sandboxMode;
		}
	}

	public bool SandboxModeDebugging {
		get {
			return this.sandboxModeDebug;
		}
	}

	public bool ShowBetaOverlay {
		get {
			return this.showBetaOverlay;
		}
	}

	private void SelectFolder() {
		this.buildFolder = ApplicationBuilder.GetBuildFolder(this.buildFolder);
	}

	private void Build() {
		this.UpdateSettings();
		this.executableName = Path.GetFileNameWithoutExtension(this.executableName);
		ApplicationBuilder.Build(this.buildFolder, this.executableName, this.Windows, this.MacOSX, this.Linux, this.Development);
	}

	private void UpdateSettings() {}

	private void FixGoGMacBuild() {
		GoGMacFix.FixBuild(this.buildFolder + "/mac", this.executableName);
	}

	public static void FixMacAppStoreBuild() {
		MacAppStoreFix.FixBuild(BuildInfo.Instance.buildFolder + "/mac", BuildInfo.Instance.executableName);
	}

	public static BuildInfo Instance {
		get {
			if (BuildInfo.instance == null) {
				BuildInfo.instance = DataAssetManager.LoadOrCreateDataAsset < BuildInfo > ("Data/BuildInfo");
			}
			return BuildInfo.instance;
		}
	}

	private const string AssetName = "Data/BuildInfo";

	private const string MenuName = "This is the Police/Build Settings...";

	[PropertyChange("UpdateSettings")][SerializeField]
	private string versionNumber = "1.0.0";

	[InspectorButton("Select", "SelectFolder", true, 50f)][SerializeField]
	private string buildFolder = string.Empty;

	[SerializeField]
	private string executableName = "Game";

	[SerializeField]
	private string macAppStoreExecutable;

	[Header("Targets")][SerializeField]
	private bool Windows = true;

	[SerializeField]
	private bool MacOSX = true;

	[SerializeField]
	private bool Linux = true;

	[PropertyChange("UpdateSettings")][SerializeField][Space(20f)]
	private bool SteamIntegration;

	[InspectorButton("Fix GoG Mac", "FixGoGMacBuild", true, 150f)][PropertyChange("UpdateSettings")][SerializeField]
	private bool goGIntegration;

	[InspectorButton("Fix MacAppStore", "FixMacAppStoreBuild", true, 150f)][PropertyChange("UpdateSettings")][SerializeField]
	private bool macAppStore;

	[SerializeField]
	private bool censorSexualContent;

	[PropertyChange("UpdateSettings")][SerializeField][Space(20f)]
	private bool DebugOverlay;

	[SerializeField]
	private bool Development = true;

	[SerializeField]
	private bool showIntro;

	[SerializeField]
	private bool sandboxMode;

	[SerializeField]
	private bool sandboxModeDebug;

	[InspectorButton("Build", "Build", false, 50f)][SerializeField]
	private bool showBetaOverlay;

	private static BuildInfo instance;
}
