using System;
using System.Collections;

/// <summary>
/// 
/// </summary>
namespace CodeCrateData {


  public class Cipher
  {
    private ArrayList _key = new ArrayList() { 1, 2, 3, 4, 5, 6};
    private int _keySize = 6;
    
    /* -- Theory --
    * Type conversion is necessary for this to work.
    * Character by character, convert <char> to <uint>
    * the bit shift operation is as follows:
    * ((uint_Character_Value << degree_Of_Rotation) | (uint_Character_Value >> (32 - degree_Of_Rotation)))
    * convert <uint> back to <char>
    * append each <char> in order to the string
    */
    
    /// <summary>
    /// Increments the values of the characters
    /// </summary>
    /// <param name="inString">a string of characters to be encoded.</param>
    /// <returns>an encrypted string</returns>
    public string Encrypt(string inString) {

      string outString = "";
      int i = 0;

      for (int j = 0; j < inString.Length; j++)
      {
        int digit = Convert.ToInt32(inString[j]);
        digit += _key.IndexOf(i);
        outString += (Convert.ToChar(digit));
        i = (i == _keySize - 1) ? 0 : i + 1;
      }
      return outString;
    } 

    /// <summary>
    /// Decrements the values of the characters
    /// </summary>
    /// <param name="inString">a string of characters to be decoded.</param>
    /// <returns>a decrypted string</returns>
    public string Decrypt(string inString) {

      string outString = "";
      int i = 0;
      
      for (int j = 0; j < inString.Length; j++)
      {
        int digit = Convert.ToInt32(inString[j]);
        digit -= _key.IndexOf(i);
        outString += (Convert.ToChar(digit));
        i = (i == _keySize - 1) ? 0 : i + 1;
      }
      return outString;
    }
  }
}
