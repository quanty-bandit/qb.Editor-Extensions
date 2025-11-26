#define QB_ATTRIBUTE
/*
* Comments attribute definition
* Allows to add comments above parameters in Editor inspector
* Copyright (C) 2022  quanty bandit 
*
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace qb
{
    /// <summary>
    /// Inserts a remarks drop down field filled from string entries
    /// 
    /// </summary>
    /// <param name="entries">Each entry string can be a just a rich text string, a string command, or a json formatted data
    ///
    /// Valid tag for rich text are:
    /// <b></b>
    /// <i></i>
    /// <color=#FFFFFF></color>
    /// <size=20></size>
    /// <br> for line feed
    /// 
    /// Commands string are formatted as $command:value
    /// anchor 
    ///     command 
    ///         $a: or $anchor:
    ///     value 
    ///         UpperLeft     or 0
    ///         UpperCenter   or 1
    ///         UpperRight    or 2 
    ///         MiddleLeft    or 3
    ///         MiddleCenter  or 4
    ///         MiddleRight   or 5 
    ///         LowerLeft     or 6
    ///         LowerCenter   or 7
    ///         LowerRight    or 8
    ///         
    /// default text color 
    ///     command 
    ///         $c: or $tc or $textColor:
    ///     value 
    ///         html color as #rrggbb or #rrggbbaa
    ///         
    /// default background color 
    ///     command 
    ///         $bc: or $backgroundColor:
    ///     value 
    ///         html color as #rrggbb or #rrggbbaa
    ///         
    /// dropdown tint color 
    ///     command 
    ///         $t: or $tint
    ///     value 
    ///         html color as #rrggbb or #rrggbbaa
    /// 
    /// For Json format entry the string must be formatted as:  
    ///     {
    ///		    \"text\":\"A ritch formatted text plus the </br> html tag to line feed \",
    ///		    \"anchor\":\"MiddleCenter\",
    ///		    \"textColor\":\"#333333\",
    ///		    \"backgroundColor\":\"#333333\"
    ///	    }
    ///	
    ///     All json fields are optionnal
    /// 
    ///     If one of the anchor, textColor or backgroundColor is not define the previous values of the missing fields will be used.
    ///
    /// 
    /// </param>
    public sealed class CommentsAttribute : PropertyAttribute
    {
        internal struct Settings
        {
            public string textColor;
            public string backgroundColor;
            public string text;
            public string anchor;
            public TextAnchor textAnchor
            {
                get
                {
                    if (!string.IsNullOrEmpty(anchor))
                    {
                        if (int.TryParse(anchor, out int index))
                        {
                            if (index >= 0 && index < 10)
                            {
                                return (TextAnchor)index;
                            }
                        }

                        var ac = anchor.ToLower();

                        if (ac == TextAnchor.UpperCenter.ToString().ToLower())
                            return TextAnchor.UpperCenter;
                        if (ac == TextAnchor.UpperLeft.ToString().ToLower())
                            return TextAnchor.UpperLeft;
                        if (ac == TextAnchor.UpperRight.ToString().ToLower())
                            return TextAnchor.UpperRight;

                        if (ac == TextAnchor.MiddleCenter.ToString().ToLower())
                            return TextAnchor.MiddleCenter;
                        if (ac == TextAnchor.MiddleLeft.ToString().ToLower())
                            return TextAnchor.MiddleLeft;
                        if (ac == TextAnchor.MiddleRight.ToString().ToLower())
                            return TextAnchor.MiddleRight;

                        if (ac == TextAnchor.LowerCenter.ToString().ToLower())
                            return TextAnchor.LowerCenter;
                        if (ac == TextAnchor.LowerLeft.ToString().ToLower())
                            return TextAnchor.LowerLeft;
                        if (ac == TextAnchor.LowerRight.ToString().ToLower())
                            return TextAnchor.LowerRight;

                    }
                    return TextAnchor.UpperLeft;
                }
            }
        }

        Settings[] __settings;
        internal Settings[] settings { get => __settings; }

        string __title = "Comments...";
        public string title { get => __title; }


        string __uxTint = "";
        public string uxTint { get => __uxTint; }

        bool __isFoldable = true;
        public bool isFoldable { get => __isFoldable; }
        
        bool __displayProperty = true;
        public bool displayProperty { get => __displayProperty; }



        float __yOffset = 0;
        public float yOffset { get => __yOffset; }


        public bool isOpened = true;

        /// <summary>
        /// Inserts a remarks drop down field filled from string entries
        /// 
        /// </summary>
        /// <param name="entries">Each entry string can be a just a rich text string, a string command, or a json formatted data
        ///
        /// Valid tag for rich text are:
        /// <b></b>
        /// <i></i>
        /// <color=#FFFFFF></color>
        /// <size=20></size>
        /// <br> for line feed
        /// 
        /// Commands string are formatted as $command:value
        /// anchor 
        ///     command 
        ///         $a: or $anchor:
        ///     value 
        ///         UpperLeft     or 0
        ///         UpperCenter   or 1
        ///         UpperRight    or 2 
        ///         MiddleLeft    or 3
        ///         MiddleCenter  or 4
        ///         MiddleRight   or 5 
        ///         LowerLeft     or 6
        ///         LowerCenter   or 7
        ///         LowerRight    or 8
        ///         
        /// default text color 
        ///     command 
        ///         $c: or $tc or $textColor:
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        ///         
        /// default background color 
        ///     command 
        ///         $bc: or $backgroundColor:
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        ///         
        /// dropdown tint color 
        ///     command 
        ///         $t: or $tint
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        /// 
        /// For Json format entry the string must be formatted as:  
        ///     {
        ///		    \"text\":\"A ritch formatted text plus the </br> html tag to line feed \",
        ///		    \"anchor\":\"MiddleCenter\",
        ///		    \"textColor\":\"#333333\",
        ///		    \"backgroundColor\":\"#333333\"
        ///	    }
        ///	
        ///     All json fields are optionnal
        /// 
        ///     If one of the anchor, textColor or backgroundColor is not define the previous values of the missing fields will be used.
        ///
        /// 
        /// </param>
        public CommentsAttribute(params string[] entries)
        {
            Init(entries);
        }

        /// <summary>
        /// Init the remarks settings and content from entries
        /// </summary>
        /// <param name="entries">Each entry string can be a just a rich text string, a string command, or a json formatted data
        /// 
        /// Valid tag for rich text are:
        /// <b></b>
        /// <i></i>
        /// <color=#FFFFFF></color>
        /// <size=20></size>
        /// <br> for line feed
        /// 
        /// Commands string are formatted as $command:value
        /// anchor 
        ///     command 
        ///         $a: or $anchor:
        ///     value 
        ///         UpperLeft     or 0
        ///         UpperCenter   or 1
        ///         UpperRight    or 2 
        ///         MiddleLeft    or 3
        ///         MiddleCenter  or 4
        ///         MiddleRight   or 5 
        ///         LowerLeft     or 6
        ///         LowerCenter   or 7
        ///         LowerRight    or 8
        ///         
        /// default text color 
        ///     command 
        ///         $c: or $tc or $textColor:
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        ///         
        /// default background color 
        ///     command 
        ///         $bc: or $backgroundColor:
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        ///         
        /// dropdown tint color 
        ///     command 
        ///         $t: or $tint
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        /// 
        /// For Json format entry the string must be formatted as:  
        ///     {
        ///		    \"text\":\"A ritch formatted text plus the </br> html tag to line feed \",
        ///		    \"anchor\":\"MiddleCenter\",
        ///		    \"textColor\":\"#333333\",
        ///		    \"backgroundColor\":\"#333333\"
        ///	    }
        ///	
        ///     All json fields are optionnal
        /// 
        ///     If one of the anchor, textColor or backgroundColor is not define the previous values of the missing fields will be used.
        ///
        /// </param>
        void Init(params string[] entries)
        {
            List<Settings> ls = new List<Settings>();
            Settings currentSetting = new Settings();
            for (int i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                if (string.IsNullOrEmpty(entry))
                {
                    ls.Add(currentSetting);
                    currentSetting = new Settings();
                    continue;
                }

                var firstChar = entry[0];
                if (firstChar == '{' && entry[entry.Length - 1] == '}')
                {
                    try
                    {
                        // Try to parse json format
                        var s = JsonUtility.FromJson<Settings>(entry);
                        if (!string.IsNullOrEmpty(s.text))
                        {
                            s.text = s.text.Replace("<br>", "\n");
                        }
                        ls.Add(s);
                    }
                    catch (System.Exception _e)
                    {
                        Debug.LogWarning(_e.Message);
                        ls.Add(new Settings() { text = entry.Replace("<br>", "\n") });
                    }
                }
                else
                {
                    if (firstChar == '$')
                    {
                        var arr = entry.Split(':');
                        if (arr.Length == 2)
                        {
                            //It's may be a command
                            var com = arr[0].ToLower();
                            switch (com)
                            {
                                case "$bc":
                                case "$backgoundColor":
                                    currentSetting.backgroundColor = arr[1];
                                    break;
                                case "$c":
                                case "$tc":
                                case "$textColor":
                                    currentSetting.textColor = arr[1];
                                    break;
                                case "$a":
                                case "$anchor":
                                    currentSetting.anchor = arr[1];
                                    break;
                                case "$t":
                                case "$tint":
                                    __uxTint = arr[1];
                                    break;
                            }
                        }
                        else
                        {
                            currentSetting.text = entry.Replace("<br>", "\n");
                            ls.Add(currentSetting);
                            currentSetting = new Settings();
                        }
                    }
                    else
                    {
                        currentSetting.text = entry.Replace("<br>", "\n");
                        ls.Add(currentSetting);
                        currentSetting = new Settings();
                    }
                }
            }
            if (ls.Count > 0)
                __settings = ls.ToArray();
            
        }

        /// <summary>
        /// Inserts a remarks drop down field filled from string entries
        /// </summary>
        /// <param name="isFoldable">A remarks is foldable by default, to change its set to false this parameter</param>
        /// <param name="displayProperty">Set to false to hide the property</param>
        /// <param name="entries">Each entry string can be a just a rich text string, a string command, or a json formatted data
        ///
        /// IMPORTANT : If isFodable=true the first entry will be used to set the folldown button label
        /// 
        /// Valid tag for rich text are:
        /// <b></b>
        /// <i></i>
        /// <color=#FFFFFF></color>
        /// <size=20></size>
        /// <br> for line feed
        /// 
        /// Commands string are formatted as $command:value
        /// anchor 
        ///     command 
        ///         $a: or $anchor:
        ///     value 
        ///         UpperLeft     or 0
        ///         UpperCenter   or 1
        ///         UpperRight    or 2 
        ///         MiddleLeft    or 3
        ///         MiddleCenter  or 4
        ///         MiddleRight   or 5 
        ///         LowerLeft     or 6
        ///         LowerCenter   or 7
        ///         LowerRight    or 8
        ///         
        /// default text color 
        ///     command 
        ///         $c: or $tc or $textColor:
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        ///         
        /// default background color 
        ///     command 
        ///         $bc: or $backgroundColor:
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        ///         
        /// dropdown tint color 
        ///     command 
        ///         $t: or $tint
        ///     value 
        ///         html color as #rrggbb or #rrggbbaa
        /// 
        /// For Json format entry the string must be formatted as:  
        ///     {
        ///		    \"text\":\"A ritch formatted text plus the </br> html tag to line feed \",
        ///		    \"anchor\":\"MiddleCenter\",
        ///		    \"textColor\":\"#333333\",
        ///		    \"backgroundColor\":\"#333333\"
        ///	    }
        ///	
        ///     All json fields are optionnal
        /// 
        ///     If one of the anchor, textColor or backgroundColor is not define the previous values of the missing fields will be used.
        ///
        /// </param>
        public CommentsAttribute(bool isFoldable, bool displayProperty, params string[] entries)
        {
            __isFoldable = isFoldable;
            __displayProperty = displayProperty;
            if (__isFoldable && entries.Length > 1)
            {
                __title = entries[0];
                string[] ne = new string[entries.Length - 1];
                int j = 0;
                for (int i = 1; i < entries.Length; i++)
                    ne[j++] = entries[i];
                Init(ne);
            }
            else
            {
                Init(entries);
            }
        }

        public CommentsAttribute(bool isFoldable, bool displayProperty, float yOffset, params string[] entries)
        {
            __isFoldable = isFoldable;
            __displayProperty = displayProperty;
            __yOffset = yOffset;
            if (__isFoldable && entries.Length > 1)
            {
                __title = entries[0];
                string[] ne = new string[entries.Length - 1];
                int j = 0;
                for (int i = 1; i < entries.Length; i++)
                    ne[j++] = entries[i];
                Init(ne);
            }
            else
            {
                Init(entries);
            }
        }

        public CommentsAttribute(bool isFoldable, params string[] entries)
        {
            __isFoldable = isFoldable;
            __displayProperty = displayProperty;
            if (__isFoldable && entries.Length > 1)
            {
                __title = entries[0];
                string[] ne = new string[entries.Length - 1];
                int j = 0;
                for (int i = 1; i < entries.Length; i++)
                    ne[j++] = entries[i];
                Init(ne);
            }
            else
            {
                Init(entries);
            }
        }

    }


#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(CommentsAttribute))]
    internal sealed class CommentsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CommentsAttribute attribute = (CommentsAttribute)base.attribute;
            var settings = attribute.settings;
            if (settings == null)
                return;
            position.y += attribute.yOffset;
            position.width += 4;
            position.x -= 3;

            if (attribute.isFoldable)
            {
                var color = GUI.color;
                if (!string.IsNullOrEmpty(attribute.uxTint))
                {
                    if (ColorUtility.TryParseHtmlString(attribute.uxTint, out Color nc))
                    {
                        GUI.color = nc;
                    }
                }
                if (attribute.isOpened)
                {
                    if (EditorGUI.DropdownButton(position, new GUIContent(attribute.title, EditorGUIUtility.FindTexture("console.infoicon.sml")), FocusType.Passive))
                    {
                        attribute.isOpened = false;
                    }
                    position.y += EditorGUIUtility.singleLineHeight;
                }
                else
                {
                    
                    if (EditorGUI.DropdownButton(position, new GUIContent(attribute.title, EditorGUIUtility.FindTexture("console.infoicon.sml")), FocusType.Passive))
                    {
                        attribute.isOpened = true;
                    }
                }
                GUI.color = color;
            }
            if (attribute.isOpened)
            {
                ColorUtility.TryParseHtmlString("#C4C4C4", out Color previousBackgroundColor);
                ColorUtility.TryParseHtmlString("#111111", out Color previousTextColor);
                TextAnchor previousTextAnchor = TextAnchor.UpperLeft;

                //var frameRect = new Rect(position);

                position.x += 1;
                position.width -= 1;

                foreach (var entry in settings)
                {
                    GUIStyle style = new GUIStyle(EditorStyles.label);
                    style.stretchHeight = true;
                    style.richText = true;
                    if (string.IsNullOrEmpty(entry.anchor))
                    {
                        style.alignment = previousTextAnchor;
                    }
                    else
                    {
                        style.alignment = previousTextAnchor = entry.textAnchor;
                    }
                    if (!string.IsNullOrEmpty(entry.backgroundColor))
                    {
                        style.normal.background = OnePixelTextureFromColorHelper.GetOnePixelTextureFromColor(entry.backgroundColor);
                        OnePixelTextureFromColorHelper.TryToParseHtmlColor(entry.backgroundColor, out previousBackgroundColor);
                    }
                    else
                    {
                        style.normal.background = OnePixelTextureFromColorHelper.GetOnePixelTextureFromColor(previousBackgroundColor);
                    }

                    if (OnePixelTextureFromColorHelper.TryToParseHtmlColor(entry.textColor, out Color textColor))
                    {
                        style.normal.textColor = textColor;
                        previousTextColor = textColor;
                    }
                    else
                    {
                        style.normal.textColor = previousTextColor;
                    }

                    string text = string.IsNullOrEmpty(entry.text) ? "" : entry.text;
                    var content = new GUIContent(text);
                    var size = style.CalcSize(content);
                    style.fixedHeight = size.y;
                    position.height = size.y;
                    EditorGUI.LabelField(position, content, style);
                    position.y += size.y;

                    //frameRect.height += size.y;
                }
                position.y -= EditorGUIUtility.singleLineHeight;
                //frameRect.height-= EditorGUIUtility.singleLineHeight;
                //frameRect.height += 1f;

                //DrawFrame(frameRect, new Color(0,0,0,0.15f));
            }
            position.y += 3;

            if (attribute.displayProperty)
            {
                position.y += EditorGUIUtility.singleLineHeight;
                var content = new GUIContent(property.displayName);
                position.height = EditorGUI.GetPropertyHeight(property, content, true);
                EditorGUI.PropertyField(position, property, content, true);

            }
            else
                position.height = 0;

            EditorGUILayout.Space(position.y + position.height);
        }

        void DrawFrame(Rect _rect,Color _color,float _lineWidth=1f)
        {
            var w = _rect.width;
            var h = _rect.height;
            var x = _rect.x;
            var y = _rect.y;

            _rect.width = _lineWidth;
            EditorGUI.DrawRect(_rect,_color);
            _rect.x += w;
            EditorGUI.DrawRect(_rect, _color);
            _rect.x = x;
            _rect.width = w + _lineWidth;
            _rect.height = _lineWidth;
            _rect.y -= _lineWidth/2f;
            EditorGUI.DrawRect(_rect, _color);
            _rect.y += h - _lineWidth/2;
            EditorGUI.DrawRect(_rect, _color);
        }
    }
#endif
}
