using System.Reflection;

// grabs private/serialized fields on MonoBehaviours so tests can use them without needing public setters on the actual classes
public static class TestReflectionUtils
{
	public static void SetPrivateField(object target, string fieldName, object value)
	{
		var type = target.GetType();
		while (type != null)
		{
			var field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (field != null)
			{
				field.SetValue(target, value);
				return;
			}
			type = type.BaseType;
		}
		throw new System.Exception($"Field '{fieldName}' not found on {target.GetType()}");
	}



	// reads a private/protected field back out
	public static T GetPrivateField<T>(object target, string fieldName)
	{
		var type = target.GetType();
		while (type != null)
		{
			var field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (field != null)
				return (T)field.GetValue(target);
			type = type.BaseType;
		}


		throw new System.Exception($"Field '{fieldName}' not found on {target.GetType()}");
	}
}

