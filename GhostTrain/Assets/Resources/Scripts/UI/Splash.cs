using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(PlayableDirector))]
public class Splash : MonoBehaviour {
    //***
    //*** Editor-exposed variables.
    //***
    [SerializeField] private PlayableAsset SplashTimeline = null;
    [SerializeField] private int NextScene = 1;


    //***
    //*** Class-internal variables.
    //***
    private PlayableDirector director = null;


    // Use this for initialization
    void Awake() {
        // Ensure that everything's set up correctly.
        string msg = "";

        if (!this.CheckSetup(out msg)) {
            Debug.LogError(this.GetType().ToString() + ": Not set up correctly - " + msg);
            this.enabled = false;
            return;
        }


        // Get hold of our director and start playing the splash animation immediately.
        this.director = this.GetComponent<PlayableDirector>();
        this.director.Play(this.SplashTimeline);


        // Issue callbacks to load the next scene.
        this.Invoke("LoadNextScene", (float)this.SplashTimeline.duration);
    }


    //***
    //*** Callback methods.
    //***
    private void LoadNextScene() => SceneManager.LoadScene(this.NextScene);


    //***
    //*** Internal helper methods.
    //***
    private bool CheckSetup(out string msg) {
        // Check we have a timeline.
        if (this.SplashTimeline == null) {
            msg = "No timeline provided for splash animation.";
            return false;
        }


        // Exit cleanly.
        msg = "";
        return true;
    }
}
