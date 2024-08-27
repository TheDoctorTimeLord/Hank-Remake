using Core;

namespace Model
{
    /// <summary>
    /// The main model containing needed data to implement a game style 
    /// game. This class should only contain data, and methods that operate 
    /// on the data. It is initialised with data in the GameController class.
    /// </summary>
    [System.Serializable]
    public class MainGameModel
    {
        public CapabilitiesManager CapabilitiesManager = new();

        public T GetCapability<T>() where T : Capability
        {
            return CapabilitiesManager.Get<T>();
        }
    }
}