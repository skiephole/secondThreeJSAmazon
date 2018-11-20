using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models
{
	public class Van : Model
	{
		public bool vanHere = false;

		public void SetVanFullAgain(bool full){
			if (full == true)
			{
				this.Move(this.x + 2, this.y, this.z);
				vanHere = false;

			}
		}

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
			if(this._x >= 100){
				RESET();
			}

			else if (!vanHere)
			{
				this.Move(this.x + 1, this.y, this.z);
				
			}

			return base.Update(tick);

		}

		private void RESET()
		{
			vanHere = false;

			this._x = -300;
		}
	}
}
