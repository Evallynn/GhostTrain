using TMPro;
using UnityEngine;


public class BuildInfo : MonoBehaviour {
    public TMP_Text buildText;

    public string UNITY_GAME_BUILD_VERSION = "1.0";

    // Start is called before the first frame update
    void Start() {
        this.buildText.text = "v" + UNITY_GAME_BUILD_VERSION;
    }
}
