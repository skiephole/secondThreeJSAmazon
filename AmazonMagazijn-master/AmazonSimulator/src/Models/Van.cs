using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
	public class Van : Model
	{
		private int _counter = 100;
		public bool vanHere = false;
		public bool vanEmpty = false;

		public Van(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("van", x, y, z, rotationX, rotationY, rotationZ)
		{

		}

		public override bool Update(int tick)
		{
			if (this._x == -20)
			{
				vanHere = true;
			}
			else
			{
				vanHere = false;
			}

			if (vanHere && !vanEmpty)
			{

			}

			else if (!vanHere && !vanEmpty)
			{
				this.Move(this.x + 1, this.y, this.z);
			}

			else if (vanEmpty == true)
			{
				if (_counter <= 0)
				{
					this.Move(this.x + 2, this.y, this.z);
					vanHere = false;
				}

				_counter--;
			}

			else
			{
			}

			return base.Update(tick);

		}

		public void RESET()
		{
			vanHere = false;
			vanEmpty = false;
			this._x = -150;
		}
	}
}