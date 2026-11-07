using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Gatos/Ataques/Disparo")]
public class AtaqueDisparo : GatoAtaque
{
    [SerializeField] ProyectilBase _prefabDisp;
    public override bool PuedeAtacar(Gato gato)
    {
        return Physics2D.Raycast(gato.transform.position, Vector2.up,_prefabDisp.alcance,LayerMask.NameToLayer("Raton"));
    }
    public override void Atacar(Gato gato)
    {
        Instantiate(_prefabDisp, gato.transform.position,Quaternion.identity);
    }   
}
