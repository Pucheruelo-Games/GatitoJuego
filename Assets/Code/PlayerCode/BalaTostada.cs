using UnityEngine;

public class BalaTostada : ProyectilBase
{
    protected override void AlAtacar(Collider2D obj)
    {
        obj.GetComponent<IDañable>().RecibirDmg(_dmg);
        obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up*0.5f, ForceMode2D.Impulse);
    }
}
