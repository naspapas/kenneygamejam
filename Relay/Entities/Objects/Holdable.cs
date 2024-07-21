using Godot;
using System;

public partial class Holdable : Area2D {

  public void OnPickup() {
    // Disable collision
    SetCollisionLayerValue(4, false);
  }

  public void OnDrop() {
    // Enable collision
    SetCollisionLayerValue(4, true);
  }
}
