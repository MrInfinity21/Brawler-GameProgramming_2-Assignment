using UnityEngine;
public class PlayerCombat : MonoBehaviour
{
    private InputController _inputController;
    
    [Header("Weapon Details")]
    [SerializeField] private Weapon equippedWeapon;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        if (equippedWeapon == null)
        {
            equippedWeapon = GetComponentInChildren<Weapon>();
        }
    }

    private void Start()
    {
        _inputController.AttackEvent += FireWeapon;
        _inputController.AttackEventCanceled += StopFiring;
    }

    void FireWeapon()
    {
        equippedWeapon.Fire();
    }
    void StopFiring()
    {
        equippedWeapon.StopFiring();
    }
}