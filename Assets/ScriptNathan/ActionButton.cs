using UnityEngine;

public class ActionButton : MonoBehaviour
{
   public void OnButtonClicked()
    {
        Debug.Log($"Action {gameObject.name} exécutée !");
    }
}