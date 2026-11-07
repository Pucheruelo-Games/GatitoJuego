using UnityEngine;

public abstract class GatoAtaque : ScriptableObject
{
    public abstract void Atacar(Gato gato);
    public virtual bool PuedeAtacar(Gato gato) => true; 
}
