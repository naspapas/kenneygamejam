extends Node2D

@onready var timer = $Timer
@onready var enemy_scene = preload("res://Entities/Enemy.tscn")

func _on_timer_timeout():
	#Spawn an enemy
	var enemy_instance = enemy_scene.instantiate()
	add_child(enemy_instance)
	
