using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class BeanLidManager : MonoBehaviour {
    static public BeanLidManager		Inst { get; set; } = null;

	[SerializeField] Text				beanText;
	[SerializeField] Text				lidText;
	[SerializeField] Text				beanPerTimeUnitText;
	[SerializeField] int				earningTimeUnit;
	[SerializeField] BeanEarningPanel[] coffeeFarmEarningPanels;
	[SerializeField] BeanEarningPanel[] labEarningPanels;
	ReactiveProperty<int>				beanNum = new ReactiveProperty<int>();
	ReactiveProperty<int>				lidNum = new ReactiveProperty<int>();
	ReactiveProperty<int>				beanPerTimeUnit = new ReactiveProperty<int>();

	static public int					BeanNum => Inst.beanNum.Value;
	static public int					LidNum => Inst.lidNum.Value;

	private void Awake() => Inst = this;

	private void Start() {
		beanNum
			.Subscribe(_x => beanText.text = _x.ToString());
		lidNum
			.Subscribe(_x => lidText.text = _x.ToString());
		beanPerTimeUnit
			.Subscribe(_x => beanPerTimeUnitText.text = _x.ToString());
		this.UpdateAsObservable()
			.ThrottleFirst(TimeSpan.FromSeconds(earningTimeUnit))
			.Subscribe(_ => EarnByTimeUnit());
		foreach (var panel in coffeeFarmEarningPanels)
			beanPerTimeUnit.Value += panel.Speed;
		foreach (var panel in labEarningPanels)
			beanPerTimeUnit.Value += panel.Speed;
	}

	static public void AffectCoffeeBeanNum(int _num) => Inst.beanNum.Value += _num;
	static public void AffectCanLidNum(int _num) => Inst.lidNum.Value += _num;
	static public void AffectBeanPerTimeUnit(int _num) => Inst.beanPerTimeUnit.Value += _num;

	void EarnByTimeUnit() => beanNum.Value += beanPerTimeUnit.Value;
}
