//#define DEBUG_RANDOM
using System;
using System.Collections.Generic;
using System.Linq;
namespace Deluxia.Random
{
    /// <summary>
    /// Source: https://stackoverflow.com/questions/19512210/how-to-save-the-state-of-a-random-generator-in-c
    /// </summary>
    public sealed class DeluxiaRandom
    {
        private int _seed;
        private int _INext;
        private int _INextP;
        private bool disableAdvancement = false;
        public int timesRandomized{get;private set;}
        private int[] _seedArray = new int[56];
        private bool debug = false;
        public event Action beforeState;
        #if DEBUG_RANDOM
        private static uint totalID = 0;
        private uint ID;
        #endif

        /// <summary>
        /// The current seed of this instance.
        /// </summary>
        public int Seed
        {
            get
            {
                return _seed;
            }
            set
            {
                #if DEBUG_RANDOM
                UnityEngine.Debug.Log("Reseeding ID:"+ID);
                #endif
                _seed = value;
                int subtraction = (_seed == Int32.MinValue) ? Int32.MaxValue : Math.Abs(_seed);
                int mj = 0x9a4ec86 - subtraction;
                _seedArray[0x37] = mj;
                int mk = 1;
                for (int i = 1; i < 0x37; i++)
                {
                    int ii = (0x15 * i) % 0x37;
                    _seedArray[ii] = mk;
                    mk = mj - mk;
                    if (mk < 0x0)
                    {
                        mk += Int32.MaxValue;
                    }
                    mj = _seedArray[ii];
                }
                for (int k = 1; k < 0x5; k++)
                {
                    for (int i = 1; i < 0x38; i++)
                    {
                        _seedArray[i] -= _seedArray[1 + (i + 0x1e) % 0x37];
                        if (_seedArray[i] < 0)
                        {
                            _seedArray[i] += Int32.MaxValue;
                        }
                    }
                }
                _INext = 0;
                _INextP = 21;
            }
        }

        public DeluxiaRandom(bool debug = false) : this(Guid.NewGuid().GetHashCode()){ 
            this.debug = debug;
        }

        public DeluxiaRandom(string seed,bool debug = false) : this(seed.GetHashCode()){
            this.debug = debug;
        }

        public DeluxiaRandom(int[] saveState,bool debug = false) {
            #if DEBUG_RANDOM
            ID = totalID++;
            UnityEngine.Debug.Log("Creating new random ID:"+ID);
            #endif
            LoadState(saveState);
            this.debug = debug;
        }

        public DeluxiaRandom(int seed,bool debug = false)
        {
            Seed = seed;
            #if DEBUG_RANDOM
            ID = totalID++;
            UnityEngine.Debug.Log("Creating new random ID:"+ID);
            #endif
            if(debug) {
                #if UNITY_EDITOR
                UnityEngine.Debug.Log("Random seed set to " + seed);
                #else
                Console.WriteLine("Random seed set to " + seed);
                #endif
            }
            this.debug = debug;
        }
        public static int GenerateSeed(){
            return Guid.NewGuid().GetHashCode();
        }
        public void DisableAdvancement(){
            disableAdvancement = true;
        }
        public void EnableAdvancement(){
            disableAdvancement = false;
        }
        /// <summary>
        /// Resets this instance using it's current seed.
        /// This means that the RNG will start over again,
        /// repeating the same values that it originally had.
        /// </summary>
        public void Reset()
        {
            Reseed(this.Seed);
        }

        /// <summary>
        /// Reseeds this instance using a new GUID Hashcode
        /// </summary>
        public void Reseed()
        {
            Reseed(Guid.NewGuid().GetHashCode());
        }

        /// <summary>
        /// Reseeds this instance using the hashcode of a given string.
        /// </summary>
        /// <param name="seed"></param>
        public void Reseed(string seed)
        {
            Reseed(seed.GetHashCode());
        }

        /// <summary>
        /// Reseeds this instance using a given integer seed.
        /// </summary>
        /// <param name="seed"></param>
        public void Reseed(int seed, int times = 0)
        {
            this.Seed = seed;
            for(int i = 0; i < times; i++){
                NextSample();
            }
        }

        public int NextInteger()
        {
            return NextSample();
        }

        public void NextIntegers(int[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextInteger();
            }
        }

        public int[] NextIntegers(int quantity)
        {
            int[] buffer = new int[quantity];
            NextIntegers(buffer);
            return buffer;
        }


        public int NextInteger(int minValue, int maxExclusive)
        {
            return (int)(this.NextSample() * (1.0D / Int32.MaxValue) * (maxExclusive - minValue)) + minValue;
        }


        public void NextIntegers(int minValue, int maxValue, int[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextInteger(minValue, maxValue);
            }
        }


        public int[] NextIntegers(int minValue, int maxValue, int quantity)
        {
            int[] buffer = new int[quantity];
            NextIntegers(minValue, maxValue, buffer);
            return buffer;
        }

        public int NextInteger(int maxExclusive)
        {
            return NextInteger(0, maxExclusive);
        }

        public void NextIntegers(int maxValue, int[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextInteger(maxValue);
            }
        }

        public int[] NextIntegers(int maxValue, int quantity)
        {
            int[] buffer = new int[quantity];
            NextIntegers(maxValue, buffer);
            return buffer;
        }

        public double NextDouble()
        {
            return NextSample() * (1.0D / Int32.MaxValue);
        }

        public void NextDoubles(double[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextDouble();
            }
        }

        public double[] NextDoubles(int quantity)
        {
            double[] buffer = new double[quantity];
            NextDoubles(buffer);
            return buffer;
        }

        public double NextDouble(double minValue, double maxValue)
        {
            return NextDouble() * (maxValue - minValue) + minValue;
        }

        public void NextDoubles(double minValue, double maxValue, double[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextDouble(minValue, maxValue);
            }
        }

        public double[] NextDoubles(double minValue, double maxValue, int quantity)
        {
            double[] buffer = new double[quantity];
            NextDoubles(minValue, maxValue, buffer);
            return buffer;
        }

        public float NextFloat()
        {
            return (float)(NextSample() * (1.0D / Int32.MaxValue));
        }

        public void NextFloats(float[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextFloat();
            }
        }

        public float[] NextFloats(int quantity)
        {
            float[] buffer = new float[quantity];
            NextFloats(buffer);
            return buffer;
        }

        public float NextFloat(float minValue, float maxValue)
        {
            return NextFloat() * (maxValue - minValue) + minValue;
        }

        public void NextFloats(float minValue, float maxValue, float[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextFloat(minValue, maxValue);
            }
        }

        public float[] NextFloats(float minValue, float maxValue, int quantity)
        {
            float[] buffer = new float[quantity];
            NextFloats(minValue, maxValue, buffer);
            return buffer;
        }

        public int NextRange(Range range)
        {
            return NextInteger(range.Start.Value, range.End.Value + 1);
        }

        public void NextRanges(Range range, int[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextRange(range);
            }
        }

        public int[] NextRanges(Range range, int quantity)
        {
            int[] buffer = new int[quantity];
            NextRanges(range, buffer);
            return buffer;
        }

        public int NextRange(int minValue, int maxValue)
        {
            return NextInteger(minValue, maxValue + 1);
        }

        public void NextRanges(int minValue, int maxValue, int[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextRange(minValue, maxValue);
            }
        }

        public int[] NextRanges(int minValue, int maxValue, int quantity)
        {
            int[] buffer = new int[quantity];
            NextRanges(minValue, maxValue, buffer);
            return buffer;
        }

        public byte NextByte()
        {
            return (byte)NextInteger(0, 256);
        }

        public void NextBytes(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextByte();
            }
        }

        public byte[] NextBytes(int quantity)
        {
            byte[] buffer = new byte[quantity];
            NextBytes(buffer);
            return buffer;
        }

        public byte NextByte(byte minValue, byte maxValue)
        {
            return (byte)NextInteger(minValue, maxValue);
        }

        public void NextBytes(byte minValue, byte maxValue, byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextByte(minValue, maxValue);
            }
        }

        public byte[] NextBytes(byte minValue, byte maxValue, int quantity)
        {
            byte[] buffer = new byte[quantity];
            NextBytes(minValue, maxValue, buffer);
            return buffer;
        }

        public byte NextByte(byte maxValue)
        {
            return (byte)NextInteger(0, maxValue);
        }

        public void NextBytes(byte maxValue, byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextByte(maxValue);
            }
        }

        public byte[] NextBytes(byte maxValue, int quantity)
        {
            byte[] buffer = new byte[quantity];
            NextBytes(maxValue, buffer);
            return buffer;
        }

        public bool NextBool()
        {
            return NextInteger(0, 2) == 1;
        }

        public void NextBools(bool[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextBool();
            }
        }

        public bool[] NextBools(int quantity)
        {
            bool[] buffer = new bool[quantity];
            NextBools(buffer);
            return buffer;
        }

        public string NextString(char[] possibleCharacters, int length)
        {
            char[] buffer = new char[length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = possibleCharacters[NextInteger(0, possibleCharacters.Length)];
            }
            return new string(buffer);
        }

        public void NextStrings(char[] possibleCharacters, int length, string[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextString(possibleCharacters, length);
            }
        }

        public string[] NextStrings(char[] possibleCharacters, int length, int quantity)
        {
            string[] buffer = new string[quantity];
            NextStrings(possibleCharacters, length, buffer);
            return buffer;
        }

        public T[] RandomizeArray<T>(T[] list){
			if(list.Length <= 1){
				return list;
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new List<T>(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++){
				int nextNum = NextIndex(listCopy);
				listToSend.Add(listCopy[nextNum]);
				listCopy.RemoveAt(nextNum);
			}
			return listToSend.ToArray();
		}
        public List<T> RandomizeList<T>(IEnumerable<T> list){
			if(list.Count() <= 1){
				return list.ToList();
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new();
			for(int i = 0;i < originalList.Count;i++){
				int rand = NextIndex(listCopy);
				listToSend.Add(listCopy[rand]);
				listCopy.RemoveAt(rand);
			}
			return listToSend;
		}
        public int NextIndex<T>(T[] array)
        {
            return NextInteger(array.Length);
        }

        public int NextIndex<T>(List<T> list)
        {
            return NextInteger(list.Count);
        }

        public T Choose<T>(T[] items)
        {
            return items[NextInteger(items.Length)];
        }

        public void Choose<T>(T[] items, T[] resultBuffer)
        {
            for (int i = 0; i < resultBuffer.Length; i++)
            {
                resultBuffer[i] = Choose(items);
            }
        }

        public T[] Choose<T>(T[] items, int quantity)
        {
            T[] buffer = new T[quantity];
            Choose(items, buffer);
            return buffer;
        }
        public T Choose<T>(List<T> items)
        {
            if(items.Count() == 0) {
                return default;
            }
            return items[NextInteger(0, items.Count())];
        }
        public T Choose<T>(IEnumerable<T> items)
        {
            if(items.Count() == 0) {
                return default;
            }
            return items.ToList()[NextInteger(0, items.Count())];
        }
        /// <summary>
        /// Keeps choosing until it finds a match, throws InvalidOperationException if there is no match
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T ChooseUntil<T>(IEnumerable<T> items, Func<T,bool> condition){
            List<T> itemsLeft = new(items);
            T next;
			while(itemsLeft.Any()){
				next = Choose(itemsLeft);
                itemsLeft.Remove(next);
                if(condition.Invoke(next)){
                    return next;
                }
			}
            throw new InvalidOperationException("No match");
        }
        public T ChooseUntilOrDefault<T>(IEnumerable<T> items, Func<T,bool> condition){
            List<T> itemsLeft = new(items);
            T next;
			while(itemsLeft.Any()){
				next = Choose(itemsLeft);
                itemsLeft.Remove(next);
                if(condition.Invoke(next)){
                    return next;
                }
			}
            return default;
        }
        public List<T> Choose<T>(IEnumerable<T> items, int quantity)
        {
            List<T> buffer = new(quantity);
            for (int i = 0; i < quantity; i++)
            {
                buffer.Add(Choose(items));
            }
            return buffer;
        }

		public void ChooseDistinct<T>(T[] items,T[] resultBuffer) {
            List<T> itemsLeft = new(items);
			for(int i = 0;i < resultBuffer.Length && itemsLeft.Count > 0 ;i++) {
				resultBuffer[i] = Choose(itemsLeft);
                itemsLeft.Remove(resultBuffer[i]);
			}
		}
		public List<T> ChooseDistinct<T>(List<T> items,int quantity) {
			List<T> buffer = new List<T>(quantity);
			List<T> itemsLeft = new(items);
			for(int i = 0;i < quantity && itemsLeft.Count > 0;i++) {
				buffer.Add(Choose(itemsLeft));
                itemsLeft.Remove(buffer[i]);
			}
			return buffer;
		}
		public bool NextProbability(float percent)
        {
            if (percent >= 1.0f) return true;
            if (percent <= 0.0f) return false;
            return NextDouble() <= percent;
        }

        public void NextProbability(float percent, bool[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextProbability(percent);
            }
        }

        public bool[] NextProbabilities(float percent, int quantity)
        {
            bool[] buffer = new bool[quantity];
            NextProbability(percent, buffer);
            return buffer;
        }

        public bool NextProbability(int percent)
        {
            if (percent >= 100) return true;
            if (percent <= 0) return false;
            return NextInteger(101) <= percent;
        }

        public void NextProbabilities(int percent, bool[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextProbability(percent);
            }
        }

        public bool[] NextProbabilities(int percent, int quantity)
        {
            bool[] buffer = new bool[quantity];
            NextProbabilities(percent, buffer);
            return buffer;
        }

        public bool NextOdds(int a, int b)
        {
            return NextProbability(((float)a / b));
        }

        public void NextOdds(int a, int b, bool[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = NextOdds(a, b);
            }
        }

        public bool[] NextOdds(int a, int b, int quantity)
        {
            bool[] buffer = new bool[quantity];
            NextOdds(a, b, buffer);
            return buffer;
        }
        private int NextSample()
        {
            if(disableAdvancement){
                int locINext = _INext;
                int locINextp = _INextP;
                if (++locINext >= 56)
                {
                    locINext = 1;
                }
                if (++locINextp >= 56)
                {
                    locINextp = 1;
                }
                int retVal = _seedArray[locINext] - _seedArray[locINextp];
                if (retVal == Int32.MaxValue)
                {
                    retVal--;
                }
                if (retVal < 0)
                {
                    retVal += Int32.MaxValue;
                }
                if(debug){
#if UNITY_EDITOR
                    UnityEngine.Debug.Log(timesRandomized+ " Advancement Disabled");
#else
                    Console.WriteLine(timesRandomized +  " Advancement Disabled");
#endif
                }
                return retVal;
            }
            else{
                beforeState?.Invoke();
                int locINext = _INext;
                int locINextp = _INextP;
                if (++locINext >= 56)
                {
                    locINext = 1;
                }
                if (++locINextp >= 56)
                {
                    locINextp = 1;
                }
                int retVal = _seedArray[locINext] - _seedArray[locINextp];
                if (retVal == Int32.MaxValue)
                {
                    retVal--;
                }
                if (retVal < 0)
                {
                    retVal += Int32.MaxValue;
                }
                _seedArray[locINext] = retVal;
                _INext = locINext;
                _INextP = locINextp;
                timesRandomized++;
                if(debug){
    #if UNITY_EDITOR
                #if DEBUG_RANDOM
                UnityEngine.Debug.Log($"{timesRandomized} ID:{ID}");
                #else
                UnityEngine.Debug.Log($"{timesRandomized}");
                #endif
    #else
                #if DEBUG_RANDOM
                Console.WriteLine($"{timesRandomized}  ID:{ID}");
                
                #else
                Console.WriteLine(timesRandomized);
                #endif
    #endif
                }
                return retVal;
            }
        }
        public int[] GetState()
        {
            int[] state = new int[60];
            state[0] = _seed;
            state[1] = _INext;
            state[2] = _INextP;
            state[3] = timesRandomized;
            for (int i = 4; i < this._seedArray.Length+4; i++)
            {
                state[i] = _seedArray[i - 4];
            }
            #if DEBUG_RANDOM
                UnityEngine.Debug.Log("Get state ID:"+ID + "\n"+state.SerializeToString("\n"));
            #endif
            return state;
        }

        public void LoadState(int[] saveState)
        {
            if (saveState.Length != 60)
            {
                throw new Exception("GrimoireRandom state was corrupted!");
            }
            #if DEBUG_RANDOM
                UnityEngine.Debug.Log("Load state ID:"+ID + "\n"+saveState.SerializeToString("\n"));
            #endif
            _seed = saveState[0];
            _INext = saveState[1];
            _INextP = saveState[2];
            timesRandomized = saveState[3];
            _seedArray = new int[56];
            for (int i = 4; i < 60; i++)
            {
                _seedArray[i - 4] = saveState[i];
            }
        }
        /// <summary>
        /// Returns the seed first, then times randomized
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"({Seed}): Count: {timesRandomized}";
        }

        internal void EnableDebug(bool val) {
            debug = val;
        }
    }
}
