class_name EnemyWanderState
extends State

@export var timer: Timer
@export var actor: Enemy
@export var animator: AnimatedSprite2D

signal idle

func _ready():
	set_physics_process(false)

func _enter_state() -> void:
	print_debug("Wandering")
	set_physics_process(true)
	timer.start(randf_range(1.5, 3.0))
	timer.one_shot = true
	animator.play("move")
	if actor.velocity == Vector2.ZERO:
		actor.velocity = Vector2.RIGHT.rotated(randf_range(0, TAU)).normalized() * actor.speed/2

func _exit_state() -> void:
	set_physics_process(false)

func _physics_process(delta):
	animator.scale.x = -sign(actor.velocity.x)
	if animator.scale.x == 0: animator.scale.x = 1.0
	actor.move_and_slide()
	if timer.is_stopped():
		idle.emit()
