/*
	$Id: Extensions.cs 333 2014-01-20 02:31:45Z catwalkagogo@gmail.com $
*/
using System;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace CatWalk {
	public static partial class Ext{
		#region Exception
		public static void ThrowIfNull(this object obj) {
			if(obj == null) {
				throw new ArgumentNullException();
			}
		}

		public static void ThrowIfNull(this object obj, string message) {
			if(obj == null) {
				throw new ArgumentNullException(message);
			}
		}

		public static void ThrowIfNull(this IntPtr obj) {
			if(obj == IntPtr.Zero) {
				throw new ArgumentNullException();
			}
		}

		public static void ThrowIfNull(this IntPtr obj, string message) {
			if(obj == IntPtr.Zero) {
				throw new ArgumentNullException(message);
			}
		}

		public static void ThrowIfOutOfRange(this int n, int min, int max) {
			if((n < min) || (max < n)) {
				throw new ArgumentOutOfRangeException();
			}
		}

		public static void ThrowIfOutOfRange(this int n, int min, int max, string message) {
			if((n < min) || (max < n)) {
				throw new ArgumentOutOfRangeException(message);
			}
		}

		public static void ThrowIfOutOfRange(this int n, int min) {
			if(n < min) {
				throw new ArgumentOutOfRangeException();
			}
		}

		public static void ThrowIfOutOfRange(this int n, int min, string message) {
			if(n < min) {
				throw new ArgumentOutOfRangeException(message);
			}
		}

		public static void ThrowIfOutOfRange(this double n, double min, double max) {
			if((n < min) || (max < n)) {
				throw new ArgumentOutOfRangeException();
			}
		}

		public static void ThrowIfOutOfRange(this double n, double min, double max, string message) {
			if((n < min) || (max < n)) {
				throw new ArgumentOutOfRangeException(message);
			}
		}

		public static void ThrowIfOutOfRange(this double n, double min) {
			if(n < min) {
				throw new ArgumentOutOfRangeException();
			}
		}

		public static void ThrowIfOutOfRange(this double n, double min, string message) {
			if(n < min) {
				throw new ArgumentOutOfRangeException(message);
			}
		}

		public static void ThrowIfOutOfRange(this long n, long min, long max) {
			if((n < min) || (max < n)) {
				throw new ArgumentOutOfRangeException();
			}
		}

		public static void ThrowIfOutOfRange(this long n, long min, long max, string message) {
			if((n < min) || (max < n)) {
				throw new ArgumentOutOfRangeException(message);
			}
		}

		public static void ThrowIfOutOfRange(this long n, long min) {
			if(n < min) {
				throw new ArgumentOutOfRangeException();
			}
		}

		public static void ThrowIfOutOfRange(this long n, long min, string message) {
			if(n < min) {
				throw new ArgumentOutOfRangeException(message);
			}
		}

		public static void ThrowIfNullOrEmpty(this string str, string message) {
			if(str == null) {
				throw new ArgumentNullException(message);
			}
			if(str == "") {
				throw new ArgumentException(message);
			}
		}

		#endregion

		#region Assembly

		public static string GetInformationalVersion(this Assembly asm) {
			var ver = asm.GetCustomAttributes().OfType<AssemblyInformationalVersionAttribute>().FirstOrDefault();
			return (ver != null) ? ver.InformationalVersion : null;
		}

		public static Version GetVersion(this Assembly asm) {
			return asm.GetName().Version;
		}

		public static string GetCopyright(this Assembly asm) {
			var copy = asm.GetCustomAttributes().OfType<AssemblyCopyrightAttribute>().FirstOrDefault();
			return (copy != null) ? copy.Copyright : null;
		}

		public static string GetDescription(this Assembly asm) {
			var dscr = asm.GetCustomAttributes().OfType<AssemblyDescriptionAttribute>().FirstOrDefault();
			return (dscr != null) ? dscr.Description : null;
		}

		#endregion

		#region string

		public static bool IsNullOrEmpty(this string str) {
			return String.IsNullOrEmpty(str);
		}

		public static bool IsNullOrWhitespace(this string str){
			return String.IsNullOrWhiteSpace(str);
		}

		/*
		public static bool IsMatchWildCard(this string str, string mask) {
			return PathMatchSpec(str, mask);
		}
		[DllImport("shlwapi.dll", EntryPoint = "PathMatchSpec", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool PathMatchSpec([MarshalAs(UnmanagedType.LPTStr)] string path, [MarshalAs(UnmanagedType.LPTStr)] string spec);
		*/
		public static int IndexOfRegex(this string str, string pattern) {
			return IndexOfRegex(str, pattern, 0, RegexOptions.None);
		}

		public static int IndexOfRegex(this string str, string pattern, int start) {
			return IndexOfRegex(str, pattern, start, RegexOptions.None);
		}

		public static int IndexOfRegex(this string str, string pattern, int start, RegexOptions options) {
			var rex = new Regex(pattern);
			var match = rex.Match(str, start);
			return (match.Success) ? match.Index : match.Index;
		}
		public static string ReplaceRegex(this string str, string pattern, string replacement) {
			return Regex.Replace(str, pattern, replacement);
		}

		public static string ReplaceRegex(this string str, string pattern, string replacement, RegexOptions option) {
			return Regex.Replace(str, pattern, replacement, option);
		}

		public static string ReplaceRegex(this string str, string pattern, MatchEvaluator eval) {
			return Regex.Replace(str, pattern, eval);
		}

		public static string ReplaceRegex(this string str, string pattern, MatchEvaluator eval, RegexOptions option) {
			return Regex.Replace(str, pattern, eval, option);
		}

		public static string Replace(this string str, string[] oldValues, string newValue) {
			foreach(var value in oldValues){
				str = str.Replace(value, newValue);
			}
			return str;
		}

		public static Match Match(this string str, string pattern){
			return Regex.Match(str, pattern);
		}

		public static Match Match(this string str, string pattern, RegexOptions options){
			return Regex.Match(str, pattern, options);
		}

		public static Match MatchIgnoreCase(this string str, string pattern, RegexOptions options){
			return Regex.Match(str, pattern, RegexOptions.IgnoreCase);
		}
		
		public static string Join(this IEnumerable<string> source, string separator) {
			return String.Join(separator, source.ToArray());
		}

		public static string Join<T>(this IEnumerable<T> source, string separator) {
			return String.Join(separator, source.Select(o => o.ToString()).ToArray());
		}

		#endregion

		#region char

		public static bool IsDecimalNumber(this char c){
			return (('0' <= c) && (c <= '9'));
		}

		public static bool IsSmallAlphabet(this char c){
			int n = (int)c;
			return (('a' <= n) && (n <= 'z'));
		}

		public static bool IsLargeAlphabet(this char c){
			int n = (int)c;
			return (('A' <= n) && (n <= 'Z'));
		}

		public static bool IsHiragana(this char c) {
			return 0x3040 <= c && c <= 0x309F;
		}

		public static bool IsKatakana(this char c) {
			return (0x30a1 <= c) && (c <= 0x30fa);
		}

		public static bool IsKanji(this char c) {
			return (0x4e00 <= c) && (c <= 0x9fff);
		}

		#endregion

		#region Array

		public static void Shuffle<T>(T[] array){
			var n = array.Length; 
			var rnd = new Random();
			while(n > 1){
				var k = rnd.Next(n); 
				n--; 
				var temp = array[n]; 
				array[n] = array[k]; 
				array[k] = temp; 
			}
		}

		public static void StableSort<T>(this T[] array){
			array.MergeSort(Comparer<T>.Default);
		}
	
		public static void StableSort<T>(this T[] array, IComparer<T> comparer){
			array.MergeSort(comparer);
		}
	
		public static void MergeSort<T>(this T[] array){
			array.MergeSort(Comparer<T>.Default);
		}
	
		public static void MergeSort<T>(this T[] array, IComparer<T> comparer){
			array.MergeSort(comparer, 0, array.Length, new T[array.Length]);
		}
	
		private const int MergeSortThreshold = 16;
	
		private static void MergeSort<T>(this T[] array, IComparer<T> comparer, int left, int right, T[] temp){
			if(left >= (right - 1)){
				return;
			}
			int count = right - left;
			if(count <= MergeSortThreshold){
				array.InsertSort(comparer, left, right);
			}else{
				int middle = (left + right) >> 1;
				array.MergeSort(comparer, left, middle, temp);
				array.MergeSort(comparer, middle, right, temp);
				array.Merge(comparer, left, middle, right, temp);
			}
		}
	
		private static void Merge<T>(this T[] array, IComparer<T> comparer, int left, int middle, int right, T[] temp){
			Array.Copy(array, left, temp, left, middle - left);
			int i, j;
			for(i = middle, j = right - 1; i < right; i++, j--){
				temp[i] = array[j];
			}
		
			i = left;
			j = right - 1;
			for(int k = left; k < right; k++){
				if(comparer.Compare(temp[i], temp[j]) < 0){
					array[k] = temp[i++];
				}else{
					array[k] = temp[j--];
				}
			}
		}
	/*
		public static void ParallelMergeSort<T>(this T[] array){
			array.ParallelMergeSort(Comparer<T>.Default);
		}
	
		public static void ParallelMergeSort<T>(this T[] array, IComparer<T> comparer){
			if(array.Length < 1024){
				array.MergeSort(comparer);
			}else{
				var temp = new T[array.Length];
				array.ParallelMergeSort(comparer, 0, array.Length, temp);
			}
		}
		
		private static int threadCount = 1;
		
		private static void ParallelMergeSort<T>(this T[] array, IComparer<T> comparer, int left, int right, T[] temp){
			if(left >= (right - 1)){
				return;
			}
			int count = right - left;
			if(count <= MergeSortThreshold){
				array.InsertSort(comparer, left, right);
			}else if(threadCount < Environment.ProcessorCount){
				int middle = (left + right) >> 1;
				Interlocked.Increment(ref threadCount);
				var thread1 = new Thread(new ThreadStart(delegate{
					array.ParallelMergeSort(comparer, left, middle, temp);
					Interlocked.Decrement(ref threadCount);
				}));
				thread1.Start();
				array.ParallelMergeSort(comparer, middle, right, temp);
				thread1.Join();
				array.Merge(comparer, left, middle, right, temp);
			}else{
				int middle = (left + right) >> 1;
				array.ParallelMergeSort(comparer, left, middle, temp);
				array.ParallelMergeSort(comparer, middle, right, temp);
				array.Merge(comparer, left, middle, right, temp);
			}
		}
	*/
		public static void InsertSort<T>(this T[] array){
			array.InsertSort(Comparer<T>.Default, 0, array.Length);
		}
	
		private static void InsertSort<T>(this T[] array, IComparer<T> comparer, int left, int right){
			for(int i = left + 1; i < right; i++){
				for(int j = i; j >= left + 1 && comparer.Compare(array[j - 1], array[j]) > 0; --j){
					Swap(ref array[j], ref array[j - 1]);
				}
			}
		}
	
		public static void ShellSort<T>(this T[] array){
			array.ShellSort(Comparer<T>.Default, 0, array.Length);
		}
	
		private static void ShellSort<T>(this T[] array, IComparer<T> comparer, int left, int right){
			int j, h;
			T temp;
			h = left + 1;
			while (h < right){
				h = (h * 3) + 1;
			}
			int left2 = left + 1;
			while(h > left2){
				h = h / 3;
				for(int i = h; i < right; i++){
					temp = array[i];
					j = i - h;
					while(comparer.Compare(temp, array[j]) < 0){
						array[j + h] = array[j];
						j = j - h;
						if(j < left){
							break;
						}
					}
					array[j + h] = temp;
				}
			}
		}
	
		private static void Swap<T>(ref T x, ref T y){
			T temp = x;
			x = y;
			y = temp;
		}
	
		public static void HeapSort<T>(this T[] array){
			array.HeapSort(Comparer<T>.Default, 0, array.Length);
		}

		private static void HeapSort<T>(this T[] array, IComparer<T> comparer, int left, int right){
			int max = right - 1;
			for(int i = (max - 1) >> 1; i >= left; i--){
				array.MakeHeap(comparer, i, max);
			}
			for(int i = max; i > left; i--){
				Swap(ref array[left], ref array[i]);
				array.MakeHeap(comparer, left, i - 1);
			}
		}
	
		private static void MakeHeap<T>(this T[] array, IComparer<T> comparer, int i, int right){
			T v = array[i];
			while(true){
				int j = (i << 1) + 1;
				if(j > right){
					break;
				}
				if(j != right){
					if(comparer.Compare(array[j + 1], array[j]) > 0){
						j = j + 1;
					}
				}
				if(comparer.Compare(v, array[j]) >= 0){
					break;
				}
				array[i] = array[j];
				i = j;
			}
			array[i] = v;
		}

		#endregion

		#region Event

		public static void Raise(this Delegate handler, params object[] args){
			if(handler != null){
				handler.DynamicInvoke(args);
			}
		}

		#endregion

		#region WeakReference

		public static bool IsAlive<T>(this WeakReference<T> reference) where T : class {
			reference.ThrowIfNull("reference");
			T v;
			return reference.TryGetTarget(out v);
		}

		#endregion
	}
}