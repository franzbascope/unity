using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpawnerScript : MonoBehaviour
{
    public Transform[] markers;
    public GameObject box;
    public InputField frecuenciaField;
    public InputField mediaField;
    public InputField desviacionField;
    public InputField nroObjetosField;
    public float frecuencia;
    public float media;
    public float desviacion;
    public int nroObjetos;
    public GameObject panel;
    List<Bitacora> listaBitacoras = new List<Bitacora>();
    public static DateTime horaInicio = DateTime.Now;
    public static DateTime horaFinalizacion;
    public int distancia = 4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Showboxes(){
        foreach (Bitacora bit in listaBitacoras)
        {
            Vector3 position = new Vector3(8f, 0f, -3f);
            var newBox = Instantiate(box, position, Quaternion.identity);
            newBox.GetComponent<CoroutineScript>().CargarMarkets(markers);
            newBox.GetComponent<CoroutineScript>().CargarMarkets(markers);
            newBox.GetComponent<CoroutineScript>().bitacora = bit;
            yield return new WaitForSeconds(frecuencia);
        }
    }
    public void onSubmit()
    {
        if (!string.IsNullOrEmpty(frecuenciaField.text) && !string.IsNullOrEmpty(frecuenciaField.text) && !string.IsNullOrEmpty(mediaField.text) && !string.IsNullOrEmpty(desviacionField.text) && !string.IsNullOrEmpty(nroObjetosField.text))
        {
            frecuencia = float.Parse(frecuenciaField.text);
            media = float.Parse(mediaField.text);
            desviacion = float.Parse(desviacionField.text);
            try
            {
                Debug.Log(nroObjetosField.text);
                horaFinalizacion = DateTime.ParseExact(nroObjetosField.text, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Debug.Log("error al parsear fecha");
                return;
            }
            if (horaInicio > horaFinalizacion)
                return;
            panel.gameObject.SetActive(false);
            realizarSimulacion();
            StartCoroutine(Showboxes());
            

        }
    }
    public void realizarSimulacion()
    {
        while ((horaFinalizacion - horaInicio).TotalSeconds>0)
        {
            cargarBitacora();
        }
        double tiempoTotal = 0;
        foreach (Bitacora bit in listaBitacoras)
        {
            Debug.Log(bit.print());
            tiempoTotal += bit.tiempoTotal;
        }
        Debug.Log("Numero de Cajas:" + listaBitacoras.Count);
        Debug.Log("Promedio de duracion de proceso: " + tiempoTotal/ listaBitacoras.Count);

    }
    public void cargarBitacora()
    {
        Bitacora bit = new Bitacora();
        bit.horasEstacion.Add(horaInicio);
        for (int i = 0; i < 4; i++)
        {
            double tiempo = calcularVAD();
            bit.listaVelocidades.Add(tiempo);
            horaInicio = horaInicio.AddSeconds(tiempo);
            bit.horasEstacion.Add(horaInicio);
        }
        listaBitacoras.Add(bit);
    }
    public float calcularVAD()
    {
        float primerAleatorio = UnityEngine.Random.Range(0.0f, 1.0f);
        float segundoAleatorio = UnityEngine.Random.Range(0.0f, 1.0f);
        float contenido = 2 * Mathf.Log(primerAleatorio) * Mathf.Cos(2 * Mathf.PI * segundoAleatorio);
        float z = Mathf.Sqrt(Mathf.Abs(contenido));
        float vad = media - (z * desviacion);
        return vad;
    }
}
