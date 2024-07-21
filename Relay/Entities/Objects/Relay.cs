using Godot;
using System;

public partial class Relay : Holdable {

  private bool _isPowered;
  private Light _light;
  private Area2D _poweredArea;
  private AnimatedSprite2D _sprite;

  public override void _Ready() {
    base._Ready();
    _light = GetNode<Light>("Light");
    _poweredArea = GetNode<Area2D>("PoweredArea");
    _poweredArea.AreaEntered += OnAreaEnter;
    _poweredArea.AreaExited += OnAreaExit;
    _sprite = GetNode<AnimatedSprite2D>("Sprite");
    _sprite.Play("Unpowered");
  }

  public bool IsPowered {
    get => _isPowered;
    set => SetIsPowered(value);
  }
  private void SetIsPowered(bool value) {
    _isPowered = value;
    _light.Visible = _isPowered;
    _sprite.Play(_isPowered ? "Powered" : "Unpowered");
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
