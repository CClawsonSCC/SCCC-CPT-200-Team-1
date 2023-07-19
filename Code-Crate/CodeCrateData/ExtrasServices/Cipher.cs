using System;
using System.Collections;

namespace CodeCrateData {

  /// <summary>
  /// The Cipher class takes a string of UTF-16 characters and creates an encrypted or decrypted version.
  /// </summary>
  public class Cipher
  {
    private readonly ushort[] _key = { 1, 2, 3, 4, 5, 6 }; // FIXME: Replace with a more complicated key.
    
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
        ushort digit = Convert.ToUInt16(inString[j]);

        digit += _key[i]; // Increment the character...
        digit = (ushort)((digit << _key[i]) | digit >> (16 - _key[i])); // then rotate the bits.

        outString += (Convert.ToChar(digit));
        i = (i >= _key.Count() - 1) ? 0 : i + 1;
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
        ushort digit = Convert.ToUInt16(inString[j]);

        digit = (ushort)((digit >> _key[i]) | digit << (16 - _key[i])); // Rotate the bits...
        digit -= _key[i]; // then decrement the character.

        outString += (Convert.ToChar(digit));
        i = (i >= _key.Count() - 1) ? 0 : i + 1;
      }
      return outString;
    }
  }
}
