using UnityEngine;
using Xmpt.Core.Inputs;


public class QuitGame : MonoBehaviour {
    //***
    //*** Initialisation.
    //***
    public void Awake() => InputManager.Inst.RegisterKeyUp(this, KeyCode.Escape, Quit);


    //***
    //*** Callbacks.
    //***
    private bool Quit(KeyData data) {
        Application.Quit();
        return false;
    }
}
