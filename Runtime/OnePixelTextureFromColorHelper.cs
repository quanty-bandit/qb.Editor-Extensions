using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace qb
{
    public static class OnePixelTextureFromColorHelper
    {
        static Dictionary<string, Texture2D> onePixelTextures = new Dictionary<string, Texture2D>();

        public static bool TryToParseHtmlColor(string _htmlColor, out Color _color)
        {
            if (!string.IsNullOrEmpty(_htmlColor))
            {

                if (_htmlColor[0] != '#')
                    _htmlColor = "#" + _htmlColor;
                if (ColorUtility.TryParseHtmlString(_htmlColor, out Color nc))
                {
                    _color = nc;
                    return true;
                }
            }
            _color = Color.white;
            return false;
        }
        public static Texture2D GetOnePixelTextureFromColor(string _htmlColor)
        {
            TryToParseHtmlColor(_htmlColor, out Color color);
            return GetOnePixelTextureFromColor(color);
        }
        public static Texture2D GetOnePixelTextureFromColor(Color _color)
        {
            string key = $"{ColorUtility.ToHtmlStringRGB(_color)}{_color.a}";
            if (!onePixelTextures.ContainsKey(key))
            {
                var backgroundTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                backgroundTexture.SetPixel(0, 0, new Color(_color.r, _color.g, _color.b, _color.a));
                backgroundTexture.Apply();
                onePixelTextures.Add(key, backgroundTexture);
            }
            if (onePixelTextures[key] == null)
            {
                var backgroundTexture = new Texture2D(1, 1);
                backgroundTexture.SetPixel(0, 0, new Color(_color.r, _color.g, _color.b, _color.a));
                backgroundTexture.Apply();
                onePixelTextures[key] = backgroundTexture;
            }
            return onePixelTextures[key];
        }
        public static void ClearTexturesCache()
        {
            if (Application.isPlaying)
                foreach (var entry in onePixelTextures)
                {
                        Object.Destroy(entry.Value);
                }
            else
                foreach (var entry in onePixelTextures)
                {
                    Object.DestroyImmediate(entry.Value);
                }
            onePixelTextures.Clear();
        }
    }
}
