using TMPro;
using UnityEngine;


public class StationButton : MonoBehaviour {
    //***
    //*** Editor-exposed variables.
    //***
    [SerializeField] private StationGenerator _generator;
    [SerializeField] private TMP_Text _text;


    //***
    //*** External methods.
    //***
    public void Generate() => this._text.text = this._generator.Generate();
}
