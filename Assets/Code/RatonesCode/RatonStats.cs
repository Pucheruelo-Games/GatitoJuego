using UnityEngine;

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
    [SerializeField] Sprite _skin;
    public Sprite sprite => _skin;
    
}
