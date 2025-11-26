using UnityEngine;
using UnityEditor;

namespace qb
{
    public static class OpenApplicationPersistentDataFolder
    {
        [MenuItem("Tools/Open application persistent data folder")]
        static void OpenDataFolder()
        { 
            string path = Application.persistentDataPath.Replace("/", "\\");
            if (System.IO.Directory.Exists(path))
            {
                var psi = new System.Diagnostics.ProcessStartInfo();
                psi.FileName = @"c:\windows\explorer.exe";
                psi.Arguments = path;
                System.Diagnostics.Process.Start(psi);
            }
            else
            {
                EditorUtility.DisplayDialog("Information", $"{path}\n Doesn't exist yet!", "CLOSE");
            }
        }
    }
}
