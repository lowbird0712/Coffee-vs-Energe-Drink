using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
	[SerializeField] GameObject[]	childBGs;
	[SerializeField] bool			isInitialBG;
	int								currentTapButtonIndex;

	private void Start() {
		if (!isInitialBG)
			gameObject.SetActive(false);
	}

	public void ChangeBG(int _index) {
		if (_index == currentTapButtonIndex)
			return;
		childBGs[currentTapButtonIndex].SetActive(false);
		childBGs[_index].SetActive(true);
		currentTapButtonIndex = _index;
	}
}
