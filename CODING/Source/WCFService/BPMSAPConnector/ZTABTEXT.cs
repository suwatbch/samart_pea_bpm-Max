
//------------------------------------------------------------------------------
// 
//     This code was generated by a SAP. NET Connector Proxy Generator Version 2.0
//     Created at 4/27/2011
//     Created from Windows
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// 
//------------------------------------------------------------------------------
using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using SAP.Connector;

namespace BPMSAPConnector
{
  /// <summary>
  /// A typed collection of string elements.
  /// </summary>
  [Serializable]
  public class ZTABTEXT : SAPTable 
  {
  
    /// <summary>
    /// Returns the element type string.
    /// </summary>
    /// <returns>The type string.</returns>
    public override Type GetElementType() 
    {
        return (typeof(string));
    }

    /// <summary>
    /// Creates an empty new row of type string.
    /// </summary>
    /// <returns>The newstring.</returns>
    public override object CreateNewRow()
    { 
        return ""; 
    }
     
    /// <summary>
    /// The indexer of the collection.
    /// </summary>
    public string this[int index] 
    {
        get 
        {
            return ((string)(List[index]));
        }
        set 
        {
            List[index] = value;
        }
    }
        
    /// <summary>
    /// Adds a string to the end of the collection.
    /// </summary>
    /// <param name="value">The string to be added to the end of the collection.</param>
    /// <returns>The index of the newstring.</returns>
    public int Add(string value) 
    {
        return List.Add(value);
    }
        
    /// <summary>
    /// Inserts a string into the collection at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which value should be inserted.</param>
    /// <param name="value">The string to insert.</param>
    public void Insert(int index, string value) 
    {
        List.Insert(index, value);
    }
        
    /// <summary>
    /// Searches for the specified string and returnes the zero-based index of the first occurrence in the collection.
    /// </summary>
    /// <param name="value">The string to locate in the collection.</param>
    /// <returns>The index of the object found or -1.</returns>
    public int IndexOf(string value) 
    {
        return List.IndexOf(value);
    }
        
    /// <summary>
    /// Determines wheter an element is in the collection.
    /// </summary>
    /// <param name="value">The string to locate in the collection.</param>
    /// <returns>True if found; else false.</returns>
    public bool Contains(string value) 
    {
        return List.Contains(value);
    }
        
    /// <summary>
    /// Removes the first occurrence of the specified string from the collection.
    /// </summary>
    /// <param name="value">The string to remove from the collection.</param>
    public void Remove(string value) 
    {
        List.Remove(value);
    }

    /// <summary>
    /// Copies the contents of the ZTABTEXT to the specified one-dimensional array starting at the specified index in the target array.
    /// </summary>
    /// <param name="array">The one-dimensional destination array.</param>           
    /// <param name="index">The zero-based index in array at which copying begins.</param>           
    public void CopyTo(string[] array, int index) 
    {
        List.CopyTo(array, index);
	}
  }
}
