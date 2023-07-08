using UnityEngine;

public interface IMapStation {
    public string StationName { get; set; }
    public GameObject GameObject { get; }
    public Color Colour { get; set; }
}
