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
    /// Creates an encrypted string from another string.
    /// </summary>
    /// <param name="inString">A string of characters to be encoded.</param>
    /// <returns>An encrypted string</returns>
    public string Encrypt(string inString) {

      string outString = "";
      int i = 0;

      foreach (char character in inString)
      {
        ushort digit = Convert.ToUInt16(character);

        digit += _key[i]; // Increment the character...
        digit = (ushort)((digit << _key[i]) | digit >> (16 - _key[i])); // then rotate the bits.

        outString += (Convert.ToChar(digit));
        i = (i >= _key.Count() - 1) ? 0 : i + 1;
      }
      return outString;
    } 

    /// <summary>
    /// Creates a decrypted string from another string.
    /// </summary>
    /// <param name="inString">A string of characters to be decoded.</param>
    /// <returns>A decrypted string</returns>
    public string Decrypt(string inString) {

      string outString = "";
      int i = 0;
      foreach (char character in inString)
      {
        ushort digit = Convert.ToUInt16(character);

        digit = (ushort)((digit >> _key[i]) | digit << (16 - _key[i])); // Rotate the bits...
        digit -= _key[i]; // then decrement the character.

        outString += (Convert.ToChar(digit));
        i = (i >= _key.Count() - 1) ? 0 : i + 1;
      }
      return outString;
    }
  }
}
