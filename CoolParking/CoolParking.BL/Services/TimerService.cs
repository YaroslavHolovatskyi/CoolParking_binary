// TODO: implement class TimerService from the ITimerService interface.
//       Service have to be just wrapper on System Timers.
using CoolParking.BL.Interfaces;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class TimerService : ITimerService
    {
        public event ElapsedEventHandler Elapsed;
        public double Interval { get; set; }
        public Timer timer;
        public void Start()
        {
            timer = new Timer();
            timer.Interval = this.Interval;
            timer.Elapsed += this.Elapsed;
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }
        public void Dispose()
        {

        }
    }
}