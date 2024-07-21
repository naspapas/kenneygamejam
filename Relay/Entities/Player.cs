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
  public int Health = 3;

  [Export]
  public float MaxEnergy = 1f;
  [Export]
  public float MinEnergy = 0.2f;
  [Export]
  public float EnergyDecayRate = 0.01f;
  [Export]
  public float EnergyRechargeRate = 0.05f;
  public bool IsPowered;


  // [Export]
  // public int DropItemOffsetDistance = 12;

  private AnimatedSprite2D _sprite;
  private Area2D _interactArea;
  private Holdable _heldObject;
  private Light _light;
  private Godot.Collections.Array<Node> _hpBarChildren;
  private Texture2D _fullHeart;
  private Texture2D _emptyHeart;
  

  public override void _Ready() {
	base._Ready();
	_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	_sprite.Play("walk");
	_interactArea = GetNode<Area2D>("InteractArea");
	_light = GetNode<Light>("Light");
	_hpBarChildren = GetNode<HBoxContainer>("hpBar").GetChildren();
	_fullHeart = (Texture2D)GD.Load("res://Assets/Sprites/full_heart.png");
	_emptyHeart = (Texture2D)GD.Load("res://Assets/Sprites/empty_heart.png");

  }

  public override void _Input(InputEvent @event) {
	if (@event.IsActionPressed("interact"))
	  if (_heldObject == null) PickupNearest();
	  else DropHeldObject();
  }

  private void PickupNearest() {
	var detectedObjects = _interactArea.GetOverlappingAreas();
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
	var newLightEnergy = _light.LightEnergy;
	newLightEnergy += IsPowered ? EnergyRechargeRate : -EnergyDecayRate;
	_light.LightEnergy = Math.Clamp(newLightEnergy, MinEnergy, MaxEnergy);
  }

  private void GetInput() {
	Vector2 inputDirection = Input.GetVector("move left", "move right", "move up", "move down");
	Velocity = inputDirection * Speed;
  }

  private void UpdateAnimationState() {
	var isPlaying = _sprite.IsPlaying();
	var isStationary = GetRealVelocity() == Vector2.Zero;

	if (isPlaying ^ isStationary) return; // Only update animation state if these are both true or both false

	if (isStationary) _sprite.Pause();
	else _sprite.Play();
  }

  public void GetHit(int dmg){
	Debug.Print($"Got hit for {dmg}");
	Health -= 1;
	Debug.Print($"Health has fallen to: {Health}");
	for (int i = 0; i < _hpBarChildren.Count(); i++)
	{
		if(_hpBarChildren[i] is TextureRect heartTexture)
		if(i < Health){
			heartTexture.Texture = _fullHeart;
		}
		else{
			heartTexture.Texture = _emptyHeart;
		}
		
	}
  }
 
 private void HPUpdate()
{
	//
}
}
