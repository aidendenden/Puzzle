using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute()]
	public class ES3UserType_GraphicRaycaster : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_GraphicRaycaster() : base(typeof(UnityEngine.UI.GraphicRaycaster)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.UI.GraphicRaycaster)obj;
			
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.UI.GraphicRaycaster)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_GraphicRaycasterArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_GraphicRaycasterArray() : base(typeof(UnityEngine.UI.GraphicRaycaster[]), ES3UserType_GraphicRaycaster.Instance)
		{
			Instance = this;
		}
	}
}