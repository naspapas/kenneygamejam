using Godot;
using System;

public partial class Generator : Node {

  private Area2D _poweredArea;

  public override void _Ready() {
    base._Ready();
    _poweredArea = GetNode<Area2D>("PoweredArea");
    _poweredArea.BodyEntered += OnBodyEnter;
    _poweredArea.BodyExited += OnBodyExit;
    _poweredArea.AreaEntered += OnAreaEnter;
    _poweredArea.AreaExited += OnAreaExit;
  }

  void OnBodyEnter(Node2D node) {
    if (node is Player player) {
      player.IsPowered = true;
      return;
    }
  }

  void OnBodyExit(Node2D node) {
    if (node is Player player) {
      player.IsPowered = false;
      return;
    }
  }

  void OnAreaEnter(Area2D node) {
    if (node is Relay relay) {
      relay.IsPowered = true;
      return;
    }
  }

  void OnAreaExit(Area2D node) {
    if (node is Relay relay) {
      relay.IsPowered = false;
      return;
    }
  }
}
