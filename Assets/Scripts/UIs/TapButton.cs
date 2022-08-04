using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapButton : MonoBehaviour {
	[SerializeField] BackGround		parentBG;
	[SerializeField] int			tapIndex;

	public void TapButtonClicked() => parentBG.ChangeBG(tapIndex);
}
