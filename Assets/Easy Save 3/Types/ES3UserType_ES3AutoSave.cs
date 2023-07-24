using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("saveChildren", "isQuitting", "componentsToSave", "enabled", "name")]
	public class ES3UserType_ES3AutoSave : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ES3AutoSave() : base(typeof(ES3AutoSave)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ES3AutoSave)obj;
			
			writer.WriteProperty("saveChildren", instance.saveChildren, ES3Type_bool.Instance);
			writer.WritePrivateField("isQuitting", instance);
			writer.WriteProperty("componentsToSave", instance.componentsToSave);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ES3AutoSave)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "saveChildren":
						instance.saveChildren = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isQuitting":
					reader.SetPrivateField("isQuitting", reader.Read<System.Boolean>(), instance);
					break;
					case "componentsToSave":
						instance.componentsToSave = reader.Read<System.Collections.Generic.List<UnityEngine.Component>>();
						break;
					case "enabled":
						instance.enabled = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ES3AutoSaveArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ES3AutoSaveArray() : base(typeof(ES3AutoSave[]), ES3UserType_ES3AutoSave.Instance)
		{
			Instance = this;
		}
	}
}