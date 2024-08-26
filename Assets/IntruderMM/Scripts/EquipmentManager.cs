using System;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	public EquipmentSet guardEquipmentSet;
	public EquipmentSet intruderEquipmentSet;

	[Header("DEPRECATED OLD SYSTEM")]
	public bool guardSMG = false;
	public bool guardSniper = false;
	public bool guardPistol = false;
	public bool guardShotgun = false;
	public bool guardGrenade = false;
	public bool guardCharges = false;
	public bool guardSensors = false;
	public bool guardSmoke = false;
	public bool guardGas = false;
	public bool guardDecoy = false;
	public bool guardRedDot = false;

	public bool intruderSMG = false;
	public bool intruderSniper = false;
	public bool intruderPistol = false;
	public bool intruderShotgun = false;
	public bool intruderGrenade = false;
	public bool intruderCharges = false;
	public bool intruderSensors = false;
	public bool intruderSmoke = false;
	public bool intruderGas = false;
	public bool intruderDecoy = false;
	public bool intruderRedDot = false;

	public int guardSMGAmmo = 90;
	public int guardSniperAmmo = 15;
	public int guardPistolAmmo = 45;
	public int guardShotgunAmmo = 18;
	public int guardGrenadeAmmo = 1;
	public int guardChargesAmmo = 3;
	public int guardSensorsAmmo = 3;
	public int guardSmokeAmmo = 0;
	public int guardGasAmmo = 0;
	public int guardDecoyAmmo = 0;

	public int intruderSMGAmmo = 90;
	public int intruderSniperAmmo = 15;
	public int intruderPistolAmmo = 45;
	public int intruderShotgunAmmo = 18;
	public int intruderGrenadeAmmo = 1;
	public int intruderChargesAmmo = 3;
	public int intruderSensorsAmmo = 3;
	public int intruderSmokeAmmo = 0;
	public int intruderGasAmmo = 0;
	public int intruderDecoyAmmo = 0;

	public int guardSMGLoadedAmmo = 30;
	public int guardSniperLoadedAmmo = 5;
	public int guardPistolLoadedAmmo = 15;
	public int guardShotgunLoadedAmmo = 6;
	public int guardGrenadeLoadedAmmo = 1;
	public int guardChargesLoadedAmmo = 1;
	public int guardSensorsLoadedAmmo = 1;
	public int guardSmokeLoadedAmmo = 1;
	public int guardGasLoadedAmmo = 1;
	public int guardDecoyLoadedAmmo = 1;

	public int intruderSMGLoadedAmmo = 30;
	public int intruderSniperLoadedAmmo = 5;
	public int intruderPistolLoadedAmmo = 15;
	public int intruderShotgunLoadedAmmo = 6;
	public int intruderGrenadeLoadedAmmo = 1;
	public int intruderChargesLoadedAmmo = 1;
	public int intruderSensorsLoadedAmmo = 1;
	public int intruderSmokeLoadedAmmo = 1;
	public int intruderGasLoadedAmmo = 1;
	public int intruderDecoyLoadedAmmo = 1;
}