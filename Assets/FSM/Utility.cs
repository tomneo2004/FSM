using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NP.Util{

	public static class Texture2DExtensions {

		public static void SetColor(this Texture2D tex2, Color32 color) {


			var fillColorArray = tex2.GetPixels32();

			for (var i = 0; i < fillColorArray.Length; ++i) {
				fillColorArray[i] = color;
			}

			tex2.SetPixels32(fillColorArray);

			tex2.Apply();
		}


	}

	public class TextureUtil{
	
		public static Texture2D Texture2DSolidColor(Vector2Int textureSize, Color32 color){

			Texture2D newTex2D = new Texture2D (textureSize.x, textureSize.y);

			newTex2D.SetColor (color);

			return newTex2D;
		}
	}
}
