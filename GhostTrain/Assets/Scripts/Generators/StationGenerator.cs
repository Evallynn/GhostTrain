using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xmpt.Core.Collections;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "StationGenerator", menuName = "XMPT/Ghost Train/Generators/Station", order = 1)]
public sealed class StationGenerator : ScriptableSingleton<StationGenerator> {
    //***
    //*** Editor-exposed variables.
    //***
    [Header("Station Name Components")]
    [SerializeField] private List<string> _prefixes = new List<string>();
    [SerializeField] private List<string> _openers = new List<string>();
    [SerializeField] private List<string> _padders = new List<string>();
    [SerializeField] private List<string> _closers = new List<string>();
    [SerializeField] private List<string> _postfixes = new List<string>();

    [Header("Probabilities")]
    [SerializeField, Range(0.0f, 1.0f)] private float _prefixChance = 0.25f;
    [SerializeField, Range(0.0f, 1.0f)] private float _postfixChance = 0.25f;
    [SerializeField, Range(0.0f, 1.0f)] private float _padChance = 0.5f;
    [SerializeField, Range(1, 5)] private int _maxPads = 5;


    //***
    //*** External utility methods.
    //***
    public string Generate() {
        this.ValidateSetup();

        string station = this.GenerateMainName();
        station = this.ApplyPrefix(station);
        station = this.ApplyPostfix(station);

        return station;
    }


    //***
    //*** Class-internal utility methods.
    //***
    private void ValidateSetup() {
        if (this._openers.Count <= 0) throw new ArgumentException("At least one opener must be provided.");
        if (this._closers.Count <= 0) throw new ArgumentException("At least one closer must be provided.");
    }

    private string GenerateMainName() {
        string opener = this.GenerateOpener();
        string closer = this.GenerateCloser(opener);
        string padding = this.GeneratePadding(opener, closer);

        string name = opener;
        name += padding.StartsWith(name[^1]) ? padding[1..] : padding;
        name += closer.StartsWith(name[^1]) ? closer[1..] : closer;
        return name;
    }

    private string GenerateOpener() => GenerateFrom(this._openers);

    private string GenerateCloser(string opener = "") {
        opener = opener.ToLower();

        for (int i = 0; i < 10; i++) {
            string closer = GenerateFrom(this._closers).ToLower();
            if (!opener.Contains(closer)) return closer;
        }
        return "";
    }

    private string GenerateSinglePad() => GenerateFrom(this._padders).ToLower();

    private string ApplyPrefix(string station) {
        if (this._prefixes.Count <= 0) return station;

        if (Random.Range(0.0f, 1.0f) < this._prefixChance) {
            string prefix = GenerateFrom(this._prefixes);

            if (!station.Contains(prefix, StringComparison.InvariantCultureIgnoreCase))
                station = $"{prefix} {station}";
        }

        return station;
    }

    private string ApplyPostfix(string station) {
        if (this._postfixes.Count <= 0) return station;

        if (Random.Range(0.0f, 1.0f) < this._postfixChance) {
            string postfix = GenerateFrom(this._postfixes);

            if (!station.Contains(postfix, StringComparison.InvariantCultureIgnoreCase))
                station += $" {postfix}";
        }

        return station;
    }

    private static string GenerateFrom(List<string> target) {
        int index = Random.Range(0, target.Count);
        return target[index];
    }

    private string GeneratePadding(string opener, string closer) {
        if (this._padders.Count <= 0) return "";

        opener = opener.ToLower();
        string padding = "";

        for (int i = 0; i < this._maxPads; i++) {
            if (Random.Range(0.0f, 1.0f) < this._padChance) {
                string newPad = GenerateSinglePad();

                if (!opener.Contains(newPad) && !closer.Contains(newPad) && !padding.Contains(newPad))
                    padding += newPad;
            }
            else break;
        }

        return padding;
    }
}
