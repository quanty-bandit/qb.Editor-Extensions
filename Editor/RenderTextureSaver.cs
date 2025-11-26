using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace Helpers.Editor
{
    public class RenderTextureSaver : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Texture/Save")]
        public static void SaveTexture()
        {
            RenderTexture target = Selection.activeObject as RenderTexture;
            if (target == null)
                target = Selection.activeObject as CustomRenderTexture;

            var path = EditorUtility.SaveFilePanel("save texture", Application.dataPath, target.name, "png");
            if (!string.IsNullOrEmpty(path))
            {
                RenderTexture.active = target;
                Texture2D tex = new Texture2D(target.width, target.height, TextureFormat.RGB24, false);
                tex.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
                RenderTexture.active = null;

                byte[] bytes;
                bytes = tex.EncodeToPNG();
                System.IO.File.WriteAllBytes(path, bytes);
            }
        }
        [MenuItem("Assets/Texture/Save", true)]
        public static bool SaveTextureTest()
        {
            return Selection.activeObject is RenderTexture || Selection.activeObject is CustomRenderTexture;
        }

#endif
    }
}
