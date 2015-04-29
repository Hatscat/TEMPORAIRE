using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor( typeof(SeeThroughSystem))]
public class STS_editor : Editor {

	private SerializedProperty triggerLayers;
	private SerializedProperty obstacleLayers;
	private SerializedProperty backgroundLayers;
	private SerializedProperty replacementShader;
	private SerializedProperty messageReciever;
	private SerializedProperty backgroundCamera;
	private SerializedProperty disabledTriggers;
	private SerializedProperty disabledObstacles;
	private SerializedProperty coloredTriggers;


	public void OnEnable () 
	{
		triggerLayers = serializedObject.FindProperty("TriggerLayers");
		obstacleLayers = serializedObject.FindProperty("ObstacleLayers");
		backgroundLayers = serializedObject.FindProperty("BackgroundLayers");
		replacementShader = serializedObject.FindProperty("replacementShader");
		messageReciever = serializedObject.FindProperty("messageReciever");
		backgroundCamera = serializedObject.FindProperty("backgroundCamera");
		disabledTriggers = serializedObject.FindProperty("disabledTriggers");
		disabledObstacles = serializedObject.FindProperty("disabledObstacles");
		coloredTriggers = serializedObject.FindProperty("coloredTriggers");
	}


	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		SeeThroughSystem sts = (SeeThroughSystem)target;

		//EditorGUILayout.BeginVertical("box");
		EditorGUILayout.HelpBox("Layers&Objects setup",MessageType.None);
		EditorGUILayout.PropertyField(triggerLayers);
		EditorGUILayout.PropertyField(obstacleLayers);
		EditorGUILayout.PropertyField(backgroundLayers);

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(disabledTriggers, true);
		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(disabledObstacles, true);
		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();

		EditorGUILayout.Space();
		EditorGUILayout.HelpBox("Colorized triggers setup",MessageType.None);
		if (sts.colorizeTriggers = EditorGUILayout.ToggleLeft("Colorize triggers",sts.colorizeTriggers))
		{
			sts.colorTriggersRange = EditorGUILayout.ToggleLeft("Expand color mask to range",sts.colorTriggersRange);
			sts.colorStrength = EditorGUILayout.Slider("Color strength",sts.colorStrength,0,1);
			sts.coloredTriggersDefaultColor = EditorGUILayout.ColorField("Default trigger color",sts.coloredTriggersDefaultColor);
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(coloredTriggers, true);
			if(EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
		}


		//EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//EditorGUILayout.BeginVertical("box");
		EditorGUILayout.HelpBox("Background setup",MessageType.None);
		sts.backgroundRenderType = (SeeThroughSystem.BackgroundRender)EditorGUILayout.EnumPopup("Background type",sts.backgroundRenderType);
		switch (sts.backgroundRenderType)
		{
		case SeeThroughSystem.BackgroundRender.simple:
			sts.tintColor = EditorGUILayout.ColorField("Tint color",sts.tintColor);
			break;
		case SeeThroughSystem.BackgroundRender.alpha_hologram:
			sts.tintColor = EditorGUILayout.ColorField("Tint color",sts.tintColor);
			sts.effectColor = EditorGUILayout.ColorField("Hologram color",sts.effectColor);
			sts.backgroundColor = EditorGUILayout.ColorField("Background color",sts.backgroundColor);
			break;
		case SeeThroughSystem.BackgroundRender.hologram:
			sts.tintColor = EditorGUILayout.ColorField("Tint color",sts.tintColor);
			sts.effectColor = EditorGUILayout.ColorField("Hologram color",sts.effectColor);
			sts.backgroundColor = EditorGUILayout.ColorField("Background color",sts.backgroundColor);
			break;
		case SeeThroughSystem.BackgroundRender.outline:
			sts.tintColor = EditorGUILayout.ColorField("Tint color",sts.tintColor);
			sts.effectColor = EditorGUILayout.ColorField("Outline color",sts.effectColor);
			sts.backgroundColor = EditorGUILayout.ColorField("Background color",sts.backgroundColor);
			sts.outline = EditorGUILayout.Slider("Outline thickness",sts.outline,0,0.01f);
			break;
		case SeeThroughSystem.BackgroundRender.custom_shader_replacement:
			EditorGUILayout.PropertyField(replacementShader);
			sts.shaderTags = EditorGUILayout.TextField("Shader tags",sts.shaderTags);
			break;
		}
		EditorGUILayout.Space();
		sts.messageBeforeRender = EditorGUILayout.ToggleLeft("Message before background rendering",sts.messageBeforeRender);
		if (sts.messageBeforeRender)
		{
			EditorGUILayout.BeginVertical("box");
			EditorGUILayout.PropertyField(messageReciever);
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.Space();
		EditorGUILayout.HelpBox("Leave empty for auto-generation",MessageType.None);
		EditorGUILayout.PropertyField(backgroundCamera);
		//EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//EditorGUILayout.BeginVertical("box");
		EditorGUILayout.HelpBox("Transparent area setup",MessageType.None);
		sts.transparency = EditorGUILayout.Slider("Transparency",sts.transparency,0,1);
		sts.sensitivity = EditorGUILayout.Slider("Sensitivity",sts.sensitivity,-0.5f,0.5f);
		sts.hardBlur = 	EditorGUILayout.ToggleLeft("Transparent area range",sts.hardBlur);
		if (sts.hardBlur)
		{
			EditorGUILayout.BeginVertical("box");
			sts.HBblurSize = EditorGUILayout.Slider("Range size",sts.HBblurSize,0,15);
			sts.HBblurIterations = EditorGUILayout.IntSlider("Iterations",sts.HBblurIterations,1,5);
			sts.HBdownsample = EditorGUILayout.IntSlider("Downsample",sts.HBdownsample,0,5);
			EditorGUILayout.EndVertical();
		}
		sts.softBlur = 	EditorGUILayout.ToggleLeft("Transparent area blur",sts.softBlur);
		if (sts.softBlur)
		{
			EditorGUILayout.BeginVertical("box");
			sts.SBblurSize = EditorGUILayout.Slider("Blur size",sts.SBblurSize,0,15);
			sts.SBblurIterations = EditorGUILayout.IntSlider("Iterations",sts.SBblurIterations,1,5);
			sts.SBdownsample = EditorGUILayout.IntSlider("Downsample",sts.SBdownsample,0,5);
			EditorGUILayout.EndVertical();
		}
		if (sts.hardBlur || sts.softBlur)
			sts.blurSpilling = EditorGUILayout.Slider("Range&Blur spilling",sts.blurSpilling,0,1);
		EditorGUILayout.Space();
		//EditorGUILayout.EndVertical();
		EditorGUILayout.HelpBox("Advanced settings",MessageType.None);
		sts.forceForwardRenderingOnBackground = EditorGUILayout.ToggleLeft("Force Forward rendering on background",sts.forceForwardRenderingOnBackground);
		sts.triggerCheck = EditorGUILayout.ToggleLeft("Check for transparency",sts.triggerCheck);
		if (sts.checkRenderTypes = EditorGUILayout.ToggleLeft("Check render types",sts.checkRenderTypes))
			sts.alphaDiscard = EditorGUILayout.Slider("Alpha discard level",sts.alphaDiscard,0,1);
		sts.preserveDepthTexture = EditorGUILayout.ToggleLeft("Preserve depth texture",sts.preserveDepthTexture);
		sts.showObscuranceMask = EditorGUILayout.ToggleLeft("Show obscurance mask",sts.showObscuranceMask);
		sts.showColorMask = EditorGUILayout.ToggleLeft("Show color trigger mask",sts.showColorMask);
	}
}
