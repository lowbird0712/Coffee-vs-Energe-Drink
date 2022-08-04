using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BeanEarningPanel : MonoBehaviour {
	[SerializeField] Text	levelText;
	[SerializeField] Text	speedText;
	[SerializeField] int	initialSpeed;
	ReactiveProperty<int>	level = new ReactiveProperty<int>(1);
	ReactiveProperty<int>	speed = new ReactiveProperty<int>();

	public int				Speed => speed.Value;

	private void Awake() => speed.Value = initialSpeed;

	private void Start() {
		level
			.Subscribe(_x => levelText.text = "Lv." + level.Value.ToString());
		speed
			.Subscribe(_x => speedText.text = speed.Value.ToString() + " ¿øµÎ / ÃÊ");
	}

	public void LevelUp() {
		level.Value++;
		speed.Value++;
		BeanLidManager.AffectBeanPerTimeUnit(1);
	}
}