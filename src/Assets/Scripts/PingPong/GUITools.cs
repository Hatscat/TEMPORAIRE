using UnityEngine;
using System.Collections;
using System;

public static class GUITools  {
	/*
	public static void TOPORLEFT(Action guiMethod)
	{
		if(guiMethod!=null) guiMethod();
		GUILayout.FlexibleSpace();
	}

	public static void BOTORRIGHT(Action guiMethod)
	{
		GUILayout.FlexibleSpace();
		if(guiMethod!=null) guiMethod();
	}

	public static void CENTER(Action guiMethod)
	{
		GUILayout.FlexibleSpace();
		if(guiMethod!=null) guiMethod();
		GUILayout.FlexibleSpace();
	}

	public static void HGUI(Action guiMethod)
	{
		GUILayout.BeginHorizontal();
		if(guiMethod!=null) guiMethod();
		GUILayout.EndHorizontal();

	}

	public static void VGUI(Action guiMethod)
	{
		GUILayout.BeginVertical();
		if(guiMethod!=null) guiMethod();
		GUILayout.EndVertical();
		
	}

	public static void HCENTER(Action guiMethod)
	{
		GUITools.HGUI(()=>{
			GUITools.CENTER(guiMethod);
		});
	}

	public static void VCENTER(Action guiMethod)
	{
		GUITools.VGUI(()=>{
			GUITools.CENTER(guiMethod);
		});
	}


	public static void TOP(Action guiMethod)
	{
		GUITools.VGUI(()=>{
			GUITools.TOPORLEFT(guiMethod);
		});
	}

	public static void LEFT(Action guiMethod)
	{
		GUITools.HGUI(()=>{
			GUITools.TOPORLEFT(guiMethod);
		});
	}

	public static void RIGHT(Action guiMethod)
	{
		GUITools.HGUI(()=>{
			GUITools.BOTORRIGHT(guiMethod);
		});
	}

	public static void BOTTOM(Action guiMethod)
	{
		GUITools.VGUI(()=>{
			GUITools.BOTORRIGHT(guiMethod);
		});
	}



	public static void HCENTERLABEL( string text,GUIStyle guiStyle)
	{
		GUITools.HCENTER(()=>{
			GUILayout.Label(text,guiStyle);
		});
	}

	public static void LEFTLABEL( string text,GUIStyle guiStyle)
	{
		GUITools.LEFT(()=>{
			GUILayout.Label(text,guiStyle);
		});
	}

	public static void RIGHTLABEL(string text,GUIStyle guiStyle)
	{
		GUITools.RIGHT(()=>{
			GUILayout.Label(text,guiStyle);
		});
	}
     */
}
