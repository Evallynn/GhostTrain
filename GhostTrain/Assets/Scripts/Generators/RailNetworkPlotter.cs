using System;
using System.Collections.Generic;
using UnityEngine;


public class RailNetworkPlotter {
    //***
    //*** Class-internal variables.
    //***
    private RailNetworkModel _network = null;
    private Dictionary<StationModel, Vector2> _positions = new();


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
        
    }
}
