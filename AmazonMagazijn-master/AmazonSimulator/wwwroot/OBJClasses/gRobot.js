class gRobot extends THREE.Group
{
	constructor()
	{
		super();

		this._loadState = LoadStates.NOT_LOADING;

		this.init();
	}

	get loadState()
	{
		return this._loadState;
	}

	init()
	{
		function addSpotLight(object, color, x, y, z, intensity, targetx, targety, targetz)
		{
			//spotlight values in order= color, intensity, distance, angle, penumbra, decay
			var spotLight = new THREE.SpotLight(color, intensity, 100, 0.5, 2, 1);
			spotLight.position.set(x, y, z);
			spotLight.castShadow = true;
			object.add(spotLight);
			object.add(spotLight.target);
			spotLight.target.position.set(targetx, targety, targetz)
		}

		function addPointLight(object, color, x, y, z, intensity, distance)
		{
			var pointLight = new THREE.PointLight(color, intensity, distance);
			pointLight.position.set(x, y, z);
			object.add(pointLight);
		}

		if (this._loadState != LoadStates.NOT_LOADING) return;

		this._loadState = LoadStates.LOADING;

		var selfRef = this;

		var mtlLoader = new THREE.MTLLoader();
		mtlLoader.setTexturePath('/textures/robot/');
		mtlLoader.setPath('/textures/robot/');
		mtlLoader.load('wheelBarrel.mtl', function (materials)
		{
			materials.preload();
			var objLoader = new THREE.OBJLoader();

			objLoader.setMaterials(materials);
			objLoader.setPath('/textures/robot/');

			objLoader.load('wheelBarrel.obj', function (object)
			{
				object.scale.set(1, 1, 1);
				object.receiveShadow = true;
				object.castShadow = true;

				addPointLight(selfRef, 0x02ff2c, 0, 0, 0, 2, 10);

				selfRef.add(object);
			}
			);
		}
		);
	}
}
