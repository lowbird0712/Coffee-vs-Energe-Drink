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
					Debug.Log("���ΰ� ������ ���׷��̵带 ������ �� �����ϴ�!");
					return;
				}
				BeanLidManager.AffectCoffeeBeanNum(-cost.Value);
				break;
			case CostType.LID:
				if (BeanLidManager.LidNum < cost.Value) {
					Debug.Log("ĵ�Ѳ��� ������ ���׷��̵带 ������ �� �����ϴ�!");
					return;
				}
				BeanLidManager.AffectCanLidNum(-cost.Value);
				break;
		}

		parentEarningPanel.LevelUp();
		cost.Value++;
	}
}
