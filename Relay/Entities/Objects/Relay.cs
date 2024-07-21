using Godot;
using System;

public partial class Relay : Holdable {

  private Node2D _light;
  private Area2D _poweredArea;
  private bool _isPowered;


  public override void _Ready() {
    base._Ready();
    _light = GetNode<Node2D>("Light");
    _poweredArea = GetNode<Area2D>("PoweredArea");
    _poweredArea.AreaEntered += OnAreaEnter;
    _poweredArea.AreaExited += OnAreaExit;
  }

  public bool IsPowered {
    get => _isPowered;
    set => SetIsPowered(value);
  }
  private void SetIsPowered(bool value) {
    _isPowered = value;
    _light.Visible = _isPowered;
  }

  void OnAreaEnter(Area2D node) {
    if (node == this) return;
    if (node is Relay relay && IsPowered) {
      relay.IsPowered = true;
      return;
    }
  }

  void OnAreaExit(Area2D node) {
    if (node == this) return;
    if (node is Relay relay) {
      relay.IsPowered = false;
      return;
    }
  }
}
