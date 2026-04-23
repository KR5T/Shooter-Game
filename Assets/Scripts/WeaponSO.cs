using UnityEngine;

[CreateAssetMenu(fileName ="WeaponSO", menuName ="Scriptable Object/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public int Damage = 1;
    public float FireRate = .5f;
    public GameObject hitParticle;
    public bool IsAutomatic = false;
    public bool canZoom = false;
    public float zoomAmount = 10f;
    public enum PlayerState {Gun, Melee}
    public PlayerState currentState;
    public int magazineSize = 7;
}
