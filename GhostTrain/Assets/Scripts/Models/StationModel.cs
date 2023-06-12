using System;
using System.Collections.Generic;


public class StationModel : IEquatable<StationModel> {
    //***
    //*** Class properties.
    //***
    public string Name { get; set; }
    public Guid Id { get; }
    public IEnumerable<StationLinkModel> Links => this._connectedStations;
    public int TotalLinks => this._connectedStations.Count;


    //***
    //*** Class-internal variables.
    //***
    private List<StationLinkModel> _connectedStations = new();


    //***
    //*** Class constructors.
    //***
    public StationModel(string name) {
        this.Name = name;
        this.Id = Guid.NewGuid();
    }


    //***
    //*** IEquitable overrides.
    //***
    public bool Equals(StationModel other) => this.Id == other.Id;
    public static bool operator ==(StationModel first, StationModel second) => first.Id == second.Id;
    public static bool operator !=(StationModel first, StationModel second) => first.Id != second.Id;


    //***
    //*** External utility methods.
    //***
    public void AddConnectedStation(StationLinkModel connection) {
        if (!this._connectedStations.Contains(connection)) {
            this._connectedStations.Add(connection);
            connection.GetOpposite(this).AddConnectedStation(connection);
        }
    }
}
