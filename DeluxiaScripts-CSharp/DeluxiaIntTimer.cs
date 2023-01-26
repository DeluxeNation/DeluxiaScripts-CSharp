using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deluxia{
	public class DeluxiaIntTimer {
		public int timeLeft { get; private set; }
		public bool pause;
		private Action action;
		public DeluxiaIntTimer(int timer,Action activate,bool start) {
			pause = false;
			timeLeft = timer;
			action = activate;
			if(start) {
				_ = Start();
			}
			
		}
		public async Task Start() {
			while(timeLeft > 0 && !pause) {
				timeLeft--;
				await Task.Delay(1000);
			}
			if(!pause) {
				action();
			}

		}
	}
}
