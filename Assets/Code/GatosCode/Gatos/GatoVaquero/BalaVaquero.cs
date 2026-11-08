using UnityEngine;

public class BalaVaquero : ProyectilBase
{
    protected override void AlAtacar(Collider2D obj)
    {
        obj.GetComponent<IDañable>().RecibirDmg(_dmg);
    }
}
