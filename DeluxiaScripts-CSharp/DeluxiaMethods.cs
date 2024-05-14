using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Deluxia {
	/// <summary>
	/// This is a class of methods that I use in my programs.
	/// </summary>
	public static class DeluxiaMethods {
		public enum CondenseModifier {
			SwapXY, InvertX, InvertY
		}
		public delegate void ModifyAll();
		public delegate void ModifyAll<T>(T toModify);
		/// <summary>
		/// Activates a method in each object in a 2D array.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="modify"></param>
		/// <typeparam name="T"></typeparam>
		public static void ModifyAllIn2DArray<T>(T[,] array,ModifyAll<T> modify) {
			for(int x = 0;x < array.GetLength(0);x++) {
				for(int y = 0;y < array.GetLength(1);y++) {
					modify(array[x,y]);
				}
			}
		}
		/// <summary>
		/// Randomize the order of a list
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> RandomizeList<T>(this IEnumerable<T> list,int seed = -1) {
			System.Random ran;
			if(seed == -1) {
				ran = new System.Random();
			}
			else {
				ran = new System.Random(seed);
			}
			if(list.Count() <= 1) {
				return list.ToList();
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++) {
				int rand = ran.Next(0,listCopy.Count);
				listToSend.Add(listCopy[rand]);
				listCopy.RemoveAt(rand);
			}
			return listToSend;
		}
		/// <summary>
		/// Randomize the order of a list
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> RandomizeList<T>(this IEnumerable<T> list,System.Random random) {
			if(list.Count() <= 1) {
				return list.ToList();
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++) {
				int rand = random.Next(0,listCopy.Count);
				listToSend.Add(listCopy[rand]);
				listCopy.RemoveAt(rand);
			}
			return listToSend;
		}
		/// <summary>
		/// Randomize the order of an array
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] RandomizeArray<T>(this T[] list,int seed = -1) {
			System.Random ran;
			if(seed == -1) {
				ran = new System.Random();
			}
			else {
				ran = new System.Random(seed);
			}
			if(list.Length <= 1) {
				return list;
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++) {
				int nextNum = ran.Next(0,listCopy.Count);
				listToSend.Add(listCopy[nextNum]);
				listCopy.RemoveAt(nextNum);
			}
			return listToSend.ToArray();
		}
		/// <summary>
		/// Randomize the order of an array
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] RandomizeArray<T>(this T[] list,System.Random random) {
			if(list.Length <= 1) {
				return list;
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++) {
				int nextNum = random.Next(0,listCopy.Count);
				listToSend.Add(listCopy[nextNum]);
				listCopy.RemoveAt(nextNum);
			}
			return listToSend.ToArray();
		}
		/// <summary>
		/// Counts the number of times a specific element shows up in a string.
		/// </summary>
		/// <param name="check">The string to check.</param>
		/// <param name="element">The element to look for. Only accepts elements with a length of 1.</param>
		/// <returns></returns>
		public static int CountElementInString(this string check,string element) {
			if(element.Length <= 0) {
				return 0;
			}
			int times = 0;
			for(int i = 0;i < check.Length - element.Length + 1;i++) {
				if(check.Substring(i,element.Length) == element) {
					times++;
				}
			}
			return times;
		}
		/// <summary>
		/// Counts the number of times a specific element shows up in a list.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="find">The element to find.</param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static int CountInList<T>(this IEnumerable<T> list,T find) {
			if(list == null) {
				return 0;
			}
			int times = 0;
			foreach(T ob in list) {
				if(ob.Equals(find)) {
					times++;
				}
			}
			return times;
		}
		///<summary>This is mostly used to generate seeds for random generation scripts</summary>
		/// <param name="str">The string to decode.</param>
		public static int DecodeString(string str) {
			if(str == null) {
				return 0;
			}
			//UnityEngine.Debug.Log("=>"+str);
			int code = 0;
			for(int i = 0;i < str.Length;i++) {
				int added = ((int)char.Parse(str.Substring(i,1)) * (i + 1));
				code += added;
				//UnityEngine.Debug.Log(str.Substring(i,1)+"=>"+added+"=>"+code);
				if(code > i * 100000) {
					code /= 2;
				}
			}
			//UnityEngine.Debug.Log("CODE =>"+code);
			return code;
		}
		/// <summary>
		/// Takes a collection of ints and returns the sum of all of them.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static long GetSum(this IEnumerable<long> list) {
			long total = 0;
			foreach(long num in list) {
				total += num;
			}
			return total;
		}
		/// <summary>
		/// Takes a collection of ints and returns the sum of all of them.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static int GetSum(this IEnumerable<int> list) {
			int total = 0;
			foreach(int num in list) {
				total += num;
			}
			return total;
		}
		/// <summary>
		/// Takes a collection of bytes and returns the sum of all of them.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static int GetSum(this IEnumerable<byte> list) {
			int total = 0;
			foreach(byte num in list) {
				total += num;
			}
			return total;
		}
		/// <summary>
		/// Takes a nested byte array and returns the sum of all of the numbers.
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static int GetSum(int[][] array) {
			int total = 0;
			foreach(int[] arr in array) {
				foreach(int num in arr) {
					total += num;
				}
			}
			return total;
		}
		/// <summary>
		/// Takes a nested byte array and returns the sum of all of the numbers.
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static int GetSum(byte[][] array) {
			int total = 0;
			foreach(byte[] arr in array) {
				foreach(byte num in arr) {
					total += num;
				}
			}
			return total;
		}
		/// <summary>
		/// Converts a nested array into a single array
		/// </summary>
		/// <param name="array"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] Condense<T>(this T[][] array) {
			if(array == null) {
				return null;
			}
			List<T> toSend = new List<T>();
			foreach(T[] ar in array) {
				toSend.AddRange(ar);
			}
			return toSend.ToArray();
		}
		/// <summary>
		/// Converts a nested array into a single array
		/// </summary>
		/// <param name="array"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] Condense<T>(this T[,] array,params CondenseModifier[] modifiers) {
			if(array == null) {
				return null;
			}

			List<T> toSend = new List<T>();
			int yStart = 0, yEnd = array.GetLength(1), yAdd = 1;
			int xStart = 0, xEnd = array.GetLength(0), xAdd = 1;
			int xD = 0, yD = 1;
			foreach(CondenseModifier modifier in modifiers) {
				switch(modifier) {
					case CondenseModifier.SwapXY:
						xD = 1;
						yD = 0;
						xEnd = array.GetLength(1);
						yEnd = array.GetLength(0);
						break;
					case CondenseModifier.InvertX:
						xStart = array.GetLength(xD) - 1;
						xEnd = -1;
						xAdd = -1;
						break;
					case CondenseModifier.InvertY:
						yStart = array.GetLength(yD) - 1;
						yEnd = -1;
						yAdd = -1;
						break;

					default:
						throw new NotImplementedException();
				}
			}
			for(int y = yStart;yStart > 0 ? y > yEnd : y < yEnd;y += yAdd) {
				for(int x = xStart;xStart > 0 ? x > xEnd : x < xEnd;x += xAdd) {
					toSend.Add(array[x,y]);
				}
			}
			return toSend.ToArray();
		}
		/// <summary>
		/// Converts a nested array of lists into a single list.
		/// </summary>
		/// <param name="array"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> Condense<T>(this List<T>[] array) {
			if(array == null) {
				return null;
			}
			List<T> toSend = new List<T>();
			foreach(List<T> ar in array) {
				toSend.AddRange(ar);
			}
			return toSend;
		}
		//https://stackoverflow.com/a/42535
		public static T[,] RotateMatrix<T>(this T[,] matrix,bool clockwise) {
			if(matrix.GetLength(0) != matrix.GetLength(1)) {
				throw new ArgumentException("X and Y length must be the same.");
			}
			int len = matrix.GetLength(0);
			T[,] ret = new T[len,len];

			if(clockwise) {
				for(int i = 0;i < len;++i) {
					for(int j = 0;j < len;++j) {
						ret[i,j] = matrix[j,len - i - 1];
					}
				}

			}
			else {
				for(int i = 0;i < len;++i) {
					for(int j = 0;j < len;++j) {
						ret[i,j] = matrix[len - j - 1,i];
					}
				}
			}

			return ret;
		}
		/// <summary>
		/// Converts a nested list of lists into a single list.
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> Condense<T>(this List<List<T>> list) {
			if(list == null) {
				return null;
			}
			List<T> toSend = new List<T>();
			foreach(List<T> ar in list) {
				toSend.AddRange(ar);
			}
			return toSend;
		}
		public static T[][] Spread<T>(T[] array,int addEvery) {
			if(array == null || addEvery <= 0) {
				return null;
			}
			List<T[]> toSend = new List<T[]>();
			int currentX = 0, currentY = 0;
			toSend.Add(new T[addEvery]);
			for(int i = 0;i < array.Length;i++) {
				if(currentY == addEvery) {
					currentX++;
					currentY = 0;
					toSend.Add(new T[addEvery]);
				}
				toSend[currentX][currentY] = array[i];
				currentY++;
			}
			return toSend.ToArray();
		}
		/// <summary>
		/// Takes a 2D int array and returns the sum of all of the numbers.
		/// </summary>
		/// <param name="array"></param>
		/// <param name = "dimension">-1 = All, 0-1 = Dimension
		/// <returns></returns>
		public static int GetSum(int[,] array,int spot,int dimension) {
			int total = 0;
			if(dimension == 0 || dimension == -1) {
				for(int i = 0;i < array.GetLength(1);i++) {
					total += array[spot,i];
				}
			}
			if(dimension == 1 || dimension == -1) {
				for(int i = 0;i < array.GetLength(0);i++) {
					total += array[i,spot];
				}
			}
			return total;
		}
		public static T[,] Reverse<T>(this T[,] array,int spot,bool asX) {
			T[,] toSend = array;
			List<T> spotsToReverse = new();
			if(asX) {
				for(int i = 0;i < array.GetLength(1);i++) {
					spotsToReverse.Add(array[spot,i]);
				}
				for(int i = 0;i < array.GetLength(1);i++) {
					toSend[spot,i] = spotsToReverse[spotsToReverse.Count - 1 - i];
				}
			}
			else {
				for(int i = 0;i < array.GetLength(0);i++) {
					spotsToReverse.Add(array[i,spot]);
				}
				for(int i = 0;i < array.GetLength(0);i++) {
					toSend[i,spot] = spotsToReverse[spotsToReverse.Count - 1 - i];
				}
			}
			return toSend;
		}
		public static T[,] Reverse<T>(this T[,] array,bool reverseX) {
			T[,] toSend = new T[array.GetLength(0),array.GetLength(1)];
			int xLength = array.GetLength(0), yLength = array.GetLength(1);
			List<T> spotsToReverse = new();
			if(reverseX) {
				for(int y = 0;y < yLength;y++) {
					for(int x = 0;x < xLength;x++) {
						toSend[x,y] = array[xLength - 1 - x,y];
					}
				}
			}
			else {
				for(int y = 0;y < yLength;y++) {
					for(int x = 0;x < xLength;x++) {
						toSend[x,y] = array[x,yLength - 1 - y];
					}
				}
			}
			return toSend;
		}
		/// <summary>
		/// Takes a 2D byte array and returns the sum of all of the numbers.
		/// </summary>
		/// <param name="array"></param>
		/// <param name = "dimension">-1 = All, 0-1 = Dimension
		/// <returns></returns>
		public static int GetSum(byte[,] array,int spot,int dimension) {
			int total = 0;
			if(dimension == 0 || dimension == -1) {
				for(int i = 0;i < array.GetLength(1);i++) {
					total += array[spot,i];
				}
			}
			if(dimension == 1 || dimension == -1) {
				for(int i = 0;i < array.GetLength(0);i++) {
					total += array[i,spot];
				}
			}
			return total;
		}
		/// <summary>
		/// Takes 2 int collections and adds all the numbers together
		/// EXAMPLE: [3,6,2],[1,5,7,9] would return [4,11,9,9]
		/// </summary>
		/// <param name="a1"></param>
		/// <returns></returns>
		public static int[] AddAllInCollections(this IEnumerable<int> a1,IEnumerable<int> a2) {
			int[] array1 = new List<int>(a1).ToArray(), array2 = new List<int>(a2).ToArray();
			for(int i = 0;i < Math.Min(array1.Length,array2.Length);i++) {
				array1[i] += array2[i];
			}
			return array1;
		}
		/// <summary>
		/// Takes an array of ints and adds all numbers into 1 array.
		/// EXAMPLE: [3,6,2],[1,5,7,9],[40,2],[1,2,4,9,12] would return [45,15,13,21,12]
		/// </summary>
		/// <param name="list">The int[][] to add</param>
		/// <returns></returns>

		public static int[] AddAllInCollections(int[][] list) {
			if(list == null || list.Length == 0) {
				return null;
			}
			int[][] list2 = list.OrderByDescending(x => x.Length).ToArray();
			int[] baseArray = list2[0];
			for(int i = 1;i < list2.Length;i++) {
				for(int j = 0;j < list2[i].Length;j++) {
					baseArray[j] += list2[i][j];
				}
			}
			return baseArray;
		}
		public static List<int> AddAllInCollections(List<List<int>> list) {
			if(list == null || list.Count == 0) {
				return null;
			}
			List<List<int>> list2 = list.OrderByDescending(x => x.Count).ToList();
			List<int> baseList = list2[0];
			for(int i = 1;i < list2.Count;i++) {
				for(int j = 0;j < list2[i].Count;j++) {
					baseList[j] += list2[i][j];
				}
			}
			return baseList;
		}
		/// <summary>
		/// Takes 2 int collections and adds subtract all the numbers together
		/// EXAMPLE: [3,6,2],[1,5,7,9] would return [2,1,-5,9]
		/// </summary>
		/// <param name="a1"></param>
		/// <returns></returns>
		public static int[] SubtractAllInCollection(this IEnumerable<int> a1,IEnumerable<int> a2) {
			int[] array1 = a1.ToArray(), array2 = a2.ToArray();
			for(int i = 0;i < Math.Min(array1.Length,array2.Length);i++) {
				array1[i] -= array2[i];
			}
			return array1;
		}
		/// <summary>
		/// Output extra if (num) does not equal 1. This is used for adding the letter s to the end of a word if there is more than 1 of something. 
		/// </summary>
		/// <param name="message">The extra text to add.</param>
		/// <param name="num">The number to check</param>
		/// <returns>If num == 1, it will return nothing. Else it will return (message).</returns>
		public static string IgnoreOn1(string message,int num) {
			return num != 1 ? message : "";
		}
		/// <summary>
		/// This makes an exact clone of the object, allowing you to change an object without affecting the other one.
		/// </summary>
		/// <param name="obj"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T CloneObject<T>(this T obj) {
			if(obj == null || obj.Equals(default(T))) {
				return default;
			}
			if(typeof(T).IsValueType){
				return obj;
			}
			var inst = obj.GetType().GetMethod("MemberwiseClone",System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			return (T)inst.Invoke(obj,null);
		}
		/// <summary>
		/// Applies the CloneObject method to all objects in a collection. Returns an Array
		/// </summary>
		/// <param name="array"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] CloneAllInArray<T>(IEnumerable<T> array) {
			T[] toSend = array.ToArray();
			for(int i = 0;i < toSend.Length;i++) {
				toSend[i] = toSend[i].CloneObject();
			}
			return toSend;
		}
		/// <summary>
		/// Adds value to a list until the list's length equals max
		/// </summary>
		/// <param name="list">The list</param>
		/// <param name="value">What to keep adding</param>
		/// <param name="max">The maximum length the list can reach</param>
		/// <typeparam name="T"></typeparam>
		public static void PopulateList<T>(this List<T> list,T value,int max) {
			for(int i = list.Count;i < max;i++) {
				list.Add(value);
			}
		}
		public static void FillDictionary<T, U>(this Dictionary<T,U> dictionary,IEnumerable<T> names,IEnumerable<U> vals) {
			T[] toAdd = names.ToArray();
			U[] toAdd2 = vals.ToArray();
			if(toAdd.Length != toAdd2.Length) {
				throw new Exception("The 2 arrays must have the same length");
			}
			for(int i = 0;i < toAdd.Length;i++) {
				dictionary.Add(toAdd[i],toAdd2[i]);
			}
		}
		public static void FillDictionary<T, U>(this Dictionary<T,U> dictionary,IEnumerable<T> names,U val) {
			T[] toAdd = names.ToArray();
			for(int i = 0;i < toAdd.Length;i++) {
				dictionary.Add(toAdd[i],val);
			}
		}
		/// <summary>
		/// Get the first index that element appears in a collection.
		/// </summary>
		/// <param name="list">The list</param>
		/// <param name="element">The element to find</param>
		/// <typeparam name="T"></typeparam>
		/// <returns>The first spot in an array where element shows up</returns>
		public static int GetIndexOfElement<T>(this IEnumerable<T> list,T element,int startAt = 0) {
			if(list == null) {
				return -1;
			}
			T[] check = list.ToArray();
			for(int i = startAt;i < check.Length;i++) {
				if(check[i] != null && check[i].Equals(element)) {
					return i;
				}
			}
			return -1;
		}
		/// <summary>
		/// Takes 2 Dictionaries and combines them.
		/// </summary>
		/// <returns></returns>
		public static Dictionary<T,U> Merge<T, U>(this Dictionary<T,U> origin,Dictionary<T,U> toAdd,bool overrideDuplicates) {
			Dictionary<T,U> toSend = new Dictionary<T,U>(origin);
			foreach(var item in toAdd) {
				if(toSend.Contains(item) && !overrideDuplicates) {
				}
				else {
					toSend[item.Key] = item.Value;
				}
			}
			return toSend;
		}
		/// <summary>
		/// Takes 2 Dictionaries and combines them.
		/// </summary>
		/// <returns></returns>
		public static Dictionary<T,U> Merge<T, U>(this Dictionary<T,U> origin,Dictionary<T,U> toAdd,bool overrideDuplicates,IEqualityComparer<T> comparer) {
			Dictionary<T,U> toSend = new(origin,comparer);
			foreach(var item in toAdd) {
				toSend[item.Key] = item.Value;
			}
			return toSend;
		}
		public static T[][] GetEveryCombination<T>(this T[] toCheck,int outputLength, bool allowDuplicateIndexes, bool allowDifferentOrder) {
			if(toCheck == null) {
				return null;
			}
			if(toCheck.Length <= 1) {
				return new T[][] { toCheck };
			}
			if(!allowDuplicateIndexes && toCheck.Length < outputLength) {
				throw new ArgumentException("array length is less than output length");
			}
			if(!allowDifferentOrder) {
				Array.Sort(toCheck);
			}
			List<T[]> toReturn = new();
			int[] spot = new int[outputLength],spotOrdered = new int[outputLength];
			int maxSpot = toCheck.Length, spotMin1 = spot.Length - 1;
			if(!allowDuplicateIndexes) {
				for(int i = 0;i < spot.Length;i++) {
					spot[i] = i;
				}
				spot[spotMin1] = spot.Length - 2;
			}
			while(spot[0] < maxSpot) {
				spot[spotMin1]++;
				for(int i = spotMin1; i >= 0;i--) {
					if(spot[i] >= maxSpot) {
						spot[i] = 0;
						if(i == 0) {
							spot[0] = -1;
							break;
						}
						spot[i - 1]++;
					}
				}
				if(spot[0] == -1) {
					break; 
				}
				if(!allowDuplicateIndexes && !spot.AllDistinct()) {
					continue;
				}
				if(!allowDifferentOrder && !spot.SequenceEqual(spot.OrderBy(x=>x))) {
					continue;
				}
				toReturn.Add(spot.Select(x => toCheck[x]).ToArray());
			}
			return toReturn.ToArray();
		}
		public static IEnumerable<IEnumerable<T>> AllShifts<T>(IEnumerable<T> collection) {
			List<IEnumerable<T>> toReturn = new();
			Queue<T> shifter = new(collection);
			for(int i = 0; i < collection.Count(); i++) {
				toReturn.Add(new List<T>(shifter));
				shifter.Enqueue(shifter.Dequeue());
			}
			return toReturn;
		}
		public static bool AllDistinct<T>(this IEnumerable<T> t) {
			HashSet<T> test = new();
			foreach(T item in t) {
				if(!test.Add(item)) {
					return false;
				}
            }
			return true;
		}
		/// <summary>
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns>The first word of sentence.</returns>
		public static string GetFirstWord(this string sentence) {
			if(sentence.Contains(" ")) {
				return sentence.Substring(0,sentence.IndexOf(" "));
			}
			return sentence;

		}
		/// <summary>
		/// Converts a string sentence into a list of words. Note: each word must be separated by a space to count.
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns>A list of all the words in a string.</returns>
		public static List<string> GetWordsInString(this string sentence,string separator = " ") {
			if(separator == null || separator.Length == 0) {
				throw new Exception($"{separator} is not a valid separator.");
			}
			string newSentence = sentence;
			newSentence = newSentence.Replace("\n",separator).Replace("\r",separator);
			List<string> toSend = new();
			int times = 0;
			while(newSentence.IndexOf(separator) != -1 && times < 500) {
				toSend.Add(newSentence[..newSentence.IndexOf(separator)]);
				newSentence = newSentence.Remove(0,newSentence.IndexOf(separator) + 1);
				times++;
			}
			if(newSentence.Length != 0) {
				toSend.Add(newSentence);
			}
			toSend.RemoveAll(x => x == separator);
			return toSend;
		}
		public static string GetFirstRange(this string sentence,string begin,string end,bool includeRangeStart) {
			if(!sentence.Contains(begin) || !sentence.Contains(end)) {
				return null;
			}
			string toSend = sentence.Substring(sentence.IndexOf(begin),sentence.IndexOf(end) - sentence.IndexOf(begin) + end.Length);
			if(includeRangeStart) {
				return toSend;
			}
			else {
				return toSend.Replace(begin,"").Replace(end,"");
			}
		}
		public static string StringListToString(this IEnumerable<string> list) {
			string toSend = "";
			foreach(string str in list) {
				toSend += str;
			}
			return toSend;
		}
		public static List<int> GetIntsInString(this string sentence) {
			string newSentence = sentence.Replace("\n","").Replace("\r","");
			List<int> toSend = new();
			bool keepAdding = false;
			int lastNum = 0;
			for(int i = 0;i < newSentence.Length;i++) {
				if(char.Parse(newSentence.Substring(i,1)) >= 48 && char.Parse(newSentence.Substring(i,1)) <= 57) {
					if(!keepAdding) {
						keepAdding = true;
						lastNum = 0;
					}
					lastNum += int.Parse(newSentence.Substring(i,1));
				}
				else {
					if(keepAdding) {
						keepAdding = false;
						toSend.Add(lastNum);
					}
				}
			}
			return toSend;
		}
		public static List<string> GetLettersInString(this string sentence) {
			List<string> toSend = new();
			for(int i = 0;i < sentence.Length;i++) {
				toSend.Add(sentence.Substring(i,1));
			}
			return toSend;
		}
		/// <summary>
		/// Checks if a string contains only capital letters.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool IsAllUpper(this string input) {
			for(int i = 0;i < input.Length;i++) {
				if(!char.IsUpper(input[i])) {
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// Compares 2 decimals and returns the percentage difference.
		/// </summary>
		/// <param name="previous">The original number.</param>
		/// <param name="current">The new number.</param>
		/// <returns>How much bigger (current) is compared to (previous)</returns>
		public static decimal CalculateChange(decimal previous,decimal current) {
			if(previous == 0) {
				return 0;
			}
			if(current == 0) {
				return -100;
			}
			var change = ((current - previous) / previous) * 100;
			return change;
		}
		/// <summary>
		/// Compares 2 floats and returns the percentage difference.
		/// </summary>
		/// <param name="previous">The original number.</param>
		/// <param name="current">The new number.</param>
		/// <returns>How much bigger (current) is compared to (previous)</returns>
		public static float CalculateChangeF(float previous,float current) {
			if(previous == 0) {
				return 0;
			}
			if(current == 0) {
				return -100;
			}
			var change = ((current - previous) / previous) * 100;
			return change;
		}
		public static bool ContainsAny<T>(this IEnumerable<T> list,IEnumerable<T> toCheck) {
			if(list == null || !list.Any() || toCheck == null || !toCheck.Any()) {
				return false;
			}
			foreach(T t in toCheck) {
				if(list.Contains(t)) {
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Rounds a number to the nearest interval
		/// For example RoundOff(3,10) would equal 0
		/// </summary>
		/// <param name="number"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public static int RoundOff(this int number,int interval) {
			if(interval <= 1) {
				return number;
			}
			int remainder = number % interval;
			number += (remainder < interval / 2) ? -remainder : (interval - remainder);
			return number;
		}
		/// <summary>
		/// Takes 2 ints and returns how far off the number is. Always returns positive
		/// </summary>
		/// <returns></returns>
		public static int OffBy(this int numA,int numB) {
			if(numA > numB) {
				//UnityEngine.Debug.Log(numA - numB);
				return numA - numB;
			}
			else {
				//UnityEngine.Debug.Log(numB - numA);
				return numB - numA;
			}
		}
		public static void Swap<T>(IList<T> list,int indexA,int indexB) {
			T tmp = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = tmp;
		}
		public static string ReplaceFirst(this string text,string search,string replace) {
			int pos = text.IndexOf(search);
			if(pos < 0) {
				return text;
			}
			return text[..pos] + replace + text[(pos + search.Length)..];
		}
		public static string ReplaceLast(this string text,string search,string replace) {
			int pos = text.LastIndexOf(search);
			if(pos < 0) {
				return text;
			}
			return text[..pos] + replace + text[(pos + search.Length)..];
		}
		/// <summary>
		/// Blocks while condition is true or timeout occurs.
		/// </summary>
		/// <param name="condition">The condition that will perpetuate the block.</param>
		/// <param name="frequency">The frequency at which the condition will be check, in milliseconds.</param>
		/// <param name="timeout">Timeout in milliseconds.</param>
		/// <exception cref="TimeoutException"></exception>
		/// <returns></returns>
		public static async Task<bool> WaitWhile(Func<bool> condition,int frequency = 25,int timeout = -1) {
			if(timeout == 0) {
				while(condition()) {
					await Task.Delay(frequency);
					//times++;
				}
				return true;
			}
			else {
				Stopwatch time = new();
				time.Start();
				while(!condition() && time.ElapsedMilliseconds < timeout) {
					await Task.Delay(frequency);
					//times++;
				}
				time.Stop();
				return time.ElapsedMilliseconds < timeout;
			}
		}

		/// <summary>
		/// Blocks until condition is true or timeout occurs.
		/// </summary>
		/// <remarks>https://stackoverflow.com/questions/29089417/c-sharp-wait-until-condition-is-true</remarks>
		/// <param name="condition">The break condition.</param>
		/// <param name="frequency">The frequency at which the condition will be checked.</param>
		/// <param name="timeout">The timeout in milliseconds.</param>
		/// <returns></returns>
		public static async Task<bool> WaitUntil(Func<bool> condition,int frequency = 25,uint timeout = 0) {
			//ulong times = 0;
			if(timeout == 0) {
				while(!condition()) {
					await Task.Delay(frequency);
					//times++;
				}
				return true;
			}
			else {
				Stopwatch time = new();
				time.Start();
				while(!condition() && time.ElapsedMilliseconds < timeout) {
					await Task.Delay(frequency);
					//times++;
				}
				time.Stop();
				return time.ElapsedMilliseconds < timeout;
			}
		}
		public static bool TryFirst<T>(this IEnumerable<T> source,out T value) {
			value = default;
			using var iterator = source.GetEnumerator();
			if(iterator.MoveNext()) {
				value = iterator.Current;
				return true;
			}
			return false;
		}
		public static string ToRoman(this int number) {
			if((number < 0) || (number > 3999))
				throw new ArgumentOutOfRangeException(nameof(number));
			if(number < 1)
				return string.Empty;
			if(number >= 1000)
				return "M" + ToRoman(number - 1000);
			if(number >= 900)
				return "CM" + ToRoman(number - 900);
			if(number >= 500)
				return "D" + ToRoman(number - 500);
			if(number >= 400)
				return "CD" + ToRoman(number - 400);
			if(number >= 100)
				return "C" + ToRoman(number - 100);
			if(number >= 90)
				return "XC" + ToRoman(number - 90);
			if(number >= 50)
				return "L" + ToRoman(number - 50);
			if(number >= 40)
				return "XL" + ToRoman(number - 40);
			if(number >= 10)
				return "X" + ToRoman(number - 10);
			if(number >= 9)
				return "IX" + ToRoman(number - 9);
			if(number >= 5)
				return "V" + ToRoman(number - 5);
			if(number >= 4)
				return "IV" + ToRoman(number - 4);
			if(number >= 1)
				return "I" + ToRoman(number - 1);
			throw new Exception("Impossible state reached");
		}
		public static List<T> GetRange<T>(this IEnumerable<T> list,int start) {
			List<T> toSend = new(), current = new(list);
			for(int i = start;i < current.Count;i++) {
				toSend.Add(current[i]);
			}
			return toSend;
		}
		public static T[] GetColumn<T>(T[,] matrix,int columnNumber) {
			return Enumerable.Range(0,matrix.GetLength(0))
					.Select(x => matrix[x,columnNumber])
					.ToArray();
		}

		public static T[] GetRow<T>(T[,] matrix,int rowNumber) {
			return Enumerable.Range(0,matrix.GetLength(1))
					.Select(x => matrix[rowNumber,x])
					.ToArray();
		}
		public class Branch<T>: IList<Branch<T>> {
			public T parent;
			private readonly List<Branch<T>> children;

			public int Count => children.Count;

			public bool IsReadOnly => false;

			public Branch<T> this[int index] { get => children[index]; set => children[index] = value; }

			public Branch(T parent) {
				this.parent = parent;
				children = new();
			}
			/*public void Add(Branch<T> child) {
				children.Add(child);
			}*/
			/*public string Serialize() {
				string result = parent.ToString();
				for(int i = 0;i < children.Count;i++) {
					result += Serialize(new() { i });
				}
				return result;
			}
			private string Serialize(List<int> depth) {
				string result = "\n";
				string depthBars = "";
				for(int i = 0;i < depth.Count;i++) {
					depthBars += "-";
				}
				result += depthBars + parent.ToString();
				try {
					Branch<T> children = GetBranch(depth);
					for(int i = 0;i < children.Count;i++) {
						depth.Add(i);
						result += Serialize(new() { i });
						depth.RemoveAt(depth.Count - 1);
					}
					return result;
				}
				catch(IndexOutOfRangeException) {
					return result;
				}
			}*/
			public string Serialize() {
				string result = parent.ToString();
				for(int i = 0;i < children.Count;i++) {
					result += children[i].SerializeContinue(1);
				}
				return result;
			}
			private string SerializeContinue(int depth) {
				string result = "\n";
				string depthBars = "";
				for(int i = 0;i < depth;i++) {
					depthBars += "-";
				}
				if(children.Count == 0) {
					depthBars += "|";
				}
				result += depthBars + parent.ToString();

				for(int i = 0;i < children.Count;i++) {
					result += children[i].SerializeContinue(depth+1);
				}
				return result;
			}
			public string Serialize(IList<int> branch) {
				try{
					string result = parent.ToString();
					if(branch.Count == 0) {
						return result;
					}
					result += children[branch[0]].SerializeContinue(1,branch);
					return result;
				}
				catch (ArgumentOutOfRangeException){
					throw new Exception($"Failed to find branch. Depth: 0\n{parent}: {children.Count}/{branch.SerializeToString()}");
				}
			}
			private string SerializeContinue(int depth,IList<int> branch) {
				try{
					string result = "\n";
					string depthBars = "";
					for(int i = 0;i < depth;i++) {
						depthBars += "-";
					}
					if(children.Count == 0) {
						depthBars += "|";
					}
					result += depthBars + parent.ToString();
					if(branch.Count > depth) {
						result += children[branch[depth]].SerializeContinue(depth + 1,branch);
					}
					return result;
				}
				catch (ArgumentOutOfRangeException){
					throw new Exception($"Failed to find branch. Depth: {depth}\n{parent}: {children.Count}/{branch.SerializeToString()}");
				}
			}
			public List<T> GetPath(IList<int> branch){
				List<T> result = new(){parent};
				if(branch.Count == 0) {
					return result;
				}
				//UnityEngine.Debug.Log(result.SerializeToString());
				children[branch[0]].GetPathContinue(1,branch,result);
				
				return result;
			}
			private void GetPathContinue(int depth,IList<int> branch, List<T> soFar){
				soFar.Add(parent);
				//UnityEngine.Debug.Log(soFar.SerializeToString());
				if(children.Count != 0 && branch.Count > depth){
					children[branch[depth]].GetPathContinue(depth + 1,branch,soFar);
				}
			}
			public string Serialize(Func<T,bool> condition) {
				string result = parent.ToString();
				for(int i = 0;i < children.Count;i++) {
					result += children[i].SerializeContinue(1,condition);
				}
				return result;
			}
			private string SerializeContinue(int depth,Func<T,bool> condition) {
				
				if(condition.Invoke(parent)) {
					string result = "\n";
					string depthBars = "";
					for(int i = 0;i < depth;i++) {
						depthBars += "-";
					}
					if(children.Count == 0) {
						depthBars += "|";
					}
					result += depthBars + parent.ToString();
					for(int i = 0;i < children.Count;i++) {
						result += children[i].SerializeContinue(depth + 1,condition);
					}
					return result;
				}
				return "";
				
			}
			public int IndexOf(Branch<T> item) {
				return children.IndexOf(item);
			}

			public void Insert(int index,Branch<T> item) {
				children.Insert(index,item);
			}

			public void RemoveAt(int index) {
				children.RemoveAt(index);
			}

			public void Add(Branch<T> item) {
				children.Add(item);
			}

			public void Clear() {
				children.Clear();
			}

			public bool Contains(Branch<T> item) {
				return children.Contains(item);
			}

			public void CopyTo(Branch<T>[] array,int arrayIndex) {
				children.CopyTo(array,arrayIndex);
			}

			public bool Remove(Branch<T> item) {
				return children.Remove(item);
			}

			public IEnumerator<Branch<T>> GetEnumerator() {
				return children.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator() {
				return children.GetEnumerator();
			}
			public Branch<T> GetBranch(List<int> i) {
				try {
					return i.Count switch {
						0 => this,
						1 => children[i[0]],
						2 => children[i[0]][i[1]],
						3 => children[i[0]][i[1]][i[2]],
						4 => children[i[0]][i[1]][i[2]][i[3]],
						5 => children[i[0]][i[1]][i[2]][i[3]][i[4]],
						6 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]],
						7 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]],
						8 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]],
						9 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]],
						10 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]],
						11 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]],
						12 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]],
						13 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]][i[12]],
						14 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]][i[12]][i[13]],
						15 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]][i[12]][i[13]][i[14]],
						16 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]][i[12]][i[13]][i[14]][i[15]],
						17 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]][i[12]][i[13]][i[14]][i[15]][i[16]],
						18 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]][i[12]][i[13]][i[14]][i[15]][i[16]][i[17]],
						19 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]][i[12]][i[13]][i[14]][i[15]][i[16]][i[17]][i[18]],
						20 => children[i[0]][i[1]][i[2]][i[3]][i[4]][i[5]][i[6]][i[7]][i[8]][i[9]][i[10]][i[11]][i[12]][i[13]][i[14]][i[15]][i[16]][i[17]][i[18]][i[19]],
						_ => throw new ArgumentException(i + " is too long of a branch, max supported is 20"),
					};
				}
				catch(IndexOutOfRangeException) {
					throw new IndexOutOfRangeException($"{i.SerializeToString()} could not be found.");
				}
				catch(ArgumentOutOfRangeException){
					throw new IndexOutOfRangeException($"{i.SerializeToString()} could not be found.");
				}
			}
		}
	}
	public class DictionaryQueue<TKey, TValue>: IDictionary<TKey,TValue> {
		//private lis
		private IList<TKey> byIndex;
		private IDictionary<TKey, TValue> collection;
		public TValue this[TKey key] { get => collection[key]; set => collection[key] = value; }

		public ICollection<TKey> Keys => byIndex;

		public ICollection<TValue> Values => byIndex.Select(x => collection[x]).ToList();

		public int Count => collection.Count;

		public bool IsReadOnly => collection.IsReadOnly;
		public DictionaryQueue() {
			byIndex = new List<TKey>();
			collection = new Dictionary<TKey, TValue>();
		}
		public DictionaryQueue(DictionaryQueue<TKey,TValue> queue){
			byIndex = new List<TKey>(queue.byIndex);
			collection = new Dictionary<TKey,TValue>(queue.collection);
		}
		public void Add(TKey key,TValue value) {
			collection.Add(key, value);
			byIndex.Add(key);
		}

		public void Add(KeyValuePair<TKey,TValue> item) {
			collection.Add(item);
			byIndex.Add(item.Key);
		}

		public void Clear() {
			collection.Clear();
			byIndex.Clear();
		}

		public bool Contains(KeyValuePair<TKey,TValue> item) {
			return collection.Contains(item);
		}

		public bool ContainsKey(TKey key) {
			return collection.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<TKey,TValue>[] array,int arrayIndex) {
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<TKey,TValue>> GetEnumerator() {
			return collection.OrderBy(x=>byIndex.IndexOf(x.Key)).GetEnumerator();// byIndex.Select(x => collection[x]).AsEnumerable();
		}

		public bool Remove(TKey key) {
			if(collection.ContainsKey(key)) {
				collection.Remove(key);
				byIndex.Remove(key);
				return true;
			}
			return false;
		}

		public bool Remove(KeyValuePair<TKey,TValue> item) {
			if(collection.Remove(item)) {
				byIndex.Remove(item.Key);
				return true;
			}
			return false;
		}

		public bool TryGetValue(TKey key,out TValue value) {
			return collection.TryGetValue(key, out value);
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return collection.OrderBy(x => byIndex.IndexOf(x.Key)).GetEnumerator();
		}
	}
	class BiDictionary<TMain, TSecondary> {
		IDictionary<TMain,TSecondary> firstToSecond = new Dictionary<TMain,TSecondary>();
		IDictionary<TSecondary,TMain> secondToFirst = new Dictionary<TSecondary,TMain>();
		public TSecondary this[TMain key] { get => firstToSecond[key]; }
		public void Add(TMain first,TSecondary second) {
			if(firstToSecond.ContainsKey(first) ||
				secondToFirst.ContainsKey(second)) {
				throw new ArgumentException("Duplicate first or second");
			}
			firstToSecond.Add(first,second);
			secondToFirst.Add(second,first);
		}

		public bool TryGetByFirst(TMain first,out TSecondary second) {
			return firstToSecond.TryGetValue(first,out second);
		}

		public bool TryGetBySecond(TSecondary second,out TMain first) {
			return secondToFirst.TryGetValue(second,out first);
		}
	}
}
