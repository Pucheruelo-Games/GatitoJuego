using UnityEngine;

public class BalaVaquero : ProyectilBase
{
    protected override void AlAtacar(IDañable obj)
    {
        obj.RecibirDmg(_dmg);
    }
}
