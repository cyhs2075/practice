using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

/// <summary>
/// 將Array或List轉成字串(用StringBuilder轉，效能比較好!!)
/// </summary>
public class iG_ArrayToStringEx
{
	static StringBuilder m_rtn = new StringBuilder();

	/// <summary>
	/// 將陣列轉為字串(自訂型態中需要有 override string ToString()，否則會印出Class名，不過會比設定Method快)
	/// </summary>
	/// <typeparam name="T">陣列中元素的型態</typeparam>
	/// <param name="arr">要轉換的陣列</param>
	/// <returns></returns>
	public static string ToString<T>(T[] arr)
	{
		m_rtn.Length = 0;
		int i, len = arr.Length;
		for (i = 0; i < len; i++)
		{
			m_rtn.Append(arr[i]);

			if (i < len - 1)
			{
				m_rtn.Append(", ");
			}
		}
		string rtn = m_rtn.ToString();
		m_rtn.Length = 0;
		return rtn;
	}

	/// <summary>
	///  將List轉為字串(自訂型態中需要有 override string ToString()，否則會印出Class名，不過會比設定Method快)
	/// </summary>
	/// <typeparam name="T">List中元素的型態</typeparam>
	/// <param name="arr">要轉換的List</param>
	public static string ToString<T>(List<T> arr)
	{
		return ToString<T>(arr.ToArray());
	}


	///////////////////////////////////////////////////////////////////////////////////


	/// <summary>
	/// 將陣列轉為字串，自訂型態可依設定的函式名執行(預設為"toString")
	/// </summary>
	/// <typeparam name="T">陣列中元素的型態</typeparam>
	/// <param name="arr">要轉換的陣列</param>
	/// <param name="MethodName">自訂型態中，負責印出內容的函式名(預設為"toString")</param>
	/// <returns></returns>
	public static string toString<T>(T[] arr, string MethodName = "")
	{
		MethodInfo Method = null;
		if (MethodName != "")
			Method = typeof(T).GetMethod(MethodName);
		else
			Method = typeof(T).GetMethod("toString");

		m_rtn.Length = 0;
		int i, len = arr.Length;
		for (i = 0; i < len; i++)
		{
			if (Method != null)
				m_rtn.Append(Method.Invoke(arr[i], null));
			else
				m_rtn.Append(arr[i]);	// 自訂型態中需要有 override string ToString()，否則會印出Class名

			if (i < len - 1)
			{
				m_rtn.Append(", ");
			}
		}
		string rtn = m_rtn.ToString();
		m_rtn.Length = 0;
		return rtn;
	}

	/// <summary>
	///  將List轉為字串，自訂型態可依設定的函式名執行(預設為"toString")
	/// </summary>
	/// <typeparam name="T">List中元素的型態</typeparam>
	/// <param name="arr">要轉換的List</param>
	/// <param name="MethodName">自訂型態中，負責印出內容的函式名(預設為"toString")</param>
	/// <returns></returns>
	public static string toString<T>(List<T> arr, string MethodName = "")
	{
		return toString<T>(arr.ToArray(), MethodName);
	}

}
