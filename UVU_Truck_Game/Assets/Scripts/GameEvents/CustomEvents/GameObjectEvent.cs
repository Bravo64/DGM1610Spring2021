using UnityEngine;
using UnityEngine.Events;

namespace GameEvents
{
    [CreateAssetMenu(fileName = "New GameObject Event", menuName = "Game Events/GameObject Event")]
    public class GameObjectEvent : BaseGameEvent<GameObject>
    {
        
    }

}