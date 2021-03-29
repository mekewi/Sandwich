using UnityEditor;

public class CreateNewScriptFromCustomTemplate
{
    private const string eventTemplatePath = "Assets/Editor/GameEvent.cs.txt";

    [MenuItem(itemName: "Assets/Create/Class/Create Event Class", isValidateFunction: false, priority: 51)]
    public static void CreateEventTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(eventTemplatePath, "GameEvent.cs");
    }
}
