
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
/// <summary>
/// 脚本位置：UGUI按钮组件身上
/// 脚本功能：实现按钮长按状态的判断
/// 创建时间：2015年11月17日
/// </summary>

// 继承：按下，抬起和离开的三个接口
public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
	public GameObject player;
	public PlayerController pc;
	public bool isDown = false;
	void Start()
	{
		 pc = player.GetComponent<PlayerController>();
	}
	
	// 当按钮被按下后系统自动调用此方法
	public void OnPointerDown(PointerEventData eventData)
	{	if(gameObject.name=="JumpButton")
		pc.JumpPress();
	
		isDown = true;
	}

	// 当按钮抬起的时候自动调用此方法
	public void OnPointerUp(PointerEventData eventData)
	{
		isDown = false;
		Debug.Log('a');
	}

	// 当鼠标从按钮上离开的时候自动调用此方法
	public void OnPointerExit(PointerEventData eventData)
	{
	
    }
}
