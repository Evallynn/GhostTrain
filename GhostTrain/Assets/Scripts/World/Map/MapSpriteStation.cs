using TMPro;
using UnityEngine;


public class MapSpriteStation : MonoBehaviour, IMapStation {
    //***
    //*** Class properties.
    //***
    public string StationName {
        get => this._label.text ?? "";
        set => this._label?.SetText(value);
    }

    public GameObject GameObject => this.gameObject;

    public Color Colour {
        get => this._image.color;
        set => this._image.color = value;
    }


    //***
    //*** Class-internal variables.
    //***
    private TMP_Text _label;
    private SpriteRenderer _image;


    //***
    //*** Initialisation.
    //***
    private void Awake() {
        this._label = this.GetComponentInChildren<TMP_Text>();
        this._image = this.GetComponentInChildren<SpriteRenderer>();
    }
}
