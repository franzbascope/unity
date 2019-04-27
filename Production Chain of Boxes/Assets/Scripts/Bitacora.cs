using System.Collections;
using System.Collections.Generic;
using System;

public class Bitacora
{
    public Bitacora()
    {
    }
    public List<DateTime> horasEstacion = new List<DateTime>();
    public double tiempoEstacion1;
    public double tiempoEstacion2;
    public double tiempoEstacion3;
    public double tiempoEstacion4;
    public double tiempoTotal;
    public List<Double> listaVelocidades = new List<Double>();

    public void cargarTiempos()
    {
        tiempoEstacion1 = (horasEstacion[1] - horasEstacion[0]).TotalSeconds;
        tiempoEstacion2 = (horasEstacion[2] - horasEstacion[1]).TotalSeconds;
        tiempoEstacion3 = (horasEstacion[3] - horasEstacion[2]).TotalSeconds;
        tiempoEstacion4 = (horasEstacion[4] - horasEstacion
            [3]).TotalSeconds;
        tiempoTotal = tiempoEstacion1 + tiempoEstacion2 + tiempoEstacion3 + tiempoEstacion4; ;
    }
    public string print()
    {
        cargarTiempos();
        return "Hora Estacion 0: " + horasEstacion[0].ToString("HH:mm:ss") +
            "Tiempo a Estacion 1: " + tiempoEstacion1 +
            "Tiempo a Estacion 2: " + tiempoEstacion2 +
            "Tiempo a Estacion 3: " + tiempoEstacion3 +
            "Tiempo a Estacion 4 :" + tiempoEstacion4 +
            "Tiempo Total: " + tiempoTotal +
            "Hora Salida: " + horasEstacion[4].ToString("HH:mm:ss");
    }
}
