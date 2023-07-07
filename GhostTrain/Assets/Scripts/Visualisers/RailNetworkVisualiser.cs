using System;
using System.Collections.Generic;
using UnityEngine;


public class RailNetworkVisualiser : MonoBehaviour {
    //***
    //*** Class properties.
    //***
    [field: SerializeField, Min(0.0f)] public float ConnectionVariance { get; set; } = 0.0f;
    [field: SerializeField, Range(0.0f, 1.0f)] public float RotationalVariance { get; set; } = 0.0f;
    [field: SerializeField, Min(0.001f)] public float NetworkScale { get; set; } = 1.0f;


    //***
    //*** Class-internal variables.
    //***
    [SerializeField] private GameObject _stationPrefab = null;
    [SerializeField] private GameObject _linePrefab = null;
    [SerializeField] private RailNetworkGenerator _generator = null;

    private RailNetworkPlotter _plotter;
    private RailNetworkModel _network;

    private Dictionary<StationModel, IMapStation> _stations = new();
    private List<IMapLine> _lines = new();


    //***
    //*** Initialisation.
    //***
    private void Awake() {
        CheckEditorInputs();

        this._plotter = new(this._generator.MinLinkDistance, this.ConnectionVariance) {
            NetworkScale = this.NetworkScale,
            RotationalVariancePercentage = this.RotationalVariance
        };

        this.GenerateMap();
    }


    //***
    //*** Class-internal utility methods.
    //***
    private void CheckEditorInputs() {
        if (this._stationPrefab?.GetComponent<IMapStation>() == null)
            throw new ArgumentException($"The object provided for Station Prefab did not have a component that implements IMapStation.");

        if (this._linePrefab?.GetComponent<IMapLine>() == null)
            throw new ArgumentException($"The object provided for Line Prefab did not have a component that implements IMapLine.");

        if (this._generator == null)
            throw new ArgumentException($"No generator was set.");
    }

    [ContextMenu("Regenerate")]
    private void GenerateMap() {
        if (!Application.isPlaying) return;
        this.Reset();

        this._network = this._generator.Generate();
        this._plotter.ApplyNetwork(this._network, true);

        this.GenerateStation(this._plotter.Home);
        this._stations[this._plotter.Home].Colour = Color.red;
    }

    private void GenerateStation(StationModel model) {
        Vector2 pos = this._plotter.GetPosition(model);
        IMapStation station = this.InstantiateStation(pos, model);
        this._stations.Add(model, station);

        foreach (StationLinkModel link in model.Links) {
            StationModel child = link.GetOpposite(model);

            if (!this._stations.ContainsKey(child)) {
                this.GenerateStation(child);

                IMapLine line = this.InstantiateLine(pos, this._plotter.GetPosition(child));
                this._lines.Add(line);
            }
        }
    }

    private IMapStation InstantiateStation(Vector3 position, StationModel data) {
        IMapStation station = Instantiate(this._stationPrefab)?.GetComponent<IMapStation>();
        Transform transform = station.GameObject.GetComponent<Transform>();

        transform.SetParent(this.transform);
        transform.localScale = Vector3.one;
        transform.position = position;

        station.StationName = data.Name;
        return station;
    }

    private IMapLine InstantiateLine(Vector2 start, Vector2 end) {
        IMapLine line = Instantiate(this._linePrefab)?.GetComponent<IMapLine>();
        Transform transform = line.GameObject.GetComponent<Transform>();

        transform.SetParent(this.transform, true);
        transform.position = start;
        transform.localScale = Vector3.one;

        line.StartPosition = start;
        line.EndPosition = end;
        return line;
    }

    private void Reset() {
        foreach (IMapStation station in this._stations.Values)
            Destroy(station.GameObject);

        foreach (IMapLine line in this._lines)
            Destroy(line.GameObject);

        this._stations.Clear();
        this._lines.Clear();
    }
}
