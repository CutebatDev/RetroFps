using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{

    public float secondsPerCharacter = .2f;
    
    public string[] introDialogue =
    {
        " ",
        
        "You find yourself in a cold, stinking, sewage system.",
        
        "You do not remember the day before, nor who you are.",
        
        "The slow and wet sounds of water dropping\n" +
        "is the only thing keeping you company.",
        
        "You are tired, you will not last long \n" +
        "before succumbing to the somnia once more",
        
        "Find the keys, open the doors,\n" +
        "Escape"
    };
    public string[] exitEndingDialogue;
    public string[] slumberEndingDialogue;
    
    public string[] currentDialogue;

    public TextMeshProUGUI text;

    public string mainSceneName = "SampleScene";
    
    void Start()
    {
        if (SceneData.CutsceneType == null)
            SceneData.CutsceneType = CutsceneType.Intro;
        
        switch (SceneData.CutsceneType)
        {
            case  CutsceneType.SlumberEnding:
                currentDialogue = slumberEndingDialogue;
                break;
            case CutsceneType.ExitEnding:
                currentDialogue = exitEndingDialogue;
                break;
            case CutsceneType.Intro:
                currentDialogue = introDialogue;
                break;
        }
        
        StartCoroutine(PlayCutscene());
    }


    IEnumerator PlayCutscene()
    {
        int index = 0;
        while (index < currentDialogue.Length)
        {
            string line = currentDialogue[index];
            text.text = line;
            index++;
            float timeToWait = line.Length* secondsPerCharacter;
            Debug.Log(timeToWait);
            yield return new WaitForSeconds(timeToWait);
        }

        if (SceneData.CutsceneType == CutsceneType.Intro) {
            SceneManager.LoadScene(mainSceneName);
            yield break;
        }
        
        // TODO-hl
        // GAME ENDS HERE
        Application.Quit();
    }
    
}
