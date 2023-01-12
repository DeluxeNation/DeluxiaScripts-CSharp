using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Deluxia{
	/// <summary>
	/// This is a class of methods that I use in my programs.
	/// </summary>
	public static class DeluxiaMethods{
		public delegate void ModifyAll();
		public delegate void ModifyAll<T>(T toModify);
		/// <summary>
		/// Activates a method in each object in a 2D array.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="modify"></param>
		/// <typeparam name="T"></typeparam>
		public static void ModifyAllIn2DArray<T>(T[,] array,ModifyAll<T> modify){
			for(int x = 0;x < array.GetLength(0);x++){
				for(int y = 0;y < array.GetLength(1);y++){
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
		public static List<T> RandomizeList<T>(this IEnumerable<T> list,int seed = -1){
			System.Random ran;
			if(seed == -1){
				ran = new System.Random();
			}
			else{
				ran = new System.Random(seed);
			}
			if(list.Count() <= 1){
				return list.ToList();
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++){
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
		public static List<T> RandomizeList<T>(this IEnumerable<T> list,System.Random random){
			if(list.Count() <= 1){
				return list.ToList();
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++){
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
		public static T[] RandomizeArray<T>(this T[] list,int seed = -1){
			System.Random ran;
			if(seed == -1){
				ran = new System.Random();
			}
			else{
				ran = new System.Random(seed);
			}
			if(list.Length <= 1){
				return list;
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++){
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
		public static T[] RandomizeArray<T>(this T[] list,System.Random random){
			if(list.Length <= 1){
				return list;
			}
			List<T> originalList = list.ToList();
			List<T> listCopy = new(originalList), listToSend = new List<T>();
			for(int i = 0;i < originalList.Count;i++){
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
		public static int CountElementInString(this string check,string element){
			if(element.Length <= 0){
				return 0;
			}
			int times = 0;
			for(int i = 0;i < check.Length-element.Length+1;i++){
				if(check.Substring(i,element.Length) == element){
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
		public static int CountInList<T>(this IEnumerable<T> list,T find){
			if(list == null){
				return 0;
			}
			int times = 0;
			foreach(T ob in list){
				if(ob.Equals(find)){
					times++;
				}
			}
			return times;
		}
		///<summary>This is mostly used to generate seeds for random generation scripts</summary>
		/// <param name="str">The string to decode.</param>
		public static int DecodeString(string str){
			//UnityEngine.Debug.Log("=>"+str);
			int code = 0;
			for(int i = 0;i < str.Length;i++){
				int added = ((int)char.Parse(str.Substring(i,1)) * (i + 1));
				code += added;
				//UnityEngine.Debug.Log(str.Substring(i,1)+"=>"+added+"=>"+code);
				if(code > i * 100000){
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
		public static long GetSum(this IEnumerable<long> list){
			long total = 0;
			foreach(long num in list){
				total += num;
			}
			return total;
		}
		/// <summary>
		/// Takes a collection of ints and returns the sum of all of them.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static int GetSum(this IEnumerable<int> list){
			int total = 0;
			foreach(int num in list){
				total += num;
			}
			return total;
		}
		/// <summary>
		/// Takes a collection of bytes and returns the sum of all of them.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static int GetSum(this IEnumerable<byte> list){
			int total = 0;
			foreach(byte num in list){
				total += num;
			}
			return total;
		}
		/// <summary>
		/// Takes a nested byte array and returns the sum of all of the numbers.
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static int GetSum(int[][] array){
			int total = 0;
			foreach(int[] arr in array){
				foreach(int num in arr){
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
		public static int GetSum(byte[][] array){
			int total = 0;
			foreach(byte[] arr in array){
				foreach(byte num in arr){
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
		public static T[] Condense<T>(this T[][] array){
			if(array == null){
				return null;
			}
			List<T> toSend = new List<T>();
			foreach(T[] ar in array){
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
		public static T[] Condense<T>(this T[,] array) {
			if(array == null) {
				return null;
			}
			List<T> toSend = new List<T>();
			for(int y = 0; y < array.GetLength(1);y++) {
				for(int x = 0; x < array.GetLength(0);x++) {
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
		public static List<T> Condense<T>(this List<T>[] array){
			if(array == null){
				return null;
			}
			List<T> toSend = new List<T>();
			foreach(List<T> ar in array){
				toSend.AddRange(ar);
			}
			return toSend;
		}
        /// <summary>
		/// Converts a nested list of lists into a single list.
		/// </summary>
		/// <param name="list"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> Condense<T>(this List<List<T>> list){
			if(list == null){
				return null;
			}
			List<T> toSend = new List<T>();
			foreach(List<T> ar in list){
				toSend.AddRange(ar);
			}
			return toSend;
		}
		public static T[][] Spread<T>(T[] array, int addEvery){
			if(array == null || addEvery <= 0){
				return null;
			}
			List<T[]> toSend = new List<T[]>();
			int currentX = 0,currentY = 0;
			toSend.Add(new T[addEvery]);
			for(int i = 0; i < array.Length;i++){
				if(currentY == addEvery){
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
		public static int GetSum(int[,] array,int spot,int dimension){
			int total = 0;
			if(dimension == 0 || dimension == -1){
				for(int i = 0;i < array.GetLength(1);i++){
					total += array[spot,i];
				}
			}
			if(dimension == 1 || dimension == -1){
				for(int i = 0;i < array.GetLength(0);i++){
					total += array[i,spot];
				}
			}
			return total;
		}
		/// <summary>
		/// Takes a 2D byte array and returns the sum of all of the numbers.
		/// </summary>
		/// <param name="array"></param>
		/// <param name = "dimension">-1 = All, 0-1 = Dimension
		/// <returns></returns>
		public static int GetSum(byte[,] array,int spot,int dimension){
			int total = 0;
			if(dimension == 0 || dimension == -1){
				for(int i = 0;i < array.GetLength(1);i++){
					total += array[spot,i];
				}
			}
			if(dimension == 1 || dimension == -1){
				for(int i = 0;i < array.GetLength(0);i++){
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
		public static int[] AddAllInCollections(this IEnumerable<int> a1,IEnumerable<int> a2){
			int[] array1 = new List<int>(a1).ToArray(), array2 = new List<int>(a2).ToArray();
			for(int i = 0;i < Math.Min(array1.Length,array2.Length);i++){
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

		public static int[] AddAllInCollections(int[][] list){
			if(list == null || list.Length == 0){
				return null;
			}
			int[][] list2 = list.OrderByDescending(x=>x.Length).ToArray();
			int[] baseArray = list2[0];
			for(int i = 1;i < list2.Length;i++){
				for(int j = 0; j< list2[i].Length;j++){
					baseArray[j]+=list2[i][j];
				}
			}
			return baseArray;
		}
		public static List<int> AddAllInCollections(List<List<int>> list){
			if(list == null || list.Count == 0){
				return null;
			}
			List<List<int>> list2 = list.OrderByDescending(x=>x.Count).ToList();
			List<int> baseList = list2[0];
			for(int i = 1;i < list2.Count;i++){
				for(int j = 0; j< list2[i].Count;j++){
					baseList[j]+=list2[i][j];
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
		public static int[] SubtractAllInCollection(this IEnumerable<int> a1,IEnumerable<int> a2){
			int[] array1 = a1.ToArray(), array2 = a2.ToArray();
			for(int i = 0;i < Math.Min(array1.Length,array2.Length);i++){
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
		public static string IgnoreOn1(string message,int num){
			return num != 1 ? message : "";
		}
		/// <summary>
		/// This makes an exact clone of the object, allowing you to change an object without affecting the other one.
		/// </summary>
		/// <param name="obj"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T CloneObject<T>(this T obj){
			var inst = obj.GetType().GetMethod("MemberwiseClone",System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			return (T)inst.Invoke(obj,null);
		}
		/// <summary>
		/// Applies the CloneObject method to all objects in a collection. Returns an Array
		/// </summary>
		/// <param name="array"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] CloneAllInArray<T>(IEnumerable<T> array){
			T[] toSend = array.ToArray();
			for(int i = 0;i < toSend.Length;i++){
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
		public static void PopulateList<T>(this List<T> list,T value,int max){
			for(int i = list.Count;i < max;i++){
				list.Add(value);
			}
		}
		public static void FillDictionary<T,U>(this Dictionary<T,U> dictionary,IEnumerable<T> names,IEnumerable<U> vals){
			T[] toAdd = names.ToArray();
			U[] toAdd2 = vals.ToArray();
			if(toAdd.Length != toAdd2.Length){
				throw new Exception("The 2 arrays must have the same length");
			}
			for(int i = 0; i < toAdd.Length;i++){
				dictionary.Add(toAdd[i],toAdd2[i]);
			}
		}
		public static void FillDictionary<T,U>(this Dictionary<T,U> dictionary,IEnumerable<T> names,U val){
			T[] toAdd = names.ToArray();
			for(int i = 0; i < toAdd.Length;i++){
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
		public static int GetIndexOfElement<T>(this IEnumerable<T> list,T element,int startAt = 0){
			if(list == null) {
				return -1;
			}
			T[] check = list.ToArray();
			for(int i = startAt;i < check.Length;i++){
				if(check[i] != null && check[i].Equals(element)){
					return i;
				}
			}
			return -1;
		}
		/// <summary>
		/// Takes 2 Dictionaries and combines them.
		/// </summary>
		/// <returns></returns>
		public static Dictionary<T,U> Merge<T,U>(this Dictionary<T,U> origin,Dictionary<T,U> toAdd,bool overrideDuplicates){
			Dictionary<T,U> toSend = new Dictionary<T,U>(origin);
			foreach(var item in toAdd){
				if(toSend.Contains(item) && !overrideDuplicates){
				}
				else{
					toSend[item.Key] = item.Value;
				}
			}
			return toSend;
		}
		/// <summary>
		/// Takes 2 Dictionaries and combines them.
		/// </summary>
		/// <returns></returns>
		public static Dictionary<T,U> Merge<T, U>(this Dictionary<T,U> origin,Dictionary<T,U> toAdd,bool overrideDuplicates,IEqualityComparer<T> comparer){
			Dictionary<T,U> toSend = new(origin,comparer);
			foreach(var item in toAdd){
				toSend[item.Key] = item.Value;
			}
			return toSend;
		}
		/// <summary>
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns>The first word of sentence.</returns>
		public static string GetFirstWord(this string sentence){
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
		public static List<string> GetWordsInString(this string sentence,string separator = " "){
			if(separator == null || separator.Length == 0) {
				throw new Exception($"{separator} is not a valid seperator.");
			}
			string newSentence = sentence;
			newSentence = newSentence.Replace("\n",separator).Replace("\r",separator);
			List<string> toSend = new();
			int times = 0;
			while(newSentence.IndexOf(separator) != -1 && times < 500){
				toSend.Add(newSentence[..newSentence.IndexOf(separator)]);
				newSentence = newSentence.Remove(0,newSentence.IndexOf(separator) + 1);
				times++;
			}
			if(newSentence.Length != 0){
				toSend.Add(newSentence);
			}
			toSend.RemoveAll(x=>x == separator);
			return toSend;
		}
		public static string GetFirstRange(this string sentence, string begin, string end, bool includeRangeStart) {
			if(!sentence.Contains(begin) || !sentence.Contains(end) ){
				return null;
			}
			string toSend = sentence.Substring(sentence.IndexOf(begin),sentence.IndexOf(end) -sentence.IndexOf(begin) + end.Length);
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
			for(int i = 0; i < newSentence.Length;i++) {
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
			List<string> toSend = new List<string>();
			int times = 0;
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
		public static bool IsAllUpper(this string input){
			for(int i = 0;i < input.Length;i++){
				if(!char.IsUpper(input[i])){
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
		public static decimal CalculateChange(decimal previous,decimal current){
			if(previous == 0){
				return 0;
			}
			if(current == 0){
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
		public static float CalculateChangeF(float previous,float current){
			if(previous == 0){
				return 0;
			}
			if(current == 0){
				return -100;
			}
			var change = ((current - previous) / previous) * 100;
			return change;
		}
		public static bool ContainsAny<T>(this IEnumerable<T> list,IEnumerable<T> toCheck){
			if(list == null || !list.Any() || toCheck == null || !toCheck.Any()) {
				return false;
			}
			foreach(T t in toCheck){
				if(list.Contains(t)){
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
		/// <summary>
		/// Blocks while condition is true or timeout occurs.
		/// </summary>
		/// <param name="condition">The condition that will perpetuate the block.</param>
		/// <param name="frequency">The frequency at which the condition will be check, in milliseconds.</param>
		/// <param name="timeout">Timeout in milliseconds.</param>
		/// <exception cref="TimeoutException"></exception>
		/// <returns></returns>
		public static async Task WaitWhile(Func<bool> condition,int frequency = 25,int timeout = -1) {
			var waitTask = Task.Run(async () =>
			{
				while(condition())
					await Task.Delay(frequency);
			});

			if(waitTask != await Task.WhenAny(waitTask,Task.Delay(timeout)))
				throw new TimeoutException();
		}

		/// <summary>
		/// Blocks until condition is true or timeout occurs.
		/// </summary>
		/// <param name="condition">The break condition.</param>
		/// <param name="frequency">The frequency at which the condition will be checked.</param>
		/// <param name="timeout">The timeout in milliseconds.</param>
		/// <returns></returns>
		public static async Task WaitUntil(Func<bool> condition,int frequency = 25,int timeout = -1) {
			var waitTask = Task.Run(async () =>
			{
				while(!condition())
					await Task.Delay(frequency);
			});

			if(waitTask != await Task.WhenAny(waitTask,
					Task.Delay(timeout)))
				throw new TimeoutException();
		}
		public static string ToRoman(this int number) {
			if((number < 0) || (number > 3999))
				throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
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
	}
}
