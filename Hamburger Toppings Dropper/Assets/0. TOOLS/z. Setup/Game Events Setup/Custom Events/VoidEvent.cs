using UnityEngine;
using UnityEngine.Events;

namespace GameEvents
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Event/Void Event")]
    public class VoidEvent: BaseGameEvent<Void>
    {
        public void Raise()
        {
            Raise(new Void());
        }
    }

}