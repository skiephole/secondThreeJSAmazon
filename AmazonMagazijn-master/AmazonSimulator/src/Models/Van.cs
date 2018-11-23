using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
	public class Van : Model
	{
		private bool vanhere = false;
		public bool VanHere
		{
			get { return vanhere; }
			set { vanhere = value; }
		}

		public void SetVanFullAgain(bool full)
		{
			if (full == true)
			{
				this.Move(this.x + 2, this.y, this.z);
				VanHere = false;

			}
		}

		public Van(double x, double y, double z, double rotationX, double rotationY, double rotationZ) : base("van", x, y, z, rotationX, rotationY, rotationZ)
		{

		}

		public override bool Update(int tick)
		{
			if (this._x == -20)
			{
				VanHere = true;
			}
			else
			{
				VanHere = false;
			}
			if (this._x >= 100)
			{
				RESET();
			}

			else if (!VanHere)
			{
				this.Move(this.x + 1, this.y, this.z);

			}

			return base.Update(tick);

		}

		private void RESET()
		{
			VanHere = false;

			this._x = -300;
		}
	}
}
