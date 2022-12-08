using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace SearchText
{
    /// <summary>
    /// Provides methods for searching text in appropriate file.
    /// </summary>
    public class SearchInFile
    {
        private string path; //path to the file we want to search in;
        private string[] text; //array of lines from the file;
        private char[] signs; //array with the signs we want to avoide while searching;

        /// <summary>
        /// Reads file content and prepares stuf for searching.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        public SearchInFile(string path)
        {
            this.path = Path.GetFullPath(path);
            FileToArray(); //
            signs = Signs();
        }

        /// <summary>
        /// Reads file and writes ii down to the string array.
        /// </summary>
        private void FileToArray()
        {
            text = File.ReadAllLines(path);
        }

        /// <summary>
        /// Fills an array with the signs we want to avoide while searching. We avoid punctuation marks, space, another signs like $, #, %, ets.
        /// </summary>
        /// <returns>Array with the signs.</returns>
        private char[] Signs()
        {
            char[] temp = new char[16 + 7 + 6 + 4];

            int i = 0;

            for (int ascii = 32; ascii <= 47; ascii++, i++)
            {
                temp[i] = (char)ascii;
            }

            for (int ascii = 58; ascii <= 64; ascii++, i++)
            {
                temp[i] = (char)ascii;
            }

            for (int ascii = 91; ascii <= 96; ascii++, i++)
            {
                temp[i] = (char)ascii;
            }

            for (int ascii = 123; ascii <= 126; ascii++, i++)
            {
                temp[i] = (char)ascii;
            }
            return temp;
        }

        /// <summary>
        /// Checks if the chars are equal.
        /// </summary>
        /// <param name="inputChar">Char from users input.</param>
        /// <param name="textChar">Char from txt file.</param>
        /// <returns>True if they are equal and false if not.</returns>
        private bool CheckChar (char inputChar, char textChar)
        {
            return inputChar == textChar;
        }

        /// <summary>
        /// We use this method to check if the input char is equal to the one of signs we have to avoid. They are located at the signs array.
        /// </summary>
        /// <param name="inputChar"></param>
        /// <returns>True if the input char is the sign we want to evoid and false if it is not.</returns>
        private bool CheckSign(char inputChar)
        {
            return Array.Exists(signs, element => element == inputChar);
        }

        /// <summary>
        /// Searchs if the input text exists in the file. 
        /// </summary>
        /// <param name="input">Input text fron the user</param>
        /// <returns>List of lines were the searched text is found. If there are no searched text, the method will return no lines.</returns>
        public List<string> Search (string input)
        {
            List<string> result = new List<string>();

            bool textStart = false;
            for(int i = 0, npt = 0; (i < text.Length && npt < input.Length); i++)
            {
                for(int j = 0; (j < text[i].Length && npt < input.Length); j++)
                {
                    char textJ = text[i][j];
                    if (CheckSign(input[npt])) { npt++; j--; continue; } //checks if the input char is an avoiding sign
                    if (CheckSign(text[i][j])) { checkIfEnd(); continue; } //checks if the file char is an avoiding sign
                    if (CheckChar(input[npt], text[i][j])) //Checks if every chat from input is equal to the char from the file.
                    {
                        textStart = true;
                        // method
                        checkIfEnd();
                        npt++;
                    }
                    else //If the signs are no equal we start to check from the first sign of the input and clear the result, because it does not match with the input.
                    {
                        textStart = false;
                        result.Clear();
                        npt = 0;
                        continue;
                    }

                    void checkIfEnd ()
                    {
                        if (textStart == true && j == text[i].Length - 1)
                        {
                            
                            result.Add(text[i]);
                            return;

                        }
                        else if (npt == input.Length - 1)
                        {
                            result.Add(text[i]);
                            return;
                        }
                    }
                }
            }
            return result;
        }
    }
}
