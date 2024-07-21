class_name EnemyChaseState
extends State

@export var actor: Enemy
@export var animator: AnimatedSprite2D
var target: Node2D

signal lost_player

func _ready():
	set_physics_process(false)
	target = get_tree().current_scene.find_child("Player")

func _enter_state() -> void:
	set_physics_process(true)
	animator.play("chase")

func _exit_state() -> void:
	set_physics_process(false)

func _physics_process(delta) -> void:
	animator.scale.x = -sign(actor.velocity.x)
	if animator.scale.x == 0: animator.scale.x = 1.0
	#var direction = (target.global_position - global_position).normalized()
	var direction = actor.global_position.direction_to(target.global_position)
	actor.velocity = actor.velocity.move_toward(direction * actor.speed, 50 * delta)
	actor.move_and_slide()
