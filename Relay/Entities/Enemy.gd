extends CharacterBody2D

@export var speed = 200
var target = Vector2(0,0)
var move_x = 0
var	move_y = 0

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	print("Position x ", global_position.x)
	var x_diff = target.x - global_position.x
	if (x_diff > 0):
		move_x = 1
	if (x_diff < 0):
		print("move left")
		move_x = -1
	if (x_diff == 0):
		move_x = 0
	
	print("Position y ", global_position.y)
	var y_diff = target.y - global_position.y
	if (y_diff > 0):
		move_y = 1
	if (y_diff < 0):
		move_y = -1
	if (y_diff == 0):
		move_y = 0

	velocity = Vector2(move_x, move_y) * speed
	move_and_slide()
