using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace UI.Dates
{    
	public class CalendarManager: MonoBehaviour
	{
		public DatePicker _datePicker;
		public DatePicker datePicker
		{
			get
			{
				if (_datePicker == null) _datePicker = this.GetComponent<DatePicker>();
				Debug.Log("Text: " + _datePicker.SelectedDate);

				return _datePicker;
			}
		}

	}
}

