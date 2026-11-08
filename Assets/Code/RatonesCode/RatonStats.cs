using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Ratones/RatonStats")]
public class RatonStats : ScriptableObject
{
    [SerializeField] float _hp;
    public float hp => _hp;
    [SerializeField] float _vel;
    public float vel => _vel;
    [SerializeField] float _dmg;
    public float dmg => _dmg;
    [SerializeField] float _atkSpeed;
    public float atkSpeed => _atkSpeed;
    [SerializeField] int _recompensa;
    public int recompensa => _recompensa;
    [SerializeField] int _dificultad;
    public int dificultad => _dificultad;
    [SerializeField] Sprite _skin;
    public Sprite sprite => _skin;
    
    
}
