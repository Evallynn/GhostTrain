using UnityEngine;


[CreateAssetMenu(fileName = "RailNetworkGenerator", menuName = "XMPT/Ghost Train/Generators/Rail Network", order = 1)]
public class RailNetworkGenerator : ScriptableObject {
    //***
    //*** Editor-exposed variables.
    //***
    [Header("Stations")]
    [SerializeField, Min(5)] private int _minStations = 5;
    [SerializeField, Min(5)] private int _maxStations = 10;
    [SerializeField, Min(1)] private int _minGhostStations = 1;
    [SerializeField, Min(1)] private int _maxGhostStations = 1;

    [Header("Connections")]
    [SerializeField, Min(2)] private int _minConnectionsToHome = 2;
    [SerializeField, Min(2)] private int _maxConnectionsToHome = 5;
    [SerializeField, Min(1)] private int _maxConnectionsPerStation = 4;
    [SerializeField, Min(2)] private int _maxConnectionDepth = 5;
    [SerializeField, Min(1)] private int _minLinkDistance = 15;
    [SerializeField, Min(1)] private int _maxLinkDistance = 60;


    //***
    //*** External utility methods.
    //***
    [ContextMenu("Generate")]
    public RailNetworkModel Generate() {
        RailNetworkModel network = new();

        StationModel home = GenerateStation();
        network.AddStation(home);
        Debug.Log($"Home: {home.Name} ({home.Id})");

       this.GenerateStations(home, ref network, 1, this._minConnectionsToHome, this._maxConnectionsToHome);
        return network;
    }

    
    //***
    //*** Internal utility methods.
    //***

    // TODO Variance in the number of stations is too high - look for ways to make it more uniform.

    private void GenerateStations(StationModel from, ref RailNetworkModel network, int depth, int min, int max) {
        // Prevent generation past a certain depth.
        if (depth > this._maxConnectionDepth) return;

        // Decide how many connections to the station we need.
        int connections = Random.Range(min, max + 1);

        // Generate that many stations and connect them up.
        for (int i = 0; i < connections; i++) {
            StationModel to = GenerateStation();

            StationLinkModel link = this.GenerateLink(from, to);
            from.AddConnectedStation(link);

            Debug.Log($"{from.Name} --> {to.Name}: {link.Distance * 15} minutes.");
            network.AddStation(to);

            this.GenerateStations(to, ref network, depth + 1, 0, this._maxConnectionsPerStation);
        }
    }


    private static StationModel GenerateStation() => new (StationGenerator.Inst.Generate());

    private StationLinkModel GenerateLink(StationModel from, StationModel to) {
        int distance = Random.Range(this._minLinkDistance, this._maxLinkDistance + 1);
        return new(from, to, distance);
    }
}
