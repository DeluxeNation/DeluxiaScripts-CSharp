using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deluxia{
	public class DeluxiaSecondTimer {
		public int timeLeft { get; private set; }
		private int startingTime;
		public bool pause;
		private Action action;
		public DeluxiaSecondTimer(int timer,Action activate,bool start) {
			pause = false;
			timeLeft = timer;
			startingTime = timeLeft;
			action = activate;
			if(start) {
				_ = Start();
			}
			
		}
		public async Task Restart(int set = -1) {
			startingTime = set == -1 ? startingTime : set;
			timeLeft = startingTime;
			await Start();
		}
		public async Task Start() {
			//UnityEngine.Debug.LogWarning("Start");
			pause = false;
			while(timeLeft > 0 && !pause) {
				timeLeft--;
				await Task.Delay(1000);
			}
			//UnityEngine.Debug.LogWarning("End Timer");
			if(!pause) {
				action();
			}

		}
		public void Stop() {
			//UnityEngine.Debug.LogWarning("Stop");
			pause = true;
			timeLeft = startingTime;
		}
	}
}
