using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(StationGenerator))]
public class StationGeneratorEditor : Editor {
    //***
    //*** Class-internal variables.
    //***
    private List<string> _lastGenerated = new(0);
    private int _totalSamples = 10;


    //***
    //*** Class constants.
    //***
    private const int MAX_SAMPLES = 100;


    //***
    //*** Overrides from Editor.
    //***
    public override void OnInspectorGUI() {
        StationGenerator target = (StationGenerator)this.target;

        this.DrawDefaultInspector();
        this.DrawGenerateSection(target);
        base.serializedObject.ApplyModifiedProperties();
    }


    //***
    //*** Class-internal utility methods.
    //***
    private void DrawGenerateSection(StationGenerator generator) {
        GUILayout.Space(15);
        GUILayout.BeginVertical(EditorStyles.helpBox);

        this._totalSamples = (int)EditorGUILayout.Slider("Total Samples", this._totalSamples, 1, MAX_SAMPLES);
        if (GUILayout.Button("Generate Samples")) this.GenerateSamples(generator);

        this._lastGenerated.ForEach(i => GUILayout.Label(i, EditorStyles.boldLabel));

        GUILayout.EndVertical();
    }

    private void GenerateSamples(StationGenerator generator) {
        this._lastGenerated.Clear();

        for (int i = 0; i < this._totalSamples; i++) {
            this._lastGenerated.Add(generator.Generate());
        }
    }
}
