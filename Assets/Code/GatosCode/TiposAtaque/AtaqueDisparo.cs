using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Gatos/Ataques/Disparo")]
public class AtaqueDisparo : GatoAtaque
{
    [SerializeField] GameObject _prefabDisp;
    [SerializeField] LayerMask _layerEnemigo;
    public override bool PuedeAtacar(Gato gato)
    {
        float dist = _prefabDisp.GetComponent<BalaVaquero>().GetTiempo();
        return Physics2D.Raycast(gato.transform.position, Vector2.up,dist,_layerEnemigo);
    }
    public override void Atacar(Gato gato)
    {
        Instantiate(_prefabDisp, gato.transform.position,Quaternion.identity);
    }   
}
