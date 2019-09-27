namespace TipezeNyumbaService.Interfaces
{
    // Manages the state of objects in the system
    public interface IStates
   {
        // Activates state of object based on its primary key
        bool ActivateObject(int id);
       // Deactivates state of object based on its primary key
        bool DeactivateObject(int id);
   }
}
