using System;
using System.Collections;

/// <summary>
/// 
/// </summary>
namespace CodeCrateData {


  public class Cipher
  {
    private ArrayList _key = new ArrayList() { 8, 3, 2, 5, 2, 4 };
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
    /// Bitwise rotation to the left
    /// </summary>
    /// <param name="inString">a string of characters to be encoded.</param>
    /// <returns>an encrypted string</returns>
    public string Encrypt(string inString) {
      // for each character in inString
      string outString = "";
      int i = 0;
      foreach (char item in inString)
      {
        // Step 1: convert <char> to <int>
        int character = Convert.ToInt32(item);

        // Step 2: Rotate bits
        character = (character << _key.IndexOf(i)) | (character >> (32 - _key.IndexOf(i)));

        // Step 3: convert back to <char> and insert to outString
        outString.Append(Convert.ToChar(character));

        // Step 4: if <i> is too large, set it to 0.
        i = (i == _keySize - 1) ? 0 : i + 1;

      }
      return outString;
    } 

    /// <summary>
    /// bitwise rotation to the right
    /// </summary>
    /// <param name="inString">a string of characters to be decoded.</param>
    /// <returns>a decrypted string</returns>
    public string Decrypt(string inString) {
      string outString = "";
      int i = 0;
      foreach (char item in inString)
      {
        // Step 1: convert <char> to <int>
        int character = Convert.ToInt32(item);

        // Step 2: Rotate bits
        character = (character >> _key.IndexOf(i)) | (character << (32 - _key.IndexOf(i)));

        // Step 3: convert back to <char> and insert to outString
        outString.Append(Convert.ToChar(character));

        // Step 4: if <i> is too large, set it to 0.
        i = (i == _keySize - 1) ? 0 : i + 1;

      }
      return outString;
    }
  }
}
