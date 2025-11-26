using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Helpers.Editor
{
    public class ComponentClipBoard : MonoBehaviour
    {
        public static List<Component> clipboard = new List<Component>();

        [MenuItem("CONTEXT/Component/Clipboard->Copy to")]
        public static void AddToClipBoard(MenuCommand command)
        {
            Component component = (Component)command.context;
            if (!clipboard.Contains(component))
                clipboard.Add(component);
        }
        [MenuItem("CONTEXT/Component/Clipboard->Copy to", true)]
        public static bool ValidateAddToClipBoard(MenuCommand command)
        {
            Component component = (Component)command.context;
            return !clipboard.Contains(component)&&component.GetType()!=typeof(Transform);
        }



        [MenuItem("CONTEXT/Component/Clipboard->Remove from")]
        public static void RemoveFromClipBoard(MenuCommand command)
        {
            Component component = (Component)command.context;
            if (clipboard.Contains(component))
                clipboard.Remove(component);
        }
        [MenuItem("CONTEXT/Component/Clipboard->Remove from", true)]
        public static bool ValidateRemoveFromClipBoard(MenuCommand command)
        {
            Component component = (Component)command.context;
            return clipboard.Contains(component);
        }

        [MenuItem("CONTEXT/Component/Clipboard->Clear")]
        public static void ClearClipBoard(MenuCommand command)
        {
            clipboard.Clear();
        }
        [MenuItem("CONTEXT/Component/Clipboard->Clear", true)]
        public static bool ValidateClearClipBoard(MenuCommand command)
        {
            return clipboard.Count > 0;
        }

        [MenuItem("CONTEXT/Component/Clipboard->Paste")]
        public static void PasteClipBoard(MenuCommand command)
        {
            Component compo = (Component)command.context;
            GameObject target = compo.gameObject;
            foreach (var component in clipboard)
            {
                UnityEditorInternal.ComponentUtility.CopyComponent(component);
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(target);
            }
        }
        [MenuItem("CONTEXT/Component/Clipboard->Paste",true)]
        public static bool ValidatePasteClipBoard(MenuCommand command)
        {
            return clipboard.Count > 0;
        }

        [MenuItem("CONTEXT/Component/Clipboard->Paste and clear")]
        public static void PasteClipBoardAndClear(MenuCommand command)
        {
            Component compo = (Component)command.context;
            GameObject target = compo.gameObject;
            foreach (var component in clipboard)
            {
                UnityEditorInternal.ComponentUtility.CopyComponent(component);
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(target);
            }
            clipboard.Clear();
        }
        [MenuItem("CONTEXT/Component/Clipboard->Paste and clear", true)]
        public static bool ValidatePasteClipBoardAndClear(MenuCommand command)
        {
            return clipboard.Count > 0;
        }
    }
}
