﻿using UnityEngine;

namespace GamePlay
{
	public class LevelPassDestructionrv : MonoBehaviour
	{
		[SerializeField]
		private GameObject _regularrv;
		[SerializeField]
		private GameObject _destroyedrv;

		private Vector3[] _originalPositionsrv;

		private void Awake()
		{
			_originalPositionsrv = new Vector3[_destroyedrv.transform.childCount];
		
			for (int i = 0; i < _destroyedrv.transform.childCount; i++)
			{
				_originalPositionsrv[i] = _destroyedrv.transform.GetChild(i).localPosition;
			}
		}
	
		private void Start()
		{
			RestoreLevelPass();
		}
	
		public void DestroyLevelPass()
		{
			_regularrv.SetActive(false);
			_destroyedrv.SetActive(true);
		
			for (int i = 0; i < _destroyedrv.transform.childCount; i++)
			{
				_destroyedrv.transform.GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(2000f,_destroyedrv.transform.position,3f);
			}
		
		}

		public void RestoreLevelPass()
		{	
			for (int i = 0; i < _destroyedrv.transform.childCount; i++)
			{
				_destroyedrv.transform.GetChild(i).localPosition = _originalPositionsrv[i];
				_destroyedrv.transform.GetChild(i).GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		
			_regularrv.SetActive(true);
			_destroyedrv.SetActive(false);
		}
	}
}
