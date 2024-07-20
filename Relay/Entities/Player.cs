using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class Player : CharacterBody2D {
  [Export]
  public int Speed { get; set; } = 400;

  [Export]
  public Node GroundObjects;

  [Export]
  public float EnergyDecay = 0.01f;
  [Export]
  public float EnergyRecharge = 0.05f;

  private float _maxEnergy = 1f;

  private float _lightEnergy = 1f;

  // [Export]
  // public int DropItemOffsetDistance = 12;

  private AnimatedSprite2D _sprite;
  private Area2D _pickupArea;
  private Holdable _heldObject;
  private Node2D _light;

  public override void _Ready() {
    base._Ready();
    _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    _sprite.Play("walk");
    _pickupArea = GetNode<Area2D>("InteractArea");
    _lightEnergy = 1;
    _light = GetNode<Node2D>("Light");
  }

  public override void _Input(InputEvent @event) {
    if (@event.IsActionPressed("interact"))
      if (_heldObject == null) PickupNearest();
      else DropHeldObject();
  }

  private void PickupNearest() {
    var detectedObjects = _pickupArea.GetOverlappingBodies();
    Debug.Print($"{detectedObjects.Count} pickup objects detected");
    foreach (var detectedObject in detectedObjects) {
      if (detectedObject is Holdable holdable) {
        Pickup(holdable);
        return;
      }
    }
  }

  private void Pickup(Holdable holdable) {
    Debug.Print($"Picked up {holdable.Name}");
    _heldObject = holdable;
    holdable.OnPickup();
    holdable.Reparent(this, false);
    holdable.Position = Vector2.Zero;
  }

  private void DropHeldObject() {
    Debug.Print($"Picked up {_heldObject.Name}");
    _heldObject.Reparent(GroundObjects);
    // var dropOffset = (GetGlobalMousePosition() - GlobalPosition).Normalized() * DropItemOffsetDistance;
    // _heldObject.GlobalPosition += dropOffset;
    _heldObject.OnDrop();
    _heldObject = null;
  }

  public override void _PhysicsProcess(double delta) {
    GetInput();
    MoveAndSlide();
    UpdateAnimationState();
    UpdateLightEnergy();
  }

  private void UpdateLightEnergy() {
    var detectedObjects = _pickupArea.GetOverlappingBodies();
    foreach (var detectedObject in detectedObjects) {
      if (detectedObject.Name == "Generator") {
        _lightEnergy = Math.Clamp(_lightEnergy + EnergyRecharge, 0f, 1f);
        return;
      }
    }
    _lightEnergy = Math.Clamp(_lightEnergy - EnergyDecay, 0f, 1f);
  }

  private void GetInput() {
    Vector2 inputDirection = Input.GetVector("move left", "move right", "move up", "move down");
    Velocity = inputDirection * Speed;
  }

  private void UpdateAnimationState() {
    var isPlaying = _sprite.IsPlaying();
    var isStationary = GetRealVelocity() == Vector2.Zero;

    if (isPlaying ^ isStationary) return; // Only update animation state if these are both true or both false

    if (isStationary) {
      _sprite.Pause();
    } else {
      _sprite.Play();
    }
  }

  public override void _Process(double delta) {
    base._Process(delta);
    _light.Scale = Vector2.One * _lightEnergy;
  }
}
