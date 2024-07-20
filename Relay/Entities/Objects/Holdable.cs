using Godot;
using System;

public partial class Holdable : StaticBody2D {

  public override void _Ready() {
    base._Ready();
  }

  public void OnPickup() {
    // Disable collision
    // SetCollisionLayerValue(2, false);
  }

  public void OnDrop() {
    // Enable collision
    // SetCollisionLayerValue(2, true);
  }
}
