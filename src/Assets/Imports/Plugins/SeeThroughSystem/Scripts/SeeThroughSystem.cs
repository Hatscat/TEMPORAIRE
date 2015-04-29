using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Image Effects/See-Through System/See-Through System")]
public class SeeThroughSystem : MonoBehaviour {
	
	public LayerMask TriggerLayers;
	public LayerMask ObstacleLayers;
	public LayerMask BackgroundLayers = (LayerMask)int.MaxValue;
	public Renderer[] disabledTriggers;
	public Renderer[] disabledObstacles;
	public bool colorizeTriggers;
	public bool colorTriggersRange;
	public float colorStrength;
	public Color coloredTriggersDefaultColor = Color.white;
	public ColoredObject[] coloredTriggers;
	public bool preserveDepthTexture;
	public bool checkRenderTypes = true;
	public float alphaDiscard = 0.01f;
	public float transparency = 1;
	public float sensitivity = 0;
	public bool triggerCheck;
	public float blurSpilling = 0;
	public bool hardBlur = true; 
	public int HBdownsample = 1;
	public float HBblurSize = 5;
	public int HBblurIterations = 1;
	public bool softBlur = true;
	public int SBdownsample = 1;
	public float SBblurSize = 4;
	public int SBblurIterations = 1;
	public Color tintColor = Color.gray;
	public BackgroundRender backgroundRenderType = BackgroundRender.simple;
	public float outline = 0.001f;
	public Color effectColor = Color.blue;
	public Color backgroundColor = Color.black;
	public Shader replacementShader;
	public string shaderTags;
	public bool messageBeforeRender;
	public GameObject messageReciever;
	public string messageMethod = "STS_BeforeBackRender";
	public bool forceForwardRenderingOnBackground;
	public bool showObscuranceMask = false;
	public bool showColorMask = false;
	
	public Camera backgroundCamera;
	
	private Camera objectsCamera;
	new private Camera camera;
	
	
	public enum BackgroundRender
	{
		simple = 0,
		outline = 1,
		hologram = 2,
		alpha_hologram = 3,
		custom_shader_replacement = 100
	}
	
	[System.Serializable]
	public class ColoredObject
	{
		public Color color;
		public Renderer[] triggers;
	}
	
	
	private int downsample;
	private float blurSize;
	private int blurIterations;
	
	private RenderTexture backgroundTexture;
	private RenderTexture objectsTexture;
	private RenderTexture obstaclesTexture;
	private RenderTexture originalZBuffer;
	private RenderTexture checkBuffer1;
	private RenderTexture checkBuffer2;
	private RenderTexture checkBuffer3;
	private RenderTexture checkBuffer4;
	
	private Shader whiteMaskShader;
	private Shader redMaskShader;
	private Shader redMaskShaderRT;
	private Shader greenMaskShader;
	private Shader greenMaskShaderRT;
	private Shader coloredMaskShader;
	private Shader coloredMaskShaderRT;
	private Shader outlineShader;
	private Shader hologramShader;
	private Shader alphaHologramShader;
	
	
	private Texture2D checkTexture;		
	
	private Material curMaterial;
	private Material curMaskMaterial;
	private Material curOrigMaterial;
	private Material curSatMaterial;
	private Material curSatColMaterial;
	private Material curCheckMaterial;
	private Material curBlurMaterial;
	private Material[][] origRendMats;
	private Material[] rendMats;
	
	private Vector2 newResolution;
	private Vector2 oldResolution;
	
	private bool oldColorizeTriggers;
	
	const int CheckMaskRes = 8;
	
	
	Material checkMaterial
	{
		get
		{
			if (curCheckMaterial == null) 
			{
				curCheckMaterial = new Material(Shader.Find("Hidden/STS_mask_check"));
				curCheckMaterial.hideFlags = HideFlags.DontSave;
			}
			return curCheckMaterial;
		}
	}
	
	Material satMaterial 
	{
		get 
		{
			if (curSatMaterial == null) 
			{
				curSatMaterial = new Material(Shader.Find("Hidden/STS_saturate_mask"));
				curSatMaterial.hideFlags = HideFlags.DontSave;
			}
			return curSatMaterial;
		} 
	} 
	
	Material satColMaterial 
	{
		get 
		{
			if (curSatColMaterial == null) 
			{
				curSatColMaterial = new Material(Shader.Find("Hidden/STS_saturate_color_mask"));
				curSatColMaterial.hideFlags = HideFlags.DontSave;
			}
			return curSatColMaterial;
		} 
	} 
	
	
	
	Material blurMaterial 
	{
		get 
		{
			if (curBlurMaterial == null) 
			{
				curBlurMaterial = new Material(Shader.Find("Hidden/STS_FastBlur"));
				curBlurMaterial.hideFlags = HideFlags.DontSave;
			}
			return curBlurMaterial;
		} 
	} 
	
	Material material
	{
		get
		{
			if(curMaterial == null || oldColorizeTriggers != colorizeTriggers)
			{
				if(curMaterial != null)
				{
					DestroyImmediate(curMaterial);
				}
				if (colorizeTriggers)
					curMaterial = new Material(Shader.Find("Hidden/STS_compose"));
				else
					curMaterial = new Material(Shader.Find("Hidden/STS_compose_no_color"));
				curMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return curMaterial;
		}
	}
	
	Material maskMaterial
	{
		get
		{
			if(curMaskMaterial == null)
			{
				if(curMaskMaterial != null)
				{
					DestroyImmediate(curMaskMaterial);
				}
				curMaskMaterial = new Material(Shader.Find("Hidden/STS_trigger_mask_pass"));
				curMaskMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return curMaskMaterial;
		}
	}
	
	
	
	Material origMaterial
	{
		get
		{
			if(curOrigMaterial == null)
			{
				if(curOrigMaterial != null)
				{
					DestroyImmediate(curOrigMaterial);
				}
				curOrigMaterial = new Material(Shader.Find("Hidden/STS_save_orig_depth"));
				curOrigMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return curOrigMaterial;
		}
	}
	
	public void SetScissorRect( Camera cam, Rect r )
	{		
		if ( r.x < 0 )
		{
			r.width += r.x;
			r.x = 0;
		}
		
		if ( r.y < 0 )
		{
			r.height += r.y;
			r.y = 0;
		}
		
		r.width = Mathf.Min( 1 - r.x, r.width );
		r.height = Mathf.Min( 1 - r.y, r.height );			
		cam.rect = new Rect (0,0,1,1);
		cam.ResetProjectionMatrix ();
		Matrix4x4 m = cam.projectionMatrix;
		cam.rect = r;
		//Matrix4x4 m1 = Matrix4x4.TRS( new Vector3( r.x, r.y, 0 ), Quaternion.identity, new Vector3( r.width, r.height, 1 ) );
		Matrix4x4 m2 = Matrix4x4.TRS (new Vector3 ( ( 1/r.width - 1), ( 1/r.height - 1 ), 0), Quaternion.identity, new Vector3 (1/r.width, 1/r.height, 1));
		Matrix4x4 m3 = Matrix4x4.TRS( new Vector3( -r.x  * 2 / r.width, -r.y * 2 / r.height, 0 ), Quaternion.identity, Vector3.one );
		cam.projectionMatrix = m3 * m2 * m; 
	} 
	
	
	public void DisableTrigger(Renderer rend)
	{
		Array.Resize(ref disabledTriggers,disabledTriggers.Length+1);
		disabledTriggers[disabledTriggers.Length-1] = rend;
	}
	
	public void DisableObstacle(Renderer rend)
	{
		Array.Resize(ref disabledObstacles,disabledObstacles.Length+1);
		disabledTriggers[disabledObstacles.Length-1] = rend;
	}
	
	public void EnableTrigger(Renderer rend)
	{
		bool found = false;
		for (int i = 0; i < disabledTriggers.Length-1; i++)
		{
			if (disabledTriggers[i] == rend)
				found = true;
			if (found)
				disabledTriggers[i] = disabledTriggers[i+1];
		}
		Array.Resize(ref disabledTriggers,disabledTriggers.Length+1);
	}
	
	public void EnableObstacle(Renderer rend)
	{
		bool found = false;
		for (int i = 0; i < disabledObstacles.Length-1; i++)
		{
			if (disabledObstacles[i] == rend)
				found = true;
			if (found)
				disabledObstacles[i] = disabledObstacles[i+1];
		}
		Array.Resize(ref disabledObstacles,disabledObstacles.Length+1);
	}
	
	public void ColorizeTriggerObject(Renderer triggerRend, Color color, bool newArrayField = false)
	{
		DecolorizeTriggerObject(triggerRend);
		
		if (!newArrayField)
		{
			for (int i = 0; i < coloredTriggers.Length-1; i++)
			{
				if (coloredTriggers[i].color == color)
				{
					ColorizeTriggerObject(triggerRend,i);
					return;
				}
			}
		}
		
		Array.Resize(ref coloredTriggers, coloredTriggers.Length+1);
		coloredTriggers[coloredTriggers.Length+1] = new ColoredObject();
		coloredTriggers[coloredTriggers.Length+1].color = color;
		coloredTriggers[coloredTriggers.Length+1].triggers = new Renderer[1];
		coloredTriggers[coloredTriggers.Length+1].triggers[0] = triggerRend;
	}
	
	public void ColorizeTriggerObject(Renderer triggerRend, int coloredTriggersInd)
	{
		DecolorizeTriggerObject(triggerRend);
		Array.Resize(ref coloredTriggers[coloredTriggersInd].triggers, coloredTriggers[coloredTriggersInd].triggers.Length + 1);
		coloredTriggers[coloredTriggersInd].triggers[coloredTriggers[coloredTriggersInd].triggers.Length+1] = triggerRend;
	}
	
	public void ColorizeTriggerObject(GameObject trigger, Color color, bool newArrayField = false)
	{
		Component[] rends;
		rends = trigger.GetComponentsInChildren<Renderer>();
		if (!newArrayField)
		{
			for (int i = 0; i < coloredTriggers.Length-1; i++)
			{
				if (coloredTriggers[i].color == color)
				{
					foreach(Renderer rend in rends)
						ColorizeTriggerObject(rend,i);
					return;
				}
			}
		}
		Array.Resize(ref coloredTriggers, coloredTriggers.Length+1);
		coloredTriggers[coloredTriggers.Length+1] = new ColoredObject();
		coloredTriggers[coloredTriggers.Length+1].color = color;
		coloredTriggers[coloredTriggers.Length+1].triggers = new Renderer[rends.Length];
		for (int i = 0; i < rends.Length; i++)
		{
			DecolorizeTriggerObject((Renderer)rends[i]);
			coloredTriggers[coloredTriggers.Length+1].triggers[i] = (Renderer)rends[i];
		}
	}
	
	public void DecolorizeTriggerObject(Renderer triggerRend)
	{
		bool found = false;
		foreach(ColoredObject colObj in coloredTriggers)
		{
			for (int i = 0; i < colObj.triggers.Length; i++)
			{
				if (colObj.triggers[i] == triggerRend)
					found = true;
				if (found && i != colObj.triggers.Length-1)
					colObj.triggers[i] = colObj.triggers[i+1];
			}
			if (found)
			{
				Array.Resize(ref colObj.triggers, colObj.triggers.Length-1);
				return;
			}
		}
	}
	
	
	
	private void Blur (Material satMat, RenderTexture source, RenderTexture destination)
	{
		float widthMod = 1.0f / (1.0f * (1<<downsample));
		blurMaterial.SetVector("_Parameter", new Vector4(blurSize * widthMod, -blurSize * widthMod, 0.0f, 0.0f)); 
		source.filterMode = FilterMode.Bilinear;
		int rtW = source.width >> downsample;
		int rtH = source.height >> downsample;
		RenderTexture rt = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);
		rt.filterMode = FilterMode.Bilinear; 
		Graphics.Blit (source, rt, blurMaterial, 0); 
		for(int i = 0; i < blurIterations; i++) 
		{
			float iterationOffs  = (i*1.0f);
			blurMaterial.SetVector ("_Parameter", new Vector4 (blurSize * widthMod + iterationOffs, -blurSize * widthMod - iterationOffs, 0.0f, 0.0f));
			
			// vertical blur
			RenderTexture rt2  = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);
			rt2.filterMode = FilterMode.Bilinear;
			Graphics.Blit (rt, rt2, blurMaterial, 1);
			RenderTexture.ReleaseTemporary (rt);
			rt = rt2;
			
			// horizontal blur
			rt2 = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);
			rt2.filterMode = FilterMode.Bilinear;
			Graphics.Blit (rt, rt2, blurMaterial, 2);
			RenderTexture.ReleaseTemporary (rt);
			rt = rt2;
		} 
		Graphics.Blit (rt, destination,satMat);
		
		RenderTexture.ReleaseTemporary (rt); 
	}
	
	
	void GetDepthTransMask(RenderTexture source, RenderTexture dest)
	{
		RenderTexture depthTransBuffer = RenderTexture.GetTemporary(source.width,source.height,0);
		Graphics.Blit(source,depthTransBuffer,maskMaterial);
		
		RenderTexture saturatedMask = RenderTexture.GetTemporary(source.width,source.height,0);
		if (hardBlur)
		{
			satMaterial.SetFloat("_power",10000);
			blurIterations = HBblurIterations;
			blurSize = HBblurSize;
			downsample = HBdownsample;
			if (softBlur)
			{
				Blur (satMaterial, depthTransBuffer,saturatedMask);
			}
			else
			{
				Blur (satMaterial, depthTransBuffer,dest);
			}		
		}
		if (softBlur)
		{
			satMaterial.SetFloat("_power",1);
			blurIterations = SBblurIterations;
			blurSize = SBblurSize;
			downsample = SBdownsample;
			if (hardBlur)
			{
				Blur (satMaterial, saturatedMask,dest);
			}
			else
			{
				Blur (satMaterial, depthTransBuffer,dest);
			}
		}
		satMaterial.SetFloat("_transparency",transparency);
		if (!hardBlur && !softBlur)
			Graphics.Blit(depthTransBuffer,dest,satMaterial);
		RenderTexture.ReleaseTemporary(depthTransBuffer);
		RenderTexture.ReleaseTemporary(saturatedMask);
	}
	
	void BlurColorMask(RenderTexture source, RenderTexture dest)
	{
		RenderTexture saturatedMask;
		satColMaterial.SetFloat("_ColorStrength",colorStrength);
		saturatedMask = RenderTexture.GetTemporary(source.width,source.height,0);
		if (hardBlur)
		{
			blurIterations = HBblurIterations;
			blurSize = HBblurSize;
			downsample = HBdownsample;
			if (softBlur)
			{
				Blur (satColMaterial, source,saturatedMask);
			}
			else
				Blur (satColMaterial,source,dest);
		}
		if (softBlur)
		{
			blurIterations = SBblurIterations;
			blurSize = SBblurSize;
			downsample = SBdownsample;
			if (hardBlur)
				Blur (satColMaterial,saturatedMask,dest);
			else
				Blur (satColMaterial,source,dest);
		}
		RenderTexture.ReleaseTemporary(saturatedMask);
	}
	
	private Color InvertColor(Color col)
	{
		return new Color(1 - col.r,1 - col.g,1 - col.b);
	}
	
	void OnRenderImage(RenderTexture src, RenderTexture dest) 
	{
		if (transparency > 0)
		{
			if (checkRenderTypes)
				Shader.SetGlobalFloat("_alphaDiscard",alphaDiscard);

			Shader.SetGlobalFloat("_STSObstDepthShift",sensitivity);
			
			newResolution = new Vector2((int)camera.pixelWidth,(int)camera.pixelHeight);
			
			if (oldResolution != newResolution)
			{
				objectsTexture.Release();
				backgroundTexture.Release();
				obstaclesTexture.Release();
				objectsTexture = new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,24);
				backgroundTexture = new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,24);
				obstaclesTexture = new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,24);
				if (originalZBuffer != null)
					originalZBuffer.Release();
				if (preserveDepthTexture)
				{
					originalZBuffer =  new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,0);
					originalZBuffer.format = RenderTextureFormat.RFloat;
				}
			}
			
			//Saving original depth buffer;
			if (preserveDepthTexture)
			{
				if (originalZBuffer == null)
				{
					originalZBuffer =  new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,0);
					originalZBuffer.format = RenderTextureFormat.RFloat;
				}
				else
				{
					originalZBuffer.DiscardContents();
				}
				Graphics.Blit(obstaclesTexture,originalZBuffer,origMaterial);
			}
			
			//Disabling renderers
			foreach (Renderer rend in disabledTriggers)
				if (rend != null)
					rend.enabled = false;
			foreach (Renderer rend in disabledObstacles)
				if (rend != null)
					rend.enabled = false;
			
			//Rendering obstacle mask
			objectsCamera.CopyFrom(camera);
			objectsCamera.renderingPath = RenderingPath.VertexLit;
			objectsCamera.targetTexture = obstaclesTexture;
			objectsCamera.backgroundColor = Color.black;
			objectsCamera.clearFlags = CameraClearFlags.Color;
			objectsCamera.depthTextureMode = DepthTextureMode.None;
			
			objectsCamera.cullingMask = ObstacleLayers.value;
			if (!checkRenderTypes)
				objectsCamera.RenderWithShader(redMaskShader,"");
			else
				objectsCamera.RenderWithShader(redMaskShaderRT,"");
			
			objectsCamera.cullingMask = TriggerLayers.value;
			objectsCamera.clearFlags = CameraClearFlags.Nothing;
			if (!checkRenderTypes)
				objectsCamera.RenderWithShader(greenMaskShader,"");
			else
				objectsCamera.RenderWithShader(greenMaskShaderRT,"RenderType");
			
			material.SetTexture("_ObstMaskTex",obstaclesTexture);
			material.SetFloat("_BlurSpilling",blurSpilling);
			
			
			maskMaterial.SetTexture("_ObjTex",obstaclesTexture);
			
			objectsCamera.clearFlags = CameraClearFlags.Color;
			objectsCamera.targetTexture = objectsTexture;
			if (!checkRenderTypes)
				objectsCamera.RenderWithShader(greenMaskShader,"");
			else
				objectsCamera.RenderWithShader(greenMaskShaderRT,"RenderType");
			RenderTexture buffer2;
			buffer2 = RenderTexture.GetTemporary(backgroundTexture.width, backgroundTexture.height, 0);
			GetDepthTransMask(objectsTexture,buffer2);
			
			//Show debug mask if toggled on
			if (showObscuranceMask)
			{
				Graphics.Blit (buffer2,dest);
				objectsTexture.DiscardContents();
				backgroundTexture.DiscardContents();
				obstaclesTexture.DiscardContents();
				
				RenderTexture.ReleaseTemporary(buffer2);
				
				oldResolution = newResolution;
				oldColorizeTriggers = colorizeTriggers;
				return;
			}
			
			//Check if depth-transparency mask have something on it - if it doesn't, we don't need to do anything further (Cost 1-2ms CPU time due to ReadPixels call and 3 drawcalls)
			//Also changing projection matrix of background camera to render only visible parts
			if (triggerCheck)
			{
				Graphics.Blit(buffer2,checkBuffer1,checkMaterial);
				Graphics.Blit(checkBuffer1,checkBuffer2,checkMaterial);
				Graphics.Blit(checkBuffer2,checkBuffer3,checkMaterial);
				RenderTexture oldRT = RenderTexture.active;
				RenderTexture.active = checkBuffer3;
				checkTexture.ReadPixels(new Rect(0,0,8,8),0,0,false);
				RenderTexture.active = oldRT;
				
				checkBuffer1.DiscardContents();
				checkBuffer2.DiscardContents();
				checkBuffer3.DiscardContents();
				
				Color[] pixels = checkTexture.GetPixels();
				bool black = true;
				int minX = 8;
				int maxX = 0;
				int minY = 8;
				int maxY = 0;
				int x;
				int y;
				for (int i = 0; i < pixels.Length; i++)
				{
					if (pixels[i].g > 0)
					{
						black = false;
						x = i % 8;
						y = i / 8;
						if (x < minX)
							minX = x;
						if (x > maxX)
							maxX = x;
						if (y < minY)
							minY = y;
						if (y > maxY)
							maxY = y;
					}
				}
				
				if (black)
				{
					//If trigger mask is black, just passing input image to output and cleaning up;
					Graphics.Blit(src,dest);
					RenderTexture.ReleaseTemporary(buffer2);
					oldResolution = newResolution;
					oldColorizeTriggers = colorizeTriggers;
					
					//Restoring disabled renderers
					foreach (Renderer rend in disabledTriggers)
						if (rend != null)
							rend.enabled = true;
					foreach (Renderer rend in disabledObstacles)
						if (rend != null)
							rend.enabled = true;
					
					//Restoring original depth texture
					if (preserveDepthTexture)
					{
						Shader.SetGlobalTexture("_CameraDepthTexture",originalZBuffer);
					}
					return;
				}
				
				backgroundCamera.CopyFrom(camera);
				if (forceForwardRenderingOnBackground)
					backgroundCamera.renderingPath = RenderingPath.Forward;
				Rect camScissorRect = new Rect(minX * 0.125f,minY * 0.125f, (maxX-minX+1) * 0.125f,(maxY-minY+1) * 0.125f);
				SetScissorRect(backgroundCamera,camScissorRect);
			}
			else
			{
				backgroundCamera.CopyFrom(camera);
				if (forceForwardRenderingOnBackground)
					backgroundCamera.renderingPath = RenderingPath.Forward;
			}
			
			//Rendering color mask if colorize toggled on
			RenderTexture colorMask = RenderTexture.GetTemporary(src.width,src.height,24);
			if (colorizeTriggers && coloredTriggers.Length > 0)
			{
				Shader.SetGlobalColor("_STScolor",InvertColor(coloredTriggersDefaultColor));
				for (int i = 0; i < coloredTriggers.Length; i++)
				{
					Color col = InvertColor(coloredTriggers[i].color);
					float maxC = Mathf.Max(Mathf.Max(col.r,col.g), col.b);
					col = col / maxC;
					for (int x = 0; x < coloredTriggers[i].triggers.Length; x++)
					{
						foreach(Material mat in coloredTriggers[i].triggers[x].materials)
						{
							mat.SetColor("_STScolor",col * colorStrength);
						}
					}
				}
				objectsCamera.targetTexture = colorMask;
				//objectsCamera.backgroundColor = Color.white;
				if (!checkRenderTypes)
					objectsCamera.RenderWithShader(coloredMaskShader,"");
				else
					objectsCamera.RenderWithShader(coloredMaskShaderRT,"RenderType");
				if (colorTriggersRange)
					BlurColorMask(colorMask,colorMask);
				
				material.SetTexture("_ColorMask",colorMask);
				
				/*
				Graphics.Blit(colorMask,dest);

				objectsTexture.DiscardContents();
				backgroundTexture.DiscardContents();
				obstaclesTexture.DiscardContents();
				RenderTexture.ReleaseTemporary(buffer2);
				RenderTexture.ReleaseTemporary(colorMask);
				return;
				*/
			}
			
			//rendering color mask if debug option toggled
			if (showColorMask)
			{
				Graphics.Blit (colorMask,dest);
				objectsTexture.DiscardContents();
				backgroundTexture.DiscardContents();
				obstaclesTexture.DiscardContents();
				
				
				RenderTexture.ReleaseTemporary(buffer2);
				RenderTexture.ReleaseTemporary(colorMask);
				
				oldResolution = newResolution;
				oldColorizeTriggers = colorizeTriggers;
				return;
			}
			
			backgroundCamera.cullingMask = BackgroundLayers.value;
			backgroundCamera.targetTexture = backgroundTexture;
			
			
			material.SetTexture("_MaskTex",buffer2);
			material.SetColor("_TintColor",tintColor);
			
			
			
			
			//Rendering background
			if (messageBeforeRender && messageReciever != null)
				messageReciever.SendMessage(messageMethod,backgroundCamera);
			
			switch (backgroundRenderType)
			{
			case BackgroundRender.simple:		
				backgroundCamera.Render();
				break;
			case BackgroundRender.outline:		
				Shader.SetGlobalFloat("_stw_outline",outline);
				Shader.SetGlobalColor("_sts_effect_color",effectColor);
				backgroundCamera.clearFlags = CameraClearFlags.Color;
				backgroundCamera.backgroundColor = backgroundColor;
				backgroundCamera.RenderWithShader(outlineShader,"");
				break;
			case BackgroundRender.hologram:		
				Shader.SetGlobalColor("_sts_effect_color",effectColor);
				backgroundCamera.clearFlags = CameraClearFlags.Color;
				backgroundCamera.backgroundColor = backgroundColor;	
				backgroundCamera.RenderWithShader(hologramShader,"");
				break;
			case BackgroundRender.alpha_hologram:		
				Shader.SetGlobalColor("_sts_effect_color",effectColor);
				backgroundCamera.clearFlags = CameraClearFlags.Color;
				backgroundCamera.backgroundColor = backgroundColor;	
				backgroundCamera.RenderWithShader(alphaHologramShader,"");
				break;
			case BackgroundRender.custom_shader_replacement:
				backgroundCamera.RenderWithShader(replacementShader,shaderTags);
				break;
			}
			
			material.SetTexture("_BackTex",backgroundTexture);
			
			Graphics.Blit(src,dest,material);
			//Graphics.Blit(backgroundTexture,dest);
			
			//Restoring disabled renderers
			foreach (Renderer rend in disabledTriggers)
				if (rend != null)
					rend.enabled = true;
			foreach (Renderer rend in disabledObstacles)
				if (rend != null)
					rend.enabled = true;
			
			//Restoring original depth texture
			if (preserveDepthTexture)
			{
				Shader.SetGlobalTexture("_CameraDepthTexture",originalZBuffer);
			}
			
			//Clean up
			objectsTexture.DiscardContents();
			backgroundTexture.DiscardContents();
			obstaclesTexture.DiscardContents();
			RenderTexture.ReleaseTemporary(buffer2);
			RenderTexture.ReleaseTemporary(colorMask);
			
			oldResolution = newResolution;
			oldColorizeTriggers = colorizeTriggers;
		}
		else
		{
			Graphics.Blit(src,dest);
		}
	}
	
	
	
	// Use this for initialization
	void Start () 
	{
		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
		
		camera = GetComponent<Camera>();           
		
		//Buffers for mask checking. Not the most elegant solution, but least CPU-intensive - still pretty heavy CPU-wise due to ReadPixels (pipeline break)
		checkBuffer1 = new RenderTexture(512,512,0);
		checkBuffer2 = new RenderTexture(64,64,0);
		checkBuffer3 = new RenderTexture(8,8,0);
		
		checkTexture = new Texture2D(8,8);
		
		//Shaders for mask rendering
		redMaskShader = Shader.Find("Hidden/STS_red_mask");
		redMaskShaderRT = Shader.Find("Hidden/STS_red_mask_RT");
		greenMaskShader = Shader.Find("Hidden/STS_green_mask");
		greenMaskShaderRT = Shader.Find("Hidden/STS_green_mask_RT");
		coloredMaskShader = Shader.Find("Hidden/STS_colored_mask");
		coloredMaskShaderRT = Shader.Find("Hidden/STS_colored_mask_RT");
		
		//Shaders for sfx background rendering
		outlineShader = Shader.Find("See-Through System/Outline");
		hologramShader = Shader.Find("See-Through System/Hologram");
		alphaHologramShader = Shader.Find("See-Through System/Alpha_Hologram");
		
		//for monitoring camera render area resizes
		oldResolution = new Vector2((int)camera.pixelWidth,(int)camera.pixelHeight);
		
		//Creating cameras;
		GameObject obj = new GameObject("STS_mask_camera");
		obj.transform.parent = transform;
		objectsCamera = obj.AddComponent<Camera>();
		objectsCamera.CopyFrom(camera);
		objectsCamera.cullingMask = TriggerLayers;
		objectsCamera.backgroundColor = Color.black;
		objectsCamera.clearFlags = CameraClearFlags.Color;
		objectsCamera.depthTextureMode = DepthTextureMode.None;
		objectsTexture = new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,24);
		obstaclesTexture = new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,24);
		if (preserveDepthTexture)
		{
			originalZBuffer =  new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,0);
			originalZBuffer.format = RenderTextureFormat.RFloat;
		}
		objectsCamera.targetTexture = objectsTexture;
		objectsCamera.enabled = false;
		
		if (backgroundCamera == null)
		{
			obj = new GameObject("STS_background_camera");
			obj.transform.parent = transform;
			backgroundCamera = obj.AddComponent<Camera>();
			backgroundCamera.CopyFrom(camera);
			backgroundCamera.cullingMask = backgroundCamera.cullingMask & (~ObstacleLayers.value);
			backgroundTexture = new RenderTexture((int)camera.pixelWidth, (int)camera.pixelHeight,24);
			backgroundCamera.depthTextureMode = DepthTextureMode.None;
			backgroundCamera.enabled = false;
		}
		
		backgroundCamera.targetTexture = backgroundTexture;
		satMaterial.SetFloat("_power",1);
	}
	
	//When we disable or delete the effect.....
	void OnDisable ()
	{
		if (backgroundTexture != null)
			backgroundTexture.Release();
		if (objectsTexture != null)
			objectsTexture.Release();
		if (obstaclesTexture != null)
			obstaclesTexture.Release();
		if (originalZBuffer != null)
			originalZBuffer.Release();
		if (checkBuffer1 != null)
		{
			checkBuffer1.Release();
			checkBuffer2.Release();
			checkBuffer3.Release();
		}
		
		if(curCheckMaterial != null)		
			DestroyImmediate(curCheckMaterial);
		if(curMaterial != null)		
			DestroyImmediate(curMaterial);
		if(curOrigMaterial != null)		
			DestroyImmediate(curOrigMaterial);
		if(curSatMaterial != null)		
			DestroyImmediate(curSatMaterial);
		if(curMaskMaterial != null)		
			DestroyImmediate(curMaskMaterial);
		if(curBlurMaterial != null)		
			DestroyImmediate(curBlurMaterial);
	}
	
}
