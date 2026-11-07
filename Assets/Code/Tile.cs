using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool _conGato = false;
    public Gato _elGato;

    public void Colocar(Gato gato)
    {
        _elGato = gato;
    }
    public void Quitar()
    {
        _elGato = null;
    }
}
