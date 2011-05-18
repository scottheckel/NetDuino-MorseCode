// By Scott Heckel
using System;
using System.Threading;
namespace MorseCodeLibrary
{
    /// <summary>
    /// Morse Code
    /// </summary>
    public class MorseCode
    {
        /// <summary>Dot Length in Milliseconds</summary>
        private int mDotLength;

        /// <summary>Blink Handler</summary>
        private MorseCodeBlinkEventHandler mToggle;

        /// <summary>
        /// Constructor
        /// </summary>
        public MorseCode()
        {
            this.mDotLength = 200;
        }

        /// <summary>
        /// Set the Dot Length
        /// </summary>
        /// <param name="milliseconds">Milliseconds Wait</param>
        /// <returns>The Morse Code object</returns>
        public MorseCode SetDotLength(int milliseconds)
        {
            this.mDotLength = milliseconds;
            return this;
        }

        /// <summary>
        /// Set what to call to toggle on or off morse code signal
        /// </summary>
        /// <param name="blinkHandler"></param>
        /// <returns></returns>
        public MorseCode CallOnToggle(MorseCodeBlinkEventHandler toggle)
        {
            this.mToggle = toggle;
            return this;
        }

        /// <summary>
        /// Display a string of text
        /// </summary>
        /// <param name="text">Text to display as morse code</param>
        /// <returns>The Morse Code object</returns>
        public MorseCode Display(string text)
        {
            foreach (char character in text)
            {
                string code;

                // alphabet
                if (character >= 'a' && character <= 'z')
                {
                    code = MorseCodeSignals[(int)character - (int)'a'];
                }
                // numeric
                else if (character >= '0' && character <= '9')
                {
                    code = MorseCodeSignals[(int)character - (int)'0' + (int)'z'];
                }
                // anything else treat as a space
                else
                {
                    code = " ";
                }

                // Loop through the entire signal
                foreach (char signal in code)
                {
                    switch (signal)
                    {
                        case ' ':
                            this.OnBlink(false); // 7 total
                            break;
                        case '.':
                            this.OnBlink(true);
                            break;
                        case '-':
                            this.OnBlink(true);
                            this.OnBlink(true);
                            this.OnBlink(true);
                            break;
                        default:
                            throw new InvalidOperationException("Invalid signal");
                    }

                    this.OnBlink(false); // between signals gap
                }

                // between character gap (3 breaks, 1 done after signal)
                this.OnBlink(false);
                this.OnBlink(false);
            }
            
            return this;
        }

        /// <summary>
        /// Called when a Blink (either on or off) occurs
        /// </summary>
        /// <param name="isOn"></param>
        private void OnBlink(bool isOn)
        {
            if (this.mToggle != null)
            {
                this.mToggle(this, new MorseCodeBlinkEventArgs { IsOn = isOn });
            }

            // Sleep for the dot length
            Thread.Sleep(this.mDotLength);
        }

        #region Signal Table
        /// <summary>
        /// Morse Code Signals for each alphanumeric character
        /// </summary>
        private static string[] MorseCodeSignals = new string[]
        {
            ".-",       // a
            "-...",     // b
            "-.-.",     // c
            "-..",      // d
            ".",        // e
            "..-.",     // f
            "--.",      // g
            "....",     // h
            "..",       // i
            ".---",     // j
            "-.-",      // k
            ".-..",     // l
            "--",       // m
            "-.",       // n
            "---",      // o
            ".--.",     // p
            "--.-",     // q
            ".-.",      // r
            "...",      // s
            "-",        // t
            "..-",      // u
            "...-",     // v
            ".--",      // w
            "-..-",     // x
            "-.--",     // y
            "--..",     // z
            "-----",    // 0
            ".----",    // 1
            "..---",    // 2
            "...--",    // 3
            "....-",    // 4
            ".....",    // 5
            "-....",    // 6
            "--...",    // 7
            "---..",    // 8
            "----."     // 9
        };
        #endregion
    }
}
