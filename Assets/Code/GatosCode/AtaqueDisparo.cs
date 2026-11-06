using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Gatos/Ataques/Disparo")]
public class AtaqueDisparo : GatoAtaque
{
    [SerializeField] GameObject _prefabDisp;
    public override void Atacar(Gato gato)
    {
        Instantiate(_prefabDisp, gato.transform.localPosition,Quaternion.identity);
    }   
}
