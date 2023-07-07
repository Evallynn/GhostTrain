using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class RailNetworkPlotter {
    //***
    //*** Class properties.
    //***
    public IEnumerable<KeyValuePair<StationModel, Vector2>> Positions => this._positions;
    public int TotalPositions => this._positions.Count;
    public float MinConnectionLength { get; set; }
    public float MaxConnectionLength { get; set; }
    public StationModel Home => this._network?.Home;


    //***
    //*** Class-internal variables.
    //***
    private RailNetworkModel _network = null;
    private Dictionary<StationModel, Vector2> _positions = new();


    //***
    //*** Constructors.
    //***
    public RailNetworkPlotter(float minConnectionLength, float maxConnectionLength) {
        this.MinConnectionLength = minConnectionLength;
        this.MaxConnectionLength = maxConnectionLength;
    }


    //***
    //*** External utility methods.
    //***
    public void ApplyNetwork(RailNetworkModel network, bool generateLayout = true) {
        this._network = network;
        this._positions.Clear();
        if (generateLayout) this.GenerateLayout();
    }

    public void GenerateLayout() {
        if (this._network?.Home is null)
            throw new NullReferenceException($"Unable to generate layout as network was null or network had no home station.");

        this._positions.Clear();
        this._positions.Add(this._network.Home, Vector2.zero);
        this.GenerateChildPositions(this._network.Home, Vector2.zero);
    }

    public Vector2 GetPosition(StationModel station) => this._positions[station];


    //***
    //*** Class-internal utility methods.
    //***
    private void GenerateChildPositions(StationModel parent, Vector2 parentPos) {
        if (parent.TotalLinks <= 0) return;
        float segmentSize = 360.0f / (float)parent.TotalLinks;
        int i = 0;

        foreach (StationLinkModel link in parent.Links) {
            StationModel child = link.GetOpposite(parent);
            float angle = i * segmentSize;
            Vector2 newPos = this.CalculatePosition(parentPos, angle, 2.0f);

            this._positions.Add(child, newPos);
            this.GenerateChildPositions(child, parent, newPos, parentPos, 90.0f);
            i++;
        }
    }


    private void GenerateChildPositions(StationModel parent, StationModel grandparent, Vector2 parentPos, Vector2 grandparentPos, float radius) {
        if (parent.TotalLinks <= 1) return;
        float segmentSize = radius / (float)Mathf.Max(parent.TotalLinks - 2, 1);

        float startAngle = parent.TotalLinks == 2 ?
            -Vector2.SignedAngle(parentPos - grandparentPos, Vector2.right) :
            -(Vector2.SignedAngle(parentPos - grandparentPos, Vector2.right) + (radius * 0.5f));

        int i = 0;

        foreach (StationLinkModel link in parent.Links) {
            StationModel child = link.GetOpposite(parent);
            if (child == grandparent) continue;

            float angle = startAngle + (i * segmentSize);
            Vector2 newPos = this.CalculatePosition(parentPos, angle, 1.0f);

            this._positions.Add(child, newPos);
            this.GenerateChildPositions(child, parent, newPos, parentPos, radius / Mathf.Max(1.0f, parent.TotalLinks - 1));
            i++;
        }
    }


    private Vector2 CalculatePosition(Vector2 from, float angle, float scale) {
        float distance = Random.Range(this.MinConnectionLength, this.MaxConnectionLength) * scale;

        var x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        var y = distance * Mathf.Sin(angle * Mathf.Deg2Rad);

        return new(from.x + x, from.y + y);
    }
}
