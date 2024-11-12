using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para usar elementos de la UI como Text

public class CameraPointerManager : MonoBehaviour
{
    [SerializeField] private GameObject pointer; // El puntero que se mover� con la c�mara
    [SerializeField] private float masDistance = 20f; // Incrementamos la distancia m�xima
    [SerializeField] private float distantPointerObject = 0.95f; // Ajuste de distancia del puntero
    [Range(0f, 1f)] private const float _maxDistance = 15; // Aumentamos la distancia m�xima para el raycast
    private GameObject _gazedAtObject = null; // Objeto con el que estamos interactuando

    //[SerializeField] private Text gazeInfoText; // Campo de texto para mostrar la informaci�n en pantalla

    private readonly string tag = "xd"; // Etiqueta que deben tener los objetos que activan el puntero
    private float scalesize = 0.05f; // Aumentamos el tama�o base del puntero

    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection; // Suscribirse al evento de selecci�n
        //gazeInfoText.text = ""; // Inicializamos el texto vac�o
    }

    private void GazeSelection()
    {
        _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver); // Llamar al evento de clic
    }

    public void Update()
    {
        // Actualizamos la posici�n del puntero cada vez que la c�mara se mueve.
        UpdatePointerPositionAndRotation();

        // Lanza un rayo hacia adelante desde la c�mara para detectar un GameObject.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // Si se detecta un nuevo GameObject frente a la c�mara.
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
                //gazeInfoText.text = ""; // Limpiar el texto cuando no est� mirando al objeto
            }
        }
        else
        {
            // Si no se detecta ning�n objeto.
            _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
            //gazeInfoText.text = ""; // Limpiar el texto cuando no se detecta un objeto
        }
    }

    private void PointerOnGaze(Vector3 hitpoint)
    {
        // Ajustar el tama�o del puntero en funci�n de la distancia al objeto detectado
        float scaleFactor = scalesize * Vector3.Distance(transform.position, hitpoint);
        pointer.transform.localScale = Vector3.one * scaleFactor;
        pointer.transform.position = CalculatePointerPosition(transform.position, hitpoint, distantPointerObject); // Posicionamos el puntero en el lugar correcto
    }

    private void PointerOutGaze()
    {
        // Restablecer el tama�o del puntero cuando no se mira ning�n objeto
        pointer.transform.localScale = Vector3.one * 0.15f; // Aumentar el tama�o por defecto del puntero para hacerlo m�s visible
        pointer.transform.localPosition = new Vector3(0, 0, masDistance); // Mover el puntero a una posici�n predeterminada
        pointer.transform.rotation = transform.rotation; // Alinearlo con la c�mara
        GazeManager.Instance.CancelGazeSelection(); // Cancelar la selecci�n de la mirada
    }

    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        // C�lculo de interpolaci�n lineal entre dos puntos
        return p0 + t * (p1 - p0);
    }

    private void UpdatePointerPositionAndRotation()
    {
        // Asegura que el puntero siga siempre a la c�mara o jugador
        Vector3 forwardDirection = transform.forward;
        Vector3 newPointerPosition = transform.position + forwardDirection * masDistance;

        pointer.transform.position = newPointerPosition; // Actualizamos la posici�n global del puntero
        pointer.transform.rotation = Quaternion.LookRotation(forwardDirection); // El puntero siempre debe mirar hacia adelante
    }
}