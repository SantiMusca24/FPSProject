using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IntermitenteSpotlight : MonoBehaviour
{
    public Light spotlight;
    public float cortoDuracion = 0.1f;
    public float largoDuracion = 0.5f;
    public float intensidadMaxima = 8.0f;
    public float intensidadMinima = 0.0f;
    private float tiempoInicio;
    private int estado = 0;

    void Start()
    {
        tiempoInicio = Time.time;
    }

    void Update()
    {
        float tiempoActual = Time.time - tiempoInicio;
        switch (estado)
        {
            case 0:
                if (tiempoActual > cortoDuracion)
                {
                    tiempoInicio = Time.time;
                    estado = 1;
                    spotlight.intensity = intensidadMinima;
                }
                else
                {
                    spotlight.intensity = intensidadMaxima;
                }
                break;
            case 1:
                if (tiempoActual > cortoDuracion)
                {
                    tiempoInicio = Time.time;
                    estado = 2;
                    spotlight.intensity = intensidadMaxima;
                }
                else
                {
                    spotlight.intensity = intensidadMinima;
                }
                break;
            case 2:
                if (tiempoActual > largoDuracion)
                {
                    tiempoInicio = Time.time;
                    estado = 0;
                    spotlight.intensity = intensidadMinima;
                }
                else
                {
                    spotlight.intensity = intensidadMaxima;
                }
                break;
        }
    }
}

