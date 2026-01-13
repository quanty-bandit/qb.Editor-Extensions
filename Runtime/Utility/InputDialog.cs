
namespace qb.Utility
{
    public static class InputDialog
    {
#if UNITY_EDITOR
        public static string Show(string title, string description, string inputText, string okButton = "OK", string cancelButton = "Cancel")
        {
            return EditorInputDialog.Show(title, description, inputText, okButton, cancelButton);
        }
#endif
    }

}
