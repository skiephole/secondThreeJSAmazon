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
	public abstract class Model : IUpdatable
	{
		public Model(String type, double x, double y, double z, double rotationX, double rotationY, double rotationZ)
		{
			this.type = type;
			this.guid = Guid.NewGuid();

			this._x = x;
			this._y = y;
			this._z = z;

			this._rX = rotationX;
			this._rY = rotationY;
			this._rZ = rotationZ;
		}

		public double _x = 0;
		public double _y = 0;
		public double _z = 0;
		public double _rX = 0;
		public double _rY = 0;
		public double _rZ = 0;

		public string type { get; }
		public Guid guid { get; }
		public double x { get { return _x; } }
		public double y { get { return _y; } }
		public double z { get { return _z; } }
		public double rotationX { get { return _rX; } }
		public double rotationY { get { return _rY; } }
		public double rotationZ { get { return _rZ; } }


		public bool needsUpdate = true;

		public void Move(double x, double y, double z)
		{
			this._x = x;
			this._y = y;
			this._z = z;

			this.needsUpdate = true;
		}

		public void Rotate(double rotationX, double rotationY, double rotationZ)
		{
			this._rX = rotationX;
			this._rY = rotationY;
			this._rZ = rotationZ;

			this.needsUpdate = true;
		}

		public virtual bool Update(int tick)
		{
			if (this.needsUpdate)
			{
				this.needsUpdate = false;
				return true;
			}

			return false;
		}
	}
}
