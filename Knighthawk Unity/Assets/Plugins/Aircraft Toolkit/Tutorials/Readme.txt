This tutorial will introduce you to the Aircraft Toolkit aircraft design.

Each file is a new step towards designing an flying airplane.

step1 - create an empty gameobject, rename it to Aircraft group (or something arbitrary), inside create a cube, scale the Y dimension to 0.01 and rename it _surface_, rotate the aircraft group so the group flyes cutting thru the air.

step2 - clone the _surface_ four times and place the four elements around the (0, 0, 0) of the group

step3 - create two empty gameobjects, rename it arbitrary, move them to x = -0.5 and x = 0.5, place the two rear _surface_ inside respective empty groups, attach GPivot to each one of the empty gameobjects and setup the elevators (you can see in the example the values).

step4 - in addition to the elevators, use one of the other two empty channels of the already attached GPivot to program the ailerons. This would be the elevators and ailerons at a time for the moment.

step5 - add two vertical stabilizers so the group cannot strafe to left and right as flying

step6 - repeat the process in step3 but this time with only one empty gameobject, and do not move it. Attach a GPivot and program the rudder channel. Rememeber to change the rotating axis from right to up (very important).

step7 - add a small cube with scale (0.2, 0.2, 0.2) and rename it counterweight. You can place the counterweight more forward or backward to equilibrate the group as you wish. Usually a little forward, but not too much. Add also a surface perpendicular to the z axis, rename it _surface_ and scale it small to simulate the drag of the airplane (airplanes are aerodynamic, so small drags are generated).

step8 - add a engine... the engine power depends on the aircraft group rigidbody mass. The more mass you add, the more power you will need. Aditionally the more mass you add, you also will need to add more lifting surface.

step9 - now, let's test some aircraft configurations... first of all, expand the x scale of the wings

step10 - to improve turns, you should separate the ailerons from the center. Duplicate elevators-ailerons, move one of the clones of each one to the wings and then disable the ailerons channels in the elevator. In the ailerons disable the elevators.

step11 - now let's test the V wing shape. The flight is a bit more stable, but turning is more slow.

step12 - the inverted wing shape is more unstable. Depending on various factors, specially if you use delta wings you can get oscillations like dutch roll.

step13 - you can add some colliders so you can land and takeoff

step14 - and finally, going back to the V shape, it is important to use the appropriate ammount of lift on each aircraft part: reducing wings makes overall aerodynamics better in response. The counterweight position to control the center of mass is also important, specially for landings, when near-to stall situations will be triggered.