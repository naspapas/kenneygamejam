class_name Enemy
extends CharacterBody2D

@onready var LOS = $RayCast2D
@export var speed = 50
@export var dmg = 1
var target: Node2D
var move_x = 0
var move_y = 0

func _ready():
	target = get_tree().current_scene.find_child("Player")

func _physics_process(delta):
	LOS.target_position = target.global_position - global_position
	if target:
		var direction = (target.global_position - global_position).normalized()
		velocity = direction * speed
		move_and_slide()
	
#func _process(delta):	


func _on_hitbox_body_entered(body):
	if (body.name == "Player"):
		# Hit player
		# Enter cooldown state
		body.GetHit(dmg)
