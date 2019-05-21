
namespace NetCrowdDemo.Intro
{ 
    public class NotSafeCounter : ICounter
    {
        private int counter = 0;

        public void Increase()
        {
            counter++;
        }

        public int GetValue()
        {
            return counter;
        }
    }
}
