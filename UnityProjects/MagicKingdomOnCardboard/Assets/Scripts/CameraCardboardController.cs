using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para usar elementos de la UI como Text

public class CameraPointerManager : MonoBehaviour
{
    [SerializeField] private GameObject pointer; // El puntero que se moverá con la cámara
    [SerializeField] private float masDistance = 20f; // Incrementamos la distancia máxima
    [SerializeField] private float distantPointerObject = 0.95f; // Ajuste de distancia del puntero
    [Range(0f, 1f)] private const float _maxDistance = 15; // Aumentamos la distancia máxima para el raycast
    private GameObject _gazedAtObject = null; // Objeto con el que estamos interactuando

    //[SerializeField] private Text gazeInfoText; // Campo de texto para mostrar la información en pantalla

    private readonly string tag = "xd"; // Etiqueta que deben tener los objetos que activan el puntero
    private float scalesize = 0.05f; // Aumentamos el tamaño base del puntero

    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection; // Suscribirse al evento de selección
        //gazeInfoText.text = ""; // Inicializamos el texto vacío
    }

    private void GazeSelection()
    {
        _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver); // Llamar al evento de clic
    }

    public void Update()
    {
        // Actualizamos la posición del puntero cada vez que la cámara se mueve.
        UpdatePointerPositionAndRotation();

        // Lanza un rayo hacia adelante desde la cámara para detectar un GameObject.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // Si se detecta un nuevo GameObject frente a la cámara.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter", null, SendMessageOptions.DontRequireReceiver);
                GazeManager.Instance.StartGazeSelection();
            }

            // Comprobar si el objeto tiene la etiqueta esperada
            if (hit.transform.CompareTag(tag))
            {
                PointerOnGaze(hit.point);
                //gazeInfoText.text = "Gazed at: " + _gazedAtObject.name + "\nDistance: " + hit.distance.ToString("F2") + " meters";
            }
            else
            {
                PointerOutGaze();
                //gazeInfoText.text = ""; // Limpiar el texto cuando no esté mirando al objeto
            }
        }
        else
        {
            // Si no se detecta ningún objeto.
            _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
            //gazeInfoText.text = ""; // Limpiar el texto cuando no se detecta un objeto
        }
    }

    private void PointerOnGaze(Vector3 hitpoint)
    {
        // Ajustar el tamaño del puntero en función de la distancia al objeto detectado
        float scaleFactor = scalesize * Vector3.Distance(transform.position, hitpoint);
        pointer.transform.localScale = Vector3.one * scaleFactor;
        pointer.transform.position = CalculatePointerPosition(transform.position, hitpoint, distantPointerObject); // Posicionamos el puntero en el lugar correcto
    }

    private void PointerOutGaze()
    {
        // Restablecer el tamaño del puntero cuando no se mira ningún objeto
        pointer.transform.localScale = Vector3.one * 0.15f; // Aumentar el tamaño por defecto del puntero para hacerlo más visible
        pointer.transform.localPosition = new Vector3(0, 0, masDistance); // Mover el puntero a una posición predeterminada
        pointer.transform.rotation = transform.rotation; // Alinearlo con la cámara
        GazeManager.Instance.CancelGazeSelection(); // Cancelar la selección de la mirada
    }

    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        // Cálculo de interpolación lineal entre dos puntos
        return p0 + t * (p1 - p0);
    }

    private void UpdatePointerPositionAndRotation()
    {
        // Asegura que el puntero siga siempre a la cámara o jugador
        Vector3 forwardDirection = transform.forward;
        Vector3 newPointerPosition = transform.position + forwardDirection * masDistance;

        pointer.transform.position = newPointerPosition; // Actualizamos la posición global del puntero
        pointer.transform.rotation = Quaternion.LookRotation(forwardDirection); // El puntero siempre debe mirar hacia adelante
    }
}