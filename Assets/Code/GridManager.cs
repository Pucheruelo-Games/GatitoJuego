using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _filas = 9;
    [SerializeField] private int _columnas = 6;
    [SerializeField] private float _tamano = 1f;
    [SerializeField] private GameObject _tile;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        
        for(int fila = 0; fila < _filas; fila++)
        {
            for(int colum = 0; colum < _columnas; colum++)
            {
                float posX = colum * _tamano;
                float posY = fila * -_tamano;
                GameObject creao = Instantiate(_tile);

                creao.transform.position = new Vector2(posX, posY);
            }
        }
        float gridW = _columnas * _tamano;
        float gridH = _filas * _tamano;
        transform.position = new Vector2(-gridW / 2 + _tamano / 2, gridH / 2 - _tamano / 2);
    }
}
