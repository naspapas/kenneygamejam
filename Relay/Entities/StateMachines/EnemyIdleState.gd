class_name EnemyIdleState
extends State

@export var timer: Timer
@export var actor: Enemy
@export var animator: AnimatedSprite2D

signal start_wandering

func _ready():
	set_physics_process(false)

func _enter_state() -> void:
	print_debug("Idle")
	set_physics_process(true)
	timer.start(randf_range(1.5, 6.0))
	animator.play("idle")
	actor.velocity = Vector2.ZERO

func _exit_state() -> void:
	set_physics_process(false)

func _physics_process(delta):
	if timer.is_stopped():
		start_wandering.emit()
