using UnityEngine;
using Xmpt.Core.Extensions.Unity;


public interface IMapLine {
    public Vector2 StartPosition { get; set; }
    public Vector2 EndPosition { get; set; }
    public float Width { get; set; }
    public GameObject GameObject { get; }
}


[RequireComponent(typeof(LineRenderer))]
public class MapLine : MonoBehaviour, IMapLine {
    //***
    //*** Class properties.
    //***
    public Vector2 StartPosition {
        get => this.GetPointPosition(0);
        set => this.SetPointPosition(0, value);
    }

    public Vector2 EndPosition {
        get => this.GetPointPosition(1);
        set => this.SetPointPosition(1, value);
    }

    public float Width {
        get => this._line.startWidth;
        set {
            this._line.startWidth = value;
            this._line.endWidth = value;
        }
    }

    public GameObject GameObject => this.gameObject;


    //***
    //*** Class-internal variables.
    //***
    private LineRenderer _line = null;


    //***
    //*** Initialisation.
    //***
    private void Awake() {
        this._line = this.GetComponent<LineRenderer>();
        InitLineRenderer(this._line);
    }


    //***
    //*** Class-internal utility methods.
    //***
    private static void InitLineRenderer(LineRenderer line) =>
        line.positionCount = 2;


    private Vector2 GetPointPosition(int index) => this._line.GetPosition(index).xy();

    private void SetPointPosition(int index, Vector2 value) {
        Vector3 newPos = new(
            value.x,
            value.y,
            0.0f
        );

        this._line.SetPosition(index, newPos);
    }
}
