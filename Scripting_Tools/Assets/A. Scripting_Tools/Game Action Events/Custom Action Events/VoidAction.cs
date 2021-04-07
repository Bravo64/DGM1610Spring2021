using UnityEngine;
using UnityEngine.Events;

namespace ActionEvents
{
    [CreateAssetMenu(fileName = "New Void Action Event", menuName = "Action Events/Void Action Event")]
    public class VoidAction : BaseGameEvent<Void>
    {
        public void Raise()
        {
            Raise(new Void());
        }
    }

}