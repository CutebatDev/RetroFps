using System;
using DefaultNamespace;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public SceneHandler sceneHandler;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            sceneHandler.ChangeScene(CutsceneType.ExitEnding);
    }
}
