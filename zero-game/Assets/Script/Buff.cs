using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff {
	/// 属性类型 hp mp...
	public string type{ set; get;}
	/// 属性值
	public int num{ set; get;}
	/// 开始时间
	public float beginTime{ set; get;}
	/// 结束时间 几秒结束
	public float endTime{ set; get;}

}
