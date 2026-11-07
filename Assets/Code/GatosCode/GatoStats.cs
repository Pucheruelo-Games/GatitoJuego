using UnityEngine;

[CreateAssetMenu(fileName = "MaterializarGato", menuName = "Gatos/Gato Stats")]
public class GatoStats : ScriptableObject
{
    [SerializeField] float _hp;
    public float hp => _hp;
    
    [SerializeField] float _atkSpeed;
    public float atkSpeed => _atkSpeed;
    [SerializeField] Sprite _skin;
    public Sprite sprite => _skin;
    [SerializeField] GatoAtaque _atkType;
    public GatoAtaque atkType => _atkType;
}
