using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class Player : CharacterBody2D {
  [Export]
  public int Speed { get; set; } = 400;

  [Export]
  public Node GroundObjects;

  // [Export]
  // public int DropItemOffsetDistance = 12;

  private AnimatedSprite2D _sprite;
  private Area2D _pickupArea;
  private Holdable _heldObject;

  public override void _Ready() {
    base._Ready();
    _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    _sprite.Play("walk");
    _pickupArea = GetNode<Area2D>("PickupArea");
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
}
