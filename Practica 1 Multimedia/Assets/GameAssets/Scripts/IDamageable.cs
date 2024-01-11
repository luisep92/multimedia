using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Luis Escolano Piquer
// Si las cosas que reciben daño implementan esto, es mas facil hacer las colisiones

public interface IDamageable
{
    void GetDamage(int damage);
}
