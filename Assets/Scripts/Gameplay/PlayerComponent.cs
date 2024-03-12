using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public  IEnumerable<BaseWeapon> ActiveWeapons => m_activeWeapons;
    
    public float Speed;
    public List<BaseWeapon> DefaultWeapons;

    private List<BaseWeapon> m_activeWeapons;
    private List<IWeaponEffect> m_weaponEffects;

    public void Start()
    {
        m_activeWeapons = new List<BaseWeapon>();
        m_activeWeapons.AddRange(DefaultWeapons);
        
        m_weaponEffects = new List<IWeaponEffect>();
        foreach (var weapon in m_activeWeapons)
            m_weaponEffects.Add(weapon.GetWeaponEffect());
    }

    public void Update()
    {
        float inputHorizontal = 0.0f;
        if (Input.GetKey(KeyCode.RightArrow)) inputHorizontal++;
        if (Input.GetKey(KeyCode.LeftArrow)) inputHorizontal--;

        float inputVertical = 0.0f;
        if (Input.GetKey(KeyCode.UpArrow)) inputVertical++;
        if (Input.GetKey(KeyCode.DownArrow)) inputVertical--;

        Vector3 moveDirection = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        
        transform.position += Time.deltaTime * Speed * moveDirection;
        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        foreach (var weaponEffect in m_weaponEffects)
            weaponEffect.UpdateAndShoot(transform.position, transform.forward);
    }

    public void ApplyDamage(int damage) { }

    public void ReplaceWeapon(BaseWeapon oldWeapon, BaseWeapon newWeapon)
    {
        m_activeWeapons.Remove(oldWeapon);
        m_activeWeapons.Add(newWeapon);
        
        m_weaponEffects = new List<IWeaponEffect>();
        foreach (var weapon in m_activeWeapons)
            m_weaponEffects.Add(weapon.GetWeaponEffect());
    }
}