using System;


public class StationLinkModel {
    //***
    //*** Class properties.
    //***
    public StationModel FirstStation { get; set; }
    public StationModel SecondStation { get; set; }
    public int Distance { get; set; }


    //***
    //*** Constructors.
    //***
    public StationLinkModel(StationModel firstStation, StationModel secondStation, int distance) {
        this.FirstStation = firstStation;
        this.SecondStation = secondStation;
        this.Distance = distance;
    }


    //***
    //*** External utility methods.
    //***
    public bool HasStation(StationModel station) => this.FirstStation == station || this.SecondStation == station;
    
    public StationModel GetOpposite(StationModel current) {
        if (this.FirstStation == current) return this.SecondStation;
        else if (this.SecondStation == current) return this.FirstStation;
        else throw new ArgumentException($"Attempted to get opposite station to {current.Name} ({current.Id}) in a link which did not contain it.");
    }
}
