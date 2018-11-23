class gVan extends THREE.Group
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
		

		if (this._loadState != LoadStates.NOT_LOADING) return;

		this._loadState = LoadStates.LOADING;

		var selfRef = this;

		var mtlLoader = new THREE.MTLLoader();

		mtlLoader.setTexturePath('/textures/van/');
		mtlLoader.setPath('/textures/van/');
		mtlLoader.load('cake.mtl', function (materials)
		{
			materials.preload();
			var objLoader = new THREE.OBJLoader();
			objLoader.setMaterials(materials);
			objLoader.setPath('/textures/van/');

			objLoader.load('cake.obj', function (object)
			{
				object.scale.set(1, 1, 1);
				object.receiveShadow = true;
				object.castShadow = true;

			

				selfRef.add(object);
			}
			);
		}
		);
	}
}
