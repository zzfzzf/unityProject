using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSet : MonoBehaviour {
	public static DefaultSet instance = null;
	public Sprite QuickSkillIcon;
	public Sprite MouseSkillLeftIcon;
	public Sprite MouseSkillRightIcon;
	public Sprite DefaultNone;
	void Awake()
	{
		instance = SingletonManger.GetSingleton<DefaultSet> ();
	}

}
