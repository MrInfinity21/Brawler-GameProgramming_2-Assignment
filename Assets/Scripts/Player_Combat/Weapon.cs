using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Details")]
    [SerializeField] protected Transform muzzle;
    [SerializeField] private float fireRate = .2f;
    [SerializeField] private bool bAutomatic;
   
    
    [Header("Ammunition Details")]
    [SerializeField] private int maxAmmo;
    [SerializeField] private int ammoCost = 1;
    private int _currentAmmo;
    
    private bool _autoActive;
    private bool _onCooldown;

    [Header("Shooting Settings")] 
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private LayerMask shootableLayers;
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

        ShootRaycast();
        
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

    private void ShootRaycast()
    {
        if (Camera.main == null)
        {
            Debug.LogWarning("No main camera found");
            return;
        }

        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;
        
        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, shootRange, shootableLayers))
        {
            Debug.Log($"Hit: {hit.collider.name}");
        }
        
        Debug.DrawRay(ray.origin, ray.direction * shootRange, Color.red, 0.5f);
    }
}