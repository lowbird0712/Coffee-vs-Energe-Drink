using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public enum CostType {
	BEAN,
	LID
}

public class UpgradeButton : MonoBehaviour {
	[SerializeField] BeanEarningPanel		parentEarningPanel;
	[SerializeField] Text					costText;
	[SerializeField] CostType				type;
	[SerializeField] ReactiveProperty<int>	cost = new ReactiveProperty<int>();

	private void Start() {
		cost
			.Subscribe(_x => costText.text = _x.ToString());
	}

	public void UpgradeButtonClicked() {
		switch (type) {
			case CostType.BEAN:
				if (BeanLidManager.BeanNum < cost.Value) {
					Debug.Log("원두가 부족해 업그레이드를 실행할 수 없습니다!");
					return;
				}
				BeanLidManager.AffectCoffeeBeanNum(-cost.Value);
				break;
			case CostType.LID:
				if (BeanLidManager.LidNum < cost.Value) {
					Debug.Log("캔뚜껑이 부족해 업그레이드를 실행할 수 없습니다!");
					return;
				}
				BeanLidManager.AffectCanLidNum(-cost.Value);
				break;
		}

		parentEarningPanel.LevelUp();
		cost.Value++;
	}
}
