using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Set this up to allow Getting a MonoBehaviour on a spawned object
namespace Gold {
	[System.Serializable]
	public class ObjectPool {
		GameObject _objectToPool;
		Stack<GameObject> _pooledObjects;
		[SerializeField] List<GameObject> _pooledObjectsRefrences;
		Transform _parent;
		
		public int pooledAmount;
		public bool canGrow;


		public ObjectPool (GameObject obj, int num, bool grow = false, Transform parent = null) {
			_pooledObjects = new Stack<GameObject> ();
			_pooledObjectsRefrences = new List<GameObject>();
			pooledAmount = num;
			_objectToPool = obj;
			canGrow = grow;
			_parent = parent;

			for (int i = 0; i < pooledAmount; i++) {
				SpawnObj ();
			}
		}

		//Need to check if this is the proper gameObject
		void Add (GameObject obj) {
			_pooledObjects.Push (obj);
		}

		public GameObject Get () {
			if (_pooledObjects.Count <= 0) {
				if (canGrow) {
					SpawnObj ();
					pooledAmount++;
					return _pooledObjects.Pop ();
				} else {
					return null;
				}
			}

			return _pooledObjects.Pop ();
		}

		void SpawnObj () {
			GameObject spawnedObj = GameObject.Instantiate (_objectToPool, Vector3.zero, Quaternion.identity);
			spawnedObj.SetActive (false);
			spawnedObj.AddComponent<ObjectPoolComponent> ().Setup (this, Add);
			_pooledObjects.Push (spawnedObj);
			_pooledObjectsRefrences.Add(spawnedObj);

			if(_parent != null){
				spawnedObj.transform.parent = _parent;
			}
		}

		//Can only have one GameObject to pool
		//Can optionally have a number of objects pooled initialy
		//Option to grow
		//Option for max number of pooled items

		//Objects are sent back to the stack when they are done
		public GameObject GetPooledObject(){
			return _objectToPool;
		}

		public List<GameObject> GetPooledObjectsRefs(){
			return _pooledObjectsRefrences;
		}
	}

}