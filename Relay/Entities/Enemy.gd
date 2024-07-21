class_name Enemy
extends CharacterBody2D

@onready var state_machine = $StateMachine
@onready var enemy_idle_state = $StateMachine/EnemyIdleState
@onready var enemy_wander_state = $StateMachine/EnemyWanderState
@onready var enemy_chase_state = $StateMachine/EnemyChaseState

@onready var LOS = $Vision/RayCast2D
@export var speed = 50
@export var dmg = 1
var target: Node2D
var move_x = 0
var move_y = 0

func _ready():
	enemy_idle_state.start_wandering.connect(state_machine.change_state.bind(enemy_wander_state))
	enemy_idle_state.start_wandering.connect(state_machine.change_state.bind(enemy_wander_state))
	enemy_wander_state.idle.connect(state_machine.change_state.bind(enemy_idle_state))
	target = get_tree().current_scene.find_child("Player")

func _physics_process(delta):
	LOS.target_position = target.global_position - global_position

func _on_hitbox_body_entered(body):
	if body.name == "Player" : body.GetHit(dmg)

func _on_vision_body_entered(body):
	if body.name == "Player" : state_machine.change_state(enemy_chase_state)
