using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Models;
using Views;

namespace Controllers
{
	struct ObservingClient
	{
		public View currentView;
		public IDisposable unsubscribe;
	}

	public class SimulationController
	{
		private World world;
		private List<ObservingClient> views = new List<ObservingClient>();
		private bool running = false;
		private int tickTime = 50;
		private int ReadySetGo = 0;
		private int cycles = 0;

		public SimulationController(World world)
		{
			this.world = world;
		}

		public void AddView(View view)
		{
			ObservingClient oc = new ObservingClient();

			oc.unsubscribe = this.world.Subscribe(view);
			oc.currentView = view;

			views.Add(oc);
		}

		public void RemoveView(View view)
		{
			for (int i = 0; i < views.Count; i++)
			{
				ObservingClient currentOC = views[i];

				if (currentOC.currentView == view)
				{
					views.Remove(currentOC);
					currentOC.unsubscribe.Dispose();
				}
			}
		}

		public void Simulate()
		{
			running = true;

			while (running)
			{
				Van van = (Van)world.worldObjects[4];
				Robot robot1 = (Robot)world.worldObjects[0];
				Robot robot2 = (Robot)world.worldObjects[1];
				Robot robot3 = (Robot)world.worldObjects[2];
				Robot robot4 = (Robot)world.worldObjects[3];
				// Shelf shelf1 = (Shelf)world.worldObjects[28 + (4 * cycles)];
				// Shelf shelf2 = (Shelf)world.worldObjects[29 + (4 * cycles)];
				// Shelf shelf3 = (Shelf)world.worldObjects[30 + (4 * cycles)];
				// Shelf shelf4 = (Shelf)world.worldObjects[31 + (4 * cycles)];

				if (van.VanHere == true)
				{
					robot1.vanHere = true;
					robot2.vanHere = true;
					robot3.vanHere = true;
					robot4.vanHere = true;
				}

				if (robot1.robotReady == true)
				{
					world.AddShelfToRobot(robot1, world.shelf1);
					world.shelf1.needsUpdate = true;
					robot1.robotLoaded = true;
					ReadySetGo++;
				}

				if (robot2.robotReady == true)
				{
					world.AddShelfToRobot(robot2, world.shelf2);
					world.shelf2.needsUpdate = true;
					robot2.robotLoaded = true;
					ReadySetGo++;
				}

				if (robot3.robotReady == true)
				{

					world.AddShelfToRobot(robot3, world.shelf3);
					world.shelf3.needsUpdate = true;
					robot3.robotLoaded = true;
					ReadySetGo++;
				}

				if (robot4.robotReady == true)
				{
					world.AddShelfToRobot(robot4, world.shelf4);
					world.shelf4.needsUpdate = true;
					robot4.robotLoaded = true;
					ReadySetGo++;
				}

				if (robot1.robotDropped == true)
				{
					world.RobotGetShelf(robot1, robot1._target, world.defShelfPlace5);
					world.shelf1.needsUpdate = true;
					ReadySetGo++;
				}

				if (robot2.robotDropped == true)
				{
					world.RobotGetShelf(robot2, robot2._target, world.defShelfPlace6);
					world.shelf2.needsUpdate = true;
					ReadySetGo++;
				}

				if (robot3.robotDropped == true)
				{
					world.RobotGetShelf(robot3, robot3._target, world.defShelfPlace7);
					world.shelf3.needsUpdate = true;
					ReadySetGo++;
				}

				if (robot4.robotDropped == true)
				{
					world.RobotGetShelf(robot4, robot4._target, world.defShelfPlace8);
					world.shelf4.needsUpdate = true;
					ReadySetGo++;
				}

				if (robot1.robotPickedUp == true)
				{
					robot1.robotStick = true;
					world.RobotGoesBack(robot1, world.defShelfPlace5, "A");
				}
				if (robot1.robotStick == true)
				{
					world.shelf5._x = robot1._x;
					world.shelf5._z = robot1._z;
					world.shelf5.needsUpdate = true;
				}
				if (robot2.robotPickedUp == true)
				{
					robot2.robotStick = true;
					world.RobotGoesBack(robot2, world.defShelfPlace6, "null1");
				}
				if (robot2.robotStick == true)
				{
					world.shelf6._x = robot2._x;
					world.shelf6._z = robot2._z;
					world.shelf6.needsUpdate = true;
				}
				if (robot3.robotPickedUp == true)
				{
					robot3.robotStick = true;
					world.RobotGoesBack(robot3, world.defShelfPlace7, "null2");
				}
				if (robot3.robotStick == true)
				{
					world.shelf7._x = robot3._x;
					world.shelf7._z = robot3._z;
					world.shelf7.needsUpdate = true;
				}
				if (robot4.robotPickedUp == true)
				{
					robot4.robotStick = true;
					world.RobotGoesBack(robot4, world.defShelfPlace8, "null3");
				}
				if (robot4.robotStick == true)
				{
					world.shelf8._x = robot4._x;
					world.shelf8._z = robot4._z;
					world.shelf8.needsUpdate = true;
				}

				if (ReadySetGo == 4)
				{

					robot1.robotDone = false;
					robot2.robotDone = false;
					robot3.robotDone = false;
					robot4.robotDone = false;
					ReadySetGo++;
					world.THHere();
				}

				if ((robot1.robotReady == true || robot1.robotDone == false) && robot1.robotPlaced == false)
				{
					world.shelf1._x = robot1._x;
					world.shelf1._z = robot1._z;
					world.shelf1.needsUpdate = true;
				}

				if ((robot2.robotReady == true || robot2.robotDone == false) && robot2.robotPlaced == false)
				{
					world.shelf2._x = robot2._x;
					world.shelf2._z = robot2._z;
					world.shelf2.needsUpdate = true;
				}

				if ((robot3.robotReady == true || robot3.robotDone == false) && robot3.robotPlaced == false)
				{
					world.shelf3._x = robot3._x;
					world.shelf3._z = robot3._z;
					world.shelf3.needsUpdate = true;
				}

				if ((robot4.robotReady == true || robot4.robotDone == false) && robot4.robotPlaced == false)
				{
					world.shelf4._x = robot4._x;
					world.shelf4._z = robot4._z;
					world.shelf4.needsUpdate = true;
				}


				if (robot1.robotPickedUp && robot2.robotPickedUp && robot3.robotPickedUp && robot4.robotPickedUp)
				{
					cycles++;
				}

				if (robot1.robotReset == true && robot2.robotReset == true && robot3.robotReset == true && robot4.robotReset == true)
				{
					

					robot1.robotStick = false;
					robot2.robotStick = false;
					robot3.robotStick = false;
					robot4.robotStick = false;
					
					world.shelf1.needsUpdate = false;
					world.shelf2.needsUpdate = false;
					world.shelf3.needsUpdate = false;
					world.shelf4.needsUpdate = false;
					world.shelf5.needsUpdate = false;
					world.shelf6.needsUpdate = false;
					world.shelf7.needsUpdate = false;
					world.shelf8.needsUpdate = false;

					switch (world.whichRun){
					case true:
						world.shelf5._z = 300;
						world.shelf6._z = 300;
						world.shelf7._z = 300;
						world.shelf8._z = 300;
						world.whichRun = false;
					break;
					case false:
						world.shelf1._z = 300;
						world.shelf2._z = 300;
						world.shelf3._z = 300;
						world.shelf4._z = 300;
						world.whichRun = true;
					break;
					}
					

					double tempxshelf1 = world.shelf1._x;
					double tempxshelf2 = world.shelf2._x;
					double tempxshelf3 = world.shelf3._x;
					double tempxshelf4 = world.shelf4._x;
					double tempxshelf5 = world.shelf5._x;
					double tempxshelf6 = world.shelf6._x;
					double tempxshelf7 = world.shelf7._x;
					double tempxshelf8 = world.shelf8._x;

					double tempzshelf1 = world.shelf1._z;
					double tempzshelf2 = world.shelf2._z;
					double tempzshelf3 = world.shelf3._z;
					double tempzshelf4 = world.shelf4._z;
					double tempzshelf5 = world.shelf5._z;
					double tempzshelf6 = world.shelf6._z;
					double tempzshelf7 = world.shelf7._z;
					double tempzshelf8 = world.shelf8._z;

					world.shelf1._x = tempxshelf5;
					world.shelf2._x = tempxshelf6;
					world.shelf3._x = tempxshelf7;
					world.shelf4._x = tempxshelf8;
					world.shelf5._x = tempxshelf1;
					world.shelf6._x = tempxshelf2;
					world.shelf7._x = tempxshelf3;
					world.shelf8._x = tempxshelf4;

					world.shelf1._z = tempzshelf5;
					world.shelf2._z = tempzshelf6;
					world.shelf3._z = tempzshelf7;
					world.shelf4._z = tempzshelf8;
					world.shelf5._z = tempzshelf1;
					world.shelf6._z = tempzshelf2;
					world.shelf7._z = tempzshelf3;
					world.shelf8._z = tempzshelf4;
					

					world.shelf1.needsUpdate = true;
					world.shelf2.needsUpdate = true;
					world.shelf3.needsUpdate = true;
					world.shelf4.needsUpdate = true;
					world.shelf5.needsUpdate = true;
					world.shelf6.needsUpdate = true;
					world.shelf7.needsUpdate = true;
					world.shelf8.needsUpdate = true;

					ReadySetGo = 0;
					cycles++;

					robot1.RESET();
					robot2.RESET();
					robot3.RESET();
					robot4.RESET();

					van.SetVanFullAgain(true);
				}

				world.Update(tickTime);
				Thread.Sleep(tickTime);
			}
		}

		public void EndSimulation()
		{
			running = false;
		}
	}
}
