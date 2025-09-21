using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public string cutsceneSceneName;
    
    public void ChangeScene(CutsceneType cutsceneType)
    {
        SceneData.CutsceneType = cutsceneType;
        SceneManager.LoadScene(cutsceneSceneName);
    }
}
