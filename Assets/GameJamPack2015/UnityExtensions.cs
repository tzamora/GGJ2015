using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class UnityExtensions
{
	//
	//
	//

	public static bool isTouchingWall (this CharacterController characterController)
	{
		return (characterController.collisionFlags & CollisionFlags.Sides) != 0;
	}
	
	//
	//
	//

	static readonly System.Random random = new System.Random ();

	public static void Shuffle<T> (this IList<T> list)
	{
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = random.Next (n + 1);
			T value = list [k];
			list [k] = list [n];
			list [n] = value;
		}
	}
	
	/// <summary>
	/// Taken from http://stackoverflow.com/questions/9033/hidden-features-of-c/407325#407325
	/// instead of doing (enum & value) == value you can now use enum.Has(value)
	/// </summary>
	/// <typeparam name="T">Type of enum</typeparam>
	/// <param name="type">The enum value you want to test</param>
	/// <param name="value">Flag Enum Value you're looking for</param>
	/// <returns>True if the type has value bit set</returns>
	public static bool Has<T> (this System.Enum type, T value)
	{
		return (((int)(object)type & (int)(object)value) == (int)(object)value);
	}

	//
	//checks if a bound rect is visible from a specified camera
	//
	public static bool IsVisibleFrom(this Bounds bounds, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, bounds);
	}


	//
	// Get the bounds of any object based on the renders it has
	// and its children
	//
	public static Bounds GetBounds(this Transform t)
	{
		Bounds bounds = new Bounds (t.position, Vector3.zero);
		
		Renderer[] renderers = t.GetComponentsInChildren<Renderer> ();
		
		foreach (Renderer renderer in renderers)
		{
			bounds.Encapsulate (renderer.bounds);   
		}
		
		return bounds;
	}
	
	//
	//
	//

	public static T Pop<T>(this List<T> theList)
    {
         var local = theList[theList.Count - 1];
         theList.RemoveAt(theList.Count - 1);
         return local;
    }
      
      //
      //
      //
    public static void Push<T>(this List<T> theList, T item)
    {
        theList.Add(item);
    }
	
	public static void ClearChildren(this GameObject go)
	{
		if(go == null)
		{
			return;
		}
		
		var children = new List<GameObject>();
		
		foreach (Transform child in go.transform) 
		{
			children.Add(child.gameObject);	
		}
		
		children.ForEach(child => Object.Destroy(child));
	}

	public static void SetX(this Transform transform, float x)
	{
		Vector3 newPosition = 
			new Vector3(x, transform.position.y, transform.position.z);
		
		transform.position = newPosition;
	}

	public static void SetY(this Transform transform, float y)
	{
		Vector3 newPosition = 
			new Vector3(transform.position.x, y, transform.position.z);
		
		transform.position = newPosition;
	}

	public static void SetZ(this Transform transform, float z)
	{
		Vector3 newPosition = 
			new Vector3(transform.position.x, transform.position.y, z);
		
		transform.position = newPosition;
	}

	public static void SetLocalX(this Transform transform, float x)
	{
		Vector3 newPosition = 
			new Vector3(x, transform.position.y, transform.position.z);
		
		transform.localPosition = newPosition;
	}
	
	public static void SetLocalY(this Transform transform, float y)
	{
		Vector3 newPosition = 
			new Vector3(transform.position.x, y, transform.position.z);
		
		transform.localPosition = newPosition;
	}
	
	public static void SetLocalZ(this Transform transform, float z)
	{
		Vector3 newPosition = 
			new Vector3(transform.position.x, transform.position.y, z);
		
		transform.localPosition = newPosition;
	}

	public static void SetTextureOffset(Renderer renderer, float offset)
	{
		//example
		//offset = Time.time * 0.5f;

		renderer.material.SetTextureOffset("_MainTex", new Vector2(Mathf.Repeat(offset,1f), 0));
	}
	
	public static string TextWrapper(this string text)
	{
        string result="", next="";
 
		int charByLine = 70, cursor = 0, charCount = 0;
 
		bool breakLine = false;
 
        while(cursor<text.Length)
		{					
			result += (next = (text[cursor++])+"");
			
			charCount++;
			
			if(((next = ((next==" ") && breakLine) ? "\n" :""))=="\n") breakLine= (charCount=0)!=0;
			
			result += next;
			
			breakLine = ((charCount >= charByLine-1) || breakLine);
		}
        
		return result;
	}
}