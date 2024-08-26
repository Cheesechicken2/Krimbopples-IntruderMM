using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EquipmentSet", menuName = "Create Equipment Set")]
public class EquipmentSet : ScriptableObject
{
	public List<GunItemVariant> gunItems = new List<GunItemVariant>();
	public List<ThrowableItemVariant> throwableItems = new List<ThrowableItemVariant>();
	public List<AmmoItemVariant> ammoItems = new List<AmmoItemVariant>();
	public List<OtherItemProxy> otherItemProxies = new List<OtherItemProxy>();

	private bool initialized = false;

}

[Serializable]
public class GunItemVariant
{
	public GunItemProxy gunItemProxy;
	public int loadedAmmo;
}

[Serializable]
public class ThrowableItemVariant
{
	public ThrowableItemProxy throwableItemProxy;
	public int amount = 1;
}

[Serializable]
public class AmmoItemVariant
{
	public AmmoItemProxy ammoItemProxy;
	public int amount;
}