using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Details")]
    [SerializeField] protected Transform muzzle;
    [SerializeField] private float fireRate = .2f;
    [SerializeField] private bool bAutomatic;
    private bool _autoActive;
    private bool _onCooldown;
    
    [Header("Ammunition Details")]
    [SerializeField] private int maxAmmo;
    [SerializeField] private int ammoCost = 1;
    private int _currentAmmo;
    
    private void Start()
    {
        _currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (_autoActive)
        {
            Fire();
        }
    }

    public virtual void Fire()
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo - ammoCost, 0, maxAmmo);
        StartCoroutine(InitiateFireCooldown());
        if(bAutomatic) _autoActive = true;
    }

    public void StopFiring()
    {
        if(bAutomatic) _autoActive = false;
    }

    protected bool CanShoot()
    {
        return _currentAmmo > ammoCost && !_onCooldown;
    }

    IEnumerator InitiateFireCooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(fireRate);
        _onCooldown = false;
    }
}