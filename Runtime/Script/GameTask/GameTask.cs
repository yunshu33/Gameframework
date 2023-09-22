using GameWorldFramework.RunTime;

namespace YGameFramework.Runtime.Script.Task
{
    public class GameTask : GameTaskBase
    {
        public GameTask() : base(GameWorld.Instance.GetSystem<TaskSystem>())
        {
            
        }
        
    }
}