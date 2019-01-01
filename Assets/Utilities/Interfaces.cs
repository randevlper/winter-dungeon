using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
	public float damage;
	public bool killInstantly;

	public DamageInfo (float damage, bool killInstantly = false){
		this.damage = damage;
		this.killInstantly = killInstantly;
	}
}

public interface IDamageable
{
	void Damage(DamageInfo hit);
}
