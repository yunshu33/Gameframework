

namespace LJVoyage.Game.Tasks
{
    public class GameTask : GameTaskBase
    {
        public GameTask() : base(GameWorld.Instance.GetSystem<TaskSystem>())
        {
            
        }
        
    }
}