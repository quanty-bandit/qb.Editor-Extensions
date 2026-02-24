using UnityEngine;
using UnityEditor;
namespace qb
{
    public static class ClearPlayerPrefs
    {
        [MenuItem("Tools/Delete all PlayerPrefs entries")]
        static void ClearAll()
        {
            if(EditorUtility.DisplayDialog("Warning","All players prefs entries will be deleted","Ok","Cancel"))
            {
                PlayerPrefs.DeleteAll();
                Debug.Log("<color=#FFFF00>All entries were deleted!");
            }
            else
            {
                Debug.Log("No entry was deleted");
            }
        }
    }
}
