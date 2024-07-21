class_name EnemyWanderState
extends State

@export var actor: Enemy
@export var animator: AnimatedSprite2D
@export var vision: RayCast2D

signal saw_player
#
#func _ready():
	#set_physics_process(false)

func _enter_state() -> void:
	#set_physics_process(true)
	animator.play("move")

func _exit_state() -> void:
	pass

#func _physics_process(delta):
	#animator.scale.x = -sign(actor.velocity.x)
	#if animator.scale.x == 0: animator.scale.x = 1.0
