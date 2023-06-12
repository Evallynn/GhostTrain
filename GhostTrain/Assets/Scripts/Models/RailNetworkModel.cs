using System.Collections.Generic;


public class RailNetworkModel {
    //***
    //*** Class properties.
    //***
    public StationModel Home { get; private set; } = null;


    //***
    //*** Class-internal variables.
    //***
    private List<StationModel> _stations = new();


    //***
    //*** Constructors.
    //***
    public RailNetworkModel() { }


    //***
    //*** External utility methods.
    //***
    public void AddHomeStation(StationModel station) {
        this.AddStation(station);
        this.Home = station;
    }

    public void AddStation(StationModel station) {
        if (this._stations.Contains(station)) this._stations.Add(station);
    }
}
