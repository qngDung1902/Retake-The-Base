using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Globalization;
using System;

public class TimeFromToGoogle : MonoSingleton<TimeFromToGoogle>
{

	public static DateTime GoogleTimeGMT;
	public bool isUsingUnbiasedTime = true;
	// public bool isHack = false;

	//Dtemp
	[SerializeField] bool hasTimeGoogle = false;
	[SerializeField] string realDateTime;

	void Awake()
	{
		int check = PlayerPrefs.GetInt("IsUsingUbiasedTime", 0); // 0 = first time conect Google time , 1 = using UbbiaseTime , 2 = using Datetime.Now

		switch (check)
		{
			case 0: StartCoroutine(NetTime()); break;
			case 1: isUsingUnbiasedTime = true; break;
			case 2: isUsingUnbiasedTime = false; break;

		}

		InvokeRepeating(nameof(UpdateTime), 1f, 1f);
	}

	void UpdateTime()
	{
		realDateTime = TimeGoogle().ToString();
	}

	public DateTime TimeGoogle()
	{
		DateTime now = DateTime.UtcNow;

		if (hasTimeGoogle)
		{
			now = GoogleTimeGMT.AddSeconds(Time.time);
		}

		return now;
	}

	public DateTime Now()
	{

		DateTime now = new DateTime();
		TimeSpan defferTime = DateTime.UtcNow - DateTime.Now;

		// if (isUsingUnbiasedTime)
		// {
		// 	now = UnbiasedTime.Instance.Now();
		// }
		// else
		// {
		// 	now = DateTime.Now;
		// }

		now = isUsingUnbiasedTime ? UnbiasedTime.Instance.Now() : DateTime.Now;
		return now;
		//return DateTime.Now;
	}

	public DateTime UtcNow()
	{

		DateTime now = new DateTime();
		TimeSpan defferTime = DateTime.UtcNow - DateTime.Now;

		float checkdefer = PlayerPrefs.GetFloat("DefferTimeGoogleVsUnbiasedTime", 0);
		if (checkdefer != 0)
		{
			now = UnbiasedTime.Instance.Now().AddSeconds(-checkdefer);
		}
		else
		{
			now = UnbiasedTime.Instance.Now().AddSeconds(defferTime.TotalSeconds);
		}
		return now;

		//return DateTime.UtcNow;
	}

	public bool IsUsingUbbiasedTime(DateTime googleTime, DateTime unbiasedTime)
	{
		if (Math.Abs((googleTime - unbiasedTime).TotalSeconds) <= 60)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	IEnumerator NetTime()
	{
		UnityWebRequest myHttpWebRequest = UnityWebRequest.Get("https://www.google.com");
		yield return myHttpWebRequest.SendWebRequest();
		string netTime = myHttpWebRequest.GetResponseHeader("date");
		if (string.IsNullOrEmpty(netTime))
		{
			// Check internet Connection ???
			hasTimeGoogle = false;
		}
		else
		{
			string fomartDatetime = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
			DateTime.TryParseExact(netTime, fomartDatetime, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AdjustToUniversal, out GoogleTimeGMT);
			TimeSpan defferTime = DateTime.UtcNow - DateTime.Now;
			TimeSpan defferTimeGoogleVsUnbiasedTime = UnbiasedTime.Instance.Now() - GoogleTimeGMT;
			float x = (float)defferTimeGoogleVsUnbiasedTime.TotalSeconds;
			PlayerPrefs.SetFloat("DefferTimeGoogleVsUnbiasedTime", x);
			DateTime a = UnbiasedTime.Instance.Now().AddSeconds(defferTime.TotalSeconds);
			if (IsUsingUbbiasedTime(a, GoogleTimeGMT))
			{
				PlayerPrefs.SetInt("IsUsingUbiasedTime", 1);
				isUsingUnbiasedTime = true;
			}
			else
			{
				PlayerPrefs.SetInt("IsUsingUbiasedTime", 2);
				isUsingUnbiasedTime = false;
			}
			hasTimeGoogle = true;
		}
	}
}
