using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Controllers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Models
{
	public class Robot : Movable
	{
		private int hazRun = 0;
		private int hazRunTheSecond = 0;
		private int _counter; //counter that starts when <thHere == true>, sets <rReady> to true
		private int ogCounter;
		private bool vanhere = false; //is there a van docked
		public bool vanHere
		{
			get { return vanhere; }
			set { vanhere = value; }
		}

		private bool robotpath = false;
		public bool robotPath
		{
			get { return robotpath; }
			set { robotpath = value; }
		}

		private bool robotready = false; //is the robot ready to receive a shelf
		public bool robotReady
		{
			get { return robotready; }
			set { robotready = value; }
		}

		private bool robotloaded = false; //is the robot carrying a shelf
		public bool robotLoaded
		{
			get { return robotloaded; }
			set { robotloaded = value; }
		}

		private bool robotdropped = false; //has the robot dropped the shelf
		public bool robotDropped
		{
			get { return robotdropped; }
			set { robotdropped = value; }
		}

		private bool robotpickedup = false;
		public bool robotPickedUp
		{
			get { return robotpickedup; }
			set { robotpickedup = value; }
		}

		private bool robotstick = false;
		public bool robotStick
		{
			get { return robotstick; }
			set { robotstick = value; }
		}

		private bool robotplaced = false;
		public bool robotPlaced
		{
			get { return robotplaced; }
			set { robotplaced = value; }
		}

		private bool robotdone = true; //has the robot returned to its place
		public bool robotDone
		{
			get { return robotdone; }
			set { robotdone = value; }
		}

		private bool robotreset = false;
		public bool robotReset
		{
			get { return robotreset; }
			set { robotreset = value; }
		}

		public Robot(string rName, double targetX, double targetY, double targetZ, double x, double y, double z, double rotationX, double rotationY, double rotationZ, int counter) : base("robot", x, y, z, rotationX, rotationY, rotationZ)
		{
			this._tX = targetX;
			this._tY = targetY;
			this._tZ = targetZ;

			this.ogCounter = counter;
			this._counter = counter;
		}

		public async void GetPath(string target, List<string> path, List<string> iList, List<double> xList, List<double> zList)
		{
			this._target = target;

			for (int i = 0; i < path.Count(); i++)
			{
				string next = path[i];
				int nodeindex = iList.IndexOf(next);
				double tx = xList[nodeindex];
				double tz = zList[nodeindex];
				this.MoveTarget(tx, 0.301, tz);
				await Task.Delay(4000);
				hazRun++;
			}

			robotPlaced = true;

			if (hazRun == path.Count() && hazRunTheSecond == 0)
			{
				robotDropped = true;
				hazRun = 0;
				hazRunTheSecond++;
			}

			if (hazRun == path.Count() && hazRunTheSecond == 1)
			{
				hazRun = 0;
				hazRunTheSecond++;
				robotPickedUp = true;
				this.needsUpdate = true;
			}
			if (hazRun == path.Count() && hazRunTheSecond == 2)
			{
				this._rY = 0;
				this.robotStick = false;
				this.robotReset = true;
				this.needsUpdate = true;
			}
		}

		public override bool Update(int tick)
		{
			if (this.x >= this._tX - 0.1 && this.x <= this._tX + 0.1)
			{
				if (this.z >= this._tZ - 0.1 && this.z <= this._tZ + 0.1)
				{

				}

				else
				{
					if (this.z < this._tZ)
					{
						this.Move(this.x, this.y, this.z + 0.2);
						this._rY = 0;
					}

					else if (this.z > this._tZ)
					{
						this.Move(this.x, this.y, this.z - 0.2);
						this._rY = -Math.PI;
					}
				}
			}

			else
			{
				if (this.x < this._tX)
				{
					this.Move(this.x + 0.2, this.y, this.z);
					this._rY = Math.PI / 2;
				}

				else if (this.x > this._tX)
				{
					this.Move(this.x - 0.2, this.y, this.z);
					this._rY = -Math.PI / 2;
				}
			}

			if (vanHere && _counter > 0)
			{
				_counter--;
				Console.WriteLine(_counter);
			}

			if (_counter <= 0 && !robotLoaded)
			{
				robotReady = true;
			}

			return base.Update(tick);
		}

		public void RESET()
		{
			hazRun = 0;
			hazRunTheSecond = 0;
			_counter = ogCounter;
			vanHere = false;
			robotPath = false;
			robotReady = false;
			robotLoaded = false;
			robotDropped = false;
			robotPlaced = false;
			robotDone = true;
			robotReset = false;


		}
	}
}
